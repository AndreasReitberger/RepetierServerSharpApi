using AndreasReitberger.API.Repetier.Enum;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterHeaterComponent : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonProperty("error")]
        long error;

        [ObservableProperty]
        [JsonProperty("output")]
        long output;

        [ObservableProperty]
        [JsonProperty("tempRead")]
        double tempRead;

        [ObservableProperty]
        [JsonProperty("tempSet")]
        long tempSet;

        #region Json Ignore

        [JsonIgnore]
        public RepetierToolState State { get => GetCurrentState(); }
        #endregion

        #endregion

        #region Methods
        RepetierToolState GetCurrentState()
        {
            if (Error > 1)
                return RepetierToolState.Error;
            else
            {
                if (TempSet <= 0)
                    return RepetierToolState.Idle;
                // Check if temperature is reached with a hysteresis
                else if (TempSet > TempRead && Math.Abs(TempSet - TempRead) > 2)
                    return RepetierToolState.Heating;
                else
                    return RepetierToolState.Ready;
            }
        }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

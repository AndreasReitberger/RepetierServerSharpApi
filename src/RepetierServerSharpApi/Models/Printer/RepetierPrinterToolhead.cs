using AndreasReitberger.API.Print3dServer.Core.Enums;
using AndreasReitberger.API.Print3dServer.Core.Interfaces;
using AndreasReitberger.API.Repetier.Enum;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterToolhead : ObservableObject, IToolhead
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        [property: JsonIgnore]
        Guid id;

        [ObservableProperty]
        [JsonProperty("error")]
        [property: JsonIgnore]
        long error;

        [ObservableProperty]
        [JsonProperty("output")]
        [property: JsonIgnore]
        long output;

        [ObservableProperty]
        [JsonProperty("tempRead")]
        [property: JsonIgnore]
        double? tempRead;

        [ObservableProperty]
        [JsonProperty("tempSet")]
        [property: JsonIgnore]
        double? tempSet;

        #region Interface, unsused

        [ObservableProperty, JsonIgnore]
        [property: JsonIgnore]
        string name;

        [ObservableProperty, JsonIgnore]
        [property: JsonIgnore]
        double x = 0;

        [ObservableProperty, JsonIgnore]
        [property: JsonIgnore]
        double y = 0;
        [ObservableProperty, JsonIgnore]
        [property: JsonIgnore]
        double z = 0;
        #endregion

        #region Json Ignore

        [JsonIgnore]
        public RepetierToolState State { get => GetCurrentState(); }

        [ObservableProperty]
        Printer3dHeaterType type = Printer3dHeaterType.Other;
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
                else if (TempSet > TempRead && Math.Abs(TempSet ?? 0 - TempRead ?? 0) > 2)
                    return RepetierToolState.Heating;
                else
                    return RepetierToolState.Ready;
            }
        }

        public Task<bool> SetTemperatureAsync(IPrint3dServerClient client, string command, object data) => client?.SetExtruderTemperatureAsync(command, data);
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        
        #endregion
    }
}

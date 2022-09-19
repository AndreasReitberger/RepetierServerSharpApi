using AndreasReitberger.API.Repetier.Enum;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterHeatbed
    {
        #region Properties
        [JsonProperty("error")]
        public long Error { get; set; }

        [JsonProperty("output")]
        public long Output { get; set; }

        [JsonProperty("tempRead")]
        public double TempRead { get; set; }

        [JsonProperty("tempSet")]
        public long TempSet { get; set; }

        [JsonIgnore]
        public RepetierToolState State { get => GetCurrentState(); }
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
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }

}

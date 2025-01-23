using AndreasReitberger.API.Print3dServer.Core.Enums;
using AndreasReitberger.API.Print3dServer.Core.Interfaces;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterHeaterComponent : ObservableObject, IHeaterComponent
    {
        #region Properties
        [ObservableProperty]

        public partial Guid Id { get; set; }

        [ObservableProperty]

        [JsonProperty("error")]
        public partial long Error { get; set; }

        [ObservableProperty]

        [JsonProperty("output")]
        public partial long Output { get; set; }

        [ObservableProperty]

        [JsonProperty("tempRead")]
        public partial double? TempRead { get; set; }

        [ObservableProperty]

        [JsonProperty("tempSet")]
        public partial double? TempSet { get; set; }

        #region Interface, unsused

        [ObservableProperty]

        public partial string Name { get; set; } = string.Empty;
        #endregion

        #region Json Ignore

        [JsonIgnore]
        public Printer3dToolHeadState State { get => GetCurrentState(); }

        [ObservableProperty]

        public partial Printer3dHeaterType Type { get; set; } = Printer3dHeaterType.Other;
        #endregion

        #endregion

        #region Methods
        public Printer3dToolHeadState GetCurrentState()
        {
            if (Error > 1)
                return Printer3dToolHeadState.Error;
            else
            {
                if (TempSet <= 0)
                    return Printer3dToolHeadState.Idle;
                // Check if temperature is reached with a hysteresis
                else if (TempSet > TempRead && Math.Abs(TempSet ?? 0 - TempRead ?? 0) > 2)
                    return Printer3dToolHeadState.Heating;
                else
                    return Printer3dToolHeadState.Ready;
            }
        }

        public Task<bool> SetTemperatureAsync(IPrint3dServerClient client, string command, object? data) => client.SetFanSpeedAsync(command, data);
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        #endregion
    }
}

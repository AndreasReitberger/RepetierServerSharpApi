using AndreasReitberger.API.Print3dServer.Core.Enums;
using AndreasReitberger.API.Print3dServer.Core.Interfaces;
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
        [JsonPropertyName("error")]
        public partial long Error { get; set; }

        [ObservableProperty]
        [JsonPropertyName("output")]
        public partial double Output { get; set; }

        [ObservableProperty]
        [JsonPropertyName("tempRead")]
        public partial double? TempRead { get; set; }

        [ObservableProperty]
        [JsonPropertyName("tempSet")]
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
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierPrinterHeaterComponent);

        #endregion
    }
}

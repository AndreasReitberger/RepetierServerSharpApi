using AndreasReitberger.API.Print3dServer.Core.Enums;
using AndreasReitberger.API.Print3dServer.Core.Interfaces;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterToolhead : ObservableObject, IToolhead
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        Guid id;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("error")]
        long error;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("output")]
        long output;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("tempRead")]
        double? tempRead;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("tempSet")]

        double? tempSet;

        #region Interface, unsused

        [ObservableProperty, JsonIgnore]
        string name = string.Empty;

        [ObservableProperty, JsonIgnore]
        double x = 0;

        [ObservableProperty, JsonIgnore]
        double y = 0;

        [ObservableProperty, JsonIgnore]
        double z = 0;
        #endregion

        #region Json Ignore

        [JsonIgnore]
        public Printer3dToolHeadState State { get => GetCurrentState(); }

        [ObservableProperty, JsonIgnore]
        Printer3dHeaterType type = Printer3dHeaterType.Other;
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
        public Task<bool> SetTemperatureAsync(IPrint3dServerClient client, string command, object? data) => client.SetExtruderTemperatureAsync(command, data);
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        #endregion
    }
}

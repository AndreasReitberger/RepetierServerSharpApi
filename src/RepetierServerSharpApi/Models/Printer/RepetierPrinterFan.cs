using AndreasReitberger.API.Print3dServer.Core.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterFan : ObservableObject, IPrint3dFan
    {
        #region Properties

        [ObservableProperty]
        [JsonProperty("on")]
        bool on;

        [ObservableProperty]
        [JsonProperty("voltage")]
        long? voltage;

        #region Json Ignore
        [JsonIgnore]
        public int? Speed => Convert.ToInt32(Math.Round((double)(Voltage / 255m * 100m), 0));

        #endregion

        #endregion

        #region Methods
        public Task<bool> SetFanSpeedAsync(IPrint3dServerClient client, string command, object data) => client?.SetFanSpeedAsync(command, data);
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

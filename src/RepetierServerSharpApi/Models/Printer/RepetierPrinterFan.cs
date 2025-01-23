using AndreasReitberger.API.Print3dServer.Core.Interfaces;
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
        public partial bool On { get; set; }

        [ObservableProperty]
        
        [NotifyPropertyChangedFor(nameof(Speed))]
        [JsonProperty("voltage")]
        public partial long? Voltage { get; set; }

        #region Json Ignore
        [JsonIgnore]
        public int? Speed => Convert.ToInt32(Math.Round((double)(Voltage ?? 0 / 255m * 100m), 0));

        [ObservableProperty]
        
        [JsonIgnore, System.Text.Json.Serialization.JsonIgnore, XmlIgnore]
        public partial int? Percent { get; set; } = 0;
        #endregion

        #endregion

        #region Methods
        public Task<bool> SetFanSpeedAsync(IPrint3dServerClient client, string command, object? data) => client.SetFanSpeedAsync(command, data);
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

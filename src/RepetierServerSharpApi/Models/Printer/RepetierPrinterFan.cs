using AndreasReitberger.API.Print3dServer.Core.Interfaces;
using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterFan : ObservableObject, IPrint3dFan
    {
        #region Properties

        [ObservableProperty]
        [JsonProperty("on"), JsonPropertyName("on")]
        public partial bool On { get; set; }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Speed))]
        [JsonProperty("voltage"), JsonPropertyName("voltage")]
        public partial double? Voltage { get; set; }

        #region Json Ignore
        [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore]
        public int? Speed => Convert.ToInt32(Math.Round((double)(Voltage ?? 0.0 / 255.0 * 100.0), 0));

        [ObservableProperty]
        [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore, XmlIgnore]
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

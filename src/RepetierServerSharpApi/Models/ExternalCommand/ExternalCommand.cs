using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class ExternalCommand : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("confirm"), JsonPropertyName("confirm")]
        public partial string Confirm { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("execute"), JsonPropertyName("execute")]
        public partial string Execute { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("icon"), JsonPropertyName("icon")]
        public partial string Icon { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("id"), JsonPropertyName("id")]
        public partial long Id { get; set; }

        [ObservableProperty]
        [JsonProperty("ifAllNotPrinting"), JsonPropertyName("ifAllNotPrinting")]
        public partial bool IfAllNotPrinting { get; set; }

        [ObservableProperty]
        [JsonProperty("ifThisNotPrinting"), JsonPropertyName("ifThisNotPrinting")]
        public partial bool IfThisNotPrinting { get; set; }

        [ObservableProperty]
        [JsonProperty("local"), JsonPropertyName("local")]
        public partial bool Local { get; set; }

        [ObservableProperty]
        [JsonProperty("name"), JsonPropertyName("name")]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("permAdd"), JsonPropertyName("permAdd")]
        public partial bool PermAdd { get; set; }

        [ObservableProperty]
        [JsonProperty("permConfig"), JsonPropertyName("permConfig")]
        public partial bool PermConfig { get; set; }

        [ObservableProperty]
        [JsonProperty("permDel"), JsonPropertyName("permDel")]
        public partial bool PermDel { get; set; }

        [ObservableProperty]
        [JsonProperty("permPrint"), JsonPropertyName("permPrint")]
        public partial bool PermPrint { get; set; }

        [ObservableProperty]
        [JsonProperty("remote"), JsonPropertyName("remote")]
        public partial bool Remote { get; set; }

        [ObservableProperty]
        [JsonProperty("slug"), JsonPropertyName("slug")]
        public partial string Slug { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("terminal"), JsonPropertyName("terminal")]
        public partial string Terminal { get; set; } = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

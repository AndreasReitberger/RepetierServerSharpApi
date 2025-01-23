using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class ExternalCommand : ObservableObject
    {
        #region Properties
        [ObservableProperty]

        [JsonProperty("confirm")]
        public partial string Confirm { get; set; } = string.Empty;

        [ObservableProperty]

        [JsonProperty("execute")]
        public partial string Execute { get; set; } = string.Empty;

        [ObservableProperty]

        [JsonProperty("icon")]
        public partial string Icon { get; set; } = string.Empty;

        [ObservableProperty]

        [JsonProperty("id")]
        public partial long Id { get; set; }

        [ObservableProperty]

        [JsonProperty("ifAllNotPrinting")]
        public partial bool IfAllNotPrinting { get; set; }

        [ObservableProperty]

        [JsonProperty("ifThisNotPrinting")]
        public partial bool IfThisNotPrinting { get; set; }

        [ObservableProperty]

        [JsonProperty("local")]
        public partial bool Local { get; set; }

        [ObservableProperty]

        [JsonProperty("name")]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]

        [JsonProperty("permAdd")]
        public partial bool PermAdd { get; set; }

        [ObservableProperty]

        [JsonProperty("permConfig")]
        public partial bool PermConfig { get; set; }

        [ObservableProperty]

        [JsonProperty("permDel")]
        public partial bool PermDel { get; set; }

        [ObservableProperty]

        [JsonProperty("permPrint")]
        public partial bool PermPrint { get; set; }

        [ObservableProperty]

        [JsonProperty("remote")]
        public partial bool Remote { get; set; }

        [ObservableProperty]

        [JsonProperty("slug")]
        public partial string Slug { get; set; } = string.Empty;

        [ObservableProperty]

        [JsonProperty("terminal")]
        public partial string Terminal { get; set; } = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

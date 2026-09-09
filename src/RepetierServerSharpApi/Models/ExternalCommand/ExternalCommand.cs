namespace AndreasReitberger.API.Repetier.Models
{
    public partial class ExternalCommand : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("confirm")]
        public partial string Confirm { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("execute")]
        public partial string Execute { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("icon")]
        public partial string Icon { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("id")]
        public partial long Id { get; set; }

        [ObservableProperty]
        [JsonPropertyName("ifAllNotPrinting")]
        public partial bool IfAllNotPrinting { get; set; }

        [ObservableProperty]
        [JsonPropertyName("ifThisNotPrinting")]
        public partial bool IfThisNotPrinting { get; set; }

        [ObservableProperty]
        [JsonPropertyName("local")]
        public partial bool Local { get; set; }

        [ObservableProperty]
        [JsonPropertyName("name")]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("permAdd")]
        public partial bool PermAdd { get; set; }

        [ObservableProperty]
        [JsonPropertyName("permConfig")]
        public partial bool PermConfig { get; set; }

        [ObservableProperty]
        [JsonPropertyName("permDel")]
        public partial bool PermDel { get; set; }

        [ObservableProperty]
        [JsonPropertyName("permPrint")]
        public partial bool PermPrint { get; set; }

        [ObservableProperty]
        [JsonPropertyName("remote")]
        public partial bool Remote { get; set; }

        [ObservableProperty]
        [JsonPropertyName("slug")]
        public partial string Slug { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("terminal")]
        public partial string Terminal { get; set; } = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.ExternalCommand);
        #endregion
    }
}

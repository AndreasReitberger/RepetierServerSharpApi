namespace AndreasReitberger.API.Repetier.Models
{
    public partial class ConnectionData : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonPropertyName("active")]
        public partial bool? Active { get; set; }

        [ObservableProperty]
        [JsonPropertyName("hidden")]
        public partial bool? Hidden { get; set; }

        [ObservableProperty]
        [JsonPropertyName("ignore")]
        public partial bool? Ignore { get; set; }

        [ObservableProperty]
        [JsonPropertyName("ipv4Address")]
        public partial string Ipv4Address { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("ipv4Gateway")]
        public partial string Ipv4Gateway { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("ipv4MaskBits")]
        public partial long? Ipv4MaskBits { get; set; }

        [ObservableProperty]
        [JsonPropertyName("ipv4Mode")]
        public partial string Ipv4Mode { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("ipv4Nameserver")]
        public partial string Ipv4Nameserver { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("ipv6Address")]
        public partial string Ipv6Address { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("ipv6Gateway")]
        public partial string Ipv6Gateway { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("ipv6MaskBits")]
        public partial long? Ipv6MaskBits { get; set; }

        [ObservableProperty]
        [JsonPropertyName("ipv6Mode")]
        public partial string Ipv6Mode { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("ipv6Nameserver")]
        public partial string Ipv6Nameserver { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("manualManaged")]
        public partial bool? ManualManaged { get; set; }

        [ObservableProperty]
        [JsonPropertyName("password")]
        public partial string Password { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("passwordMethod")]
        public partial string PasswordMethod { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("ssid")]
        public partial string Ssid { get; set; } = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.ConnectionData);
        #endregion
    }
}

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConnectionIp : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("address")]
        public partial string Address { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("keepAliveInterval")]
        public partial long KeepAliveInterval { get; set; }

        [ObservableProperty]
        [JsonPropertyName("port")]
        public partial long Port { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierPrinterConnectionIp);

        #endregion
    }

}

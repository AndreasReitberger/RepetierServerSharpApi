namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConnectionSerial : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("baudrate")]
        public partial long Baudrate { get; set; }

        [ObservableProperty]
        [JsonPropertyName("communicationTimeout")]
        public partial double CommunicationTimeout { get; set; }

        [ObservableProperty]
        [JsonPropertyName("connectionDelay")]
        public partial double ConnectionDelay { get; set; }

        [ObservableProperty]
        [JsonPropertyName("device")]
        public partial string Device { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("dtr")]
        public partial long Dtr { get; set; }

        [ObservableProperty]
        [JsonPropertyName("emergencySolution")]
        public partial long EmergencySolution { get; set; }

        [ObservableProperty]
        [JsonPropertyName("inputBufferSize")]
        public partial long InputBufferSize { get; set; }

        [ObservableProperty]
        [JsonPropertyName("interceptor")]
        public partial bool Interceptor { get; set; }

        [ObservableProperty]
        [JsonPropertyName("malyanHack")]
        public partial bool MalyanHack { get; set; }

        [ObservableProperty]
        [JsonPropertyName("maxParallelCommands")]
        public partial int MaxParallelCommands { get; set; }

        [ObservableProperty]
        [JsonPropertyName("pingPong")]
        public partial bool PingPong { get; set; }

        [ObservableProperty]
        [JsonPropertyName("rts")]
        public partial long Rts { get; set; }

        [ObservableProperty]
        [JsonPropertyName("usbreset")]
        public partial long Usbreset { get; set; }

        [ObservableProperty]
        [JsonPropertyName("visibleWithoutRunning")]
        public partial bool VisibleWithoutRunning { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierPrinterConnectionSerial);

        #endregion
    }
}

using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConnectionSerial : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        
        [JsonProperty("baudrate")]
        public partial long Baudrate { get; set; }

        [ObservableProperty]
        
        [JsonProperty("communicationTimeout")]
        public partial long CommunicationTimeout { get; set; }

        [ObservableProperty]
        
        [JsonProperty("connectionDelay")]
        public partial long ConnectionDelay { get; set; }

        [ObservableProperty]
        
        [JsonProperty("device")]
        public partial string Device { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("dtr")]
        public partial long Dtr { get; set; }

        [ObservableProperty]
        
        [JsonProperty("emergencySolution")]
        public partial long EmergencySolution { get; set; }

        [ObservableProperty]
        
        [JsonProperty("inputBufferSize")]
        public partial long InputBufferSize { get; set; }

        [ObservableProperty]
        
        [JsonProperty("interceptor")]
        public partial bool Interceptor { get; set; }

        [ObservableProperty]
        
        [JsonProperty("malyanHack")]
        public partial bool MalyanHack { get; set; }

        [ObservableProperty]
        
        [JsonProperty("maxParallelCommands")]
        public partial bool MaxParallelCommands { get; set; }

        [ObservableProperty]
        
        [JsonProperty("pingPong")]
        public partial bool PingPong { get; set; }

        [ObservableProperty]
        
        [JsonProperty("rts")]
        public partial long Rts { get; set; }

        [ObservableProperty]
        
        [JsonProperty("usbreset")]
        public partial long Usbreset { get; set; }

        [ObservableProperty]
        
        [JsonProperty("visibleWithoutRunning")]
        public partial bool VisibleWithoutRunning { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        #endregion
    }
}

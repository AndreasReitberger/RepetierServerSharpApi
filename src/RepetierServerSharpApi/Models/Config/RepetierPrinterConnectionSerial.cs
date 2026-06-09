using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConnectionSerial : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("baudrate"), JsonPropertyName("baudrate")]
        public partial long Baudrate { get; set; }

        [ObservableProperty]
        [JsonProperty("communicationTimeout"), JsonPropertyName("communicationTimeout")]
        public partial long CommunicationTimeout { get; set; }

        [ObservableProperty]
        [JsonProperty("connectionDelay"), JsonPropertyName("connectionDelay")]
        public partial long ConnectionDelay { get; set; }

        [ObservableProperty]
        [JsonProperty("device"), JsonPropertyName("device")]
        public partial string Device { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("dtr"), JsonPropertyName("dtr")]
        public partial long Dtr { get; set; }

        [ObservableProperty]
        [JsonProperty("emergencySolution"), JsonPropertyName("emergencySolution")]
        public partial long EmergencySolution { get; set; }

        [ObservableProperty]
        [JsonProperty("inputBufferSize"), JsonPropertyName("inputBufferSize")]
        public partial long InputBufferSize { get; set; }

        [ObservableProperty]
        [JsonProperty("interceptor"), JsonPropertyName("interceptor")]
        public partial bool Interceptor { get; set; }

        [ObservableProperty]
        [JsonProperty("malyanHack"), JsonPropertyName("malyanHack")]
        public partial bool MalyanHack { get; set; }

        [ObservableProperty]
        [JsonProperty("maxParallelCommands"), JsonPropertyName("maxParallelCommands")]
        public partial bool MaxParallelCommands { get; set; }

        [ObservableProperty]
        [JsonProperty("pingPong"), JsonPropertyName("pingPong")]
        public partial bool PingPong { get; set; }

        [ObservableProperty]
        [JsonProperty("rts"), JsonPropertyName("rts")]
        public partial long Rts { get; set; }

        [ObservableProperty]
        [JsonProperty("usbreset"), JsonPropertyName("usbreset")]
        public partial long Usbreset { get; set; }

        [ObservableProperty]
        [JsonProperty("visibleWithoutRunning"), JsonPropertyName("visibleWithoutRunning")]
        public partial bool VisibleWithoutRunning { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        #endregion
    }
}

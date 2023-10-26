using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConnectionSerial : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("baudrate")]
        [property: JsonIgnore]
        long baudrate;

        [ObservableProperty]
        [JsonProperty("communicationTimeout")]
        [property: JsonIgnore]
        long communicationTimeout;

        [ObservableProperty]
        [JsonProperty("connectionDelay")]
        [property: JsonIgnore]
        long connectionDelay;

        [ObservableProperty]
        [JsonProperty("device")]
        [property: JsonIgnore]
        string device;

        [ObservableProperty]
        [JsonProperty("dtr")]
        [property: JsonIgnore]
        long dtr;

        [ObservableProperty]
        [JsonProperty("emergencySolution")]
        [property: JsonIgnore]
        long emergencySolution;

        [ObservableProperty]
        [JsonProperty("inputBufferSize")]
        [property: JsonIgnore]
        long inputBufferSize;

        [ObservableProperty]
        [JsonProperty("interceptor")]
        [property: JsonIgnore]
        bool interceptor;

        [ObservableProperty]
        [JsonProperty("malyanHack")]
        [property: JsonIgnore]
        bool malyanHack;

        [ObservableProperty]
        [JsonProperty("maxParallelCommands")]
        [property: JsonIgnore]
        bool maxParallelCommands;

        [ObservableProperty]
        [JsonProperty("pingPong")]
        [property: JsonIgnore]
        bool pingPong;

        [ObservableProperty]
        [JsonProperty("rts")]
        [property: JsonIgnore]
        long rts;

        [ObservableProperty]
        [JsonProperty("usbreset")]
        [property: JsonIgnore]
        long usbreset;

        [ObservableProperty]
        [JsonProperty("visibleWithoutRunning")]
        [property: JsonIgnore]
        bool visibleWithoutRunning;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        
        #endregion
    }
}

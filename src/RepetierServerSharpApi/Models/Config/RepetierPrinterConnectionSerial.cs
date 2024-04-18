using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConnectionSerial : ObservableObject
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("baudrate")]
        long baudrate;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("communicationTimeout")]
        long communicationTimeout;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("connectionDelay")]
        long connectionDelay;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("device")]
        string device = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("dtr")]
        long dtr;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("emergencySolution")]
        long emergencySolution;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("inputBufferSize")]
        long inputBufferSize;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("interceptor")]
        bool interceptor;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("malyanHack")]
        bool malyanHack;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("maxParallelCommands")]
        bool maxParallelCommands;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("pingPong")]
        bool pingPong;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("rts")]
        long rts;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("usbreset")]
        long usbreset;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("visibleWithoutRunning")]
        bool visibleWithoutRunning;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        #endregion
    }
}

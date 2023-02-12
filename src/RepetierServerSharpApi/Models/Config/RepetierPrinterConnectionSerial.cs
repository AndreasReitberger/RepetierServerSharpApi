using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConnectionSerial : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("baudrate")]
        long baudrate;

        [ObservableProperty]
        [JsonProperty("communicationTimeout")]
        long communicationTimeout;

        [ObservableProperty]
        [JsonProperty("connectionDelay")]
        long connectionDelay;

        [ObservableProperty]
        [JsonProperty("device")]
        string device;

        [ObservableProperty]
        [JsonProperty("dtr")]
        long dtr;

        [ObservableProperty]
        [JsonProperty("emergencySolution")]
        long emergencySolution;

        [ObservableProperty]
        [JsonProperty("inputBufferSize")]
        long inputBufferSize;

        [ObservableProperty]
        [JsonProperty("interceptor")]
        bool interceptor;

        [ObservableProperty]
        [JsonProperty("malyanHack")]
        bool malyanHack;

        [ObservableProperty]
        [JsonProperty("maxParallelCommands")]
        bool maxParallelCommands;

        [ObservableProperty]
        [JsonProperty("pingPong")]
        bool pingPong;

        [ObservableProperty]
        [JsonProperty("rts")]
        long rts;

        [ObservableProperty]
        [JsonProperty("usbreset")]
        long usbreset;

        [ObservableProperty]
        [JsonProperty("visibleWithoutRunning")]
        bool visibleWithoutRunning;
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

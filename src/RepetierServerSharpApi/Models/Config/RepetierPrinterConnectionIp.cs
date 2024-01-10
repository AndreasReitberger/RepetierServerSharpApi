using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConnectionIp : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("address")]
        [property: JsonIgnore]
        string address;

        [ObservableProperty]
        [JsonProperty("keepAliveInterval")]
        [property: JsonIgnore]
        long keepAliveInterval;

        [ObservableProperty]
        [JsonProperty("port")]
        [property: JsonIgnore]
        long port;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        
        #endregion
    }

}

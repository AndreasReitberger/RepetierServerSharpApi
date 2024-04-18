using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConnectionIp : ObservableObject
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("address")]
        string address = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("keepAliveInterval")]
        long keepAliveInterval;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("port")]
        long port;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        
        #endregion
    }

}

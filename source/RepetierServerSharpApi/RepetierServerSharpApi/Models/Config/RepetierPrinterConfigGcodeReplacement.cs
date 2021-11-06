using Newtonsoft.Json;

namespace AndreasReitberger.Models
{
    public partial class RepetierPrinterConfigGcodeReplacement
    {
        [JsonProperty("comment", NullValueHandling = NullValueHandling.Ignore)]
        public string Comment { get; set; }

        [JsonProperty("expression", NullValueHandling = NullValueHandling.Ignore)]
        public string Expression { get; set; }

        [JsonProperty("script", NullValueHandling = NullValueHandling.Ignore)]
        public string Script { get; set; }
    }

}

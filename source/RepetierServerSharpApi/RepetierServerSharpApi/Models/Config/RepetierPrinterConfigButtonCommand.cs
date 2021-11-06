using Newtonsoft.Json;

namespace AndreasReitberger.Models
{
    public partial class RepetierPrinterConfigButtonCommand
    {
        [JsonProperty("command", NullValueHandling = NullValueHandling.Ignore)]
        public string Command { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
    }
}

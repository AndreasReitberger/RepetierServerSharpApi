using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{

    public partial class HardwareInfo
    {
        #region Properties
        [JsonProperty("icon", NullValueHandling = NullValueHandling.Ignore)]
        public long? Icon { get; set; }

        [JsonProperty("msgType", NullValueHandling = NullValueHandling.Ignore)]
        public long? MsgType { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; set; }

        [JsonProperty("unit", NullValueHandling = NullValueHandling.Ignore)]
        public string Unit { get; set; }

        [JsonProperty("urgency", NullValueHandling = NullValueHandling.Ignore)]
        public long? Urgency { get; set; }

        [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
        public string Url { get; set; }

        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public double? Value { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

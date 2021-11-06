using Newtonsoft.Json;
using System;

namespace AndreasReitberger.Models
{
    public partial class RepetierUpdateTeaser
    {
        #region Properties
        [JsonProperty("available")]
        public bool Available { get; set; }

        [JsonProperty("end")]
        public long End { get; set; }

        [JsonProperty("msg")]
        public string Msg { get; set; }

        [JsonProperty("start")]
        public long Start { get; set; }

        [JsonProperty("updated")]
        public long Updated { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }

}

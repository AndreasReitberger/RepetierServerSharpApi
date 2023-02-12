using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProject
    {
        #region Properties
        [JsonProperty("folder")]
        public long Folder { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("preview")]
        public string Preview { get; set; }

        [JsonProperty("uuid")]
        public Guid Uuid { get; set; }

        [JsonProperty("version")]
        public long Version { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

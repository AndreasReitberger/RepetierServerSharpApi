using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProjectFolder
    {
        #region Properties
        [JsonProperty("empty")]
        public bool Empty { get; set; }

        [JsonProperty("folders", NullValueHandling = NullValueHandling.Ignore)]
        public List<RepetierProjectSubFolder> Folders { get; set; } = new();

        [JsonProperty("idx")]
        public long Idx { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("parents", NullValueHandling = NullValueHandling.Ignore)]
        public List<RepetierProjectParentElement> Parents { get; set; } = new();

        [JsonProperty("projects", NullValueHandling = NullValueHandling.Ignore)]
        public List<RepetierProject> Projects { get; set; } = new();

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

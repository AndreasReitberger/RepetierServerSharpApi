﻿using Newtonsoft.Json;

namespace AndreasReitberger.Models
{
    public partial class RepetierProjectParentElement
    {
        #region Properties
        [JsonProperty("empty")]
        public bool Empty { get; set; }

        [JsonProperty("idx")]
        public long Idx { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

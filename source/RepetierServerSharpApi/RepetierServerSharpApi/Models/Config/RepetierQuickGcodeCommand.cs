using Newtonsoft.Json;

namespace AndreasReitberger.Models
{
    public partial class RepetierQuickGcodeCommand
    {
        #region Properties
        [JsonProperty("command")]
        public string Command { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

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

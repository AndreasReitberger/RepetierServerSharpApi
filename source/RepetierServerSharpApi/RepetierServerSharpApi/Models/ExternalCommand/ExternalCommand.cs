using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class ExternalCommand
    {
        #region Properties
        [JsonProperty("confirm")]
        public string Confirm { get; set; }

        [JsonProperty("execute")]
        public string Execute { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("local")]
        public bool Local { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("permAdd")]
        public bool PermAdd { get; set; }

        [JsonProperty("permConfig")]
        public bool PermConfig { get; set; }

        [JsonProperty("permDel")]
        public bool PermDel { get; set; }

        [JsonProperty("permPrint")]
        public bool PermPrint { get; set; }

        [JsonProperty("remote")]
        public bool Remote { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventNetworkInfoConnection
    {
        #region Properties
        [JsonProperty("SSID")]
        public string Ssid { get; set; }

        [JsonProperty("device")]
        public string Device { get; set; }

        [JsonProperty("uuid")]
        public Guid Uuid { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

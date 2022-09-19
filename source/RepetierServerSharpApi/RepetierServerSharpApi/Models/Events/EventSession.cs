﻿using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventSession
    {
        #region Properties
        [JsonProperty("callback_id")]
        public long CallbackId { get; set; }

        [JsonProperty("data")]
        public object Data { get; set; }

        [JsonProperty("session")]
        public string Session { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

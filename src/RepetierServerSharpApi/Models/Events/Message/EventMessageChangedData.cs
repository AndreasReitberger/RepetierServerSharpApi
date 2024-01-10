using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventMessageChangedData : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("date")]
        [property: JsonIgnore]
        public DateTimeOffset? date;

        [ObservableProperty]
        [JsonProperty("id")]
        [property: JsonIgnore]
        public long? id;

        [ObservableProperty]
        [JsonProperty("link")]
        [property: JsonIgnore]
        public string link;

        [ObservableProperty]
        [JsonProperty("msg")]
        [property: JsonIgnore]
        public string msg;

        [ObservableProperty]
        [JsonProperty("pause")]
        [property: JsonIgnore]
        public bool? pause;

        [ObservableProperty]
        [JsonProperty("slug")]
        [property: JsonIgnore]
        public string slug;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

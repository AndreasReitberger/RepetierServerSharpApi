using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventMessageChangedData : ObservableObject
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("date")]
        public DateTimeOffset? date;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("id")]
        public long? id;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("link")]
        public string link = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("msg")]
        public string msg = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("pause")]
        public bool? pause;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("slug")]
        public string slug = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

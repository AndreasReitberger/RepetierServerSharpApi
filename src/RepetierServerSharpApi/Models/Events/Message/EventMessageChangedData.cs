using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventMessageChangedData : ObservableObject
    {
        #region Properties
        [ObservableProperty]

        [JsonProperty("date")]
        public partial DateTimeOffset? Date { get; set; }

        [ObservableProperty]

        [JsonProperty("id")]
        public partial long? Id { get; set; }

        [ObservableProperty]

        [JsonProperty("link")]
        public partial string Link { get; set; } = string.Empty;

        [ObservableProperty]

        [JsonProperty("msg")]
        public partial string Msg { get; set; } = string.Empty;

        [ObservableProperty]

        [JsonProperty("pause")]
        public partial bool? Pause { get; set; }

        [ObservableProperty]

        [JsonProperty("slug")]
        public partial string Slug { get; set; } = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

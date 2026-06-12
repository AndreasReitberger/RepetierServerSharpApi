using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierMessage : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonProperty("id"), JsonPropertyName("id")]
        public partial long Id { get; set; }

        [ObservableProperty]
        [JsonProperty("msg"), JsonPropertyName("msg")]
        public partial string Msg { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("link"), JsonPropertyName("link")]
        public partial string Link { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("slug"), JsonPropertyName("slug")]
        public partial string Slug { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("date"), JsonPropertyName("date")]
        public partial string Date { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("pause"), JsonPropertyName("pause")]
        public partial bool Pause { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        public override bool Equals(object? obj)
        {
            if (obj is not RepetierMessage item)
                return false;
            return Id.Equals(item.Id);
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        #endregion
    }
}

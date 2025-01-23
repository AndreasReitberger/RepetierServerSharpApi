using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProjectSubFolder : ObservableObject
    {
        #region Properties
        [ObservableProperty]

        [JsonProperty("empty")]
        public partial bool Empty { get; set; }

        [ObservableProperty]

        [JsonProperty("idx")]
        public partial long Idx { get; set; }

        [ObservableProperty]

        [JsonProperty("name")]
        public partial string Name { get; set; } = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

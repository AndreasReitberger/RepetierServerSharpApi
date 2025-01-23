using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProjectsProjectComment : ObservableObject
    {
        #region Properties
        [ObservableProperty]

        [JsonProperty("comment")]
        public partial string Comment { get; set; } = string.Empty;

        [ObservableProperty]

        [JsonProperty("time")]
        public partial long? Time { get; set; }

        [ObservableProperty]

        [JsonProperty("user")]
        public partial string User { get; set; } = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.Repetier.Models
{

    public partial class RepetierHistorySummaryItem : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("aborted"), JsonPropertyName("aborted")]
        public partial long Aborted { get; set; }

        [ObservableProperty]
        [JsonProperty("computed"), JsonPropertyName("computed")]
        public partial double Computed { get; set; }

        [ObservableProperty]
        [JsonProperty("costs"), JsonPropertyName("costs")]
        public partial double Costs { get; set; }

        [ObservableProperty]
        [JsonProperty("filament"), JsonPropertyName("filament")]
        public partial double Filament { get; set; }

        [ObservableProperty]
        [JsonProperty("finished"), JsonPropertyName("finished")]
        public partial long Finished { get; set; }

        [ObservableProperty]
        [JsonProperty("month"), JsonPropertyName("month")]
        public partial long Month { get; set; }

        [ObservableProperty]
        [JsonProperty("num"), JsonPropertyName("num")]
        public partial long Num { get; set; }

        [ObservableProperty]
        [JsonProperty("real"), JsonPropertyName("real")]
        public partial double Real { get; set; }

        [ObservableProperty]
        [JsonProperty("year"), JsonPropertyName("year")]
        public partial long Year { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

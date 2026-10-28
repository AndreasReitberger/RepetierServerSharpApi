namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierHistorySummaryItem : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("aborted")]
        public partial long Aborted { get; set; }

        [ObservableProperty]
        [JsonPropertyName("computed")]
        public partial double Computed { get; set; }

        [ObservableProperty]
        [JsonPropertyName("costs")]
        public partial double Costs { get; set; }

        [ObservableProperty]
        [JsonPropertyName("filament")]
        public partial double Filament { get; set; }

        [ObservableProperty]
        [JsonPropertyName("finished")]
        public partial long Finished { get; set; }

        [ObservableProperty]
        [JsonPropertyName("month")]
        public partial long Month { get; set; }

        [ObservableProperty]
        [JsonPropertyName("num")]
        public partial long Num { get; set; }

        [ObservableProperty]
        [JsonPropertyName("real")]
        public partial double Real { get; set; }

        [ObservableProperty]
        [JsonPropertyName("year")]
        public partial long Year { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierHistorySummaryItem);
        #endregion
    }
}

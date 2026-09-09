namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierHistoryListItem : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("computedTime")]
        public partial double ComputedTime { get; set; }

        [ObservableProperty]
        [JsonPropertyName("costs")]
        public partial double Costs { get; set; }

        [ObservableProperty]
        [JsonPropertyName("endTime")]
        public partial double EndTime { get; set; }

        [ObservableProperty]
        [JsonPropertyName("filament")]
        public partial double Filament { get; set; }

        [ObservableProperty]
        [JsonPropertyName("filename")]
        public partial string Filename { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("id")]
        public partial long Id { get; set; }

        [ObservableProperty]
        [JsonPropertyName("month")]
        public partial long Month { get; set; }

        [ObservableProperty]
        [JsonPropertyName("notes")]
        public partial string Notes { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("pauseTime")]
        public partial double PauseTime { get; set; }

        [ObservableProperty]
        [JsonPropertyName("printerName")]
        public partial string PrinterName { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("printerSlug")]
        public partial string PrinterSlug { get; set; } = string.Empty;
        
        [ObservableProperty]
        [JsonPropertyName("printerUUID")]
        public partial string PrinterUuid { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("report")]
        public partial string Report { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("startTime")]
        public partial double StartTime { get; set; }

        [ObservableProperty]
        [JsonPropertyName("status")]
        public partial long Status { get; set; }

        [ObservableProperty]
        [JsonPropertyName("username")]
        public partial string Username { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("year")]
        public partial long Year { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierHistoryListItem);
        #endregion
    }
}

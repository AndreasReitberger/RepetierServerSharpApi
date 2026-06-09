using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierHistoryListItem : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("computedTime"), JsonPropertyName("computedTime")]
        public partial double ComputedTime { get; set; }

        [ObservableProperty]
        [JsonProperty("costs"), JsonPropertyName("costs")]
        public partial double Costs { get; set; }

        [ObservableProperty]
        [JsonProperty("endTime"), JsonPropertyName("endTime")]
        public partial double EndTime { get; set; }

        [ObservableProperty]
        [JsonProperty("filament"), JsonPropertyName("filament")]
        public partial double Filament { get; set; }

        [ObservableProperty]
        [JsonProperty("filename"), JsonPropertyName("filename")]
        public partial string Filename { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("id"), JsonPropertyName("id")]
        public partial long Id { get; set; }

        [ObservableProperty]
        [JsonProperty("month"), JsonPropertyName("month")]
        public partial long Month { get; set; }

        [ObservableProperty]
        [JsonProperty("notes"), JsonPropertyName("notes")]
        public partial string Notes { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("pauseTime"), JsonPropertyName("pauseTime")]
        public partial double PauseTime { get; set; }

        [ObservableProperty]
        [JsonProperty("printerName"), JsonPropertyName("printerName")]
        public partial string PrinterName { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("printerSlug"), JsonPropertyName("printerSlug")]
        public partial string PrinterSlug { get; set; } = string.Empty;
        
        [ObservableProperty]
        [JsonProperty("printerUUID"), JsonPropertyName("printerUUID")]
        public partial string PrinterUuid { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("report"), JsonPropertyName("report")]
        public partial string Report { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("startTime"), JsonPropertyName("startTime")]
        public partial double StartTime { get; set; }

        [ObservableProperty]
        [JsonProperty("status"), JsonPropertyName("status")]
        public partial long Status { get; set; }

        [ObservableProperty]
        [JsonProperty("username"), JsonPropertyName("username")]
        public partial string Username { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("year"), JsonPropertyName("year")]
        public partial long Year { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

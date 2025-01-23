using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierHistoryListItem : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        
        [JsonProperty("computedTime")]
        public partial double ComputedTime { get; set; }

        [ObservableProperty]
        
        [JsonProperty("costs")]
        public partial double Costs { get; set; }

        [ObservableProperty]
        
        [JsonProperty("endTime")]
        public partial double EndTime { get; set; }

        [ObservableProperty]
        
        [JsonProperty("filament")]
        public partial double Filament { get; set; }

        [ObservableProperty]
        
        [JsonProperty("filename")]
        public partial string Filename { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("id")]
        public partial long Id { get; set; }

        [ObservableProperty]
        
        [JsonProperty("month")]
        public partial long Month { get; set; }

        [ObservableProperty]
        
        [JsonProperty("notes")]
        public partial string Notes { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("pauseTime")]
        public partial double PauseTime { get; set; }

        [ObservableProperty]
        
        [JsonProperty("printerName")]
        public partial string PrinterName { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("printerSlug")]
        public partial string PrinterSlug { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("printerUUID")]
        public partial string PrinterUuid { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("report")]
        public partial string Report { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("startTime")]
        public partial double StartTime { get; set; }

        [ObservableProperty]
        
        [JsonProperty("status")]
        public partial long Status { get; set; }

        [ObservableProperty]
        
        [JsonProperty("username")]
        public partial string Username { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("year")]
        public partial long Year { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

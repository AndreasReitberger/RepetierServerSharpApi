using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigGeneral : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonPropertyName("active")]
        public partial bool Active { get; set; }

        [ObservableProperty]
        [JsonPropertyName("defaultVolumetric")]
        public partial bool DefaultVolumetric { get; set; }

        [ObservableProperty]
        [JsonPropertyName("deleteJobAfterManualStop")]
        public partial bool DeleteJobAfterManualStop { get; set; }

        [ObservableProperty]
        [JsonPropertyName("doorHandling")]
        public partial long DoorHandling { get; set; }

        [ObservableProperty]
        [JsonPropertyName("eepromType")]
        public partial string EepromType { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("enableQueue")]
        public partial bool EnableQueue { get; set; }

        [ObservableProperty]
        [JsonPropertyName("firmwareName")]
        public partial string FirmwareName { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("heatedBed")]
        public partial bool HeatedBed { get; set; }

        [ObservableProperty]
        [JsonPropertyName("logHistory")]
        public partial bool LogHistory { get; set; }

        [ObservableProperty]
        [JsonPropertyName("manufacturer")]
        public partial string Manufacturer { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("model")]
        public partial string Model { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("name")]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("numFans")]
        public partial long NumFans { get; set; }

        [ObservableProperty]
        [JsonPropertyName("pauseHandling")]
        public partial long PauseHandling { get; set; }

        [ObservableProperty]
        [JsonPropertyName("pauseSeconds")]
        public partial long PauseSeconds { get; set; }

        [ObservableProperty]
        [JsonPropertyName("printerHomepage")]
        public partial Uri? PrinterHomepage { get; set; }

        [ObservableProperty]
        [JsonPropertyName("printerManual")]
        public partial string PrinterManual { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("printerVariant")]
        public partial string PrinterVariant { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("sdcard")]
        public partial bool Sdcard { get; set; }

        [ObservableProperty]
        [JsonPropertyName("slug")]
        public partial string Slug { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("softwareLight")]
        public partial bool SoftwareLight { get; set; }

        [ObservableProperty]
        [JsonPropertyName("softwarePower")]
        public partial bool SoftwarePower { get; set; }

        [ObservableProperty]
        [JsonPropertyName("tempUpdateEvery")]
        public partial long TempUpdateEvery { get; set; }

        [ObservableProperty]
        [JsonPropertyName("useModelFromSlug")]
        public partial string UseModelFromSlug { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("useOwnModelRepository")]
        public partial bool UseOwnModelRepository { get; set; }

        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierPrinterConfigGeneral);

        #endregion
    }
}

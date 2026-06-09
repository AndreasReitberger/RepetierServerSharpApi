using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigGeneral : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonProperty("active"), JsonPropertyName("active")]
        public partial bool Active { get; set; }

        [ObservableProperty]
        [JsonProperty("defaultVolumetric"), JsonPropertyName("defaultVolumetric")]
        public partial bool DefaultVolumetric { get; set; }

        [ObservableProperty]
        [JsonProperty("deleteJobAfterManualStop"), JsonPropertyName("deleteJobAfterManualStop")]
        public partial bool DeleteJobAfterManualStop { get; set; }

        [ObservableProperty]
        [JsonProperty("doorHandling"), JsonPropertyName("doorHandling")]
        public partial long DoorHandling { get; set; }

        [ObservableProperty]
        [JsonProperty("eepromType"), JsonPropertyName("eepromType")]
        public partial string EepromType { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("enableQueue"), JsonPropertyName("enableQueue")]
        public partial bool EnableQueue { get; set; }

        [ObservableProperty]
        [JsonProperty("firmwareName"), JsonPropertyName("firmwareName")]
        public partial string FirmwareName { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("heatedBed"), JsonPropertyName("heatedBed")]
        public partial bool HeatedBed { get; set; }

        [ObservableProperty]
        [JsonProperty("logHistory"), JsonPropertyName("logHistory")]
        public partial bool LogHistory { get; set; }

        [ObservableProperty]
        [JsonProperty("manufacturer"), JsonPropertyName("manufacturer")]
        public partial string Manufacturer { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("model"), JsonPropertyName("model")]
        public partial string Model { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("name"), JsonPropertyName("name")]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("numFans"), JsonPropertyName("numFans")]
        public partial long NumFans { get; set; }

        [ObservableProperty]
        [JsonProperty("pauseHandling"), JsonPropertyName("pauseHandling")]
        public partial long PauseHandling { get; set; }

        [ObservableProperty]
        [JsonProperty("pauseSeconds"), JsonPropertyName("pauseSeconds")]
        public partial long PauseSeconds { get; set; }

        [ObservableProperty]
        [JsonProperty("printerHomepage"), JsonPropertyName("printerHomepage")]
        public partial Uri? PrinterHomepage { get; set; }

        [ObservableProperty]
        [JsonProperty("printerManual"), JsonPropertyName("printerManual")]
        public partial string PrinterManual { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("printerVariant"), JsonPropertyName("printerVariant")]
        public partial string PrinterVariant { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("sdcard"), JsonPropertyName("sdcard")]
        public partial bool Sdcard { get; set; }

        [ObservableProperty]
        [JsonProperty("slug"), JsonPropertyName("slug")]
        public partial string Slug { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("softwareLight"), JsonPropertyName("softwareLight")]
        public partial bool SoftwareLight { get; set; }

        [ObservableProperty]
        [JsonProperty("softwarePower"), JsonPropertyName("softwarePower")]
        public partial bool SoftwarePower { get; set; }

        [ObservableProperty]
        [JsonProperty("tempUpdateEvery"), JsonPropertyName("tempUpdateEvery")]
        public partial long TempUpdateEvery { get; set; }

        [ObservableProperty]
        [JsonProperty("useModelFromSlug"), JsonPropertyName("useModelFromSlug")]
        public partial string UseModelFromSlug { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("useOwnModelRepository"), JsonPropertyName("useOwnModelRepository")]
        public partial bool UseOwnModelRepository { get; set; }

        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        #endregion
    }
}

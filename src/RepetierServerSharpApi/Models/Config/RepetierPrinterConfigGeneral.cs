using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigGeneral : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        
        [JsonProperty("active")]
        public partial bool Active { get; set; }

        [ObservableProperty]
        
        [JsonProperty("defaultVolumetric")]
        public partial bool DefaultVolumetric { get; set; }

        [ObservableProperty]
        
        [JsonProperty("deleteJobAfterManualStop")]
        public partial bool DeleteJobAfterManualStop { get; set; }

        [ObservableProperty]
        
        [JsonProperty("doorHandling")]
        public partial long DoorHandling { get; set; }

        [ObservableProperty]
        
        [JsonProperty("eepromType")]
        public partial string EepromType { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("enableQueue")]
        public partial bool EnableQueue { get; set; }

        [ObservableProperty]
        
        [JsonProperty("firmwareName")]
        public partial string FirmwareName { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("heatedBed")]
        public partial bool HeatedBed { get; set; }

        [ObservableProperty]
        
        [JsonProperty("logHistory")]
        public partial bool LogHistory { get; set; }

        [ObservableProperty]
        
        [JsonProperty("manufacturer")]
        public partial string Manufacturer { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("model")]
        public partial string Model { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("name")]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("numFans")]
        public partial long NumFans { get; set; }

        [ObservableProperty]
        
        [JsonProperty("pauseHandling")]
        public partial long PauseHandling { get; set; }

        [ObservableProperty]
        
        [JsonProperty("pauseSeconds")]
        public partial long PauseSeconds { get; set; }

        [ObservableProperty]
        
        [JsonProperty("printerHomepage")]
        public partial Uri? PrinterHomepage { get; set; }

        [ObservableProperty]
        
        [JsonProperty("printerManual")]
        public partial string PrinterManual { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("printerVariant")]
        public partial string PrinterVariant { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("sdcard")]
        public partial bool Sdcard { get; set; }

        [ObservableProperty]
        
        [JsonProperty("slug")]
        public partial string Slug { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("softwareLight")]
        public partial bool SoftwareLight { get; set; }

        [ObservableProperty]
        
        [JsonProperty("softwarePower")]
        public partial bool SoftwarePower { get; set; }

        [ObservableProperty]
        
        [JsonProperty("tempUpdateEvery")]
        public partial long TempUpdateEvery { get; set; }

        [ObservableProperty]
        
        [JsonProperty("useModelFromSlug")]
        public partial string UseModelFromSlug { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("useOwnModelRepository")]
        public partial bool UseOwnModelRepository { get; set; }

        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        #endregion
    }
}

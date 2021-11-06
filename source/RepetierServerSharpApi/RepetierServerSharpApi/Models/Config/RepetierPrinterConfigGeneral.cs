using Newtonsoft.Json;

namespace AndreasReitberger.Models
{
    public partial class RepetierPrinterConfigGeneral
    {
        #region MyRegion

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("defaultVolumetric")]
        public bool DefaultVolumetric { get; set; }

        [JsonProperty("doorHandling")]
        public long DoorHandling { get; set; }

        [JsonProperty("eepromType")]
        public string EepromType { get; set; }

        [JsonProperty("firmwareName")]
        public string FirmwareName { get; set; }

        [JsonProperty("heatedBed")]
        public bool HeatedBed { get; set; }

        [JsonProperty("logHistory")]
        public bool LogHistory { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("numFans")]
        public long NumFans { get; set; }

        [JsonProperty("pauseHandling")]
        public long PauseHandling { get; set; }

        [JsonProperty("pauseSeconds")]
        public long PauseSeconds { get; set; }

        [JsonProperty("printerVariant")]
        public string PrinterVariant { get; set; }

        [JsonProperty("sdcard")]
        public bool Sdcard { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("softwarePower")]
        public bool SoftwarePower { get; set; }

        [JsonProperty("tempUpdateEvery")]
        public long TempUpdateEvery { get; set; }

        [JsonProperty("useModelFromSlug")]
        public string UseModelFromSlug { get; set; }

        [JsonProperty("useOwnModelRepository")]
        public bool UseOwnModelRepository { get; set; }

        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigGeneral : ObservableObject
    {
        #region Properties

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("active")]
        bool active;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("defaultVolumetric")]
        bool defaultVolumetric;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("deleteJobAfterManualStop")]
        bool deleteJobAfterManualStop;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("doorHandling")]
        long doorHandling;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("eepromType")]
        string eepromType = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("enableQueue")]
        bool enableQueue;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("firmwareName")]
        string firmwareName = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("heatedBed")]
        bool heatedBed;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("logHistory")]
        bool logHistory;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("manufacturer")]
        string manufacturer = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("model")]
        string model = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("name")]
        string name = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("numFans")]
        long numFans;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("pauseHandling")]
        long pauseHandling;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("pauseSeconds")]
        long pauseSeconds;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("printerHomepage")]
        Uri? printerHomepage;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("printerManual")]
        string printerManual = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("printerVariant")]
        string printerVariant = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("sdcard")]
        bool sdcard;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("slug")]
        string slug = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("softwareLight")]
        bool softwareLight;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("softwarePower")]
        bool softwarePower;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("tempUpdateEvery")]
        long tempUpdateEvery;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("useModelFromSlug")]
        string useModelFromSlug = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("useOwnModelRepository")]
        bool useOwnModelRepository;

        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        #endregion
    }
}

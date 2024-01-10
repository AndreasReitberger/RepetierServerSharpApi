using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigGeneral : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonProperty("active")]
        [property: JsonIgnore]
        bool active;

        [ObservableProperty]
        [JsonProperty("defaultVolumetric")]
        [property: JsonIgnore]
        bool defaultVolumetric;

        [ObservableProperty]
        [JsonProperty("deleteJobAfterManualStop")]
        [property: JsonIgnore]
        bool deleteJobAfterManualStop;

        [ObservableProperty]
        [JsonProperty("doorHandling")]
        [property: JsonIgnore]
        long doorHandling;

        [ObservableProperty]
        [JsonProperty("eepromType")]
        [property: JsonIgnore]
        string eepromType;

        [ObservableProperty]
        [JsonProperty("enableQueue")]
        [property: JsonIgnore]
        bool enableQueue;

        [ObservableProperty]
        [JsonProperty("firmwareName")]
        [property: JsonIgnore]
        string firmwareName;

        [ObservableProperty]
        [JsonProperty("heatedBed")]
        [property: JsonIgnore]
        bool heatedBed;

        [ObservableProperty]
        [JsonProperty("logHistory")]
        [property: JsonIgnore]
        bool logHistory;

        [ObservableProperty]
        [JsonProperty("manufacturer")]
        [property: JsonIgnore]
        string manufacturer;

        [ObservableProperty]
        [JsonProperty("model")]
        [property: JsonIgnore]
        string model;

        [ObservableProperty]
        [JsonProperty("name")]
        [property: JsonIgnore]
        string name;

        [ObservableProperty]
        [JsonProperty("numFans")]
        [property: JsonIgnore]
        long numFans;

        [ObservableProperty]
        [JsonProperty("pauseHandling")]
        [property: JsonIgnore]
        long pauseHandling;

        [ObservableProperty]
        [JsonProperty("pauseSeconds")]
        [property: JsonIgnore]
        long pauseSeconds;

        [ObservableProperty]
        [JsonProperty("printerHomepage")]
        [property: JsonIgnore]
        Uri printerHomepage;

        [ObservableProperty]
        [JsonProperty("printerManual")]
        [property: JsonIgnore]
        string printerManual;

        [ObservableProperty]
        [JsonProperty("printerVariant")]
        [property: JsonIgnore]
        string printerVariant;

        [ObservableProperty]
        [JsonProperty("sdcard")]
        [property: JsonIgnore]
        bool sdcard;

        [ObservableProperty]
        [JsonProperty("slug")]
        [property: JsonIgnore]
        string slug;

        [ObservableProperty]
        [JsonProperty("softwareLight")]
        [property: JsonIgnore]
        bool softwareLight;

        [ObservableProperty]
        [JsonProperty("softwarePower")]
        [property: JsonIgnore]
        bool softwarePower;

        [ObservableProperty]
        [JsonProperty("tempUpdateEvery")]
        [property: JsonIgnore]
        long tempUpdateEvery;

        [ObservableProperty]
        [JsonProperty("useModelFromSlug")]
        [property: JsonIgnore]
        string useModelFromSlug;

        [ObservableProperty]
        [JsonProperty("useOwnModelRepository")]
        [property: JsonIgnore]
        bool useOwnModelRepository;

        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        
        #endregion
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigGeneral : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonProperty("active")]
        bool active;

        [ObservableProperty]
        [JsonProperty("defaultVolumetric")]
        bool defaultVolumetric;

        [ObservableProperty]
        [JsonProperty("deleteJobAfterManualStop")]
        bool deleteJobAfterManualStop;

        [ObservableProperty]
        [JsonProperty("doorHandling")]
        long doorHandling;

        [ObservableProperty]
        [JsonProperty("eepromType")]
        string eepromType;

        [ObservableProperty]
        [JsonProperty("enableQueue")]
        bool enableQueue;

        [ObservableProperty]
        [JsonProperty("firmwareName")]
        string firmwareName;

        [ObservableProperty]
        [JsonProperty("heatedBed")]
        bool heatedBed;

        [ObservableProperty]
        [JsonProperty("logHistory")]
        bool logHistory;

        [ObservableProperty]
        [JsonProperty("manufacturer")]
        string manufacturer;

        [ObservableProperty]
        [JsonProperty("model")]
        string model;

        [ObservableProperty]
        [JsonProperty("name")]
        string name;

        [ObservableProperty]
        [JsonProperty("numFans")]
        long numFans;

        [ObservableProperty]
        [JsonProperty("pauseHandling")]
        long pauseHandling;

        [ObservableProperty]
        [JsonProperty("pauseSeconds")]
        long pauseSeconds;

        [ObservableProperty]
        [JsonProperty("printerHomepage")]
        Uri printerHomepage;

        [ObservableProperty]
        [JsonProperty("printerManual")]
        string printerManual;

        [ObservableProperty]
        [JsonProperty("printerVariant")]
        string printerVariant;

        [ObservableProperty]
        [JsonProperty("sdcard")]
        bool sdcard;

        [ObservableProperty]
        [JsonProperty("slug")]
        string slug;

        [ObservableProperty]
        [JsonProperty("softwareLight")]
        bool softwareLight;

        [ObservableProperty]
        [JsonProperty("softwarePower")]
        bool softwarePower;

        [ObservableProperty]
        [JsonProperty("tempUpdateEvery")]
        long tempUpdateEvery;

        [ObservableProperty]
        [JsonProperty("useModelFromSlug")]
        string useModelFromSlug;

        [ObservableProperty]
        [JsonProperty("useOwnModelRepository")]
        bool useOwnModelRepository;

        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

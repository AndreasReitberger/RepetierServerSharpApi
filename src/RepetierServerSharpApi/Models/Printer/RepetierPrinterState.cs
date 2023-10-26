using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterState : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonProperty("activeExtruder")]
        [property: JsonIgnore]
        long activeExtruder;

        [ObservableProperty]
        [JsonProperty("autostartNextPrint")]
        [property: JsonIgnore]
        bool autostartNextPrint;

        [ObservableProperty]
        [JsonProperty("condition")]
        [property: JsonIgnore]
        long condition;

        [ObservableProperty]
        [JsonProperty("conditionReason")]
        [property: JsonIgnore]
        string conditionReason;

        [ObservableProperty]
        [JsonProperty("debugLevel")]
        [property: JsonIgnore]
        long debugLevel;

        [ObservableProperty]
        [JsonProperty("doorOpen")]
        [property: JsonIgnore]
        bool doorOpen;

        [ObservableProperty]
        [JsonProperty("extruder")]
        [property: JsonIgnore]
        List<RepetierPrinterToolhead> extruder = new();

        [ObservableProperty]
        [JsonProperty("f")]
        [property: JsonIgnore]
        long f;

        [ObservableProperty]
        [JsonProperty("fans")]
        [property: JsonIgnore]
        List<RepetierPrinterFan> fans = new();

        [ObservableProperty]
        [JsonProperty("filterFan")]
        [property: JsonIgnore]
        bool filterFan;

        [ObservableProperty]
        [JsonProperty("firmware")]
        [property: JsonIgnore]
        string firmware;

        [ObservableProperty]
        [JsonProperty("firmwareStyle")]
        [property: JsonIgnore]
        string firmwareStyle;

        [ObservableProperty]
        [JsonProperty("firmwareURL")]
        [property: JsonIgnore]
        Uri firmwareUrl;

        [ObservableProperty]
        [JsonProperty("flowMultiply")]
        [property: JsonIgnore]
        long flowMultiply;

        [ObservableProperty]
        [JsonProperty("global")]
        [property: JsonIgnore]
        RepetierPrinterStateGlobal global;

        [ObservableProperty]
        [JsonProperty("gperm")]
        [property: JsonIgnore]
        RepetierPrinterStateGlobal gperm;

        [ObservableProperty]
        [JsonProperty("hasXHome")]
        [property: JsonIgnore]
        bool hasXHome;

        [ObservableProperty]
        [JsonProperty("hasYHome")]
        [property: JsonIgnore]
        bool hasYHome;

        [ObservableProperty]
        [JsonProperty("hasZHome")]
        [property: JsonIgnore]
        bool hasZHome;

        [ObservableProperty]
        [JsonProperty("heatedBeds")]
        [property: JsonIgnore]
        List<RepetierPrinterHeaterComponent> heatedBeds = new();

        [ObservableProperty]
        [JsonProperty("heatedChambers")]
        [property: JsonIgnore]
        List<RepetierPrinterHeaterComponent> heatedChambers = new();

        [ObservableProperty]
        [JsonProperty("layer")]
        [property: JsonIgnore]
        long layer;

        [ObservableProperty]
        [JsonProperty("lights")]
        [property: JsonIgnore]
        long lights;

        [ObservableProperty]
        [JsonProperty("maxLayer")]
        [property: JsonIgnore]
        long maxLayer;

        [ObservableProperty]
        [JsonProperty("notification")]
        [property: JsonIgnore]
        string notification;

        [ObservableProperty]
        [JsonProperty("numExtruder")]
        [property: JsonIgnore]
        long numExtruder;

        [ObservableProperty]
        [JsonProperty("perm")]
        [property: JsonIgnore]
        RepetierPrinterStateGlobal perm;

        [ObservableProperty]
        [JsonProperty("powerOn")]
        [property: JsonIgnore]
        bool powerOn;

        [ObservableProperty]
        [JsonProperty("rec")]
        [property: JsonIgnore]
        bool rec;

        [ObservableProperty]
        [JsonProperty("sdcardMounted")]
        [property: JsonIgnore]
        bool sdcardMounted;

        [ObservableProperty]
        [JsonProperty("sglobal")]
        [property: JsonIgnore]
        RepetierPrinterStateGlobal sglobal;

        [ObservableProperty]
        [JsonProperty("shutdownAfterPrint")]
        [property: JsonIgnore]
        bool shutdownAfterPrint;

        [ObservableProperty]
        [JsonProperty("speedMultiply")]
        [property: JsonIgnore]
        long speedMultiply;

        [ObservableProperty]
        [JsonProperty("volumetric")]
        [property: JsonIgnore]
        bool volumetric;

        [ObservableProperty]
        [JsonProperty("webcams")]
        [property: JsonIgnore]
        List<RepetierPrinterConfigWebcam> webcams = new();

        [ObservableProperty]
        [JsonProperty("x")]
        [property: JsonIgnore]
        long x;

        [ObservableProperty]
        [JsonProperty("xOff")]
        [property: JsonIgnore]
        long xOff;

        [ObservableProperty]
        [JsonProperty("y")]
        [property: JsonIgnore]
        long y;

        [ObservableProperty]
        [JsonProperty("yOff")]
        [property: JsonIgnore]
        long yOff;

        [ObservableProperty]
        [JsonProperty("z")]
        [property: JsonIgnore]
        long z;

        [ObservableProperty]
        [JsonProperty("zOff")]
        [property: JsonIgnore]
        long zOff;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

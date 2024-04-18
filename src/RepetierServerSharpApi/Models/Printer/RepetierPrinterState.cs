using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterState : ObservableObject
    {
        #region Properties

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("activeExtruder")]

        long activeExtruder;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("autostartNextPrint")]

        bool autostartNextPrint;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("condition")]

        long condition;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("conditionReason")]

        string conditionReason;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("debugLevel")]

        long debugLevel;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("doorOpen")]

        bool doorOpen;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("extruder")]

        List<RepetierPrinterToolhead> extruder = new();

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("f")]

        long f;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("fans")]

        List<RepetierPrinterFan> fans = new();

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("filterFan")]

        bool filterFan;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("firmware")]

        string firmware;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("firmwareStyle")]

        string firmwareStyle;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("firmwareURL")]

        Uri firmwareUrl;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("flowMultiply")]

        long flowMultiply;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("global")]

        RepetierPrinterStateGlobal global;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("gperm")]

        RepetierPrinterStateGlobal gperm;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("hasXHome")]

        bool hasXHome;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("hasYHome")]

        bool hasYHome;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("hasZHome")]

        bool hasZHome;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("heatedBeds")]

        List<RepetierPrinterHeaterComponent> heatedBeds = new();

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("heatedChambers")]

        List<RepetierPrinterHeaterComponent> heatedChambers = new();

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("layer")]

        long layer;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("lights")]

        long lights;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("maxLayer")]

        long maxLayer;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("notification")]

        string notification;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("numExtruder")]

        long numExtruder;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("perm")]

        RepetierPrinterStateGlobal perm;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("powerOn")]

        bool powerOn;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("rec")]

        bool rec;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("sdcardMounted")]

        bool sdcardMounted;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("sglobal")]

        RepetierPrinterStateGlobal sglobal;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("shutdownAfterPrint")]

        bool shutdownAfterPrint;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("speedMultiply")]

        long speedMultiply;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("volumetric")]

        bool volumetric;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("webcams")]

        List<RepetierPrinterConfigWebcam> webcams = new();

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("x")]

        long x;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("xOff")]

        long xOff;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("y")]

        long y;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("yOff")]

        long yOff;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("z")]

        long z;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("zOff")]

        long zOff;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

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
        long activeExtruder;

        [ObservableProperty]
        [JsonProperty("autostartNextPrint")]
        bool autostartNextPrint;

        [ObservableProperty]
        [JsonProperty("condition")]
        long condition;

        [ObservableProperty]
        [JsonProperty("conditionReason")]
        string conditionReason;

        [ObservableProperty]
        [JsonProperty("debugLevel")]
        long debugLevel;

        [ObservableProperty]
        [JsonProperty("doorOpen")]
        bool doorOpen;

        [ObservableProperty]
        [JsonProperty("extruder")]
        List<RepetierPrinterToolhead> extruder = new();

        [ObservableProperty]
        [JsonProperty("f")]
        long f;

        [ObservableProperty]
        [JsonProperty("fans")]
        List<RepetierPrinterFan> fans = new();

        [ObservableProperty]
        [JsonProperty("filterFan")]
        bool filterFan;

        [ObservableProperty]
        [JsonProperty("firmware")]
        string firmware;

        [ObservableProperty]
        [JsonProperty("firmwareStyle")]
        string firmwareStyle;

        [ObservableProperty]
        [JsonProperty("firmwareURL")]
        Uri firmwareUrl;

        [ObservableProperty]
        [JsonProperty("flowMultiply")]
        long flowMultiply;

        [ObservableProperty]
        [JsonProperty("global")]
        RepetierPrinterStateGlobal global;

        [ObservableProperty]
        [JsonProperty("gperm")]
        RepetierPrinterStateGlobal gperm;

        [ObservableProperty]
        [JsonProperty("hasXHome")]
        bool hasXHome;

        [ObservableProperty]
        [JsonProperty("hasYHome")]
        bool hasYHome;

        [ObservableProperty]
        [JsonProperty("hasZHome")]
        bool hasZHome;

        [ObservableProperty]
        [JsonProperty("heatedBeds")]
        List<RepetierPrinterHeaterComponent> heatedBeds = new();

        [ObservableProperty]
        [JsonProperty("heatedChambers")]
        List<RepetierPrinterHeaterComponent> heatedChambers = new();

        [ObservableProperty]
        [JsonProperty("layer")]
        long layer;

        [ObservableProperty]
        [JsonProperty("lights")]
        long lights;

        [ObservableProperty]
        [JsonProperty("maxLayer")]
        long maxLayer;

        [ObservableProperty]
        [JsonProperty("notification")]
        string notification;

        [ObservableProperty]
        [JsonProperty("numExtruder")]
        long numExtruder;

        [ObservableProperty]
        [JsonProperty("perm")]
        RepetierPrinterStateGlobal perm;

        [ObservableProperty]
        [JsonProperty("powerOn")]
        bool powerOn;

        [ObservableProperty]
        [JsonProperty("rec")]
        bool rec;

        [ObservableProperty]
        [JsonProperty("sdcardMounted")]
        bool sdcardMounted;

        [ObservableProperty]
        [JsonProperty("sglobal")]
        RepetierPrinterStateGlobal sglobal;

        [ObservableProperty]
        [JsonProperty("shutdownAfterPrint")]
        bool shutdownAfterPrint;

        [ObservableProperty]
        [JsonProperty("speedMultiply")]
        long speedMultiply;

        [ObservableProperty]
        [JsonProperty("volumetric")]
        bool volumetric;

        [ObservableProperty]
        [JsonProperty("webcams")]
        List<RepetierPrinterConfigWebcam> webcams = new();

        [ObservableProperty]
        [JsonProperty("x")]
        long x;

        [ObservableProperty]
        [JsonProperty("xOff")]
        long xOff;

        [ObservableProperty]
        [JsonProperty("y")]
        long y;

        [ObservableProperty]
        [JsonProperty("yOff")]
        long yOff;

        [ObservableProperty]
        [JsonProperty("z")]
        long z;

        [ObservableProperty]
        [JsonProperty("zOff")]
        long zOff;
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

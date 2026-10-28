using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterState : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonPropertyName("activeExtruder")]
        public partial long ActiveExtruder { get; set; }

        [ObservableProperty]
        [JsonPropertyName("autostartNextPrint")]
        public partial bool AutostartNextPrint { get; set; }

        [ObservableProperty]
        [JsonPropertyName("condition")]
        public partial long Condition { get; set; }

        [ObservableProperty]
        [JsonPropertyName("conditionReason")]
        public partial string ConditionReason { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("debugLevel")]
        public partial long DebugLevel { get; set; }

        [ObservableProperty]
        [JsonPropertyName("doorOpen")]
        public partial bool DoorOpen { get; set; }

        [ObservableProperty]
        [JsonPropertyName("extruder")]
        public partial List<RepetierPrinterToolhead> Extruder { get; set; } = [];

        [ObservableProperty]
        [JsonPropertyName("f")]
        public partial double F { get; set; }

        [ObservableProperty]
        [JsonPropertyName("fans")]
        public partial List<RepetierPrinterFan> Fans { get; set; } = [];

        [ObservableProperty]
        [JsonPropertyName("filterFan")]
        public partial bool FilterFan { get; set; }

        [ObservableProperty]
        [JsonPropertyName("firmware")]
        public partial string Firmware { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("firmwareStyle")]
        public partial string FirmwareStyle { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("firmwareURL")]
        public partial Uri? FirmwareUrl { get; set; }

        [ObservableProperty]
        [JsonPropertyName("flowMultiply")]
        public partial long FlowMultiply { get; set; }

        [ObservableProperty]
        [JsonPropertyName("global")]
        public partial RepetierPrinterStateGlobal? Global { get; set; }

        [ObservableProperty]
        [JsonPropertyName("gperm")]
        public partial RepetierPrinterStateGlobal? Gperm { get; set; }

        [ObservableProperty]
        [JsonPropertyName("hasXHome")]
        public partial bool HasXHome { get; set; }

        [ObservableProperty]
        [JsonPropertyName("hasYHome")]
        public partial bool HasYHome { get; set; }

        [ObservableProperty]
        [JsonPropertyName("hasZHome")]
        public partial bool HasZHome { get; set; }

        [ObservableProperty]
        [JsonPropertyName("heatedBeds")]
        public partial List<RepetierPrinterHeaterComponent> HeatedBeds { get; set; } = [];

        [ObservableProperty]
        [JsonPropertyName("heatedChambers")]
        public partial List<RepetierPrinterHeaterComponent> HeatedChambers { get; set; } = [];

        [ObservableProperty]
        [JsonPropertyName("layer")]
        public partial long Layer { get; set; }

        [ObservableProperty]
        [JsonPropertyName("lights")]
        public partial long Lights { get; set; }

        [ObservableProperty]
        [JsonPropertyName("maxLayer")]
        public partial long MaxLayer { get; set; }

        [ObservableProperty]
        [JsonPropertyName("notification")]
        public partial string Notification { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("numExtruder")]
        public partial long NumExtruder { get; set; }

        [ObservableProperty]
        [JsonPropertyName("perm")]
        public partial RepetierPrinterStateGlobal? Perm { get; set; }

        [ObservableProperty]
        [JsonPropertyName("powerOn")]
        public partial bool PowerOn { get; set; }

        [ObservableProperty]
        [JsonPropertyName("rec")]
        public partial bool Rec { get; set; }

        [ObservableProperty]
        [JsonPropertyName("sdcardMounted")]
        public partial bool SdcardMounted { get; set; }

        [ObservableProperty]
        [JsonPropertyName("sglobal")]
        public partial RepetierPrinterStateGlobal? Sglobal { get; set; }

        [ObservableProperty]
        [JsonPropertyName("shutdownAfterPrint")]
        public partial bool ShutdownAfterPrint { get; set; }

        [ObservableProperty]
        [JsonPropertyName("speedMultiply")]
        public partial long SpeedMultiply { get; set; }

        [ObservableProperty]
        [JsonPropertyName("volumetric")]
        public partial bool Volumetric { get; set; }

        [ObservableProperty]
        [JsonPropertyName("webcams")]
        public partial List<RepetierPrinterConfigWebcam> Webcams { get; set; } = [];

        [ObservableProperty]
        [JsonPropertyName("x")]
        public partial double X { get; set; }

        [ObservableProperty]
        [JsonPropertyName("xOff")]
        public partial double XOff { get; set; }

        [ObservableProperty]
        [JsonPropertyName("y")]
        public partial double Y { get; set; }

        [ObservableProperty]
        [JsonPropertyName("yOff")]
        public partial double YOff { get; set; }

        [ObservableProperty]
        [JsonPropertyName("z")]
        public partial double Z { get; set; }

        [ObservableProperty]
        [JsonPropertyName("zOff")]
        public partial double ZOff { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierPrinterState);
        #endregion
    }
}

using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterState : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonProperty("activeExtruder"), JsonPropertyName("activeExtruder")]
        public partial long ActiveExtruder { get; set; }

        [ObservableProperty]
        [JsonProperty("autostartNextPrint"), JsonPropertyName("autostartNextPrint")]
        public partial bool AutostartNextPrint { get; set; }

        [ObservableProperty]
        [JsonProperty("condition"), JsonPropertyName("condition")]
        public partial long Condition { get; set; }

        [ObservableProperty]
        [JsonProperty("conditionReason"), JsonPropertyName("conditionReason")]
        public partial string ConditionReason { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("debugLevel"), JsonPropertyName("debugLevel")]
        public partial long DebugLevel { get; set; }

        [ObservableProperty]
        [JsonProperty("doorOpen"), JsonPropertyName("doorOpen")]
        public partial bool DoorOpen { get; set; }

        [ObservableProperty]
        [JsonProperty("extruder"), JsonPropertyName("extruder")]
        public partial List<RepetierPrinterToolhead> Extruder { get; set; } = [];

        [ObservableProperty]
        [JsonProperty("f"), JsonPropertyName("f")]
        public partial double F { get; set; }

        [ObservableProperty]
        [JsonProperty("fans"), JsonPropertyName("fans")]
        public partial List<RepetierPrinterFan> Fans { get; set; } = [];

        [ObservableProperty]
        [JsonProperty("filterFan"), JsonPropertyName("filterFan")]
        public partial bool FilterFan { get; set; }

        [ObservableProperty]
        [JsonProperty("firmware"), JsonPropertyName("firmware")]
        public partial string Firmware { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("firmwareStyle"), JsonPropertyName("firmwareStyle")]
        public partial string FirmwareStyle { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("firmwareURL"), JsonPropertyName("firmwareURL")]
        public partial Uri? FirmwareUrl { get; set; }

        [ObservableProperty]
        [JsonProperty("flowMultiply"), JsonPropertyName("flowMultiply")]
        public partial long FlowMultiply { get; set; }

        [ObservableProperty]
        [JsonProperty("global"), JsonPropertyName("global")]
        public partial RepetierPrinterStateGlobal? Global { get; set; }

        [ObservableProperty]
        [JsonProperty("gperm"), JsonPropertyName("gperm")]
        public partial RepetierPrinterStateGlobal? Gperm { get; set; }

        [ObservableProperty]
        [JsonProperty("hasXHome"), JsonPropertyName("hasXHome")]
        public partial bool HasXHome { get; set; }

        [ObservableProperty]
        [JsonProperty("hasYHome"), JsonPropertyName("hasYHome")]
        public partial bool HasYHome { get; set; }

        [ObservableProperty]
        [JsonProperty("hasZHome"), JsonPropertyName("hasZHome")]
        public partial bool HasZHome { get; set; }

        [ObservableProperty]
        [JsonProperty("heatedBeds"), JsonPropertyName("heatedBeds")]
        public partial List<RepetierPrinterHeaterComponent> HeatedBeds { get; set; } = [];

        [ObservableProperty]
        [JsonProperty("heatedChambers"), JsonPropertyName("heatedChambers")]
        public partial List<RepetierPrinterHeaterComponent> HeatedChambers { get; set; } = [];

        [ObservableProperty]
        [JsonProperty("layer"), JsonPropertyName("layer")]
        public partial long Layer { get; set; }

        [ObservableProperty]
        [JsonProperty("lights"), JsonPropertyName("lights")]
        public partial long Lights { get; set; }

        [ObservableProperty]
        [JsonProperty("maxLayer"), JsonPropertyName("maxLayer")]
        public partial long MaxLayer { get; set; }

        [ObservableProperty]
        [JsonProperty("notification"), JsonPropertyName("notification")]
        public partial string Notification { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("numExtruder"), JsonPropertyName("numExtruder")]
        public partial long NumExtruder { get; set; }

        [ObservableProperty]
        [JsonProperty("perm"), JsonPropertyName("perm")]
        public partial RepetierPrinterStateGlobal? Perm { get; set; }

        [ObservableProperty]
        [JsonProperty("powerOn"), JsonPropertyName("powerOn")]
        public partial bool PowerOn { get; set; }

        [ObservableProperty]
        [JsonProperty("rec"), JsonPropertyName("rec")]
        public partial bool Rec { get; set; }

        [ObservableProperty]
        [JsonProperty("sdcardMounted"), JsonPropertyName("sdcardMounted")]
        public partial bool SdcardMounted { get; set; }

        [ObservableProperty]
        [JsonProperty("sglobal"), JsonPropertyName("sglobal")]
        public partial RepetierPrinterStateGlobal? Sglobal { get; set; }

        [ObservableProperty]
        [JsonProperty("shutdownAfterPrint"), JsonPropertyName("shutdownAfterPrint")]
        public partial bool ShutdownAfterPrint { get; set; }

        [ObservableProperty]
        [JsonProperty("speedMultiply"), JsonPropertyName("speedMultiply")]
        public partial long SpeedMultiply { get; set; }

        [ObservableProperty]
        [JsonProperty("volumetric"), JsonPropertyName("volumetric")]
        public partial bool Volumetric { get; set; }

        [ObservableProperty]
        [JsonProperty("webcams"), JsonPropertyName("webcams")]
        public partial List<RepetierPrinterConfigWebcam> Webcams { get; set; } = [];

        [ObservableProperty]
        [JsonProperty("x"), JsonPropertyName("x")]
        public partial double X { get; set; }

        [ObservableProperty]
        [JsonProperty("xOff"), JsonPropertyName("xOff")]
        public partial double XOff { get; set; }

        [ObservableProperty]
        [JsonProperty("y"), JsonPropertyName("y")]
        public partial double Y { get; set; }

        [ObservableProperty]
        [JsonProperty("yOff"), JsonPropertyName("yOff")]
        public partial double YOff { get; set; }

        [ObservableProperty]
        [JsonProperty("z"), JsonPropertyName("z")]
        public partial double Z { get; set; }

        [ObservableProperty]
        [JsonProperty("zOff"), JsonPropertyName("zOff")]
        public partial double ZOff { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

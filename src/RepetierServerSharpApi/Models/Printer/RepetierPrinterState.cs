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
        public partial long ActiveExtruder { get; set; }

        [ObservableProperty]
        
        [JsonProperty("autostartNextPrint")]
        public partial bool AutostartNextPrint { get; set; }

        [ObservableProperty]
        
        [JsonProperty("condition")]
        public partial long Condition { get; set; }

        [ObservableProperty]
        
        [JsonProperty("conditionReason")]
        public partial string ConditionReason { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("debugLevel")]
        public partial long DebugLevel { get; set; }

        [ObservableProperty]
        
        [JsonProperty("doorOpen")]
        public partial bool DoorOpen { get; set; }

        [ObservableProperty]
        
        [JsonProperty("extruder")]
        public partial List<RepetierPrinterToolhead> Extruder { get; set; } = [];

        [ObservableProperty]
        
        [JsonProperty("f")]
        public partial long F { get; set; }

        [ObservableProperty]
        
        [JsonProperty("fans")]
        public partial List<RepetierPrinterFan> Fans { get; set; } = [];

        [ObservableProperty]
        
        [JsonProperty("filterFan")]
        public partial bool FilterFan { get; set; }

        [ObservableProperty]
        
        [JsonProperty("firmware")]
        public partial string Firmware { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("firmwareStyle")]
        public partial string FirmwareStyle { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("firmwareURL")]
        public partial Uri? FirmwareUrl { get; set; }

        [ObservableProperty]
        
        [JsonProperty("flowMultiply")]
        public partial long FlowMultiply { get; set; }

        [ObservableProperty]
        
        [JsonProperty("global")]
        public partial RepetierPrinterStateGlobal? Global { get; set; }

        [ObservableProperty]
        
        [JsonProperty("gperm")]
        public partial RepetierPrinterStateGlobal? Gperm { get; set; }

        [ObservableProperty]
        
        [JsonProperty("hasXHome")]
        public partial bool HasXHome { get; set; }

        [ObservableProperty]
        
        [JsonProperty("hasYHome")]
        public partial bool HasYHome { get; set; }

        [ObservableProperty]
        
        [JsonProperty("hasZHome")]
        public partial bool HasZHome { get; set; }

        [ObservableProperty]
        
        [JsonProperty("heatedBeds")]
        public partial List<RepetierPrinterHeaterComponent> HeatedBeds { get; set; } = [];

        [ObservableProperty]
        
        [JsonProperty("heatedChambers")]
        public partial List<RepetierPrinterHeaterComponent> HeatedChambers { get; set; } = [];

        [ObservableProperty]
        
        [JsonProperty("layer")]
        public partial long Layer { get; set; }

        [ObservableProperty]
        
        [JsonProperty("lights")]
        public partial long Lights { get; set; }

        [ObservableProperty]
        
        [JsonProperty("maxLayer")]
        public partial long MaxLayer { get; set; }

        [ObservableProperty]
        
        [JsonProperty("notification")]
        public partial string Notification { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("numExtruder")]
        public partial long NumExtruder { get; set; }

        [ObservableProperty]
        
        [JsonProperty("perm")]
        public partial RepetierPrinterStateGlobal? Perm { get; set; }

        [ObservableProperty]
        
        [JsonProperty("powerOn")]
        public partial bool PowerOn { get; set; }

        [ObservableProperty]
        
        [JsonProperty("rec")]
        public partial bool Rec { get; set; }

        [ObservableProperty]
        
        [JsonProperty("sdcardMounted")]
        public partial bool SdcardMounted { get; set; }

        [ObservableProperty]
        
        [JsonProperty("sglobal")]
        public partial RepetierPrinterStateGlobal? Sglobal { get; set; }

        [ObservableProperty]
        
        [JsonProperty("shutdownAfterPrint")]
        public partial bool ShutdownAfterPrint { get; set; }

        [ObservableProperty]
        
        [JsonProperty("speedMultiply")]
        public partial long SpeedMultiply { get; set; }

        [ObservableProperty]
        
        [JsonProperty("volumetric")]
        public partial bool Volumetric { get; set; }

        [ObservableProperty]
        
        [JsonProperty("webcams")]
        public partial List<RepetierPrinterConfigWebcam> Webcams { get; set; } = [];

        [ObservableProperty]
        
        [JsonProperty("x")]
        public partial long X { get; set; }

        [ObservableProperty]
        
        [JsonProperty("xOff")]
        public partial long XOff { get; set; }

        [ObservableProperty]
        
        [JsonProperty("y")]
        public partial long Y { get; set; }

        [ObservableProperty]
        
        [JsonProperty("yOff")]
        public partial long YOff { get; set; }

        [ObservableProperty]
        
        [JsonProperty("z")]
        public partial long Z { get; set; }

        [ObservableProperty]
        
        [JsonProperty("zOff")]
        public partial long ZOff { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

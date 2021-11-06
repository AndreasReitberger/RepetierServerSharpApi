using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AndreasReitberger.Models
{
    public partial class RepetierPrinterState
    {
        #region Properties
        [JsonProperty("activeExtruder")]
        public long ActiveExtruder { get; set; }

        [JsonProperty("debugLevel")]
        public long DebugLevel { get; set; }

        [JsonProperty("extruder")]
        public List<RepetierPrinterExtruder> Extruder { get; set; } = new();

        [JsonProperty("fans")]
        public List<RepetierPrinterFan> Fans { get; set; } = new();

        [JsonProperty("firmware")]
        public string Firmware { get; set; }

        [JsonProperty("firmwareURL")]
        public Uri FirmwareUrl { get; set; }

        [JsonProperty("flowMultiply")]
        public long FlowMultiply { get; set; }

        [JsonProperty("hasXHome")]
        public bool HasXHome { get; set; }

        [JsonProperty("hasYHome")]
        public bool HasYHome { get; set; }

        [JsonProperty("hasZHome")]
        public bool HasZHome { get; set; }

        [JsonProperty("heatedBeds")]
        public List<RepetierPrinterHeatbed> HeatedBeds { get; set; } = new();

        [JsonProperty("heatedChambers")]
        public List<RepetierPrinterHeatchamber> HeatedChambers { get; set; } = new();

        [JsonProperty("layer")]
        public long Layer { get; set; }

        [JsonProperty("lights")]
        public long Lights { get; set; }

        [JsonProperty("numExtruder")]
        public long NumExtruder { get; set; }

        [JsonProperty("powerOn")]
        public bool PowerOn { get; set; }

        [JsonProperty("rec")]
        public bool Rec { get; set; }

        [JsonProperty("sdcardMounted")]
        public bool SdcardMounted { get; set; }

        [JsonProperty("speedMultiply")]
        public long SpeedMultiply { get; set; }

        [JsonProperty("volumetric")]
        public bool Volumetric { get; set; }

        [JsonProperty("x")]
        public long X { get; set; }

        [JsonProperty("y")]
        public long Y { get; set; }

        [JsonProperty("z")]
        public long Z { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

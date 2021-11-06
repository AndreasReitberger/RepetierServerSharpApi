using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.Models
{
    public partial class RepetierPrinterConfig
    {
        #region Properties
        [JsonProperty("buttonCommands", NullValueHandling = NullValueHandling.Ignore)]
        public List<RepetierPrinterConfigButtonCommand> ButtonCommands { get; set; } = new();

        [JsonProperty("connection")]
        public RepetierPrinterConnection Connection { get; set; }

        [JsonProperty("extruders")]
        public List<RepetierPrinterConfigExtruder> Extruders { get; set; } = new();

        [JsonProperty("gcodeReplacements")]
        public List<RepetierPrinterConfigGcodeReplacement> GcodeReplacements { get; set; } = new();

        [JsonProperty("general")]
        public RepetierPrinterConfigGeneral General { get; set; }

        [JsonProperty("heatedBeds")]
        public List<RepetierPrinterConfigHeatedComponent> HeatedBeds { get; set; } = new();

        [JsonProperty("heatedChambers")]
        public List<RepetierPrinterConfigHeatedComponent> HeatedChambers { get; set; } = new();

        [JsonProperty("movement")]
        public RepetierPrinterConfigMovement Movement { get; set; }

        [JsonProperty("properties")]
        public RepetierPrinterConfigProperties Properties { get; set; }

        [JsonProperty("quickCommands")]
        public List<RepetierQuickGcodeCommand> QuickCommands { get; set; } = new();

        [JsonProperty("recover")]
        public RepetierPrinterConfigRecover Recover { get; set; }

        [JsonProperty("responseEvents")]
        public List<object> ResponseEvents { get; set; } = new();

        [JsonProperty("shape")]
        public RepetierPrinterConfigShape Shape { get; set; }

        [JsonProperty("webcams")]
        public List<RepetierPrinterConfigWebcam> Webcams { get; set; } = new();
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

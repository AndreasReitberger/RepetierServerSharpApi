using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigMovement : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("G10Distance"), JsonPropertyName("G10Distance")]
        public partial long G10Distance { get; set; }

        [ObservableProperty]
        [JsonProperty("G10LongDistance"), JsonPropertyName("G10LongDistance")]
        public partial long G10LongDistance { get; set; }

        [ObservableProperty]
        [JsonProperty("G10Speed"), JsonPropertyName("G10Speed")]
        public partial long G10Speed { get; set; }

        [ObservableProperty]
        [JsonProperty("G10ZLift"), JsonPropertyName("G10ZLift")]
        public partial long G10ZLift { get; set; }

        [ObservableProperty]
        [JsonProperty("G11ExtraDistance"), JsonPropertyName("G11ExtraDistance")]
        public partial long G11ExtraDistance { get; set; }

        [ObservableProperty]
        [JsonProperty("G11ExtraLongDistance"), JsonPropertyName("G11ExtraLongDistance")]
        public partial long G11ExtraLongDistance { get; set; }

        [ObservableProperty]
        [JsonProperty("G11Speed"), JsonPropertyName("G11Speed")]
        public partial long G11Speed { get; set; }

        [ObservableProperty]
        [JsonProperty("allEndstops"), JsonPropertyName("allEndstops")]
        public partial bool AllEndstops { get; set; }

        [ObservableProperty]
        [JsonProperty("autolevel"), JsonPropertyName("autolevel")]
        public partial bool Autolevel { get; set; }

        [ObservableProperty]
        [JsonProperty("defaultAcceleration"), JsonPropertyName("defaultAcceleration")]
        public partial long DefaultAcceleration { get; set; }

        [ObservableProperty]
        [JsonProperty("defaultRetractAcceleration"), JsonPropertyName("defaultRetractAcceleration")]
        public partial long DefaultRetractAcceleration { get; set; }

        [ObservableProperty]
        [JsonProperty("defaultTravelAcceleration"), JsonPropertyName("defaultTravelAcceleration")]
        public partial long DefaultTravelAcceleration { get; set; }

        [ObservableProperty]
        [JsonProperty("invertX"), JsonPropertyName("invertX")]
        public partial bool InvertX { get; set; }

        [ObservableProperty]
        [JsonProperty("invertY"), JsonPropertyName("invertY")]
        public partial bool InvertY { get; set; }

        [ObservableProperty]
        [JsonProperty("invertZ"), JsonPropertyName("invertZ")]
        public partial bool InvertZ { get; set; }

        [ObservableProperty]
        [JsonProperty("maxXYSpeed"), JsonPropertyName("maxXYSpeed")]
        public partial long MaxXySpeed { get; set; }

        [ObservableProperty]
        [JsonProperty("maxZSpeed"), JsonPropertyName("maxZSpeed")]
        public partial long MaxZSpeed { get; set; }

        [ObservableProperty]
        [JsonProperty("movebuffer"), JsonPropertyName("movebuffer")]
        public partial long Movebuffer { get; set; }

        [ObservableProperty]
        [JsonProperty("startWithAbsolutePositions"), JsonPropertyName("startWithAbsolutePositions")]
        public partial bool StartWithAbsolutePositions { get; set; }

        [ObservableProperty]
        [JsonProperty("timeMultiplier"), JsonPropertyName("timeMultiplier")]
        public partial double TimeMultiplier { get; set; }

        [ObservableProperty]
        [JsonProperty("xEndstop"), JsonPropertyName("xEndstop")]
        public partial bool XEndstop { get; set; }

        [ObservableProperty]
        [JsonProperty("xHome"), JsonPropertyName("xHome")]
        public partial long XHome { get; set; }

        [ObservableProperty]
        [JsonProperty("xMax"), JsonPropertyName("xMax")]
        public partial long XMax { get; set; }

        [ObservableProperty]
        [JsonProperty("xMin"), JsonPropertyName("xMin")]
        public partial long XMin { get; set; }

        [ObservableProperty]
        [JsonProperty("xyJerk"), JsonPropertyName("xyJerk")]
        public partial long XyJerk { get; set; }

        [ObservableProperty]
        [JsonProperty("xyPrintAcceleration"), JsonPropertyName("xyPrintAcceleration")]
        public partial long XyPrintAcceleration { get; set; }

        [ObservableProperty]
        [JsonProperty("xySpeed"), JsonPropertyName("xySpeed")]
        public partial long XySpeed { get; set; }

        [ObservableProperty]
        [JsonProperty("xyTravelAcceleration"), JsonPropertyName("xyTravelAcceleration")]
        public partial long XyTravelAcceleration { get; set; }

        [ObservableProperty]
        [JsonProperty("yEndstop"), JsonPropertyName("yEndstop")]
        public partial bool YEndstop { get; set; }

        [ObservableProperty]
        [JsonProperty("yHome"), JsonPropertyName("yHome")]
        public partial long YHome { get; set; }

        [ObservableProperty]
        [JsonProperty("yMax"), JsonPropertyName("yMax")]
        public partial long YMax { get; set; }

        [ObservableProperty]
        [JsonProperty("yMin"), JsonPropertyName("yMin")]
        public partial long YMin { get; set; }

        [ObservableProperty]
        [JsonProperty("zEndstop"), JsonPropertyName("zEndstop")]
        public partial bool ZEndstop { get; set; }

        [ObservableProperty]
        [JsonProperty("zHome"), JsonPropertyName("zHome")]
        public partial long ZHome { get; set; }

        [ObservableProperty]
        [JsonProperty("zJerk"), JsonPropertyName("zJerk")]
        public partial double ZJerk { get; set; }

        [ObservableProperty]
        [JsonProperty("zMax"), JsonPropertyName("zMax")]
        public partial long ZMax { get; set; }

        [ObservableProperty]
        [JsonProperty("zMin"), JsonPropertyName("zMin")]
        public partial long ZMin { get; set; }

        [ObservableProperty]
        [JsonProperty("zPrintAcceleration"), JsonPropertyName("zPrintAcceleration")]
        public partial long ZPrintAcceleration { get; set; }

        [ObservableProperty]
        [JsonProperty("zSpeed"), JsonPropertyName("zSpeed")]
        public partial long ZSpeed { get; set; }

        [ObservableProperty]
        [JsonProperty("zTravelAcceleration"), JsonPropertyName("zTravelAcceleration")]
        public partial long ZTravelAcceleration { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        #endregion
    }
}

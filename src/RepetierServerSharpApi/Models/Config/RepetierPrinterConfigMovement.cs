using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigMovement : ObservableObject
    {
        #region Properties
        [ObservableProperty]

        [JsonProperty("G10Distance")]
        public partial long G10Distance { get; set; }

        [ObservableProperty]

        [JsonProperty("G10LongDistance")]
        public partial long G10LongDistance { get; set; }

        [ObservableProperty]

        [JsonProperty("G10Speed")]
        public partial long G10Speed { get; set; }

        [ObservableProperty]

        [JsonProperty("G10ZLift")]
        public partial long G10ZLift { get; set; }

        [ObservableProperty]

        [JsonProperty("G11ExtraDistance")]
        public partial long G11ExtraDistance { get; set; }

        [ObservableProperty]

        [JsonProperty("G11ExtraLongDistance")]
        public partial long G11ExtraLongDistance { get; set; }

        [ObservableProperty]

        [JsonProperty("G11Speed")]
        public partial long G11Speed { get; set; }

        [ObservableProperty]

        [JsonProperty("allEndstops")]
        public partial bool AllEndstops { get; set; }

        [ObservableProperty]

        [JsonProperty("autolevel")]
        public partial bool Autolevel { get; set; }

        [ObservableProperty]

        [JsonProperty("defaultAcceleration")]
        public partial long DefaultAcceleration { get; set; }

        [ObservableProperty]

        [JsonProperty("defaultRetractAcceleration")]
        public partial long DefaultRetractAcceleration { get; set; }

        [ObservableProperty]

        [JsonProperty("defaultTravelAcceleration")]
        public partial long DefaultTravelAcceleration { get; set; }

        [ObservableProperty]

        [JsonProperty("invertX")]
        public partial bool InvertX { get; set; }

        [ObservableProperty]

        [JsonProperty("invertY")]
        public partial bool InvertY { get; set; }

        [ObservableProperty]

        [JsonProperty("invertZ")]
        public partial bool InvertZ { get; set; }

        [ObservableProperty]

        [JsonProperty("maxXYSpeed")]
        public partial long MaxXySpeed { get; set; }

        [ObservableProperty]

        [JsonProperty("maxZSpeed")]
        public partial long MaxZSpeed { get; set; }

        [ObservableProperty]

        [JsonProperty("movebuffer")]
        public partial long Movebuffer { get; set; }

        [ObservableProperty]

        [JsonProperty("startWithAbsolutePositions")]
        public partial bool StartWithAbsolutePositions { get; set; }

        [ObservableProperty]

        [JsonProperty("timeMultiplier")]
        public partial double TimeMultiplier { get; set; }

        [ObservableProperty]

        [JsonProperty("xEndstop")]
        public partial bool XEndstop { get; set; }

        [ObservableProperty]

        [JsonProperty("xHome")]
        public partial long XHome { get; set; }

        [ObservableProperty]

        [JsonProperty("xMax")]
        public partial long XMax { get; set; }

        [ObservableProperty]

        [JsonProperty("xMin")]
        public partial long XMin { get; set; }

        [ObservableProperty]

        [JsonProperty("xyJerk")]
        public partial long XyJerk { get; set; }

        [ObservableProperty]

        [JsonProperty("xyPrintAcceleration")]
        public partial long XyPrintAcceleration { get; set; }

        [ObservableProperty]

        [JsonProperty("xySpeed")]
        public partial long XySpeed { get; set; }

        [ObservableProperty]

        [JsonProperty("xyTravelAcceleration")]
        public partial long XyTravelAcceleration { get; set; }

        [ObservableProperty]

        [JsonProperty("yEndstop")]
        public partial bool YEndstop { get; set; }

        [ObservableProperty]

        [JsonProperty("yHome")]
        public partial long YHome { get; set; }

        [ObservableProperty]

        [JsonProperty("yMax")]
        public partial long YMax { get; set; }

        [ObservableProperty]

        [JsonProperty("yMin")]
        public partial long YMin { get; set; }

        [ObservableProperty]

        [JsonProperty("zEndstop")]
        public partial bool ZEndstop { get; set; }

        [ObservableProperty]

        [JsonProperty("zHome")]
        public partial long ZHome { get; set; }

        [ObservableProperty]

        [JsonProperty("zJerk")]
        public partial double ZJerk { get; set; }

        [ObservableProperty]

        [JsonProperty("zMax")]
        public partial long ZMax { get; set; }

        [ObservableProperty]

        [JsonProperty("zMin")]
        public partial long ZMin { get; set; }

        [ObservableProperty]

        [JsonProperty("zPrintAcceleration")]
        public partial long ZPrintAcceleration { get; set; }

        [ObservableProperty]

        [JsonProperty("zSpeed")]
        public partial long ZSpeed { get; set; }

        [ObservableProperty]

        [JsonProperty("zTravelAcceleration")]
        public partial long ZTravelAcceleration { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        #endregion
    }
}

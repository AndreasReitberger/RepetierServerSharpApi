using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigMovement
    {
        #region Properties
        [JsonProperty("G10Distance")]
        public long G10Distance { get; set; }

        [JsonProperty("G10LongDistance")]
        public long G10LongDistance { get; set; }

        [JsonProperty("G10Speed")]
        public long G10Speed { get; set; }

        [JsonProperty("G10ZLift")]
        public long G10ZLift { get; set; }

        [JsonProperty("G11ExtraDistance")]
        public long G11ExtraDistance { get; set; }

        [JsonProperty("G11ExtraLongDistance")]
        public long G11ExtraLongDistance { get; set; }

        [JsonProperty("G11Speed")]
        public long G11Speed { get; set; }

        [JsonProperty("allEndstops")]
        public bool AllEndstops { get; set; }

        [JsonProperty("defaultAcceleration")]
        public long DefaultAcceleration { get; set; }

        [JsonProperty("defaultRetractAcceleration")]
        public long DefaultRetractAcceleration { get; set; }

        [JsonProperty("defaultTravelAcceleration")]
        public long DefaultTravelAcceleration { get; set; }

        [JsonProperty("invertX")]
        public bool InvertX { get; set; }

        [JsonProperty("invertY")]
        public bool InvertY { get; set; }

        [JsonProperty("invertZ")]
        public bool InvertZ { get; set; }

        [JsonProperty("maxXYSpeed")]
        public long MaxXySpeed { get; set; }

        [JsonProperty("maxZSpeed")]
        public long MaxZSpeed { get; set; }

        [JsonProperty("movebuffer")]
        public long Movebuffer { get; set; }

        [JsonProperty("timeMultiplier")]
        public double TimeMultiplier { get; set; }

        [JsonProperty("xEndstop")]
        public bool XEndstop { get; set; }

        [JsonProperty("xHome")]
        public long XHome { get; set; }

        [JsonProperty("xMax")]
        public long XMax { get; set; }

        [JsonProperty("xMin")]
        public long XMin { get; set; }

        [JsonProperty("xyJerk")]
        public long XyJerk { get; set; }

        [JsonProperty("xyPrintAcceleration")]
        public long XyPrintAcceleration { get; set; }

        [JsonProperty("xySpeed")]
        public long XySpeed { get; set; }

        [JsonProperty("xyTravelAcceleration")]
        public long XyTravelAcceleration { get; set; }

        [JsonProperty("yEndstop")]
        public bool YEndstop { get; set; }

        [JsonProperty("yHome")]
        public long YHome { get; set; }

        [JsonProperty("yMax")]
        public long YMax { get; set; }

        [JsonProperty("yMin")]
        public long YMin { get; set; }

        [JsonProperty("zEndstop")]
        public bool ZEndstop { get; set; }

        [JsonProperty("zHome")]
        public long ZHome { get; set; }

        [JsonProperty("zJerk")]
        public double ZJerk { get; set; }

        [JsonProperty("zMax")]
        public long ZMax { get; set; }

        [JsonProperty("zMin")]
        public long ZMin { get; set; }

        [JsonProperty("zPrintAcceleration")]
        public long ZPrintAcceleration { get; set; }

        [JsonProperty("zSpeed")]
        public long ZSpeed { get; set; }

        [JsonProperty("zTravelAcceleration")]
        public long ZTravelAcceleration { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

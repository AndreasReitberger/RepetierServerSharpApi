namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigMovement : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("G10Distance")]
        public partial double G10Distance { get; set; }

        [ObservableProperty]
        [JsonPropertyName("G10LongDistance")]
        public partial double G10LongDistance { get; set; }

        [ObservableProperty]
        [JsonPropertyName("G10Speed")]
        public partial double G10Speed { get; set; }

        [ObservableProperty]
        [JsonPropertyName("G10ZLift")]
        public partial double G10ZLift { get; set; }

        [ObservableProperty]
        [JsonPropertyName("G11ExtraDistance")]
        public partial double G11ExtraDistance { get; set; }

        [ObservableProperty]
        [JsonPropertyName("G11ExtraLongDistance")]
        public partial double G11ExtraLongDistance { get; set; }

        [ObservableProperty]
        [JsonPropertyName("G11Speed")]
        public partial double G11Speed { get; set; }

        [ObservableProperty]
        [JsonPropertyName("allEndstops")]
        public partial bool AllEndstops { get; set; }

        [ObservableProperty]
        [JsonPropertyName("autolevel")]
        public partial bool Autolevel { get; set; }

        [ObservableProperty]
        [JsonPropertyName("defaultAcceleration")]
        public partial double DefaultAcceleration { get; set; }

        [ObservableProperty]
        [JsonPropertyName("defaultRetractAcceleration")]
        public partial double DefaultRetractAcceleration { get; set; }

        [ObservableProperty]
        [JsonPropertyName("defaultTravelAcceleration")]
        public partial double DefaultTravelAcceleration { get; set; }

        [ObservableProperty]
        [JsonPropertyName("invertX")]
        public partial bool InvertX { get; set; }

        [ObservableProperty]
        [JsonPropertyName("invertY")]
        public partial bool InvertY { get; set; }

        [ObservableProperty]
        [JsonPropertyName("invertZ")]
        public partial bool InvertZ { get; set; }

        [ObservableProperty]
        [JsonPropertyName("maxXYSpeed")]
        public partial double MaxXySpeed { get; set; }

        [ObservableProperty]
        [JsonPropertyName("maxZSpeed")]
        public partial double MaxZSpeed { get; set; }

        [ObservableProperty]
        [JsonPropertyName("movebuffer")]
        public partial long Movebuffer { get; set; }

        [ObservableProperty]
        [JsonPropertyName("startWithAbsolutePositions")]
        public partial bool StartWithAbsolutePositions { get; set; }

        [ObservableProperty]
        [JsonPropertyName("timeMultiplier")]
        public partial double TimeMultiplier { get; set; }

        [ObservableProperty]
        [JsonPropertyName("xEndstop")]
        public partial bool XEndstop { get; set; }

        [ObservableProperty]
        [JsonPropertyName("xHome")]
        public partial double XHome { get; set; }

        [ObservableProperty]
        [JsonPropertyName("xMax")]
        public partial double XMax { get; set; }

        [ObservableProperty]
        [JsonPropertyName("xMin")]
        public partial double XMin { get; set; }

        [ObservableProperty]
        [JsonPropertyName("xyJerk")]
        public partial double XyJerk { get; set; }

        [ObservableProperty]
        [JsonPropertyName("xyPrintAcceleration")]
        public partial double XyPrintAcceleration { get; set; }

        [ObservableProperty]
        [JsonPropertyName("xySpeed")]
        public partial double XySpeed { get; set; }

        [ObservableProperty]
        [JsonPropertyName("xyTravelAcceleration")]
        public partial double XyTravelAcceleration { get; set; }

        [ObservableProperty]
        [JsonPropertyName("yEndstop")]
        public partial bool YEndstop { get; set; }

        [ObservableProperty]
        [JsonPropertyName("yHome")]
        public partial double YHome { get; set; }

        [ObservableProperty]
        [JsonPropertyName("yMax")]
        public partial double YMax { get; set; }

        [ObservableProperty]
        [JsonPropertyName("yMin")]
        public partial double YMin { get; set; }

        [ObservableProperty]
        [JsonPropertyName("zEndstop")]
        public partial bool ZEndstop { get; set; }

        [ObservableProperty]
        [JsonPropertyName("zHome")]
        public partial double ZHome { get; set; }

        [ObservableProperty]
        [JsonPropertyName("zJerk")]
        public partial double ZJerk { get; set; }

        [ObservableProperty]
        [JsonPropertyName("zMax")]
        public partial double ZMax { get; set; }

        [ObservableProperty]
        [JsonPropertyName("zMin")]
        public partial double ZMin { get; set; }

        [ObservableProperty]
        [JsonPropertyName("zPrintAcceleration")]
        public partial double ZPrintAcceleration { get; set; }

        [ObservableProperty]
        [JsonPropertyName("zSpeed")]
        public partial double ZSpeed { get; set; }

        [ObservableProperty]
        [JsonPropertyName("zTravelAcceleration")]
        public partial double ZTravelAcceleration { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierPrinterConfigMovement);

        #endregion
    }
}

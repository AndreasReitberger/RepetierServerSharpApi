using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigMovement : ObservableObject
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("G10Distance")]
        long g10Distance;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("G10LongDistance")]
        long g10LongDistance;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("G10Speed")]
        long g10Speed;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("G10ZLift")]
        long g10ZLift;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("G11ExtraDistance")]
        long g11ExtraDistance;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("G11ExtraLongDistance")]
        long g11ExtraLongDistance;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("G11Speed")]
        long g11Speed;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("allEndstops")]
        bool allEndstops;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("autolevel")]
        bool autolevel;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("defaultAcceleration")]
        long defaultAcceleration;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("defaultRetractAcceleration")]
        long defaultRetractAcceleration;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("defaultTravelAcceleration")]
        long defaultTravelAcceleration;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("invertX")]
        bool invertX;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("invertY")]
        bool invertY;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("invertZ")]
        bool invertZ;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("maxXYSpeed")]
        long maxXySpeed;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("maxZSpeed")]
        long maxZSpeed;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("movebuffer")]
        long movebuffer;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("startWithAbsolutePositions")]
        bool startWithAbsolutePositions;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("timeMultiplier")]
        double timeMultiplier;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("xEndstop")]
        bool xEndstop;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("xHome")]
        long xHome;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("xMax")]
        long xMax;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("xMin")]
        long xMin;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("xyJerk")]
        long xyJerk;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("xyPrintAcceleration")]
        long xyPrintAcceleration;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("xySpeed")]
        long xySpeed;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("xyTravelAcceleration")]
        long xyTravelAcceleration;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("yEndstop")]
        bool yEndstop;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("yHome")]
        long yHome;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("yMax")]
        long yMax;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("yMin")]
        long yMin;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("zEndstop")]
        bool zEndstop;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("zHome")]
        long zHome;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("zJerk")]
        double zJerk;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("zMax")]
        long zMax;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("zMin")]
        long zMin;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("zPrintAcceleration")]
        long zPrintAcceleration;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("zSpeed")]
        long zSpeed;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("zTravelAcceleration")]
        long zTravelAcceleration;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        #endregion
    }
}

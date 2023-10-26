using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigMovement : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("G10Distance")]
        [property: JsonIgnore]
        long g10Distance;

        [ObservableProperty]
        [JsonProperty("G10LongDistance")]
        [property: JsonIgnore]
        long g10LongDistance;

        [ObservableProperty]
        [JsonProperty("G10Speed")]
        [property: JsonIgnore]
        long g10Speed;

        [ObservableProperty]
        [JsonProperty("G10ZLift")]
        [property: JsonIgnore]
        long g10ZLift;

        [ObservableProperty]
        [JsonProperty("G11ExtraDistance")]
        [property: JsonIgnore]
        long g11ExtraDistance;

        [ObservableProperty]
        [JsonProperty("G11ExtraLongDistance")]
        [property: JsonIgnore]
        long g11ExtraLongDistance;

        [ObservableProperty]
        [JsonProperty("G11Speed")]
        [property: JsonIgnore]
        long g11Speed;

        [ObservableProperty]
        [JsonProperty("allEndstops")]
        bool allEndstops;

        [ObservableProperty]
        [JsonProperty("autolevel")]
        [property: JsonIgnore]
        bool autolevel;

        [ObservableProperty]
        [JsonProperty("defaultAcceleration")]
        [property: JsonIgnore]
        long defaultAcceleration;

        [ObservableProperty]
        [JsonProperty("defaultRetractAcceleration")]
        [property: JsonIgnore]
        long defaultRetractAcceleration;

        [ObservableProperty]
        [JsonProperty("defaultTravelAcceleration")]
        [property: JsonIgnore]
        long defaultTravelAcceleration;

        [ObservableProperty]
        [JsonProperty("invertX")]
        [property: JsonIgnore]
        bool invertX;

        [ObservableProperty]
        [JsonProperty("invertY")]
        [property: JsonIgnore]
        bool invertY;

        [ObservableProperty]
        [JsonProperty("invertZ")]
        [property: JsonIgnore]
        bool invertZ;

        [ObservableProperty]
        [JsonProperty("maxXYSpeed")]
        [property: JsonIgnore]
        long maxXySpeed;

        [ObservableProperty]
        [JsonProperty("maxZSpeed")]
        [property: JsonIgnore]
        long maxZSpeed;

        [ObservableProperty]
        [JsonProperty("movebuffer")]
        [property: JsonIgnore]
        long movebuffer;

        [ObservableProperty]
        [JsonProperty("startWithAbsolutePositions")]
        [property: JsonIgnore]
        bool startWithAbsolutePositions;

        [ObservableProperty]
        [JsonProperty("timeMultiplier")]
        [property: JsonIgnore]
        double timeMultiplier;

        [ObservableProperty]
        [JsonProperty("xEndstop")]
        [property: JsonIgnore]
        bool xEndstop;

        [ObservableProperty]
        [JsonProperty("xHome")]
        [property: JsonIgnore]
        long xHome;

        [ObservableProperty]
        [JsonProperty("xMax")]
        [property: JsonIgnore]
        long xMax;

        [ObservableProperty]
        [JsonProperty("xMin")]
        [property: JsonIgnore]
        long xMin;

        [ObservableProperty]
        [JsonProperty("xyJerk")]
        [property: JsonIgnore]
        long xyJerk;

        [ObservableProperty]
        [JsonProperty("xyPrintAcceleration")]
        [property: JsonIgnore]
        long xyPrintAcceleration;

        [ObservableProperty]
        [JsonProperty("xySpeed")]
        [property: JsonIgnore]
        long xySpeed;

        [ObservableProperty]
        [JsonProperty("xyTravelAcceleration")]
        [property: JsonIgnore]
        long xyTravelAcceleration;

        [ObservableProperty]
        [JsonProperty("yEndstop")]
        [property: JsonIgnore]
        bool yEndstop;

        [ObservableProperty]
        [JsonProperty("yHome")]
        [property: JsonIgnore]
        long yHome;

        [ObservableProperty]
        [JsonProperty("yMax")]
        [property: JsonIgnore]
        long yMax;

        [ObservableProperty]
        [JsonProperty("yMin")]
        [property: JsonIgnore]
        long yMin;

        [ObservableProperty]
        [JsonProperty("zEndstop")]
        [property: JsonIgnore]
        bool zEndstop;

        [ObservableProperty]
        [JsonProperty("zHome")]
        [property: JsonIgnore]
        long zHome;

        [ObservableProperty]
        [JsonProperty("zJerk")]
        [property: JsonIgnore]
        double zJerk;

        [ObservableProperty]
        [JsonProperty("zMax")]
        [property: JsonIgnore]
        long zMax;

        [ObservableProperty]
        [JsonProperty("zMin")]
        [property: JsonIgnore]
        long zMin;

        [ObservableProperty]
        [JsonProperty("zPrintAcceleration")]
        [property: JsonIgnore]
        long zPrintAcceleration;

        [ObservableProperty]
        [JsonProperty("zSpeed")]
        [property: JsonIgnore]
        long zSpeed;

        [ObservableProperty]
        [JsonProperty("zTravelAcceleration")]
        [property: JsonIgnore]
        long zTravelAcceleration;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        
        #endregion
    }
}

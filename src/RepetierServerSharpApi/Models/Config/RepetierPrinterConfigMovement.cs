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
        bool autolevel;

        [ObservableProperty]
        [JsonProperty("defaultAcceleration")]
        long defaultAcceleration;

        [ObservableProperty]
        [JsonProperty("defaultRetractAcceleration")]
        long defaultRetractAcceleration;

        [ObservableProperty]
        [JsonProperty("defaultTravelAcceleration")]
        long defaultTravelAcceleration;

        [ObservableProperty]
        [JsonProperty("invertX")]
        bool invertX;

        [ObservableProperty]
        [JsonProperty("invertY")]
        bool invertY;

        [ObservableProperty]
        [JsonProperty("invertZ")]
        bool invertZ;

        [ObservableProperty]
        [JsonProperty("maxXYSpeed")]
        long maxXySpeed;

        [ObservableProperty]
        [JsonProperty("maxZSpeed")]
        long maxZSpeed;

        [ObservableProperty]
        [JsonProperty("movebuffer")]
        long movebuffer;

        [ObservableProperty]
        [JsonProperty("startWithAbsolutePositions")]
        bool startWithAbsolutePositions;

        [ObservableProperty]
        [JsonProperty("timeMultiplier")]
        double timeMultiplier;

        [ObservableProperty]
        [JsonProperty("xEndstop")]
        bool xEndstop;

        [ObservableProperty]
        [JsonProperty("xHome")]
        long xHome;

        [ObservableProperty]
        [JsonProperty("xMax")]
        long xMax;

        [ObservableProperty]
        [JsonProperty("xMin")]
        long xMin;

        [ObservableProperty]
        [JsonProperty("xyJerk")]
        long xyJerk;

        [ObservableProperty]
        [JsonProperty("xyPrintAcceleration")]
        long xyPrintAcceleration;

        [ObservableProperty]
        [JsonProperty("xySpeed")]
        long xySpeed;

        [ObservableProperty]
        [JsonProperty("xyTravelAcceleration")]
        long xyTravelAcceleration;

        [ObservableProperty]
        [JsonProperty("yEndstop")]
        bool yEndstop;

        [ObservableProperty]
        [JsonProperty("yHome")]
        long yHome;

        [ObservableProperty]
        [JsonProperty("yMax")]
        long yMax;

        [ObservableProperty]
        [JsonProperty("yMin")]
        long yMin;

        [ObservableProperty]
        [JsonProperty("zEndstop")]
        bool zEndstop;

        [ObservableProperty]
        [JsonProperty("zHome")]
        long zHome;

        [ObservableProperty]
        [JsonProperty("zJerk")]
        double zJerk;

        [ObservableProperty]
        [JsonProperty("zMax")]
        long zMax;

        [ObservableProperty]
        [JsonProperty("zMin")]
        long zMin;

        [ObservableProperty]
        [JsonProperty("zPrintAcceleration")]
        long zPrintAcceleration;

        [ObservableProperty]
        [JsonProperty("zSpeed")]
        long zSpeed;

        [ObservableProperty]
        [JsonProperty("zTravelAcceleration")]
        long zTravelAcceleration;
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

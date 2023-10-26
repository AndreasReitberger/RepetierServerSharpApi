using AndreasReitberger.API.Print3dServer.Core.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigWebcam : ObservableObject, IWebCamConfig
    {
        #region Properties
        [ObservableProperty]
        [JsonIgnore]
        Guid id;

        [ObservableProperty]
        [JsonProperty("alias")]
        [property: JsonIgnore]
        string alias;

        [ObservableProperty]
        [JsonProperty("dynamicUrl")]
        [property: JsonIgnore]
        Uri? webCamUrlDynamic;

        [ObservableProperty]
        [JsonProperty("forceSnapshotPosition")]
        [property: JsonIgnore]
        bool forceSnapshotPosition;

        [ObservableProperty]
        [JsonProperty("method")]
        [property: JsonIgnore]
        long method;

        [ObservableProperty]
        [JsonProperty("orientation")]
        [property: JsonIgnore]
        long orientation;

        [ObservableProperty]
        [JsonProperty("pos")]
        [property: JsonIgnore]
        long position;

        [ObservableProperty]
        [JsonProperty("rec", NullValueHandling = NullValueHandling.Ignore)]
        [property: JsonIgnore]
        long rec;

        [ObservableProperty]
        [JsonProperty("reloadInterval")]
        [property: JsonIgnore]
        long reloadInterval;

        [ObservableProperty]
        [JsonProperty("snapshotDelay")]
        [property: JsonIgnore]
        long snapshotDelay;

        [ObservableProperty]
        [JsonProperty("snapshotStabilizeTime")]
        [property: JsonIgnore]
        long snapshotStabilizeTime;

        [ObservableProperty]
        [JsonProperty("snapshotX")]
        [property: JsonIgnore]
        long snapshotX;

        [ObservableProperty]
        [JsonProperty("snapshotY")]
        [property: JsonIgnore]
        long snapshotY;

        [ObservableProperty]
        [JsonProperty("staticUrl")]
        [property: JsonIgnore]
        Uri? webCamUrlStatic;

        [ObservableProperty]
        [JsonProperty("timelapseBitrate")]
        [property: JsonIgnore]
        long timelapseBitrate;

        [ObservableProperty]
        [JsonProperty("timelapseFramerate")]
        [property: JsonIgnore]
        long timelapseFramerate;

        [ObservableProperty]
        [JsonProperty("timelapseHeight")]
        [property: JsonIgnore]
        double timelapseHeight;

        [ObservableProperty]
        [JsonProperty("timelapseInterval")]
        [property: JsonIgnore]
        long timelapseInterval;

        [ObservableProperty]
        [JsonProperty("timelapseLayer")]
        [property: JsonIgnore]
        long timelapseLayer;

        [ObservableProperty]
        [JsonProperty("timelapseMethod")]
        [property: JsonIgnore]
        long timelapseMethod;

        [ObservableProperty]
        [JsonProperty("timelapseSelected")]
        [property: JsonIgnore]
        long timelapseSelected;
        #endregion

        #region Interface, unused
        [ObservableProperty]
        [JsonIgnore]
        bool enabled = true;

        [ObservableProperty]
        [JsonIgnore]
        bool flipX = false;

        [ObservableProperty]
        [JsonIgnore]
        bool flipY = false;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        
        #endregion

    }
}

using AndreasReitberger.API.Print3dServer.Core.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigWebcam : ObservableObject, IWebCamConfig
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        Guid id;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("alias")]
        string alias;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("dynamicUrl")]
        Uri? webCamUrlDynamic;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("forceSnapshotPosition")]
        bool forceSnapshotPosition;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("method")]
        long method;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("orientation")]
        long orientation;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("pos")]
        long position;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("rec", NullValueHandling = NullValueHandling.Ignore)]
        long rec;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("reloadInterval")]
        long reloadInterval;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("snapshotDelay")]
        long snapshotDelay;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("snapshotStabilizeTime")]
        long snapshotStabilizeTime;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("snapshotX")]
        long snapshotX;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("snapshotY")]
        long snapshotY;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("staticUrl")]
        Uri? webCamUrlStatic;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("timelapseBitrate")]
        long timelapseBitrate;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("timelapseFramerate")]
        long timelapseFramerate;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("timelapseHeight")]
        double timelapseHeight;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("timelapseInterval")]
        long timelapseInterval;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("timelapseLayer")]
        long timelapseLayer;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("timelapseMethod")]
        long timelapseMethod;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("timelapseSelected")]
        long timelapseSelected;
        #endregion

        #region Interface, unused
        [ObservableProperty, JsonIgnore]
        bool enabled = true;

        [ObservableProperty, JsonIgnore]
        bool flipX = false;

        [ObservableProperty, JsonIgnore]
        bool flipY = false;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        
        #endregion

    }
}

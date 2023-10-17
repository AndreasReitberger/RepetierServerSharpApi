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
        [JsonProperty("alias")]
        string alias;

        [ObservableProperty]
        [JsonProperty("dynamicUrl")]
        Uri? webCamUrlDynamic;

        [ObservableProperty]
        [JsonProperty("forceSnapshotPosition")]
        bool forceSnapshotPosition;

        [ObservableProperty]
        [JsonProperty("method")]
        long method;

        [ObservableProperty]
        [JsonProperty("orientation")]
        long orientation;

        [ObservableProperty]
        [JsonProperty("pos")]
        long position;

        [ObservableProperty]
        [JsonProperty("rec", NullValueHandling = NullValueHandling.Ignore)]
        long rec;

        [ObservableProperty]
        [JsonProperty("reloadInterval")]
        long reloadInterval;

        [ObservableProperty]
        [JsonProperty("snapshotDelay")]
        long snapshotDelay;

        [ObservableProperty]
        [JsonProperty("snapshotStabilizeTime")]
        long snapshotStabilizeTime;

        [ObservableProperty]
        [JsonProperty("snapshotX")]
        long snapshotX;

        [ObservableProperty]
        [JsonProperty("snapshotY")]
        long snapshotY;

        [ObservableProperty]
        [JsonProperty("staticUrl")]
        Uri? webCamUrlStatic;

        [ObservableProperty]
        [JsonProperty("timelapseBitrate")]
        long timelapseBitrate;

        [ObservableProperty]
        [JsonProperty("timelapseFramerate")]
        long timelapseFramerate;

        [ObservableProperty]
        [JsonProperty("timelapseHeight")]
        double timelapseHeight;

        [ObservableProperty]
        [JsonProperty("timelapseInterval")]
        long timelapseInterval;

        [ObservableProperty]
        [JsonProperty("timelapseLayer")]
        long timelapseLayer;

        [ObservableProperty]
        [JsonProperty("timelapseMethod")]
        long timelapseMethod;

        [ObservableProperty]
        [JsonProperty("timelapseSelected")]
        long timelapseSelected;
        #endregion

        #region Interface, unused

        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion

    }
}

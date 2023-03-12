using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigWebcam : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("alias")]
        string alias;

        [ObservableProperty]
        [JsonProperty("dynamicUrl")]
        Uri dynamicUrl;

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
        long pos;

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
        Uri staticUrl;

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

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion

    }
}

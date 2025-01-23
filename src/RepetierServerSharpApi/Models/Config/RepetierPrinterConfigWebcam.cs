using AndreasReitberger.API.Print3dServer.Core.Interfaces;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigWebcam : ObservableObject, IWebCamConfig
    {
        #region Properties
        [ObservableProperty]
        
        public partial Guid Id { get; set; }

        [ObservableProperty]
        
        [JsonProperty("alias")]
        public partial string Alias { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("dynamicUrl")]
        public partial Uri? WebCamUrlDynamic { get; set; }

        [ObservableProperty]
        
        [JsonProperty("forceSnapshotPosition")]
        public partial bool ForceSnapshotPosition { get; set; }

        [ObservableProperty]
        
        [JsonProperty("method")]
        public partial long Method { get; set; }

        [ObservableProperty]
        
        [JsonProperty("orientation")]
        public partial long Orientation { get; set; }

        [ObservableProperty]
        
        [JsonProperty("pos")]
        public partial long Position { get; set; }

        [ObservableProperty]
        
        [JsonProperty("rec", NullValueHandling = NullValueHandling.Ignore)]
        public partial long Rec { get; set; }

        [ObservableProperty]
        
        [JsonProperty("reloadInterval")]
        public partial long ReloadInterval { get; set; }

        [ObservableProperty]
        
        [JsonProperty("snapshotDelay")]
        public partial long SnapshotDelay { get; set; }

        [ObservableProperty]
        
        [JsonProperty("snapshotStabilizeTime")]
        public partial long SnapshotStabilizeTime { get; set; }

        [ObservableProperty]
        
        [JsonProperty("snapshotX")]
        public partial long SnapshotX { get; set; }

        [ObservableProperty]
        
        [JsonProperty("snapshotY")]
        public partial long SnapshotY { get; set; }

        [ObservableProperty]
        
        [JsonProperty("staticUrl")]
        public partial Uri? WebCamUrlStatic { get; set; }

        [ObservableProperty]
        
        [JsonProperty("timelapseBitrate")]
        public partial long TimelapseBitrate { get; set; }

        [ObservableProperty]
        
        [JsonProperty("timelapseFramerate")]
        public partial long TimelapseFramerate { get; set; }

        [ObservableProperty]
        
        [JsonProperty("timelapseHeight")]
        public partial double TimelapseHeight { get; set; }

        [ObservableProperty]
        
        [JsonProperty("timelapseInterval")]
        public partial long TimelapseInterval { get; set; }

        [ObservableProperty]
        
        [JsonProperty("timelapseLayer")]
        public partial long TimelapseLayer { get; set; }

        [ObservableProperty]
        
        [JsonProperty("timelapseMethod")]
        public partial long TimelapseMethod { get; set; }

        [ObservableProperty]
        
        [JsonProperty("timelapseSelected")]
        public partial long TimelapseSelected { get; set; }
        #endregion

        #region Interface, unused
        [ObservableProperty]
        
        public partial bool Enabled { get; set; } = true;

        [ObservableProperty]
        
        public partial bool FlipX { get; set; } = false;

        [ObservableProperty]
        
        public partial bool FlipY { get; set; } = false;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        #endregion

    }
}

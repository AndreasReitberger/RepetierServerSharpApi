using AndreasReitberger.API.Print3dServer.Core.Interfaces;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigWebcam : ObservableObject, IWebCamConfig
    {
        #region Properties
        [ObservableProperty]
        public partial Guid Id { get; set; }

        [ObservableProperty]
        [JsonPropertyName("alias")]
        public partial string Alias { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("dynamicUrl")]
        public partial Uri? WebCamUrlDynamic { get; set; }

        [ObservableProperty]
        [JsonPropertyName("forceSnapshotPosition")]
        public partial bool ForceSnapshotPosition { get; set; }

        [ObservableProperty]
        [JsonPropertyName("method")]
        public partial long Method { get; set; }

        [ObservableProperty]
        [JsonPropertyName("orientation")]
        public partial long Orientation { get; set; }

        [ObservableProperty]
        [JsonPropertyName("pos")]
        public partial long Position { get; set; }

        [ObservableProperty]
        [JsonPropertyName("rec")]
        public partial bool Rec { get; set; } = false;

        [ObservableProperty]
        [JsonPropertyName("reloadInterval")]
        public partial double ReloadInterval { get; set; }

        [ObservableProperty]
        [JsonPropertyName("snapshotDelay")]
        public partial long SnapshotDelay { get; set; }

        [ObservableProperty]
        [JsonPropertyName("snapshotStabilizeTime")]
        public partial long SnapshotStabilizeTime { get; set; }

        [ObservableProperty]
        [JsonPropertyName("snapshotX")]
        public partial double SnapshotX { get; set; }

        [ObservableProperty]
        [JsonPropertyName("snapshotY")]
        public partial double SnapshotY { get; set; }

        [ObservableProperty]
        [JsonPropertyName("staticUrl")]
        public partial Uri? WebCamUrlStatic { get; set; }

        [ObservableProperty]
        [JsonPropertyName("timelapseBitrate")]
        public partial long TimelapseBitrate { get; set; }

        [ObservableProperty]
        [JsonPropertyName("timelapseFramerate")]
        public partial long TimelapseFramerate { get; set; }

        [ObservableProperty]
        [JsonPropertyName("timelapseHeight")]
        public partial double TimelapseHeight { get; set; }

        [ObservableProperty]
        [JsonPropertyName("timelapseInterval")]
        public partial double TimelapseInterval { get; set; }

        [ObservableProperty]
        [JsonPropertyName("timelapseLayer")]
        public partial long TimelapseLayer { get; set; }

        [ObservableProperty]
        [JsonPropertyName("timelapseMethod")]
        public partial long TimelapseMethod { get; set; }

        [ObservableProperty]
        [JsonPropertyName("timelapseSelected")]
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
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierPrinterConfigWebcam);

        #endregion

    }
}

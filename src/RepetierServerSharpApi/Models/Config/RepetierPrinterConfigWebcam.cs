using AndreasReitberger.API.Print3dServer.Core.Interfaces;
using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigWebcam : ObservableObject, IWebCamConfig
    {
        #region Properties
        [ObservableProperty]
        public partial Guid Id { get; set; }

        [ObservableProperty]
        [JsonProperty("alias"), JsonPropertyName("alias")]
        public partial string Alias { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("dynamicUrl"), JsonPropertyName("dynamicUrl")]
        public partial Uri? WebCamUrlDynamic { get; set; }

        [ObservableProperty]
        [JsonProperty("forceSnapshotPosition"), JsonPropertyName("forceSnapshotPosition")]
        public partial bool ForceSnapshotPosition { get; set; }

        [ObservableProperty]
        [JsonProperty("method"), JsonPropertyName("method")]
        public partial long Method { get; set; }

        [ObservableProperty]
        [JsonProperty("orientation"), JsonPropertyName("orientation")]
        public partial long Orientation { get; set; }

        [ObservableProperty]
        [JsonProperty("pos"), JsonPropertyName("pos")]
        public partial long Position { get; set; }

        [ObservableProperty]
        [JsonProperty("rec", NullValueHandling = NullValueHandling.Ignore), JsonPropertyName("rec")]
        public partial bool Rec { get; set; } = false;

        [ObservableProperty]
        [JsonProperty("reloadInterval"), JsonPropertyName("reloadInterval")]
        public partial double ReloadInterval { get; set; }

        [ObservableProperty]
        [JsonProperty("snapshotDelay"), JsonPropertyName("snapshotDelay")]
        public partial long SnapshotDelay { get; set; }

        [ObservableProperty]
        [JsonProperty("snapshotStabilizeTime"), JsonPropertyName("snapshotStabilizeTime")]
        public partial long SnapshotStabilizeTime { get; set; }

        [ObservableProperty]
        [JsonProperty("snapshotX"), JsonPropertyName("snapshotX")]
        public partial double SnapshotX { get; set; }

        [ObservableProperty]
        [JsonProperty("snapshotY"), JsonPropertyName("snapshotY")]
        public partial double SnapshotY { get; set; }

        [ObservableProperty]
        [JsonProperty("staticUrl"), JsonPropertyName("staticUrl")]
        public partial Uri? WebCamUrlStatic { get; set; }

        [ObservableProperty]
        [JsonProperty("timelapseBitrate"), JsonPropertyName("timelapseBitrate")]
        public partial long TimelapseBitrate { get; set; }

        [ObservableProperty]
        [JsonProperty("timelapseFramerate"), JsonPropertyName("timelapseFramerate")]
        public partial long TimelapseFramerate { get; set; }

        [ObservableProperty]
        [JsonProperty("timelapseHeight"), JsonPropertyName("timelapseHeight")]
        public partial double TimelapseHeight { get; set; }

        [ObservableProperty]
        [JsonProperty("timelapseInterval"), JsonPropertyName("timelapseInterval")]
        public partial double TimelapseInterval { get; set; }

        [ObservableProperty]
        [JsonProperty("timelapseLayer"), JsonPropertyName("timelapseLayer")]
        public partial long TimelapseLayer { get; set; }

        [ObservableProperty]
        [JsonProperty("timelapseMethod"), JsonPropertyName("timelapseMethod")]
        public partial long TimelapseMethod { get; set; }

        [ObservableProperty]
        [JsonProperty("timelapseSelected"), JsonPropertyName("timelapseSelected")]
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

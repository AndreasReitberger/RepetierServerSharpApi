using Newtonsoft.Json;
using System;

namespace AndreasReitberger.Models
{
    public partial class RepetierPrinterConfigWebcam
    {
        #region Properties
        [JsonProperty("dynamicUrl")]
        public Uri DynamicUrl { get; set; }

        [JsonProperty("forceSnapshotPosition")]
        public bool ForceSnapshotPosition { get; set; }

        [JsonProperty("method")]
        public long Method { get; set; }

        [JsonProperty("orientation")]
        public long Orientation { get; set; }

        [JsonProperty("pos")]
        public long Pos { get; set; }

        [JsonProperty("reloadInterval")]
        public long ReloadInterval { get; set; }

        [JsonProperty("snapshotDelay")]
        public long SnapshotDelay { get; set; }

        [JsonProperty("snapshotX")]
        public long SnapshotX { get; set; }

        [JsonProperty("snapshotY")]
        public long SnapshotY { get; set; }

        [JsonProperty("staticUrl")]
        public Uri StaticUrl { get; set; }

        [JsonProperty("timelapseBitrate")]
        public long TimelapseBitrate { get; set; }

        [JsonProperty("timelapseFramerate")]
        public long TimelapseFramerate { get; set; }

        [JsonProperty("timelapseHeight")]
        public double TimelapseHeight { get; set; }

        [JsonProperty("timelapseInterval")]
        public long TimelapseInterval { get; set; }

        [JsonProperty("timelapseLayer")]
        public long TimelapseLayer { get; set; }

        [JsonProperty("timelapseMethod")]
        public long TimelapseMethod { get; set; }

        [JsonProperty("timelapseSelected")]
        public long TimelapseSelected { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion

    }
}

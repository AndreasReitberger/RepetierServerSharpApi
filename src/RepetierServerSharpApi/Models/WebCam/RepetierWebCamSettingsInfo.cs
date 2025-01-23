using AndreasReitberger.API.Repetier.Enum;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierWebCamSettingsInfo : ObservableObject
    {
        #region Properties
        [ObservableProperty]

        public partial Guid Id { get; set; } = Guid.Empty;

        [ObservableProperty]

        public partial bool IsDefault { get; set; } = false;

        [ObservableProperty]

        public partial bool Autostart { get; set; } = false;

        [ObservableProperty]

        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]

        public partial RepetierWebcamType Type { get; set; } = RepetierWebcamType.Dynamic;

        [ObservableProperty]

        public partial string Slug { get; set; } = string.Empty;

        [ObservableProperty]

        public partial Guid ServerId { get; set; } = Guid.Empty;

        [ObservableProperty]

        public partial int CamIndex { get; set; } = -1;

        [ObservableProperty]

        public partial int RotationAngle { get; set; } = 0;

        [ObservableProperty]

        public partial int NetworkBufferTime { get; set; } = 150;

        [ObservableProperty]

        public partial int FileCachingTime { get; set; } = 1000;

        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

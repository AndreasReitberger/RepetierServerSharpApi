using AndreasReitberger.API.Repetier.Enum;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierWebCamSettingsInfo : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        Guid id = Guid.Empty;

        [ObservableProperty]
        bool isDefault = false;

        [ObservableProperty]
        bool autostart = false;

        [ObservableProperty]
        string name = string.Empty;

        [ObservableProperty]
        RepetierWebcamType type = RepetierWebcamType.Dynamic;

        [ObservableProperty]
        string slug = string.Empty;

        [ObservableProperty]
        Guid serverId = Guid.Empty;

        [ObservableProperty]
        int camIndex = -1;

        [ObservableProperty]
        int rotationAngle = 0;

        [ObservableProperty]
        int networkBufferTime = 150;

        [ObservableProperty]
        int fileCachingTime = 1000;

        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

using AndreasReitberger.API.Repetier.Enum;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierWebCamSettingsInfo : ObservableObject
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        Guid id = Guid.Empty;

        [ObservableProperty, JsonIgnore]
        bool isDefault = false;

        [ObservableProperty, JsonIgnore]
        bool autostart = false;

        [ObservableProperty, JsonIgnore]
        string name = string.Empty;

        [ObservableProperty, JsonIgnore]
        RepetierWebcamType type = RepetierWebcamType.Dynamic;

        [ObservableProperty, JsonIgnore]
        string slug = string.Empty;

        [ObservableProperty, JsonIgnore]
        Guid serverId = Guid.Empty;

        [ObservableProperty, JsonIgnore]
        int camIndex = -1;

        [ObservableProperty, JsonIgnore]
        int rotationAngle = 0;

        [ObservableProperty, JsonIgnore]
        int networkBufferTime = 150;

        [ObservableProperty, JsonIgnore]
        int fileCachingTime = 1000;

        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

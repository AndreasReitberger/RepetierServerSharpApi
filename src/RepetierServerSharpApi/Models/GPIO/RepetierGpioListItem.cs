using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierGpioListItem : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("bias")]
        public partial long? Bias { get; set; }

        [ObservableProperty]
        [JsonPropertyName("chip")]
        public partial long? Chip { get; set; }

        [ObservableProperty]
        [JsonPropertyName("debounceMS")]
        public partial long? DebounceMs { get; set; }

        [ObservableProperty]
        [JsonPropertyName("description")]
        public partial string Description { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("direction")]
        public partial long? Direction { get; set; }

        [ObservableProperty]
        [JsonPropertyName("display")]
        public partial string Display { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("drive")]
        public partial long? Drive { get; set; }

        [ObservableProperty]
        [JsonPropertyName("edge")]
        public partial long? Edge { get; set; }

        [ObservableProperty]
        [JsonPropertyName("error")]
        public partial string Error { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("icon")]
        public partial string Icon { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("iconOff")]
        public partial string IconOff { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("initEnabled")]
        public partial bool? InitEnabled { get; set; }

        [ObservableProperty]
        [JsonPropertyName("name")]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("operation")]
        public partial long? Operation { get; set; }

        [ObservableProperty]
        [JsonPropertyName("parameter")]
        public partial string Parameter { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("pinNumber")]
        public partial long? PinNumber { get; set; }

        [ObservableProperty]
        [JsonPropertyName("pos")]
        public partial long? Pos { get; set; }

        [ObservableProperty]
        [JsonPropertyName("pwmDutyCycle")]
        public partial double? PwmDutyCycle { get; set; }

        [ObservableProperty]
        [JsonPropertyName("pwmFrequency")]
        public partial double? PwmFrequency { get; set; }

        [ObservableProperty]
        [JsonPropertyName("pwmInitDutyCycle")]
        public partial double? PwmInitDutyCycle { get; set; }

        [ObservableProperty]
        [JsonPropertyName("pwmPolarity")]
        public partial bool? PwmPolarity { get; set; }

        [ObservableProperty]
        [JsonPropertyName("securityQuestion")]
        public partial bool? SecurityQuestion { get; set; }

        [ObservableProperty]
        [JsonPropertyName("showInMenu")]
        public partial bool? ShowInMenu { get; set; }

        [ObservableProperty]
        [JsonPropertyName("slug")]
        public partial string Slug { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("state")]
        public partial bool? State { get; set; }

        [ObservableProperty]
        [JsonPropertyName("uuid")]
        public partial Guid? Uuid { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierGpioListItem);
        #endregion
    }
}

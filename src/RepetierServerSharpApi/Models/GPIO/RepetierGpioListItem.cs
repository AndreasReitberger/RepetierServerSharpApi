using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierGpioListItem : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("bias"), JsonPropertyName("bias")]
        public partial long? Bias { get; set; }

        [ObservableProperty]
        [JsonProperty("chip"), JsonPropertyName("chip")]
        public partial long? Chip { get; set; }

        [ObservableProperty]
        [JsonProperty("debounceMS"), JsonPropertyName("debounceMS")]
        public partial long? DebounceMs { get; set; }

        [ObservableProperty]
        [JsonProperty("description"), JsonPropertyName("description")]
        public partial string Description { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("direction"), JsonPropertyName("direction")]
        public partial long? Direction { get; set; }

        [ObservableProperty]
        [JsonProperty("display"), JsonPropertyName("display")]
        public partial string Display { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("drive"), JsonPropertyName("drive")]
        public partial long? Drive { get; set; }

        [ObservableProperty]
        [JsonProperty("edge"), JsonPropertyName("edge")]
        public partial long? Edge { get; set; }

        [ObservableProperty]
        [JsonProperty("error"), JsonPropertyName("error")]
        public partial string Error { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("icon"), JsonPropertyName("icon")]
        public partial string Icon { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("iconOff"), JsonPropertyName("iconOff")]
        public partial string IconOff { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("initEnabled"), JsonPropertyName("initEnabled")]
        public partial bool? InitEnabled { get; set; }

        [ObservableProperty]
        [JsonProperty("name"), JsonPropertyName("name")]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("operation"), JsonPropertyName("operation")]
        public partial long? Operation { get; set; }

        [ObservableProperty]
        [JsonProperty("parameter"), JsonPropertyName("parameter")]
        public partial string Parameter { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("pinNumber"), JsonPropertyName("pinNumber")]
        public partial long? PinNumber { get; set; }

        [ObservableProperty]
        [JsonProperty("pos"), JsonPropertyName("pos")]
        public partial long? Pos { get; set; }

        [ObservableProperty]
        [JsonProperty("pwmDutyCycle"), JsonPropertyName("pwmDutyCycle")]
        public partial long? PwmDutyCycle { get; set; }

        [ObservableProperty]
        [JsonProperty("pwmFrequency"), JsonPropertyName("pwmFrequency")]
        public partial long? PwmFrequency { get; set; }

        [ObservableProperty]
        [JsonProperty("pwmInitDutyCycle"), JsonPropertyName("pwmInitDutyCycle")]
        public partial long? PwmInitDutyCycle { get; set; }

        [ObservableProperty]
        [JsonProperty("pwmPolarity"), JsonPropertyName("pwmPolarity")]
        public partial bool? PwmPolarity { get; set; }

        [ObservableProperty]
        [JsonProperty("securityQuestion"), JsonPropertyName("securityQuestion")]
        public partial bool? SecurityQuestion { get; set; }

        [ObservableProperty]
        [JsonProperty("showInMenu"), JsonPropertyName("showInMenu")]
        public partial bool? ShowInMenu { get; set; }

        [ObservableProperty]
        [JsonProperty("slug"), JsonPropertyName("slug")]
        public partial string Slug { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("state"), JsonPropertyName("state")]
        public partial bool? State { get; set; }

        [ObservableProperty]
        [JsonProperty("uuid"), JsonPropertyName("uuid")]
        public partial Guid? Uuid { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

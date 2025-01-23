using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierGpioListItem : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        
        [JsonProperty("bias")]
        public partial long? Bias { get; set; }

        [ObservableProperty]
        
        [JsonProperty("chip")]
        public partial long? Chip { get; set; }

        [ObservableProperty]
        
        [JsonProperty("debounceMS")]
        public partial long? DebounceMs { get; set; }

        [ObservableProperty]
        
        [JsonProperty("description")]
        public partial string Description { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("direction")]
        public partial long? Direction { get; set; }

        [ObservableProperty]
        
        [JsonProperty("display")]
        public partial string Display { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("drive")]
        public partial long? Drive { get; set; }

        [ObservableProperty]
        
        [JsonProperty("edge")]
        public partial long? Edge { get; set; }

        [ObservableProperty]
        
        [JsonProperty("error")]
        public partial string Error { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("icon")]
        public partial string Icon { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("iconOff")]
        public partial string IconOff { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("initEnabled")]
        public partial bool? InitEnabled { get; set; }

        [ObservableProperty]
        
        [JsonProperty("name")]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("operation")]
        public partial long? Operation { get; set; }

        [ObservableProperty]
        
        [JsonProperty("parameter")]
        public partial string Parameter { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("pinNumber")]
        public partial long? PinNumber { get; set; }

        [ObservableProperty]
        
        [JsonProperty("pos")]
        public partial long? Pos { get; set; }

        [ObservableProperty]
        
        [JsonProperty("pwmDutyCycle")]
        public partial long? PwmDutyCycle { get; set; }

        [ObservableProperty]
        
        [JsonProperty("pwmFrequency")]
        public partial long? PwmFrequency { get; set; }

        [ObservableProperty]
        
        [JsonProperty("pwmInitDutyCycle")]
        public partial long? PwmInitDutyCycle { get; set; }

        [ObservableProperty]
        
        [JsonProperty("pwmPolarity")]
        public partial bool? PwmPolarity { get; set; }

        [ObservableProperty]
        
        [JsonProperty("securityQuestion")]
        public partial bool? SecurityQuestion { get; set; }

        [ObservableProperty]
        
        [JsonProperty("showInMenu")]
        public partial bool? ShowInMenu { get; set; }

        [ObservableProperty]
        
        [JsonProperty("slug")]
        public partial string Slug { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("state")]
        public partial bool? State { get; set; }

        [ObservableProperty]
        
        [JsonProperty("uuid")]
        public partial Guid? Uuid { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

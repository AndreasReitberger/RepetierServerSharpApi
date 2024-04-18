using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierGpioListItem : ObservableObject
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("bias")]
        long? bias;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("chip")]
        long? chip;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("debounceMS")]
        long? debounceMs;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("description")]
        string description = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("direction")]
        long? direction;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("display")]
        string display = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("drive")]
        long? drive;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("edge")]
        long? edge;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("error")]
        string error = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("icon")]
        string icon = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("iconOff")]
        string iconOff = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("initEnabled")]
        bool? initEnabled;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("name")]
        string name = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("operation")]
        long? operation;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("parameter")]
        string parameter = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("pinNumber")]
        long? pinNumber;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("pos")]
        long? pos;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("pwmDutyCycle")]
        long? pwmDutyCycle;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("pwmFrequency")]
        long? pwmFrequency;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("pwmInitDutyCycle")]
        long? pwmInitDutyCycle;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("pwmPolarity")]
        bool? pwmPolarity;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("securityQuestion")]
        bool? securityQuestion;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("showInMenu")]
        bool? showInMenu;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("slug")]
        string slug = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("state")]
        bool? state;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("uuid")]
        Guid? uuid;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

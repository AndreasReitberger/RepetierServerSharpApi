using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierGpioListItem : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("bias")]
        [property: JsonIgnore]
        long? bias;

        [ObservableProperty]
        [JsonProperty("chip")]
        [property: JsonIgnore]
        long? chip;

        [ObservableProperty]
        [JsonProperty("debounceMS")]
        [property: JsonIgnore]
        long? debounceMs;

        [ObservableProperty]
        [JsonProperty("description")]
        [property: JsonIgnore]
        string description;

        [ObservableProperty]
        [JsonProperty("direction")]
        [property: JsonIgnore]
        long? direction;

        [ObservableProperty]
        [JsonProperty("display")]
        [property: JsonIgnore]
        string display;

        [ObservableProperty]
        [JsonProperty("drive")]
        [property: JsonIgnore]
        long? drive;

        [ObservableProperty]
        [JsonProperty("edge")]
        [property: JsonIgnore]
        long? edge;

        [ObservableProperty]
        [JsonProperty("error")]
        [property: JsonIgnore]
        string error;

        [ObservableProperty]
        [JsonProperty("icon")]
        [property: JsonIgnore]
        string icon;

        [ObservableProperty]
        [JsonProperty("iconOff")]
        [property: JsonIgnore]
        string iconOff;

        [ObservableProperty]
        [JsonProperty("initEnabled")]
        [property: JsonIgnore]
        bool? initEnabled;

        [ObservableProperty]
        [JsonProperty("name")]
        [property: JsonIgnore]
        string name;

        [ObservableProperty]
        [JsonProperty("operation")]
        [property: JsonIgnore]
        long? operation;

        [ObservableProperty]
        [JsonProperty("parameter")]
        [property: JsonIgnore]
        string parameter;

        [ObservableProperty]
        [JsonProperty("pinNumber")]
        [property: JsonIgnore]
        long? pinNumber;

        [ObservableProperty]
        [JsonProperty("pos")]
        [property: JsonIgnore]
        long? pos;

        [ObservableProperty]
        [JsonProperty("pwmDutyCycle")]
        [property: JsonIgnore]
        long? pwmDutyCycle;

        [ObservableProperty]
        [JsonProperty("pwmFrequency")]
        [property: JsonIgnore]
        long? pwmFrequency;

        [ObservableProperty]
        [JsonProperty("pwmInitDutyCycle")]
        [property: JsonIgnore]
        long? pwmInitDutyCycle;

        [ObservableProperty]
        [JsonProperty("pwmPolarity")]
        [property: JsonIgnore]
        bool? pwmPolarity;

        [ObservableProperty]
        [JsonProperty("securityQuestion")]
        [property: JsonIgnore]
        bool? securityQuestion;

        [ObservableProperty]
        [JsonProperty("showInMenu")]
        [property: JsonIgnore]
        bool? showInMenu;

        [ObservableProperty]
        [JsonProperty("slug")]
        [property: JsonIgnore]
        string slug;

        [ObservableProperty]
        [JsonProperty("state")]
        [property: JsonIgnore]
        bool? state;

        [ObservableProperty]
        [JsonProperty("uuid")]
        [property: JsonIgnore]
        Guid? uuid;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

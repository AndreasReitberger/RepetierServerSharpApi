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
        long? bias;

        [ObservableProperty]
        [JsonProperty("chip")]
        long? chip;

        [ObservableProperty]
        [JsonProperty("debounceMS")]
        long? debounceMs;

        [ObservableProperty]
        [JsonProperty("description")]
        string description;

        [ObservableProperty]
        [JsonProperty("direction")]
        long? direction;

        [ObservableProperty]
        [JsonProperty("display")]
        string display;

        [ObservableProperty]
        [JsonProperty("drive")]
        long? drive;

        [ObservableProperty]
        [JsonProperty("edge")]
        long? edge;

        [ObservableProperty]
        [JsonProperty("error")]
        string error;

        [ObservableProperty]
        [JsonProperty("icon")]
        string icon;

        [ObservableProperty]
        [JsonProperty("iconOff")]
        string iconOff;

        [ObservableProperty]
        [JsonProperty("initEnabled")]
        bool? initEnabled;

        [ObservableProperty]
        [JsonProperty("name")]
        string name;

        [ObservableProperty]
        [JsonProperty("operation")]
        long? operation;

        [ObservableProperty]
        [JsonProperty("parameter")]
        string parameter;

        [ObservableProperty]
        [JsonProperty("pinNumber")]
        long? pinNumber;

        [ObservableProperty]
        [JsonProperty("pos")]
        long? pos;

        [ObservableProperty]
        [JsonProperty("pwmDutyCycle")]
        long? pwmDutyCycle;

        [ObservableProperty]
        [JsonProperty("pwmFrequency")]
        long? pwmFrequency;

        [ObservableProperty]
        [JsonProperty("pwmInitDutyCycle")]
        long? pwmInitDutyCycle;

        [ObservableProperty]
        [JsonProperty("pwmPolarity")]
        bool? pwmPolarity;

        [ObservableProperty]
        [JsonProperty("securityQuestion")]
        bool? securityQuestion;

        [ObservableProperty]
        [JsonProperty("showInMenu")]
        bool? showInMenu;

        [ObservableProperty]
        [JsonProperty("slug")]
        string slug;

        [ObservableProperty]
        [JsonProperty("state")]
        bool? state;

        [ObservableProperty]
        [JsonProperty("uuid")]
        Guid? uuid;
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

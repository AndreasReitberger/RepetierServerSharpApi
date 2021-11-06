using Newtonsoft.Json;
using System;

namespace AndreasReitberger.Models
{
    public partial class RepetierGpioListItem
    {
        #region Properties
        [JsonProperty("bias", NullValueHandling = NullValueHandling.Ignore)]
        public long? Bias { get; set; }

        [JsonProperty("chip", NullValueHandling = NullValueHandling.Ignore)]
        public long? Chip { get; set; }

        [JsonProperty("debounceMS", NullValueHandling = NullValueHandling.Ignore)]
        public long? DebounceMs { get; set; }

        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty("direction", NullValueHandling = NullValueHandling.Ignore)]
        public long? Direction { get; set; }

        [JsonProperty("display", NullValueHandling = NullValueHandling.Ignore)]
        public string Display { get; set; }

        [JsonProperty("drive", NullValueHandling = NullValueHandling.Ignore)]
        public long? Drive { get; set; }

        [JsonProperty("edge", NullValueHandling = NullValueHandling.Ignore)]
        public long? Edge { get; set; }

        [JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
        public string Error { get; set; }

        [JsonProperty("icon", NullValueHandling = NullValueHandling.Ignore)]
        public string Icon { get; set; }

        [JsonProperty("iconOff", NullValueHandling = NullValueHandling.Ignore)]
        public string IconOff { get; set; }

        [JsonProperty("initEnabled", NullValueHandling = NullValueHandling.Ignore)]
        public bool? InitEnabled { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("operation", NullValueHandling = NullValueHandling.Ignore)]
        public long? Operation { get; set; }

        [JsonProperty("parameter", NullValueHandling = NullValueHandling.Ignore)]
        public string Parameter { get; set; }

        [JsonProperty("pinNumber", NullValueHandling = NullValueHandling.Ignore)]
        public long? PinNumber { get; set; }

        [JsonProperty("pos", NullValueHandling = NullValueHandling.Ignore)]
        public long? Pos { get; set; }

        [JsonProperty("pwmDutyCycle", NullValueHandling = NullValueHandling.Ignore)]
        public long? PwmDutyCycle { get; set; }

        [JsonProperty("pwmFrequency", NullValueHandling = NullValueHandling.Ignore)]
        public long? PwmFrequency { get; set; }

        [JsonProperty("pwmInitDutyCycle", NullValueHandling = NullValueHandling.Ignore)]
        public long? PwmInitDutyCycle { get; set; }

        [JsonProperty("pwmPolarity", NullValueHandling = NullValueHandling.Ignore)]
        public bool? PwmPolarity { get; set; }

        [JsonProperty("showInMenu", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ShowInMenu { get; set; }

        [JsonProperty("slug", NullValueHandling = NullValueHandling.Ignore)]
        public string Slug { get; set; }

        [JsonProperty("state", NullValueHandling = NullValueHandling.Ignore)]
        public bool? State { get; set; }

        [JsonProperty("uuid", NullValueHandling = NullValueHandling.Ignore)]
        public Guid? Uuid { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

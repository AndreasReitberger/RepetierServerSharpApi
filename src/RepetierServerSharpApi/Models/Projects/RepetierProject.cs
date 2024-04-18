using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProject : ObservableObject
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("folder")]
        long folder;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("name")]
        string name = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("preview")]
        string preview = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("uuid")]
        Guid uuid;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("version")]
        long version;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

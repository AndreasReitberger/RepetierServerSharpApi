using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProject : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("folder")]
        [property: JsonIgnore]
        long folder;

        [ObservableProperty]
        [JsonProperty("name")]
        [property: JsonIgnore]
        string name;

        [ObservableProperty]
        [JsonProperty("preview")]
        [property: JsonIgnore]
        string preview;

        [ObservableProperty]
        [JsonProperty("uuid")]
        [property: JsonIgnore]
        Guid uuid;

        [ObservableProperty]
        [JsonProperty("version")]
        [property: JsonIgnore]
        long version;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

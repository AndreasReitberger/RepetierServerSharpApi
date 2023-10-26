using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierQuickGcodeCommand : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("command")]
        [property: JsonIgnore]
        string command;

        [ObservableProperty]
        [JsonProperty("icon")]
        [property: JsonIgnore]
        string icon;

        [ObservableProperty]
        [JsonProperty("name")]
        [property: JsonIgnore]
        string name;

        [ObservableProperty]
        [JsonProperty("visibleWhenPrinting")]
        [property: JsonIgnore]
        bool visibleWhenPrinting;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        
        #endregion
    }
}

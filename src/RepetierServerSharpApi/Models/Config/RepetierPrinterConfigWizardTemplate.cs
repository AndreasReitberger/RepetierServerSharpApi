using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigWizardTemplate : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("author")]
        [property: JsonIgnore]
        string author;

        [ObservableProperty]
        [JsonProperty("command")]
        [property: JsonIgnore]
        string command;

        [ObservableProperty]
        [JsonProperty("description")]
        [property: JsonIgnore]
        string description;

        [ObservableProperty]
        [JsonProperty("icon")]
        [property: JsonIgnore]
        string icon;

        [ObservableProperty]
        [JsonProperty("name")]
        [property: JsonIgnore]
        string name;

        [ObservableProperty]
        [JsonProperty("source")]
        [property: JsonIgnore]
        string source;

        [ObservableProperty]
        [JsonProperty("uuid")]
        [property: JsonIgnore]
        Guid uuid;

        [ObservableProperty]
        [JsonProperty("version")]
        [property: JsonIgnore]
        long version;

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

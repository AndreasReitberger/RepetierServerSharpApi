using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigWizardTemplate : ObservableObject
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("author")]
        string author;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("command")]
        string command;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("description")]
        string description;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("icon")]
        string icon;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("name")]
        string name;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("source")]
        string source;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("uuid")]
        Guid uuid;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("version")]
        long version;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("visibleWhenPrinting")]
        bool visibleWhenPrinting;

        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        
        #endregion
    }
}

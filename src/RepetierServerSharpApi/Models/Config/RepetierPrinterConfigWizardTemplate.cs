using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigWizardTemplate : ObservableObject
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("author")]
        string author = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("command")]
        string command = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("description")]
        string description = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("icon")]
        string icon = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("name")]
        string name = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("source")]
        string source = string.Empty;

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

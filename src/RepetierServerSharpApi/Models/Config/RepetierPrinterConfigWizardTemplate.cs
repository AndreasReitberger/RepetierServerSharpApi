using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigWizardTemplate : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("author")]
        string author;

        [ObservableProperty]
        [JsonProperty("command")]
        string command;

        [ObservableProperty]
        [JsonProperty("description")]
        string description;

        [ObservableProperty]
        [JsonProperty("icon")]
        string icon;

        [ObservableProperty]
        [JsonProperty("name")]
        string name;

        [ObservableProperty]
        [JsonProperty("source")]
        string source;

        [ObservableProperty]
        [JsonProperty("uuid")]
        Guid uuid;

        [ObservableProperty]
        [JsonProperty("version")]
        long version;

        [ObservableProperty]
        [JsonProperty("visibleWhenPrinting")]
        bool visibleWhenPrinting;

        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProject : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("folder")]
        long folder;

        [ObservableProperty]
        [JsonProperty("name")]
        string name;

        [ObservableProperty]
        [JsonProperty("preview")]
        string preview;

        [ObservableProperty]
        [JsonProperty("uuid")]
        Guid uuid;

        [ObservableProperty]
        [JsonProperty("version")]
        long version;
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

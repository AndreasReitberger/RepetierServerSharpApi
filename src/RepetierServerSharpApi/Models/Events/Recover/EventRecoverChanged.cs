using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventRecoverChanged : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("data")]
        [property: JsonIgnore]
        EventRecoverChangedData data;

        [ObservableProperty]
        [JsonProperty("event")]
        [property: JsonIgnore]
        string eventName;

        [ObservableProperty]
        [JsonProperty("printer")]
        [property: JsonIgnore]
        string printer;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }

}

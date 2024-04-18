using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventRecoverChanged : ObservableObject
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("data")]
        EventRecoverChangedData? data;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("event")]
        string eventName = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("printer")]
        string printer = string.Empty;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }

}

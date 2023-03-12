using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConfigTemperature : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("name")]
        string name;

        [ObservableProperty]
        [JsonProperty("temp")]
        long temp;

        #region Json Ignore
        [ObservableProperty]
        [JsonIgnore]
        string targetComponent;
        #endregion

        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

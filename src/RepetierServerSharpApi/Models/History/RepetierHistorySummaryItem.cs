using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{

    public partial class RepetierHistorySummaryItem : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("aborted")]
        [property: JsonIgnore]
        long aborted;

        [ObservableProperty]
        [JsonProperty("computed")]
        [property: JsonIgnore]
        double computed;

        [ObservableProperty]
        [JsonProperty("costs")]
        [property: JsonIgnore]
        double costs;

        [ObservableProperty]
        [JsonProperty("filament")]
        [property: JsonIgnore]
        double filament;

        [ObservableProperty]
        [JsonProperty("finished")]
        [property: JsonIgnore]
        long finished;

        [ObservableProperty]
        [JsonProperty("month")]
        [property: JsonIgnore]
        long month;

        [ObservableProperty]
        [JsonProperty("num")]
        [property: JsonIgnore]
        long num;

        [ObservableProperty]
        [JsonProperty("real")]
        [property: JsonIgnore]
        double real;

        [ObservableProperty]
        [JsonProperty("year")]
        [property: JsonIgnore]
        long year;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

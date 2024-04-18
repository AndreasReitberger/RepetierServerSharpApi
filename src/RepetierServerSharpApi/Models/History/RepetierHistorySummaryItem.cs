using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{

    public partial class RepetierHistorySummaryItem : ObservableObject
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("aborted")]

        long aborted;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("computed")]

        double computed;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("costs")]

        double costs;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("filament")]

        double filament;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("finished")]

        long finished;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("month")]

        long month;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("num")]

        long num;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("real")]

        double real;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("year")]

        long year;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

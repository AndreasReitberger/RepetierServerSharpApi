using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{

    public partial class RepetierHistorySummaryItem : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("aborted")]
        long aborted;

        [ObservableProperty]
        [JsonProperty("computed")]
        double computed;

        [ObservableProperty]
        [JsonProperty("costs")]
        double costs;

        [ObservableProperty]
        [JsonProperty("filament")]
        double filament;

        [ObservableProperty]
        [JsonProperty("finished")]
        long finished;

        [ObservableProperty]
        [JsonProperty("month")]
        long month;

        [ObservableProperty]
        [JsonProperty("num")]
        long num;

        [ObservableProperty]
        [JsonProperty("real")]
        double real;

        [ObservableProperty]
        [JsonProperty("year")]
        long year;
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{

    public partial class RepetierHistorySummaryItem : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        
        [JsonProperty("aborted")]
        public partial long Aborted { get; set; }

        [ObservableProperty]
        
        [JsonProperty("computed")]
        public partial double Computed { get; set; }

        [ObservableProperty]
        
        [JsonProperty("costs")]
        public partial double Costs { get; set; }

        [ObservableProperty]
        
        [JsonProperty("filament")]
        public partial double Filament { get; set; }

        [ObservableProperty]
        
        [JsonProperty("finished")]
        public partial long Finished { get; set; }

        [ObservableProperty]
        
        [JsonProperty("month")]
        public partial long Month { get; set; }

        [ObservableProperty]
        
        [JsonProperty("num")]
        public partial long Num { get; set; }

        [ObservableProperty]
        
        [JsonProperty("real")]
        public partial double Real { get; set; }

        [ObservableProperty]
        
        [JsonProperty("year")]
        public partial long Year { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

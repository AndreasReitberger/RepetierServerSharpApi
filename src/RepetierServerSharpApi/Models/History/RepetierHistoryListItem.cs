using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierHistoryListItem : ObservableObject
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("computedTime")]

        double computedTime;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("costs")]

        double costs;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("endTime")]

        double endTime;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("filament")]

        double filament;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("filename")]

        string filename;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("id")]

        long id;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("month")]

        long month;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("notes")]

        string notes;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("pauseTime")]

        double pauseTime;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("printerName")]

        string printerName;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("printerSlug")]

        string printerSlug;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("printerUUID")]

        string printerUuid;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("report")]

        string report;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("startTime")]

        double startTime;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("status")]

        long status;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("username")]

        string username;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("year")]

        long year;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

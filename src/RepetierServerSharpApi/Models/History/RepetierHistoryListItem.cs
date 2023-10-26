using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierHistoryListItem : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("computedTime")]
        [property: JsonIgnore]
        double computedTime;

        [ObservableProperty]
        [JsonProperty("costs")]
        [property: JsonIgnore]
        double costs;

        [ObservableProperty]
        [JsonProperty("endTime")]
        [property: JsonIgnore]
        double endTime;

        [ObservableProperty]
        [JsonProperty("filament")]
        [property: JsonIgnore]
        double filament;

        [ObservableProperty]
        [JsonProperty("filename")]
        [property: JsonIgnore]
        string filename;

        [ObservableProperty]
        [JsonProperty("id")]
        [property: JsonIgnore]
        long id;

        [ObservableProperty]
        [JsonProperty("month")]
        [property: JsonIgnore]
        long month;

        [ObservableProperty]
        [JsonProperty("notes")]
        [property: JsonIgnore]
        string notes;

        [ObservableProperty]
        [JsonProperty("pauseTime")]
        [property: JsonIgnore]
        double pauseTime;

        [ObservableProperty]
        [JsonProperty("printerName")]
        [property: JsonIgnore]
        string printerName;

        [ObservableProperty]
        [JsonProperty("printerSlug")]
        [property: JsonIgnore]
        string printerSlug;

        [ObservableProperty]
        [JsonProperty("printerUUID")]
        [property: JsonIgnore]
        string printerUuid;

        [ObservableProperty]
        [JsonProperty("report")]
        [property: JsonIgnore]
        string report;

        [ObservableProperty]
        [JsonProperty("startTime")]
        [property: JsonIgnore]
        double startTime;

        [ObservableProperty]
        [JsonProperty("status")]
        [property: JsonIgnore]
        long status;

        [ObservableProperty]
        [JsonProperty("username")]
        [property: JsonIgnore]
        string username;

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

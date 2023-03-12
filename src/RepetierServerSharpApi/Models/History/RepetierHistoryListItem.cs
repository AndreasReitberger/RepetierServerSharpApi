using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierHistoryListItem : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("computedTime")]
        double computedTime;

        [ObservableProperty]
        [JsonProperty("costs")]
        double costs;

        [ObservableProperty]
        [JsonProperty("endTime")]
        double endTime;

        [ObservableProperty]
        [JsonProperty("filament")]
        double filament;

        [ObservableProperty]
        [JsonProperty("filename")]
        string filename;

        [ObservableProperty]
        [JsonProperty("id")]
        long id;

        [ObservableProperty]
        [JsonProperty("month")]
        long month;

        [ObservableProperty]
        [JsonProperty("notes")]
        string notes;

        [ObservableProperty]
        [JsonProperty("pauseTime")]
        double pauseTime;

        [ObservableProperty]
        [JsonProperty("printerName")]
        string printerName;

        [ObservableProperty]
        [JsonProperty("printerSlug")]
        string printerSlug;

        [ObservableProperty]
        [JsonProperty("printerUUID")]
        string printerUuid;

        [ObservableProperty]
        [JsonProperty("report")]
        string report;

        [ObservableProperty]
        [JsonProperty("startTime")]
        double startTime;

        [ObservableProperty]
        [JsonProperty("status")]
        long status;

        [ObservableProperty]
        [JsonProperty("username")]
        string username;

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

using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierHistoryListRespone : ObservableObject
    {
        #region Properties

        [ObservableProperty]

        [JsonProperty("list"), JsonPropertyName("list")]
        public partial List<RepetierHistoryListItem> List { get; set; } = new();
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

using Newtonsoft.Json;
using System.Text.Json.Serialization;


namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierModelList : ObservableObject
    {
        #region Properties
        [ObservableProperty]

        [JsonProperty("data"), JsonPropertyName("data")]
        public partial List<RepetierModel> Data { get; set; } = new();

        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

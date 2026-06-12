using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProjectsFolderRespone : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("folder"), JsonPropertyName("folder")]
        public partial RepetierProjectFolder? Folder { get; set; }

        [ObservableProperty]
        [JsonProperty("ok"), JsonPropertyName("ok")]
        public partial bool Ok { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

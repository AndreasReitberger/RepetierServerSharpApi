using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProjectsFolderRespone : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("folder")]
        [property: JsonIgnore]
        RepetierProjectFolder folder;

        [ObservableProperty]
        [JsonProperty("ok")]
        [property: JsonIgnore]
        bool ok;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

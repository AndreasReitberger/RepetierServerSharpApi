using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProjectsFolderRespone : ObservableObject
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("folder")]

        RepetierProjectFolder folder;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("ok")]

        bool ok;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

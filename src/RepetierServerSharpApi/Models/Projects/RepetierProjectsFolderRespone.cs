using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProjectsFolderRespone : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        
        [JsonProperty("folder")]
        public partial RepetierProjectFolder? Folder { get; set; }

        [ObservableProperty]
        
        [JsonProperty("ok")]
        public partial bool Ok { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

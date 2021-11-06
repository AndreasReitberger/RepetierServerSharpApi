using Newtonsoft.Json;

namespace AndreasReitberger.Models
{
    public partial class RepetierProjectsFolderRespone
    {
        #region Properties
        [JsonProperty("folder")]
        public RepetierProjectFolder Folder { get; set; }

        [JsonProperty("ok")]
        public bool Ok { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

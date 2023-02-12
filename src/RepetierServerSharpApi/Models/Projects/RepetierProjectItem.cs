using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public class RepetierProjectItem
    {
        #region Properties
        public bool IsFolder
        {
            get => Folder != null && Project == null;
        }
        public long Index { get; set; }
        public string Path { get; set; }
        public byte[] PreviewImage { get; set; }

        public RepetierProjectSubFolder Folder { get; set; }
        public RepetierProject Project { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

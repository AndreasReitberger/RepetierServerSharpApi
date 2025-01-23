using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProjectItem : ObservableObject
    {
        #region Properties
        public bool IsFolder => Folder is not null && Project is null;

        [ObservableProperty]
        public partial long Index { get; set; }

        [ObservableProperty]
        public partial string Path { get; set; } = string.Empty;

        [ObservableProperty]
        public partial byte[]? PreviewImage { get; set; }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsFolder))]
        public partial RepetierProjectSubFolder? Folder { get; set; }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsFolder))]
        public partial RepetierProject? Project { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

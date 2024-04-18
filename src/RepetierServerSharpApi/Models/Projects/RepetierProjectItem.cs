using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProjectItem : ObservableObject
    {
        #region Properties
        public bool IsFolder => Folder is not null && Project is null;

        [ObservableProperty]
        long index;

        [ObservableProperty]
        string path = string.Empty;

        [ObservableProperty]
        byte[]? previewImage;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsFolder))]
        RepetierProjectSubFolder? folder;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsFolder))]
        RepetierProject? project;
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

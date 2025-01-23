using AndreasReitberger.API.Print3dServer.Core.Interfaces;
using AndreasReitberger.API.Print3dServer.Core.Utilities;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierModelGroup : ObservableObject, IGcodeGroup
    {
        #region Properties

        [ObservableProperty]

        public partial Guid Id { get; set; }

        [ObservableProperty]

        [JsonProperty("name")]
        public partial string Name { get; set; } = string.Empty;

        partial void OnNameChanged(string value)
        {
            DirectoryName = value;
        }

        #region Interface
        [ObservableProperty]

        public partial string DirectoryName { get; set; } = string.Empty;

        [ObservableProperty]

        public partial string Path { get; set; } = string.Empty;

        [ObservableProperty]

        public partial string Root { get; set; } = string.Empty;

        [ObservableProperty]

        [NotifyPropertyChangedFor(nameof(ModifiedGeneralized))]
        public partial double? Modified { get; set; }

        partial void OnModifiedChanged(double? value)
        {
            if (value is not null)
                ModifiedGeneralized = TimeBaseConvertHelper.FromUnixDoubleMiliseconds(value);
        }

        [ObservableProperty]

        public partial DateTime? ModifiedGeneralized { get; set; }

        [ObservableProperty]

        public partial long Size { get; set; }

        [ObservableProperty]

        public partial string Permissions { get; set; } = string.Empty;
        #endregion

        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion

    }
}

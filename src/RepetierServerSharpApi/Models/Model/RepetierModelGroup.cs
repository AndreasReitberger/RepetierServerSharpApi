using AndreasReitberger.API.Print3dServer.Core.Interfaces;
using AndreasReitberger.API.Print3dServer.Core.Utilities;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierModelGroup : ObservableObject, IGcodeGroup
    {
        #region Properties

        [ObservableProperty, JsonIgnore]
        Guid id;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("name")]
        string name = string.Empty;
        partial void OnNameChanged(string value)
        {
            DirectoryName = value;
        }

        #region Interface
        [ObservableProperty, JsonIgnore]
        string directoryName = string.Empty;

        [ObservableProperty, JsonIgnore]
        string path = string.Empty;

        [ObservableProperty, JsonIgnore]
        string root = string.Empty;

        [ObservableProperty, JsonIgnore]
        [NotifyPropertyChangedFor(nameof(ModifiedGeneralized))]
        double? modified;
        partial void OnModifiedChanged(double? value)
        {
            if (value is not null)
                ModifiedGeneralized = TimeBaseConvertHelper.FromUnixDoubleMiliseconds(value);
        }

        [ObservableProperty, JsonIgnore]
        DateTime? modifiedGeneralized;

        [ObservableProperty, JsonIgnore]
        long size;

        [ObservableProperty, JsonIgnore]
        string permissions = string.Empty;
        #endregion

        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion

    }
}

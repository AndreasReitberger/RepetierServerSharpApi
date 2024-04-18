using AndreasReitberger.API.Print3dServer.Core.Interfaces;
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

        #region Interface
        [ObservableProperty, JsonIgnore]
        string directoryName = string.Empty;

        [ObservableProperty, JsonIgnore]
        string path = string.Empty;

        [ObservableProperty, JsonIgnore]
        string root = string.Empty;

        [ObservableProperty, JsonIgnore]
        double modified;

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

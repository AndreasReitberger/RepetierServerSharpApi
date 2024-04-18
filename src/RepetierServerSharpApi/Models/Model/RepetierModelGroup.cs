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

        string name;

        #region Interface
        [ObservableProperty, JsonIgnore]

        string directoryName;

        [ObservableProperty, JsonIgnore]

        string path;

        [ObservableProperty, JsonIgnore]

        string root;

        [ObservableProperty, JsonIgnore]

        double modified;

        [ObservableProperty, JsonIgnore]

        long size;

        [ObservableProperty, JsonIgnore]

        string permissions;
        #endregion

        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion

    }
}

using AndreasReitberger.API.Print3dServer.Core.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierModelGroup : ObservableObject, IGcodeGroup
    {
        #region Properties

        [ObservableProperty, JsonIgnore]
        [property: JsonIgnore]
        Guid id;

        [ObservableProperty]
        [JsonProperty("name")]
        string name;

        #region Interface
        [ObservableProperty]
        [property: JsonIgnore]
        string directoryName;

        [ObservableProperty]
        [property: JsonIgnore]
        string path;

        [ObservableProperty]
        [property: JsonIgnore]
        string root;

        [ObservableProperty]
        [property: JsonIgnore]
        double modified;

        [ObservableProperty]
        [property: JsonIgnore]
        long size;

        [ObservableProperty]
        [property: JsonIgnore]
        string permissions;
        #endregion

        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion

    }
}

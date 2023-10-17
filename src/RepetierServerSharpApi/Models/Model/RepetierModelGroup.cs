using AndreasReitberger.API.Print3dServer.Core.Interfaces;
using AndreasReitberger.API.Repetier.Enum;
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

        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion

    }
}

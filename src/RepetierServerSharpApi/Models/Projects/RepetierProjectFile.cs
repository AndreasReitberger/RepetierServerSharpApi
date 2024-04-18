using AndreasReitberger.API.Repetier.Enum;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProjectFile : ObservableObject
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        RepetierProjectFileType type;

        [ObservableProperty, JsonIgnore]
        string name = string.Empty;

        [ObservableProperty, JsonIgnore]
        Guid projectUuid = Guid.Empty;

        [ObservableProperty, JsonIgnore]
        RepetierProjectsProjectFile? file;
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

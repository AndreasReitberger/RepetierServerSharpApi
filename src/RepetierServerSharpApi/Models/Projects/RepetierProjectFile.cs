using AndreasReitberger.API.Repetier.Enum;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProjectFile : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        
        public partial RepetierProjectFileType Type { get; set; }

        [ObservableProperty]
        
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        
        public partial Guid ProjectUuid { get; set; } = Guid.Empty;

        [ObservableProperty]
        
        public partial RepetierProjectsProjectFile? File { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

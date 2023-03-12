using AndreasReitberger.API.Repetier.Enum;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public class RepetierProjectFile
    {
        #region Properties
        public RepetierProjectFileType Type { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid ProjectUuid { get; set; } = Guid.Empty;
        public RepetierProjectsProjectFile File { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

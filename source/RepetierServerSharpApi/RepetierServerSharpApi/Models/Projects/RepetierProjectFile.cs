using AndreasReitberger.Enum;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.Models
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
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

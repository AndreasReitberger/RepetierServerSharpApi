using AndreasReitberger.API.Print3dServer.Core.Interfaces;
using Newtonsoft.Json;
using AndreasReitberger.API.Print3dServer.Core.Events;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    [Obsolete("Use JobListChangedEventArgs instead")]
    public class RepetierJobListChangedEventArgs : Print3dBaseEventArgs
    {
        #region Properties
        public List<IPrint3dJob> NewJobList { get; set; } = [];
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

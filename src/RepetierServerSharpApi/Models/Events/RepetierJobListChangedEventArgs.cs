using AndreasReitberger.API.Print3dServer.Core.Events;
using AndreasReitberger.API.Print3dServer.Core.Interfaces;
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
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierJobListChangedEventArgs);
        #endregion
    }
}

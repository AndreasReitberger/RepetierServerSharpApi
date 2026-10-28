using AndreasReitberger.API.Print3dServer.Core.Events;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    [Obsolete("Use JobsChangedEventArgs instead")]
    public class RepetierJobsChangedEventArgs : Print3dBaseEventArgs
    {
        #region Properties
        public EventJobChangedData? Data { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierJobsChangedEventArgs);
        #endregion
    }
}

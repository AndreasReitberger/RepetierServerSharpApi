using AndreasReitberger.API.Print3dServer.Core.Events;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    [Obsolete("Use JobFinishedEventArgs instead")]
    public class RepetierJobFinishedEventArgs : Print3dBaseEventArgs
    {
        #region Properties
        public EventJobFinishedData? Job { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierJobFinishedEventArgs);
        #endregion
    }
}

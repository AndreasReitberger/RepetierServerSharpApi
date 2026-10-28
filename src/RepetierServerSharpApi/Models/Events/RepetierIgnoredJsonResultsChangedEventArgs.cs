using AndreasReitberger.API.Print3dServer.Core.Events;
using System;
using System.Collections.Concurrent;

namespace AndreasReitberger.API.Repetier.Events
{
    [Obsolete("Use IgnoredJsonResultsChangedEventArgs insead")]
    public class RepetierIgnoredJsonResultsChangedEventArgs : Print3dBaseEventArgs
    {
        #region Properties
        public ConcurrentDictionary<string, string> NewIgnoredJsonResults { get; set; } = new();
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierIgnoredJsonResultsChangedEventArgs);
        #endregion
    }
}

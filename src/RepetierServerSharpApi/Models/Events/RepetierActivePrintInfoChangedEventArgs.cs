using AndreasReitberger.API.Print3dServer.Core.Events;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    [Obsolete("Use JobStatusChangedEventArgs insead")]
    public class RepetierActivePrintInfoChangedEventArgs : Print3dBaseEventArgs
    {
        #region Properties
        public RepetierCurrentPrintInfo? NewActivePrintInfo { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierActivePrintInfoChangedEventArgs);
        #endregion
    }
}

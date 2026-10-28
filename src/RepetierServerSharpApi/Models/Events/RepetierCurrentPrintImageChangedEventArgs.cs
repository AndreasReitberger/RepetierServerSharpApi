using AndreasReitberger.API.Print3dServer.Core.Events;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    [Obsolete("Use ActivePrintImageChangedEventArgs instead")]
    public class RepetierCurrentPrintImageChangedEventArgs : Print3dBaseEventArgs
    {
        #region Properties
        public byte[]? NewImage { get; set; }
        public byte[]? PreviousImage { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierCurrentPrintImageChangedEventArgs);   
        #endregion
    }
}

using AndreasReitberger.API.REST.Events;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    [Obsolete("Use SessionChangedEventArgs insead")]
    public class RepetierEventSessionChangedEventArgs : SessionChangedEventArgs
    {
        #region Properties
        public EventSession? Sesson { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierEventSessionChangedEventArgs);

        #endregion
    }
}

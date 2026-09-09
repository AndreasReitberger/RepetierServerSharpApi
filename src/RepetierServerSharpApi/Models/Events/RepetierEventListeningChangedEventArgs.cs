using System;

namespace AndreasReitberger.API.Repetier.Models
{
    [Obsolete("Use ListeningChangedEventArgs instead")]
    public class RepetierEventListeningChangedEventArgs : RepetierEventSessionChangedEventArgs
    {
        #region Properties
        public bool IsListening { get; set; } = false;
        public bool IsListeningToWebSocket { get; set; } = false;
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierEventListeningChangedEventArgs);
        #endregion
    }
}

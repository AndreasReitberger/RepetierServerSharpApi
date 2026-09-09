using AndreasReitberger.API.Print3dServer.Core.Events;

namespace AndreasReitberger.API.Repetier.Models
{
    public class RepetierJobStartedEventArgs : JobStartedEventArgs
    {
        #region Properties
        public new EventJobStartedData? Job { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierJobStartedEventArgs);
        #endregion
    }
}

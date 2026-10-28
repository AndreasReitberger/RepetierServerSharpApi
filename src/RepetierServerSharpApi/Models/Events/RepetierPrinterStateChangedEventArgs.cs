using AndreasReitberger.API.Print3dServer.Core.Events;

namespace AndreasReitberger.API.Repetier.Models
{

    public class RepetierPrinterStateChangedEventArgs : Print3dBaseEventArgs
    {
        #region Properties
        public RepetierPrinterState? NewPrinterState { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierPrinterStateChangedEventArgs);
        #endregion
    }
}

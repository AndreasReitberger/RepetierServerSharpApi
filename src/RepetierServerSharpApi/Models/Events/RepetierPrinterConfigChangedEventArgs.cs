using AndreasReitberger.API.Print3dServer.Core.Events;

namespace AndreasReitberger.API.Repetier.Models
{
    public class RepetierPrinterConfigChangedEventArgs : Print3dBaseEventArgs
    {
        #region Properties
        public RepetierPrinterConfig? NewConfiguration { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierPrinterConfigChangedEventArgs);
        #endregion
    }
}

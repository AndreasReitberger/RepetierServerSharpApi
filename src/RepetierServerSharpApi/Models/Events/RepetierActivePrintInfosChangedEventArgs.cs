using AndreasReitberger.API.Print3dServer.Core.Events;

namespace AndreasReitberger.API.Repetier.Models
{
    //[Obsolete("Use JobStatusChangedEventArgs insead")]
    public class RepetierActivePrintInfosChangedEventArgs : Print3dBaseEventArgs
    {
        #region Properties
        public List<RepetierCurrentPrintInfo> NewActivePrintInfos { get; set; } = [];
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierActivePrintInfosChangedEventArgs);
        #endregion
    }
}

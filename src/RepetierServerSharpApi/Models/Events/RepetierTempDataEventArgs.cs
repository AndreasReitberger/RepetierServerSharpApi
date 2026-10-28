using AndreasReitberger.API.Print3dServer.Core.Events;

namespace AndreasReitberger.API.Repetier.Models
{
    public class RepetierTempDataEventArgs : Print3dBaseEventArgs
    {
        #region Properties
        public EventTempData? TemperatureData { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierTempDataEventArgs);
        #endregion
    }
}

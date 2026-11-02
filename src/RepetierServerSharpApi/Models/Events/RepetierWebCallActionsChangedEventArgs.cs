using AndreasReitberger.API.Print3dServer.Core.Events;

namespace AndreasReitberger.API.Repetier.Models
{
    public class RepetierWebCallActionsChangedEventArgs : Print3dBaseEventArgs
    {
        #region Properties
        public List<RepetierWebCallAction> NewWebCallActions { get; set; } = [];
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierWebCallActionsChangedEventArgs);
        #endregion
    }
}

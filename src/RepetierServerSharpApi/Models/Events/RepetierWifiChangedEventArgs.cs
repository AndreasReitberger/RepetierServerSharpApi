using AndreasReitberger.API.Print3dServer.Core.Events;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierWifiChangedEventArgs : Print3dBaseEventArgs
    {
        #region Properties
        public EventWifiChangedData? Data { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierWifiChangedEventArgs);
        
        #endregion
    }
}

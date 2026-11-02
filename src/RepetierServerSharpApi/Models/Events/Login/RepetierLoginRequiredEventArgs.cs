using AndreasReitberger.API.REST.Events;

namespace AndreasReitberger.API.Repetier.Models
{
    public class RepetierLoginRequiredEventArgs : LoginRequiredEventArgs
    {
        #region Properties
        public RepetierLoginResult? ResultData { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierLoginRequiredEventArgs);

        #endregion
    }
}

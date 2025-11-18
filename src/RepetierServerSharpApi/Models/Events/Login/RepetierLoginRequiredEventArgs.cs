using AndreasReitberger.API.REST.Events;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public class RepetierLoginRequiredEventArgs : LoginRequiredEventArgs
    {
        #region Properties
        public RepetierLoginResult? ResultData { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        #endregion
    }
}

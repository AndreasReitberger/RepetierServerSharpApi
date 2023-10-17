using AndreasReitberger.API.Print3dServer.Core.Events;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public class RepetierLoginRequiredEventArgs : LoginRequiredEventArgs
    {
        #region Properties
        public RepetierLoginResult ResultData { get; set; }
        //public bool LoginSucceeded { get; set; } = false;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        
        #endregion
    }
}

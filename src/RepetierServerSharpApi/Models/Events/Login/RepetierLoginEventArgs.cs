using Newtonsoft.Json;
using AndreasReitberger.API.Print3dServer.Core.Events;

namespace AndreasReitberger.API.Repetier.Models
{
    public class RepetierLoginEventArgs : Print3dBaseEventArgs
    {
        #region Properties
        public RepetierLoginResult? Data { get; set; }
        public bool LoginSucceeded { get; set; } = false;
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

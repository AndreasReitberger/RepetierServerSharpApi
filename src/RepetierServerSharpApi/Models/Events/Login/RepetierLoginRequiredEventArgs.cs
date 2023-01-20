using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public class RepetierLoginRequiredEventArgs : RepetierEventArgs
    {
        #region Properties
        public RepetierLoginResult ResultData { get; set; }
        public bool LoginSucceeded { get; set; } = false;
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

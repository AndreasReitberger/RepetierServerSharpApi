using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public class RepetierApiRequestRespone
    {
        #region Properties
        public string Result { get; set; } = string.Empty;
        public bool IsOnline { get; set; } = false;
        public bool Succeeded { get; set; } = false;
        public bool HasAuthenticationError { get; set; } = false;

        public RepetierRestEventArgs EventArgs { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

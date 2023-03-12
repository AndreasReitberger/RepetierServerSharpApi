using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public class RepetierEventSessionChangedEventArgs : RepetierEventArgs
    {
        #region Properties
        public EventSession Sesson { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

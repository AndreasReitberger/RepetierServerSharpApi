using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public class RepetierHardwareInfoChangedEventArgs : RepetierEventArgs
    {
        #region Properties
        public EventHardwareInfoChangedData Info { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

using Newtonsoft.Json;

namespace AndreasReitberger.Models
{
    public class RepetierActivePrintInfoChangedEventArgs : RepetierEventArgs
    {
        #region Properties
        public RepetierCurrentPrintInfo NewActivePrintInfo { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

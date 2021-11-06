using Newtonsoft.Json;

namespace AndreasReitberger.Models
{
    public class RepetierActivePrinterChangedEventArgs : RepetierEventArgs
    {
        #region Properties
        public RepetierPrinter NewPrinter { get; set; }
        public RepetierPrinter OldPrinter { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

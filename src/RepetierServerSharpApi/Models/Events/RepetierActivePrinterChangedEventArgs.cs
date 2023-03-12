using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
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
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

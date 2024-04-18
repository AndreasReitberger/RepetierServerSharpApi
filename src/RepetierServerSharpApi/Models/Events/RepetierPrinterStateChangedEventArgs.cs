using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{

    public class RepetierPrinterStateChangedEventArgs : RepetierEventArgs
    {
        #region Properties
        public RepetierPrinterState? NewPrinterState { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

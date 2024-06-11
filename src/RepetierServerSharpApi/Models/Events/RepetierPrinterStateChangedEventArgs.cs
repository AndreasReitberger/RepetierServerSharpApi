using Newtonsoft.Json;
using AndreasReitberger.API.Print3dServer.Core.Events;

namespace AndreasReitberger.API.Repetier.Models
{

    public class RepetierPrinterStateChangedEventArgs : Print3dBaseEventArgs
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

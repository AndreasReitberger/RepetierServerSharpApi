using AndreasReitberger.API.Print3dServer.Core.Events;
using AndreasReitberger.API.Print3dServer.Core.Interfaces;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public class RepetierActivePrinterChangedEventArgs : Print3dBaseEventArgs
    {
        #region Properties
        public IPrinter3d? NewPrinter { get; set; }
        public IPrinter3d? OldPrinter { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

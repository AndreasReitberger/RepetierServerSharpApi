using AndreasReitberger.API.Print3dServer.Core.Events;
using AndreasReitberger.API.Print3dServer.Core.Interfaces;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    [Obsolete("Use ActivePrinterChangedEventArgs instead")]
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

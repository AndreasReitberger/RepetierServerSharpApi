using AndreasReitberger.API.Print3dServer.Core.Events;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    [Obsolete("Use JobStatusChangedEventArgs insead")]
    public class RepetierActivePrintInfoChangedEventArgs : Print3dBaseEventArgs
    {
        #region Properties
        public RepetierCurrentPrintInfo? NewActivePrintInfo { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

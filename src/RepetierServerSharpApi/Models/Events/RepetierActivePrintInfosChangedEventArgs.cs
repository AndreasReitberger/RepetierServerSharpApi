using Newtonsoft.Json;
using AndreasReitberger.API.Print3dServer.Core.Events;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    //[Obsolete("Use JobStatusChangedEventArgs insead")]
    public class RepetierActivePrintInfosChangedEventArgs : Print3dBaseEventArgs
    {
        #region Properties
        public List<RepetierCurrentPrintInfo> NewActivePrintInfos { get; set; } = [];
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

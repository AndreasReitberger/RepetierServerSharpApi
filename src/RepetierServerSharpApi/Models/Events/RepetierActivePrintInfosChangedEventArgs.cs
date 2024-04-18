using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.API.Repetier.Models
{
    public class RepetierActivePrintInfosChangedEventArgs : RepetierEventArgs
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

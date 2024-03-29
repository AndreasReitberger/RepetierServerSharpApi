﻿using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public class RepetierActivePrintInfoChangedEventArgs : RepetierEventArgs
    {
        #region Properties
        public RepetierCurrentPrintInfo NewActivePrintInfo { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

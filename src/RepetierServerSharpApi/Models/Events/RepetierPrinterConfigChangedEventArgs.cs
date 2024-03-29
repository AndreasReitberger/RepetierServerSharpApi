﻿using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public class RepetierPrinterConfigChangedEventArgs : RepetierEventArgs
    {
        #region Properties
        public RepetierPrinterConfig NewConfiguration { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

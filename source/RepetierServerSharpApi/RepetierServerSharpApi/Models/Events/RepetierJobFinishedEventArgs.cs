﻿using Newtonsoft.Json;

namespace AndreasReitberger.Models
{
    public class RepetierJobFinishedEventArgs : RepetierEventArgs
    {
        #region Properties
        public EventJobFinishedData Job { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

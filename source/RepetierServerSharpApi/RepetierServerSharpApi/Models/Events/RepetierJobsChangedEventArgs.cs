﻿using Newtonsoft.Json;

namespace AndreasReitberger.Models
{
    public class RepetierJobsChangedEventArgs : RepetierEventArgs
    {
        #region Properties
        public EventJobChangedData Data { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

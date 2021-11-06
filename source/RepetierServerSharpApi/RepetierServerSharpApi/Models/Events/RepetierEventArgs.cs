using Newtonsoft.Json;
using System;

namespace AndreasReitberger.Models
{
    public class RepetierEventArgs : EventArgs
    {
        #region Properties
        public string Message { get; set; }
        public string Printer { get; set; }
        public long CallbackId { get; set; }
        public string SessonId { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

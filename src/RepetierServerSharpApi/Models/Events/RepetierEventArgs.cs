using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public class RepetierEventArgs : EventArgs
    {
        #region Properties
        public string Message { get; set; } = string.Empty;
        public string Printer { get; set; } = string.Empty;
        public long CallbackId { get; set; }
        public string SessonId { get; set; } = string.Empty;
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

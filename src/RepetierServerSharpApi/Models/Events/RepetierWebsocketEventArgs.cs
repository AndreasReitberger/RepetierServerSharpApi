using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public class RepetierWebsocketEventArgs : EventArgs
    {
        #region Properties
        public string Message { get; set; }
        public byte[] Data { get; set; }
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

using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    [Obsolete("Replace with JsonConvertEventArgs")]
    internal class RepetierJsonConvertEventArgs : EventArgs
    {
        #region Properties
        public string Message { get; set; } = string.Empty;
        public string OriginalString { get; set; } = string.Empty;
        public string TargetType { get; set; } = string.Empty;
        public Exception? Exception { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

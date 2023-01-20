using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public class RepetierRestEventArgs : EventArgs
    {
        #region Properties
        public string Message { get; set; }
        public string Status { get; set; }
        public Uri Uri { get; set; }
        public Exception Exception { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return string.Format("{0} ({1}) - Target: {2}", Message, Status, Uri);
        }
        #endregion
    }
}

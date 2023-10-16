using AndreasReitberger.API.Print3dServer.Core.Interfaces;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    [Obsolete("Replace with RestEventArgs")]
    public class RepetierRestEventArgs : EventArgs, IRestEventArgs
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

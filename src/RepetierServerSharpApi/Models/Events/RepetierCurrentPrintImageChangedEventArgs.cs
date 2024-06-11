using AndreasReitberger.API.Print3dServer.Core.Events;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    [Obsolete("Use ActivePrintImageChangedEventArgs instead")]
    public class RepetierCurrentPrintImageChangedEventArgs : Print3dBaseEventArgs
    {
        #region Properties
        public byte[]? NewImage { get; set; }
        public byte[]? PreviousImage { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

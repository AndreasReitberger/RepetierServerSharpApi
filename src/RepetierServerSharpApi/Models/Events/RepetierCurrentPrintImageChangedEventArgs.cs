using AndreasReitberger.API.Print3dServer.Core.Events;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
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

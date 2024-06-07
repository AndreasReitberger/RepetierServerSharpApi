using Newtonsoft.Json;
using AndreasReitberger.API.Print3dServer.Core.Events;

namespace AndreasReitberger.API.Repetier.Models
{
    public class RepetierHardwareInfoChangedEventArgs : Print3dBaseEventArgs
    {
        #region Properties
        public EventHardwareInfoChangedData? Info { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

using AndreasReitberger.API.Print3dServer.Core.Events;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public class RepetierEventSessionChangedEventArgs : SessionChangedEventArgs
    {
        #region Properties
        public EventSession Sesson { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        
        #endregion
    }
}

using Newtonsoft.Json;
using AndreasReitberger.API.Print3dServer.Core.Events;

namespace AndreasReitberger.API.Repetier.Models
{
    public class RepetierMessagesChangedEventArgs : Print3dBaseEventArgs
    {
        #region Properties
        public EventMessageChangedData? RepetierMessage { get; set; }
        public List<RepetierMessage> RepetierMessages { get; set; } = [];
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

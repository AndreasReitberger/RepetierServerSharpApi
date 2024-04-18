using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace AndreasReitberger.API.Repetier.Models
{
    public class RepetierMessagesChangedEventArgs : RepetierEventArgs
    {
        #region Properties
        public EventMessageChangedData? RepetierMessage { get; set; }
        public ObservableCollection<RepetierMessage> RepetierMessages { get; set; } = [];
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

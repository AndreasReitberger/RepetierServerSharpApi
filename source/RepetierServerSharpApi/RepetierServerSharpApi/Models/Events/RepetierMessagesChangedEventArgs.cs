using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace AndreasReitberger.Models
{
    public class RepetierMessagesChangedEventArgs : RepetierEventArgs
    {
        #region Properties
        public EventMessageChangedData RepetierMessage { get; set; }
        public ObservableCollection<RepetierMessage> RepetierMessages { get; set; } = new ObservableCollection<RepetierMessage>();
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

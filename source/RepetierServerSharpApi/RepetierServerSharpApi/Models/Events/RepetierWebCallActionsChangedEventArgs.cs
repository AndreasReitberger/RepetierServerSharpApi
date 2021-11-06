using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace AndreasReitberger.Models
{
    public class RepetierWebCallActionsChangedEventArgs : RepetierEventArgs
    {
        #region Properties
        public ObservableCollection<RepetierWebCallAction> NewWebCallActions { get; set; } = new();
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

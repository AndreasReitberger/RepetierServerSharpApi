using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace AndreasReitberger.API.Repetier.Models
{
    public class RepetierWebCallActionsChangedEventArgs : RepetierEventArgs
    {
        #region Properties
        public ObservableCollection<RepetierWebCallAction> NewWebCallActions { get; set; } = new();
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

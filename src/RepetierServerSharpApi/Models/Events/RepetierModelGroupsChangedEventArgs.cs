using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace AndreasReitberger.API.Repetier.Models
{
    public class RepetierModelGroupsChangedEventArgs : RepetierEventArgs
    {
        #region Properties
        public ObservableCollection<string> NewModelGroups { get; set; } = new();
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

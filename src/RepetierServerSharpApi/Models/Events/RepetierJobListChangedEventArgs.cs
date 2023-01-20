using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace AndreasReitberger.API.Repetier.Models
{
    public class RepetierJobListChangedEventArgs : RepetierEventArgs
    {
        #region Properties
        public ObservableCollection<RepetierJobListItem> NewJobList { get; set; } = new();
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

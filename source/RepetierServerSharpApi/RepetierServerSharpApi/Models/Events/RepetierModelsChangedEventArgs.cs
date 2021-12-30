using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace AndreasReitberger.Models
{
    public class RepetierModelsChangedEventArgs : RepetierEventArgs
    {
        #region Properties
        public ObservableCollection<RepetierModel> NewModels { get; set; } = new();
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

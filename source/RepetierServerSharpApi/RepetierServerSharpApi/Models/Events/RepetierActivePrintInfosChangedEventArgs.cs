using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace AndreasReitberger.Models
{
    public class RepetierActivePrintInfosChangedEventArgs : RepetierEventArgs
    {
        #region Properties
        public ObservableCollection<RepetierCurrentPrintInfo> NewActivePrintInfos { get; set; } = new ObservableCollection<RepetierCurrentPrintInfo>();
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

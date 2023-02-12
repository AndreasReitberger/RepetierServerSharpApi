using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace AndreasReitberger.API.Repetier.Models
{
    public class RepetierActivePrintInfosChangedEventArgs : RepetierEventArgs
    {
        #region Properties
        public ObservableCollection<RepetierCurrentPrintInfo> NewActivePrintInfos { get; set; } = new ObservableCollection<RepetierCurrentPrintInfo>();
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

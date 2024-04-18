using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public class RepetierWebCallActionsChangedEventArgs : RepetierEventArgs
    {
        #region Properties
        public List<RepetierWebCallAction> NewWebCallActions { get; set; } = [];
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

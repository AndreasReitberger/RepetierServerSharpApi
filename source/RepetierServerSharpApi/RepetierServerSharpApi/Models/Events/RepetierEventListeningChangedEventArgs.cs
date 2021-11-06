using Newtonsoft.Json;

namespace AndreasReitberger.Models
{
    public class RepetierEventListeningChangedEventArgs : RepetierEventSessionChangedEventArgs
    {
        #region Properties
        public bool IsListening { get; set; } = false;
        public bool IsListeningToWebSocket { get; set; } = false;
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

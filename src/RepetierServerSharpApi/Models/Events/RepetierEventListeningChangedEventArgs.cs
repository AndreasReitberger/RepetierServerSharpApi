using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    [Obsolete("Use ListeningChangedEventArgs instead")]
    public class RepetierEventListeningChangedEventArgs : RepetierEventSessionChangedEventArgs
    {
        #region Properties
        public bool IsListening { get; set; } = false;
        public bool IsListeningToWebSocket { get; set; } = false;
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

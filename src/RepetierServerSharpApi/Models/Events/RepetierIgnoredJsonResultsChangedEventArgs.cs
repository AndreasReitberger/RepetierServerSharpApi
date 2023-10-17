using AndreasReitberger.API.Repetier.Models;
using Newtonsoft.Json;
using System.Collections.Concurrent;

namespace AndreasReitberger.API.Repetier.Events
{
    public class RepetierIgnoredJsonResultsChangedEventArgs : RepetierEventArgs
    {
        #region Properties
        public ConcurrentDictionary<string, string> NewIgnoredJsonResults { get; set; } = new();
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

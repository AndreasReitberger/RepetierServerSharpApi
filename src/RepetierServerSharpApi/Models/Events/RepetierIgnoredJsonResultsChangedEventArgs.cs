using Newtonsoft.Json;
using System.Collections.Concurrent;
using AndreasReitberger.API.Print3dServer.Core.Events;

namespace AndreasReitberger.API.Repetier.Events
{
    public class RepetierIgnoredJsonResultsChangedEventArgs : Print3dBaseEventArgs
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

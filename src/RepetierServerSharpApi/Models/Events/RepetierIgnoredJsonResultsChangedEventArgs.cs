using AndreasReitberger.API.Repetier.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AndreasReitberger.API.Repetier.Events
{
    public class RepetierIgnoredJsonResultsChangedEventArgs : RepetierEventArgs
    {
        #region Properties
        public Dictionary<string, string> NewIgnoredJsonResults { get; set; } = new();
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

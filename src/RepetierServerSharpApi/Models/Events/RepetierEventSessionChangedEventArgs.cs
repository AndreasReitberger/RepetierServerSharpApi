using AndreasReitberger.API.Print3dServer.Core.Events;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    [Obsolete("Use SessionChangedEventArgs insead")]
    public class RepetierEventSessionChangedEventArgs : SessionChangedEventArgs
    {
        #region Properties
        public EventSession? Sesson { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        #endregion
    }
}

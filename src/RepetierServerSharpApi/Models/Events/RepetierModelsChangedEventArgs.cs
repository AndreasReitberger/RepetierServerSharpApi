using AndreasReitberger.API.Print3dServer.Core.Events;
using AndreasReitberger.API.Print3dServer.Core.Interfaces;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{

    [Obsolete("Use GcodesChangedEventArgs instead")]
    public class RepetierModelsChangedEventArgs : Print3dBaseEventArgs
    {
        #region Properties
        public List<IGcode> NewModels { get; set; } = [];
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

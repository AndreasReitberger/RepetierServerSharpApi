using AndreasReitberger.API.Print3dServer.Core.Interfaces;
using Newtonsoft.Json;
using AndreasReitberger.API.Print3dServer.Core.Events;

namespace AndreasReitberger.API.Repetier.Models
{
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

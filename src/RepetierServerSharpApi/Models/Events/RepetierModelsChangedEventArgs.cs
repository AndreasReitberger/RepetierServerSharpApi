using AndreasReitberger.API.Print3dServer.Core.Interfaces;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public class RepetierModelsChangedEventArgs : RepetierEventArgs
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

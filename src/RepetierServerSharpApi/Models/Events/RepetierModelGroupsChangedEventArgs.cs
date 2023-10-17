using AndreasReitberger.API.Print3dServer.Core.Interfaces;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace AndreasReitberger.API.Repetier.Models
{
    public class RepetierModelGroupsChangedEventArgs : RepetierEventArgs
    {
        #region Properties
        public ObservableCollection<IGcodeGroup> NewModelGroups { get; set; } = new();
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

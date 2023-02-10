using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class GcodeCommandInfo : ObservableObject//, ICloneable
    {
        #region Id
        [ObservableProperty]
        Guid id;
        #endregion

        #region Properties
        [ObservableProperty]
        string command = string.Empty;


        [ObservableProperty]
        bool sent = false;

        [ObservableProperty]
        bool succeeded = false;

        [ObservableProperty]
        DateTime timeStamp = DateTime.Now;

        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

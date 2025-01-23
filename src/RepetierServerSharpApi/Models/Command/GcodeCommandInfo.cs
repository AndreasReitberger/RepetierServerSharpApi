using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class GcodeCommandInfo : ObservableObject//, ICloneable
    {
        #region Id
        [ObservableProperty]
        public partial Guid Id { get; set; }
        #endregion

        #region Properties
        [ObservableProperty]
        public partial string Command { get; set; } = string.Empty;

        [ObservableProperty]
        public partial bool Sent { get; set; } = false;

        [ObservableProperty]
        public partial bool Succeeded { get; set; } = false;

        [ObservableProperty]
        public partial DateTime TimeStamp { get; set; } = DateTime.Now;

        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

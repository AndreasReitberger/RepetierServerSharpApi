using AndreasReitberger.API.Print3dServer.Core.Events;
using AndreasReitberger.API.Print3dServer.Core.Interfaces;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    [Obsolete("Use GcodeGroupsChangedEventArgs instead")]
    public class RepetierModelGroupsChangedEventArgs : Print3dBaseEventArgs
    {
        #region Properties
        public List<IGcodeGroup> NewModelGroups { get; set; } = [];
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierModelGroupsChangedEventArgs);
        #endregion
    }
}

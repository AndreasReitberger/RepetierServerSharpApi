using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class ProjectsServer : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("name")]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("uuid")]
        public partial Guid Uuid { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.ProjectsServer);
        #endregion
    }
}

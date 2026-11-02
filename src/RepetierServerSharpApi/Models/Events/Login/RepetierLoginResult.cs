using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierLoginResult : ObservableObject//, IPrint3dLoginData
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("login")]
        public partial string Login { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("permissions")]
        public partial long? Permissions { get; set; }

        [ObservableProperty]
        [JsonPropertyName("serverUUID")]
        public partial Guid ServerUUID { get; set; }

        [ObservableProperty]
        [JsonPropertyName("settings")]
        public partial RepetierLoginResultSettings? Settings { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierLoginResult);
        #endregion
    }
}

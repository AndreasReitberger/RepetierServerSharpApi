namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventNetworkInfo : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonPropertyName("data")]
        public partial EventNetworkInfoData? Data { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.EventNetworkInfo);
        #endregion
    }
}

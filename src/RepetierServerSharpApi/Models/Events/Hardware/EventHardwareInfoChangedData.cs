namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventHardwareInfoChangedData : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonPropertyName("list")]
        public partial List<HardwareInfo> List { get; set; } = new();

        [ObservableProperty]
        [JsonPropertyName("maxUrgency")]
        public partial long? MaxUrgency { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.EventHardwareInfoChangedData);
        #endregion
    }
}

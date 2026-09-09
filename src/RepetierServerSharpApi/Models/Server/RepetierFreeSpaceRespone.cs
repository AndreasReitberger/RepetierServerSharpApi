namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierFreeSpaceRespone : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("available")]
        public partial long Available { get; set; }

        [ObservableProperty]
        [JsonPropertyName("capacity")]
        public partial long Capacity { get; set; }

        [ObservableProperty]
        [JsonPropertyName("free")]
        public partial long Free { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this, RepetierSourceGenerationContext.Default.RepetierFreeSpaceRespone);
        #endregion
    }
}

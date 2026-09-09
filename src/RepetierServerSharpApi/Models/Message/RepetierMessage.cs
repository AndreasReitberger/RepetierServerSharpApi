namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierMessage : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonPropertyName("id")]
        public partial long Id { get; set; }

        [ObservableProperty]
        [JsonPropertyName("msg")]
        public partial string Msg { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("link")]
        public partial string Link { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("slug")]
        public partial string Slug { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("date")]
        public partial string Date { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("pause")]
        public partial bool Pause { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierMessage);

        public override bool Equals(object? obj)
        {
            if (obj is not RepetierMessage item)
                return false;
            return Id.Equals(item.Id);
        }
        public override int GetHashCode() => Id.GetHashCode();
        
        #endregion
    }
}

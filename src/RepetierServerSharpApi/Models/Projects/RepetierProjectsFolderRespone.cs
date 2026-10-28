namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierProjectsFolderRespone : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonPropertyName("folder")]
        public partial RepetierProjectFolder? Folder { get; set; }

        [ObservableProperty]
        [JsonPropertyName("ok")]
        public partial bool Ok { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierProjectsFolderRespone);
        #endregion
    }
}

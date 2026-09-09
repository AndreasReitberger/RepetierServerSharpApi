namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterStateGlobal : ObservableObject
    {
        #region Properties
        /*
        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("on")]
        bool on;
        */
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierPrinterStateGlobal);
        #endregion
    }
}

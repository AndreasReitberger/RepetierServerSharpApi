using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierMessage : ObservableObject
    {
        #region Properties

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("id")]
        long id;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("msg")]
        string msg = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("link")]
        string link = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("slug")]
        string slug = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("date")]
        string date = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("pause")]
        bool pause;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        public override bool Equals(object? obj)
        {
            if (obj is not RepetierMessage item)
                return false;
            return Id.Equals(item.Id);
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        #endregion
    }
}

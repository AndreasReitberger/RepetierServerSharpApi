using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierMessage : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonProperty("id")]
        [property: JsonIgnore]
        long id;

        [ObservableProperty]
        [JsonProperty("msg")]
        [property: JsonIgnore]
        string msg;

        [ObservableProperty]
        [JsonProperty("link")]
        [property: JsonIgnore]
        string link;

        [ObservableProperty]
        [JsonProperty("slug")]
        [property: JsonIgnore]
        string slug;

        [ObservableProperty]
        [JsonProperty("date")]
        [property: JsonIgnore]
        string date;

        [ObservableProperty]
        [JsonProperty("pause")]
        [property: JsonIgnore]
        bool pause;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        public override bool Equals(object obj)
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

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

        string msg;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("link")]

        string link;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("slug")]

        string slug;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("date")]

        string date;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("pause")]

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

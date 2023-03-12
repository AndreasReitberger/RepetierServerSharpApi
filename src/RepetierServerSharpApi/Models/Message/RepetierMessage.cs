using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierMessage : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonProperty("id")]
        long id;

        [ObservableProperty]
        [JsonProperty("msg")]
        string msg;

        [ObservableProperty]
        [JsonProperty("link")]
        string link;

        [ObservableProperty]
        [JsonProperty("slug")]
        string slug;

        [ObservableProperty]
        [JsonProperty("date")]
        string date;

        [ObservableProperty]
        [JsonProperty("pause")]
        bool pause;
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        public override bool Equals(object obj)
        {
            if (obj is not RepetierMessage item)
                return false;
            return this.Id.Equals(item.Id);
        }
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
        #endregion
    }
}

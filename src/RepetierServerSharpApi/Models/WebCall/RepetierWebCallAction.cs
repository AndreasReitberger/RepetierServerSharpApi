using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierWebCallAction : ObservableObject
    {
        #region Properties

        [ObservableProperty]
        [JsonProperty("content_type")]
        [property: JsonIgnore]
        string contentType;

        [ObservableProperty]
        [JsonProperty("icon")]
        [property: JsonIgnore]
        string icon;

        [ObservableProperty]
        [JsonProperty("method")]
        [property: JsonIgnore]
        string method;

        [ObservableProperty]
        [JsonProperty("name")]
        [property: JsonIgnore]
        string name;

        [ObservableProperty]
        [JsonProperty("pos")]
        [property: JsonIgnore]
        long pos;

        [ObservableProperty]
        [JsonProperty("post")]
        [property: JsonIgnore]
        string post;

        [ObservableProperty]
        [JsonProperty("question")]
        [property: JsonIgnore]
        string question;

        [ObservableProperty]
        [JsonProperty("show_in_menu")]
        [property: JsonIgnore]
        bool showInMenu;

        [ObservableProperty]
        [JsonProperty("show_name")]
        [property: JsonIgnore]
        string showName;

        [ObservableProperty]
        [JsonProperty("slug")]
        [property: JsonIgnore]
        string slug;

        [ObservableProperty]
        [JsonProperty("url")]
        [property: JsonIgnore]
        Uri url;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

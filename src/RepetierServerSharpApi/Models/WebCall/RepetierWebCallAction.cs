using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierWebCallAction : ObservableObject
    {
        #region Properties

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("content_type")]

        string contentType = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("icon")]

        string icon = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("method")]

        string method = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("name")]

        string name = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("pos")]

        long pos;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("post")]

        string post = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("question")]

        string question = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("show_in_menu")]

        bool showInMenu;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("show_name")]

        string showName = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("slug")]

        string slug = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("url")]

        Uri? url;
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

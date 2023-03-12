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
        string contentType;

        [ObservableProperty]
        [JsonProperty("icon")]
        string icon;

        [ObservableProperty]
        [JsonProperty("method")]
        string method;

        [ObservableProperty]
        [JsonProperty("name")]
        string name;

        [ObservableProperty]
        [JsonProperty("pos")]
        long pos;

        [ObservableProperty]
        [JsonProperty("post")]
        string post;

        [ObservableProperty]
        [JsonProperty("question")]
        string question;

        [ObservableProperty]
        [JsonProperty("show_in_menu")]
        bool showInMenu;

        [ObservableProperty]
        [JsonProperty("show_name")]
        string showName;

        [ObservableProperty]
        [JsonProperty("slug")]
        string slug;

        [ObservableProperty]
        [JsonProperty("url")]
        Uri url;
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
            /*
            return $"{Name} - {Slug}\n" +
                $"{{\n" +
                $"ContentType: {ContentType}\n" +
                $"Icon: {Icon}\n" +
                $"Method: {Method}\n" +
                $"Pos: {Pos}\n" +
                $"Post: {Post}\n" +
                $"Question: {Question}\n" +
                $"ShowInMenu: {ShowInMenu}\n" +
                $"ShowName: {ShowName}\n" +
                $"Url: {Url}" +
                $"}}"
                ;
            */
        }
        #endregion
    }
}

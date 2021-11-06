using Newtonsoft.Json;
using System;

namespace AndreasReitberger.Models
{
    public partial class RepetierWebCallAction
    {
        #region Properties

        [JsonProperty("content_type")]
        public string ContentType { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("pos")]
        public long Pos { get; set; }

        [JsonProperty("post")]
        public string Post { get; set; }

        [JsonProperty("question")]
        public string Question { get; set; }

        [JsonProperty("show_in_menu")]
        public bool ShowInMenu { get; set; }

        [JsonProperty("show_name")]
        public string ShowName { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
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

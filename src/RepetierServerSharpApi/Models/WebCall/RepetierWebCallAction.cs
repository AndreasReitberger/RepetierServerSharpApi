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
        public partial string ContentType { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("icon")]
        public partial string Icon { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("method")]
        public partial string Method { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("name")]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("pos")]
        public partial long Pos { get; set; }

        [ObservableProperty]
        
        [JsonProperty("post")]
        public partial string Post { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("question")]
        public partial string Question { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("show_in_menu")]
        public partial bool ShowInMenu { get; set; }

        [ObservableProperty]
        
        [JsonProperty("show_name")]
        public partial string ShowName { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("slug")]
        public partial string Slug { get; set; } = string.Empty;

        [ObservableProperty]
        
        [JsonProperty("url")]
        public partial Uri? Url { get; set; }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}

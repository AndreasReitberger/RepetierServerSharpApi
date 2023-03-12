using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinterConnectionIp : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        [JsonProperty("address")]
        string address;

        [ObservableProperty]
        [JsonProperty("keepAliveInterval")]
        long keepAliveInterval;

        [ObservableProperty]
        [JsonProperty("port")]
        long port;
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }

}

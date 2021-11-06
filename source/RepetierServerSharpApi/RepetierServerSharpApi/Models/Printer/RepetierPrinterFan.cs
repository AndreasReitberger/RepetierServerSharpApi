using Newtonsoft.Json;
using System;

namespace AndreasReitberger.Models
{
    public partial class RepetierPrinterFan
    {
        #region Properties
        [JsonProperty("on")]
        public bool On { get; set; }

        [JsonProperty("voltage")]
        public long Voltage { get; set; }

        [JsonIgnore]
        public int Speed
        {
            get
            {
                return Convert.ToInt32(Math.Round((double)(this.Voltage / 255m * 100m), 0));
            }
        }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
    }
}

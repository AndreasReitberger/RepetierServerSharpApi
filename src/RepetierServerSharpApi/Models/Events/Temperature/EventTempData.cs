using AndreasReitberger.API.Print3dServer.Core.Interfaces;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventTempData :  ObservableObject, IPrint3dTemperatureInfo
    {
        #region Properties
        [ObservableProperty]
        
        [JsonProperty("O")]
        public partial long O { get; set; }

        partial void OnOChanged(long value)
        {
            TemperatureOffset = value;
        }

        [ObservableProperty]
        
        [JsonProperty("S")]
        public partial long S { get; set; }

        partial void OnSChanged(long value)
        {
            TemperatureSet = value;
        }

        [ObservableProperty]
        
        [JsonProperty("T")]
        public partial double T { get; set; }

        partial void OnTChanged(double value)
        {
            TemperatureTarget = value;
        }

        [ObservableProperty]
        
        [JsonProperty("id")]
        public partial long EventId { get; set; }

        [ObservableProperty]
        
        [JsonProperty("t")]
        public partial long DataT { get; set; }

        [ObservableProperty]
        
        [JsonIgnore]
        public partial Guid Id { get; set; }

        [ObservableProperty]
        
        public partial double? TemperatureOffset { get; set; }

        [ObservableProperty]
        
        public partial double? TemperatureSet { get; set; }

        [ObservableProperty]
        
        public partial double? TemperatureTarget { get; set; }

        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion        
        
        #region Dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected void Dispose(bool disposing)
        {
            // Ordinarily, we release unmanaged resources here;
            // but all are wrapped by safe handles.

            // Release disposable objects.
            if (disposing)
            {
                // Nothing to do here
            }
        }
        #endregion

        #region Clone

        public object Clone() => MemberwiseClone();

        #endregion
    }
}

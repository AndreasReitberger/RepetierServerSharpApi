using AndreasReitberger.API.Print3dServer.Core.Interfaces;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class EventTempData :  ObservableObject, IPrint3dTemperatureInfo
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("O")]
        long o;
        partial void OnOChanged(long value)
        {
            TemperatureOffset = value;
        }

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("S")]
        long s;
        partial void OnSChanged(long value)
        {
            TemperatureSet = value;
        }

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("T")]
        double t;
        partial void OnTChanged(double value)
        {
            TemperatureTarget = value;
        }

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("id")]
        long eventId;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("t")]
        long dataT;

        [ObservableProperty, JsonIgnore]
        [property: JsonIgnore]
        Guid id;

        [ObservableProperty, JsonIgnore]
        double? temperatureOffset;

        [ObservableProperty, JsonIgnore]
        double? temperatureSet;

        [ObservableProperty, JsonIgnore]
        double? temperatureTarget;

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

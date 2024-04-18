using AndreasReitberger.API.Print3dServer.Core.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinter : ObservableObject, IPrinter3d
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        Guid id;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("active")]
        bool isActive;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("analysed")]
        int? analysed;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("done")]
        double? done;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("job")]
        string activeJobName = string.Empty;

        [ObservableProperty, JsonIgnore]
        [NotifyPropertyChangedFor(nameof(ActiveJobId))]
        [property: JsonProperty("jobid")]
        int jobId = -1;
        partial void OnJobIdChanged(int value)
        {
            ActiveJobId = value.ToString();
        }

        [ObservableProperty, JsonIgnore]
        string activeJobId = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("jobstate")]
        string? activeJobState;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("linesSend")]
        long? lineSent;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("name")]
        string name = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("ofLayer")]
        long? layers;

        [ObservableProperty, JsonIgnore]
        [NotifyPropertyChangedFor(nameof(IsOnline))]
        [property: JsonProperty("online")]
        long online;
        partial void OnOnlineChanged(long value)
        {
            IsOnline = value > 0;
        }

        [ObservableProperty, JsonIgnore]
        bool isOnline = false;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("pauseState")]
        long? pauseState;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("paused")]
        bool paused;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("printStart")]
        double? printStart;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("printTime")]
        double? printTime;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("printedTimeComp")]
        double? printedTimeComp;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("repeat")]
        int? repeat;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("slug")]
        string slug = string.Empty;

        [ObservableProperty, JsonIgnore]
        [NotifyPropertyChangedFor(nameof(PrintStarted))]
        [property: JsonProperty("start")]
        long? start;
        partial void OnStartChanged(long? value)
        {
            PrintStarted = value;
        }

        [ObservableProperty, JsonIgnore]
        double? printStarted = 0;

        [ObservableProperty, JsonIgnore]
        double? printDuration = 0;

        [ObservableProperty, JsonIgnore]
        double? printDurationEstimated = 0;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("totalLines")]
        long? totalLines;

        #region JsonIgnored

        [ObservableProperty, JsonIgnore]
        double? extruder1Temperature = 0;

        [ObservableProperty, JsonIgnore]
        double? extruder2Temperature = 0;

        [ObservableProperty, JsonIgnore]
        double? extruder3Temperature = 0;

        [ObservableProperty, JsonIgnore]
        double? extruder4Temperature = 0;

        [ObservableProperty, JsonIgnore]
        double? extruder5Temperature = 0;

        [ObservableProperty, JsonIgnore]
        double? heatedBedTemperature = 0;

        [ObservableProperty, JsonIgnore]
        double? heatedChamberTemperature = 0;

        [ObservableProperty, JsonIgnore]
        double? printProgress = 0;

        [ObservableProperty, JsonIgnore]
        double? remainingPrintDuration = 0;

        [ObservableProperty, JsonIgnore]
        bool isPrinting = false;

        [ObservableProperty, JsonIgnore]
        bool isPaused = false;

        [ObservableProperty, JsonIgnore]
        bool isSelected = false;

        [ObservableProperty, JsonIgnore]
        byte[] currentPrintImage = [];

        #endregion

        #endregion

        #region Methods

        public Task<bool> HomeAsync(IPrint3dServerClient client, bool x, bool y, bool z) => client.HomeAsync(x, y, z);

        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        public override bool Equals(object? obj)
        {
            if (obj is not RepetierPrinter item)
                return false;
            return Slug.Equals(item.Slug);
        }

        public override int GetHashCode()
        {
            return Slug.GetHashCode();
        }

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

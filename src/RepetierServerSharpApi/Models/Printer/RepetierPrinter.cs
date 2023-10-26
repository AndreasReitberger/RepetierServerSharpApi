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
        [property: JsonIgnore]
        Guid id;

        [ObservableProperty]
        [JsonProperty("active")]
        [property: JsonIgnore]
        bool isActive;

        [ObservableProperty]
        [JsonProperty("analysed")]
        [property: JsonIgnore]
        int? analysed;

        [ObservableProperty]
        [JsonProperty("done")]
        [property: JsonIgnore]
        double? done;

        [ObservableProperty]
        [JsonProperty("job")]
        [property: JsonIgnore]
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
        [property: JsonIgnore]
        string activeJobId;

        [ObservableProperty]
        [JsonProperty("jobstate")]
        [property: JsonIgnore]
        string? activeJobState;
        
        [ObservableProperty]
        [JsonProperty("linesSend")]
        [property: JsonIgnore]
        long? lineSent;

        [ObservableProperty]
        [JsonProperty("name")]
        [property: JsonIgnore]
        string name = string.Empty;
        
        [ObservableProperty]
        [JsonProperty("ofLayer")]
        [property: JsonIgnore]
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
        [property: JsonIgnore]
        bool isOnline = false;

        [ObservableProperty]
        [JsonProperty("pauseState")]
        [property: JsonIgnore]
        long? pauseState;

        [ObservableProperty]
        [JsonProperty("paused")]
        [property: JsonIgnore]
        bool paused;

        [ObservableProperty]
        [JsonProperty("printStart")]
        [property: JsonIgnore]
        double? printStart;

        [ObservableProperty]
        [JsonProperty("printTime")]
        [property: JsonIgnore]
        double? printTime;

        [ObservableProperty]
        [JsonProperty("printedTimeComp")]
        [property: JsonIgnore]
        double? printedTimeComp;

        [ObservableProperty]
        [JsonProperty("repeat")]
        [property: JsonIgnore]
        int? repeat;

        [ObservableProperty]
        [JsonProperty("slug")]
        [property: JsonIgnore]
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
        [property: JsonIgnore]
        double? printStarted = 0;

        [ObservableProperty, JsonIgnore]
        [property: JsonIgnore]
        double? printDuration = 0;

        [ObservableProperty, JsonIgnore]
        [property: JsonIgnore]
        double? printDurationEstimated = 0;

        [ObservableProperty]
        [JsonProperty("totalLines")]
        long? totalLines;

        #region JsonIgnored

        [ObservableProperty]
        [property: JsonIgnore]
        [JsonIgnore]
        double? extruder1Temperature = 0;

        [ObservableProperty]
        [property: JsonIgnore]
        [JsonIgnore]
        double? extruder2Temperature = 0;

        [ObservableProperty]
        [property: JsonIgnore]
        [JsonIgnore]
        double? extruder3Temperature = 0;

        [ObservableProperty]
        [property: JsonIgnore]
        [JsonIgnore]
        double? extruder4Temperature = 0;

        [ObservableProperty]
        [property: JsonIgnore]
        [JsonIgnore]
        double? extruder5Temperature = 0;

        [ObservableProperty]
        [property: JsonIgnore]
        [JsonIgnore]
        double? heatedBedTemperature = 0;

        [ObservableProperty]
        [property: JsonIgnore]
        [JsonIgnore]
        double? heatedChamberTemperature = 0;

        [ObservableProperty]
        [property: JsonIgnore]
        [JsonIgnore]
        double? printProgress = 0;

        [ObservableProperty]
        [property: JsonIgnore]
        [JsonIgnore]
        double? remainingPrintDuration = 0;

        [ObservableProperty]
        [property: JsonIgnore]
        [JsonIgnore]
        bool isPrinting = false;

        [ObservableProperty]
        [property: JsonIgnore]
        [JsonIgnore]
        bool isPaused = false;

        [ObservableProperty]
        [property: JsonIgnore]
        [JsonIgnore]
        bool isSelected = false;

        [ObservableProperty]
        [property: JsonIgnore]
        [JsonIgnore]
        byte[] currentPrintImage = Array.Empty<byte>();

        #endregion

        #endregion

        #region Methods

        public Task<bool> HomeAsync(IPrint3dServerClient client, bool x, bool y, bool z) => client?.HomeAsync(x, y, z);

        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        
        public override bool Equals(object obj)
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

using AndreasReitberger.API.Print3dServer.Core.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;

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
        bool isActive;

        [ObservableProperty]
        [JsonProperty("analysed")]
        int? analysed;

        [ObservableProperty]
        [JsonProperty("done")]
        double? done;

        [ObservableProperty]
        [JsonProperty("job")]
        string activeJobName = string.Empty;

        [ObservableProperty]
        [JsonProperty("jobid")]
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
        string? activeJobState;
        
        [ObservableProperty]
        [JsonProperty("linesSend")]
        long? lineSent;

        [ObservableProperty]
        [JsonProperty("name")]
        string name = string.Empty;
        
        [ObservableProperty]
        [JsonProperty("ofLayer")]
        long? layers;

        [ObservableProperty]
        [JsonProperty("online")]
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
        long? pauseState;

        [ObservableProperty]
        [JsonProperty("paused")]
        bool paused;

        [ObservableProperty]
        [JsonProperty("printStart")]
        double? printStart;

        [ObservableProperty]
        [JsonProperty("printTime")]
        double? printTime;

        [ObservableProperty]
        [JsonProperty("printedTimeComp")]
        double? printedTimeComp;

        [ObservableProperty]
        [JsonProperty("repeat")]
        int? repeat;

        [ObservableProperty]
        [JsonProperty("slug")]
        string slug = string.Empty;

        [ObservableProperty]
        [JsonProperty("start")]
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

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

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
    }
}

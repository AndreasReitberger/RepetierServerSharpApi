using AndreasReitberger.API.Print3dServer.Core.Enums;
using AndreasReitberger.API.Print3dServer.Core.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierCurrentPrintInfo : ObservableObject, IPrint3dJobStatus
    {
        #region Properties
        [ObservableProperty, JsonIgnore]
        [property: JsonIgnore]
        Guid id;

        [ObservableProperty]
        [JsonProperty("active")]
        bool active;

        [ObservableProperty]
        [JsonProperty("analysed")]
        long analysed;

        [ObservableProperty]
        [JsonProperty("done")]
        double done;

        [ObservableProperty]
        [JsonProperty("job")]
        string job;

        [ObservableProperty]
        [JsonProperty("jobid")]
        long jobIdLong;
        partial void OnJobIdLongChanged(long value)
        {
            JobId = value.ToString() ?? "";
        }

        [ObservableProperty, JsonIgnore]
        [property: JsonIgnore]
        string jobId;

        [ObservableProperty]
        [JsonProperty("jobstate")]
        string jobState;

        [ObservableProperty]
        [JsonProperty("linesSend")]
        long linesSend;

        [ObservableProperty]
        [JsonProperty("name")]
        string fileName;

        [ObservableProperty]
        [JsonProperty("ofLayer")]
        long ofLayer;

        [ObservableProperty]
        [JsonProperty("online")]
        long online;

        [ObservableProperty]
        [JsonProperty("pauseState")]
        long pauseState;

        [ObservableProperty]
        [JsonProperty("paused")]
        bool paused;
        partial void OnPausedChanged(bool value)
        {
            //State = value ? Print3dJobState.InProgress;
        }

        [ObservableProperty]
        [JsonProperty("printStart")]
        double? startTime;

        [ObservableProperty, JsonIgnore]
        [property: JsonIgnore]
        double? endTime;

        [ObservableProperty]
        [JsonProperty("printTime")]
        double? printDuration;

        [ObservableProperty]
        [JsonProperty("printedTimeComp")]
        double? printDurationTimeComp;

        [ObservableProperty, JsonIgnore]
        [property: JsonIgnore]
        double? totalPrintDuration;

        [ObservableProperty]
        [JsonProperty("repeat")]
        long? repeat;

        [ObservableProperty]
        [JsonProperty("slug")]
        string slug;

        [ObservableProperty]
        [JsonProperty("start")]
        long? start;

        [ObservableProperty]
        [JsonProperty("totalLines")]
        long? totalLines;

        [ObservableProperty, JsonIgnore]
        [property: JsonIgnore]
        double? filamentUsed;

        [ObservableProperty, JsonIgnore]
        [property: JsonIgnore]
        bool fileExists;

        [ObservableProperty, JsonIgnore]
        [property: JsonIgnore]
        Print3dJobState state;
        
        [ObservableProperty, JsonIgnore]
        [property: JsonIgnore]
        IGcodeMeta meta;

        #region JsonIgnore

        [JsonIgnore]
        public double? RemainingPrintTime => PrintDuration > 0 ? PrintDuration - PrintDurationTimeComp : 0;
        #endregion

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

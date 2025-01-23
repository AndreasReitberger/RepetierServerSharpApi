using AndreasReitberger.API.Print3dServer.Core.Interfaces;
using AndreasReitberger.API.Print3dServer.Core.Utilities;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierPrinter : ObservableObject, IPrinter3d
    {
        #region Properties
        [ObservableProperty]

        public partial Guid Id { get; set; }

        [ObservableProperty]

        [JsonProperty("active")]
        public partial bool IsActive { get; set; }

        partial void OnIsActiveChanged(bool value)
        {
            IsPrinting = true;
        }

        [ObservableProperty]

        [JsonProperty("analysed")]
        public partial int? Analysed { get; set; }

        [ObservableProperty]

        [JsonProperty("done")]
        public partial double? Done { get; set; }

        partial void OnDoneChanged(double? value)
        {
            if (value is not null)
                PrintProgress = value / 100;
            else
                PrintProgress = 0;
        }

        [ObservableProperty]

        [JsonProperty("job")]
        public partial string ActiveJobName { get; set; } = string.Empty;

        [ObservableProperty]

        [NotifyPropertyChangedFor(nameof(ActiveJobId))]
        [JsonProperty("jobid")]
        public partial int JobId { get; set; } = -1;

        partial void OnJobIdChanged(int value)
        {
            ActiveJobId = value.ToString();
        }

        [ObservableProperty]

        public partial string ActiveJobId { get; set; } = string.Empty;

        [ObservableProperty]

        [JsonProperty("jobstate")]
        public partial string? ActiveJobState { get; set; }

        [ObservableProperty]

        [JsonProperty("linesSend")]
        public partial long? LineSent { get; set; }

        [ObservableProperty]

        [JsonProperty("name")]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]

        [JsonProperty("ofLayer")]
        public partial long? Layers { get; set; }

        [ObservableProperty]

        [NotifyPropertyChangedFor(nameof(IsOnline))]
        [JsonProperty("online")]
        public partial long Online { get; set; }

        partial void OnOnlineChanged(long value)
        {
            IsOnline = value > 0;
        }

        [ObservableProperty]

        public partial bool IsOnline { get; set; } = false;

        [ObservableProperty]

        [JsonProperty("pauseState")]
        public partial long? PauseState { get; set; }

        [ObservableProperty]

        [JsonProperty("paused")]
        public partial bool Paused { get; set; }

        /*
        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("printStart")]
        double? printStart;
        */

        [ObservableProperty]

        [NotifyPropertyChangedFor(nameof(PrintTimeGeneralized))]
        public partial
        //[property: JsonProperty("printTime")]
        double? PrintTime
        { get; set; }

        partial void OnPrintTimeChanged(double? value)
        {
            if (value is not null)
                PrintTimeGeneralized = TimeBaseConvertHelper.FromDoubleSeconds(value);
            //if (value > 0)
            //    RemainingPrintDuration = value * PrintProgress;
        }

        [ObservableProperty]

        public partial TimeSpan? PrintTimeGeneralized { get; set; }

        [ObservableProperty]

        [NotifyPropertyChangedFor(nameof(PrintedTimeCompGeneralized))]
        [JsonProperty("printedTimeComp")]
        public partial double? PrintedTimeComp { get; set; }

        partial void OnPrintedTimeCompChanged(double? value)
        {
            if (value is not null)
                PrintedTimeCompGeneralized = TimeBaseConvertHelper.FromDoubleSeconds(value);
        }

        [ObservableProperty]

        public partial TimeSpan? PrintedTimeCompGeneralized { get; set; }

        [ObservableProperty]

        [JsonProperty("repeat")]
        public partial int? Repeat { get; set; }

        [ObservableProperty]

        [JsonProperty("slug")]
        public partial string Slug { get; set; } = string.Empty;

        [ObservableProperty]

        [NotifyPropertyChangedFor(nameof(PrintStarted))]
        [JsonProperty("start")]
        public partial long? Start { get; set; }

        partial void OnStartChanged(long? value)
        {
            //PrintStarted = value;
        }

        [ObservableProperty]

        [NotifyPropertyChangedFor(nameof(PrintStartedGeneralized))]
        [JsonProperty("printStart")]
        public partial double? PrintStarted { get; set; } = 0;

        partial void OnPrintStartedChanged(double? value)
        {
            if (value is not null)
                PrintStartedGeneralized = TimeBaseConvertHelper.FromUnixDate(value);
        }
        [ObservableProperty]

        public partial DateTime? PrintStartedGeneralized { get; set; }

        [ObservableProperty]

        [NotifyPropertyChangedFor(nameof(PrintDurationGeneralized))]
        [JsonProperty("printTime")]
        public partial double? PrintDuration { get; set; } = 0;

        partial void OnPrintDurationChanged(double? value)
        {
            if (value is not null)
                PrintDurationGeneralized = TimeBaseConvertHelper.FromDoubleSeconds(value);
            if (value > 0)
                RemainingPrintDuration = value * PrintProgress;
        }

        [ObservableProperty]

        public partial TimeSpan? PrintDurationGeneralized { get; set; }

        [ObservableProperty]

        [NotifyPropertyChangedFor(nameof(PrintDurationEstimatedGeneralized))]
        public partial double? PrintDurationEstimated { get; set; } = 0;

        partial void OnPrintDurationEstimatedChanged(double? value)
        {
            if (value is not null)
                PrintDurationEstimatedGeneralized = TimeBaseConvertHelper.FromDoubleSeconds(value);
        }

        [ObservableProperty]

        public partial TimeSpan? PrintDurationEstimatedGeneralized { get; set; }

        [ObservableProperty]

        [JsonProperty("totalLines")]
        public partial long? TotalLines { get; set; }

        #region JsonIgnored

        [ObservableProperty]

        public partial double? Extruder1Temperature { get; set; } = 0;

        [ObservableProperty]

        public partial double? Extruder2Temperature { get; set; } = 0;

        [ObservableProperty]

        public partial double? Extruder3Temperature { get; set; } = 0;

        [ObservableProperty]

        public partial double? Extruder4Temperature { get; set; } = 0;

        [ObservableProperty]

        public partial double? Extruder5Temperature { get; set; } = 0;

        [ObservableProperty]

        public partial double? HeatedBedTemperature { get; set; } = 0;

        [ObservableProperty]

        public partial double? HeatedChamberTemperature { get; set; } = 0;

        [ObservableProperty]

        public partial double? PrintProgress { get; set; } = 0;

        partial void OnPrintProgressChanged(double? value)
        {
            if (value > 0)
                RemainingPrintDuration = value * PrintDuration;
            //RemainingPrintDuration = value * PrintTime;
        }

        [ObservableProperty]

        [NotifyPropertyChangedFor(nameof(RemainingPrintDurationGeneralized))]
        public partial double? RemainingPrintDuration { get; set; } = 0;

        partial void OnRemainingPrintDurationChanged(double? value)
        {
            if (value is not null)
                RemainingPrintDurationGeneralized = TimeBaseConvertHelper.FromDoubleSeconds(value);
        }
        [ObservableProperty]

        public partial TimeSpan? RemainingPrintDurationGeneralized { get; set; }

        [ObservableProperty]

        public partial bool IsPrinting { get; set; } = false;

        [ObservableProperty]

        public partial bool IsPaused { get; set; } = false;

        [ObservableProperty]

        public partial bool IsSelected { get; set; } = false;

        [ObservableProperty]

        public partial byte[] CurrentPrintImage { get; set; } = [];

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

using AndreasReitberger.API.Print3dServer.Core.Interfaces;
using AndreasReitberger.API.Print3dServer.Core.Utilities;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierJobListItem : ObservableObject, IPrint3dJob
    {
        #region Properties

        [ObservableProperty, JsonIgnore]
        Guid id;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("analysed")]
        long analysed;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("done")]
        double? done;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("extruderUsage")]
        List<double> extruderUsage = [];

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("filamentTotal")]
        double filamentTotal;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("fits")]
        bool fits;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("gcodePatch")]
        string gcodePatch = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("group")]
        string group = string.Empty;

        [ObservableProperty, JsonIgnore]
        [NotifyPropertyChangedFor(nameof(JobId))]
        [property: JsonProperty("id")]
        long identifier;
        partial void OnIdentifierChanged(long value)
        {
            JobId = value.ToString();
            Id = new Guid(value.ToString().PadLeft(32, '0'));
        }

        [ObservableProperty, JsonIgnore]
        string jobId = string.Empty;

        [ObservableProperty, JsonIgnore]
        [NotifyPropertyChangedFor(nameof(PrintTimeGeneralized))]
        [property: JsonProperty("printTime")]
        double? printTime;
        partial void OnPrintTimeChanged(double? value)
        {
            if (value is not null)
                PrintTimeGeneralized = TimeBaseConvertHelper.FromDoubleSeconds(value);
        }
        [ObservableProperty, JsonIgnore]
        TimeSpan? printTimeGeneralized;

        [ObservableProperty, JsonIgnore]
        [NotifyPropertyChangedFor(nameof(PrintTimeGeneralized))]
        [property: JsonProperty("lastPrintTime")]
        double? lastPrintTime;
        partial void OnLastPrintTimeChanged(double? value)
        {
            if (value is not null)
                LastPrintTimeGeneralized = TimeBaseConvertHelper.FromDoubleSeconds(value);
        }

        [ObservableProperty, JsonIgnore]
        TimeSpan? lastPrintTimeGeneralized;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("layer")]
        long layer;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("length")]
        long length;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("lines")]
        long lines;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("materials")]
        List<string> materials = [];

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("name")]
        string fileName = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("notes")]
        string notes = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("printed")]
        long printed;

        [ObservableProperty, JsonIgnore]
        [NotifyPropertyChangedFor(nameof(PrintedTimeCompGeneralized))]
        [property: JsonProperty("printedTimeComp")]
        long? printedTimeComp;

        partial void OnPrintedTimeCompChanged(long? value)
        {
            if (value is not null)
                PrintedTimeCompGeneralized = TimeBaseConvertHelper.FromDoubleSeconds(value);
        }

        [ObservableProperty, JsonIgnore]
        TimeSpan? printedTimeCompGeneralized;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("printerParam1")]
        long printerParam1;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("printerType")]
        long printerType;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("radius")]
        double radius;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("radiusMove")]
        long radiusMove;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("repeat")]
        long repeat;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("slicer")]
        string slicer = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("state")]
        string state = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("version")]
        long version;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("volumeTotal")]
        double volumeTotal;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("volumeUsage")]
        List<double> volumeUsage = [];

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("volumetric")]
        bool volumetric;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("xMax")]
        double xMax;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("xMaxMove")]
        double xMaxMove;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("xMaxView")]
        double xMaxView;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("xMin")]
        long xMin;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("xMinMove")]
        long xMinMove;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("xMinView")]
        double xMinView;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("yMax")]
        double yMax;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("yMaxMove")]
        long yMaxMove;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("yMaxView")]
        double yMaxView;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("yMin")]
        long yMin;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("yMinMove")]
        long yMinMove;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("yMinView")]
        double yMinView;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("zMax")]
        double zMax;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("zMin")]
        long zMin;

        #region Interface, unused

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("created")]
        [NotifyPropertyChangedFor(nameof(TimeAddedGeneralized))]
        double? timeAdded = 0;
        partial void OnTimeAddedChanged(double? value)
        {
            if (value is not null)
                TimeAddedGeneralized = TimeBaseConvertHelper.FromUnixDoubleMiliseconds(value);
        }

        [ObservableProperty, JsonIgnore]
        DateTime? timeAddedGeneralized;

        [ObservableProperty, JsonIgnore]
        [NotifyPropertyChangedFor(nameof(TimeInQueueGeneralized))]
        double? timeInQueue = 0;
        partial void OnTimeInQueueChanged(double? value)
        {
            if (value is not null)
                TimeInQueueGeneralized = TimeBaseConvertHelper.FromUnixDoubleMiliseconds(value);
        }

        [ObservableProperty, JsonIgnore]
        DateTime? timeInQueueGeneralized;
        #endregion

        #endregion

        #region Methods
        public Task<bool> StartJobAsync(IPrint3dServerClient client, string command, object? data) => client.StartJobAsync(this, command, data);

        public Task<bool> PauseJobAsync(IPrint3dServerClient client, string command, object? data) => client.PauseJobAsync(command, data);

        public Task<bool> StopJobAsync(IPrint3dServerClient client, string command, object? data) => client.StopJobAsync(command, data);

        public Task<bool> RemoveFromQueueAsync(IPrint3dServerClient client, string command, object? data) => client.RemoveJobAsync(this, command, data);

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

        public object Clone()
        {
            return MemberwiseClone();
        }

        #endregion
    }
}

using AndreasReitberger.API.Print3dServer.Core.Interfaces;
using AndreasReitberger.API.Print3dServer.Core.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierJobListItem : ObservableObject, IPrint3dJob
    {
        #region Properties

        [ObservableProperty, JsonIgnore]
        [property: JsonIgnore]
        Guid id;

        [ObservableProperty]
        [JsonProperty("analysed")]
        [property: JsonIgnore]
        long analysed;

        [ObservableProperty]
        [JsonProperty("created")]
        [property: JsonIgnore]
        long created;

        [ObservableProperty]
        [JsonProperty("done")]
        [property: JsonIgnore]
        double? done;

        [ObservableProperty]
        [JsonProperty("extruderUsage")]
        [property: JsonIgnore]
        List<double> extruderUsage;

        [ObservableProperty]
        [JsonProperty("filamentTotal")]
        [property: JsonIgnore]
        double filamentTotal;

        [ObservableProperty]
        [JsonProperty("fits")]
        [property: JsonIgnore]
        bool fits;

        [ObservableProperty]
        [JsonProperty("gcodePatch")]
        [property: JsonIgnore]
        string gcodePatch;

        [ObservableProperty]
        [JsonProperty("group")]
        [property: JsonIgnore]
        string group;

        [ObservableProperty, JsonIgnore]
        [NotifyPropertyChangedFor(nameof(JobId))]
        [property: JsonProperty("id")]
        long identifier;
        partial void OnIdentifierChanged(long value)
        {
            JobId = value.ToString();
        }

        [ObservableProperty, JsonIgnore]
        [property: JsonIgnore]
        string jobId;

        [ObservableProperty]
        [JsonProperty("lastPrintTime")]
        [property: JsonIgnore]
        double lastPrintTime;

        [ObservableProperty]
        [JsonProperty("layer")]
        [property: JsonIgnore]
        long layer;

        [ObservableProperty]
        [JsonProperty("length")]
        [property: JsonIgnore]
        long length;

        [ObservableProperty]
        [JsonProperty("lines")]
        [property: JsonIgnore]
        long lines;

        [ObservableProperty]
        [JsonProperty("materials")]
        [property: JsonIgnore]
        List<string> materials;

        [ObservableProperty]
        [JsonProperty("name")]
        [property: JsonIgnore]
        string fileName;

        [ObservableProperty]
        [JsonProperty("notes")]
        [property: JsonIgnore]
        string notes;

        [ObservableProperty]
        [JsonProperty("printTime")]
        [property: JsonIgnore]
        double printTime;

        [ObservableProperty]
        [JsonProperty("printed")]
        [property: JsonIgnore]
        long printed;

        [ObservableProperty]
        [JsonProperty("printedTimeComp")]
        [property: JsonIgnore]
        long printedTimeComp;

        [ObservableProperty]
        [JsonProperty("printerParam1")]
        [property: JsonIgnore]
        long printerParam1;

        [ObservableProperty]
        [JsonProperty("printerType")]
        [property: JsonIgnore]
        long printerType;

        [ObservableProperty]
        [JsonProperty("radius")]
        [property: JsonIgnore]
        double radius;

        [ObservableProperty]
        [JsonProperty("radiusMove")]
        [property: JsonIgnore]
        long radiusMove;

        [ObservableProperty]
        [JsonProperty("repeat")]
        [property: JsonIgnore]
        long repeat;

        [ObservableProperty]
        [JsonProperty("slicer")]
        [property: JsonIgnore]
        string slicer;

        [ObservableProperty]
        [JsonProperty("state")]
        [property: JsonIgnore]
        string state;

        [ObservableProperty]
        [JsonProperty("version")]
        [property: JsonIgnore]
        long version;

        [ObservableProperty]
        [JsonProperty("volumeTotal")]
        [property: JsonIgnore]
        double volumeTotal;

        [ObservableProperty]
        [JsonProperty("volumeUsage")]
        [property: JsonIgnore]
        List<double> volumeUsage;

        [ObservableProperty]
        [JsonProperty("volumetric")]
        [property: JsonIgnore]
        bool volumetric;

        [ObservableProperty]
        [JsonProperty("xMax")]
        [property: JsonIgnore]
        double xMax;

        [ObservableProperty]
        [JsonProperty("xMaxMove")]
        [property: JsonIgnore]
        double xMaxMove;

        [ObservableProperty]
        [JsonProperty("xMaxView")]
        [property: JsonIgnore]
        double xMaxView;

        [ObservableProperty]
        [JsonProperty("xMin")]
        [property: JsonIgnore]
        long xMin;

        [ObservableProperty]
        [JsonProperty("xMinMove")]
        [property: JsonIgnore]
        long xMinMove;

        [ObservableProperty]
        [JsonProperty("xMinView")]
        [property: JsonIgnore]
        double xMinView;

        [ObservableProperty]
        [JsonProperty("yMax")]
        [property: JsonIgnore]
        double yMax;

        [ObservableProperty]
        [JsonProperty("yMaxMove")]
        [property: JsonIgnore]
        long yMaxMove;

        [ObservableProperty]
        [JsonProperty("yMaxView")]
        [property: JsonIgnore]
        double yMaxView;

        [ObservableProperty]
        [JsonProperty("yMin")]
        [property: JsonIgnore]
        long yMin;

        [ObservableProperty]
        [JsonProperty("yMinMove")]
        [property: JsonIgnore]
        long yMinMove;

        [ObservableProperty]
        [JsonProperty("yMinView")]
        [property: JsonIgnore]
        double yMinView;

        [ObservableProperty]
        [JsonProperty("zMax")]
        [property: JsonIgnore]
        double zMax;

        [ObservableProperty]
        [JsonProperty("zMin")]
        [property: JsonIgnore]
        long zMin;

        #region Interface, unused

        [ObservableProperty, JsonIgnore]
        [NotifyPropertyChangedFor(nameof(TimeAddedGeneralized))]
        [property: JsonIgnore]
        double? timeAdded = 0;
        partial void OnTimeAddedChanged(double? value)
        {
            if (value is not null)
                TimeAddedGeneralized = TimeBaseConvertHelper.FromUnixDate(value);
        }

        [ObservableProperty, JsonIgnore]
        [property: JsonIgnore]
        DateTime? timeAddedGeneralized;

        [ObservableProperty, JsonIgnore]
        [NotifyPropertyChangedFor(nameof(TimeInQueueGeneralized))]
        [property: JsonIgnore]
        double? timeInQueue = 0;
        partial void OnTimeInQueueChanged(double? value)
        {
            if (value is not null)
                TimeInQueueGeneralized = TimeBaseConvertHelper.FromUnixDate(value);
        }

        [ObservableProperty, JsonIgnore]
        [property: JsonIgnore]
        DateTime? timeInQueueGeneralized;
        #endregion

        #endregion

        #region Methods
        public Task<bool> StartJobAsync(IPrint3dServerClient client, string command, object? data) => client?.StartJobAsync(this, command, data);

        public Task<bool> PauseJobAsync(IPrint3dServerClient client, string command, object? data) => client?.PauseJobAsync(command, data);

        public Task<bool> StopJobAsync(IPrint3dServerClient client, string command, object? data) => client?.StopJobAsync(command, data);

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

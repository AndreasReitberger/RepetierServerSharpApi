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

        [ObservableProperty]

        public partial Guid Id { get; set; }

        [ObservableProperty]

        [JsonProperty("analysed")]
        public partial long Analysed { get; set; }

        [ObservableProperty]

        [JsonProperty("done")]
        public partial double? Done { get; set; }

        [ObservableProperty]

        [JsonProperty("extruderUsage")]
        public partial List<double> ExtruderUsage { get; set; } = [];

        [ObservableProperty]

        [JsonProperty("filamentTotal")]
        public partial double FilamentTotal { get; set; }

        [ObservableProperty]

        [JsonProperty("fits")]
        public partial bool Fits { get; set; }

        [ObservableProperty]

        [JsonProperty("gcodePatch")]
        public partial string GcodePatch { get; set; } = string.Empty;

        [ObservableProperty]

        [JsonProperty("group")]
        public partial string Group { get; set; } = string.Empty;

        [ObservableProperty]

        [NotifyPropertyChangedFor(nameof(JobId))]
        [JsonProperty("id")]
        public partial long Identifier { get; set; }

        partial void OnIdentifierChanged(long value)
        {
            JobId = value.ToString();
            Id = new Guid(value.ToString().PadLeft(32, '0'));
        }

        [ObservableProperty]

        public partial string JobId { get; set; } = string.Empty;

        [ObservableProperty]

        [NotifyPropertyChangedFor(nameof(PrintTimeGeneralized))]
        [JsonProperty("printTime")]
        public partial double? PrintTime { get; set; }

        partial void OnPrintTimeChanged(double? value)
        {
            if (value is not null)
                PrintTimeGeneralized = TimeBaseConvertHelper.FromDoubleSeconds(value);
        }
        [ObservableProperty]

        public partial TimeSpan? PrintTimeGeneralized { get; set; }

        [ObservableProperty]

        [NotifyPropertyChangedFor(nameof(PrintTimeGeneralized))]
        [JsonProperty("lastPrintTime")]
        public partial double? LastPrintTime { get; set; }

        partial void OnLastPrintTimeChanged(double? value)
        {
            if (value is not null)
                LastPrintTimeGeneralized = TimeBaseConvertHelper.FromDoubleSeconds(value);
        }

        [ObservableProperty]

        public partial TimeSpan? LastPrintTimeGeneralized { get; set; }

        [ObservableProperty]

        [JsonProperty("layer")]
        public partial long Layer { get; set; }

        [ObservableProperty]

        [JsonProperty("length")]
        public partial long Length { get; set; }

        [ObservableProperty]

        [JsonProperty("lines")]
        public partial long Lines { get; set; }

        [ObservableProperty]

        [JsonProperty("materials")]
        public partial List<string> Materials { get; set; } = [];

        [ObservableProperty]

        [JsonProperty("name")]
        public partial string FileName { get; set; } = string.Empty;

        [ObservableProperty]

        [JsonProperty("notes")]
        public partial string Notes { get; set; } = string.Empty;

        [ObservableProperty]

        [JsonProperty("printed")]
        public partial long Printed { get; set; }

        [ObservableProperty]

        [NotifyPropertyChangedFor(nameof(PrintedTimeCompGeneralized))]
        [JsonProperty("printedTimeComp")]
        public partial long? PrintedTimeComp { get; set; }

        partial void OnPrintedTimeCompChanged(long? value)
        {
            if (value is not null)
                PrintedTimeCompGeneralized = TimeBaseConvertHelper.FromDoubleSeconds(value);
        }

        [ObservableProperty]

        public partial TimeSpan? PrintedTimeCompGeneralized { get; set; }

        [ObservableProperty]

        [JsonProperty("printerParam1")]
        public partial long PrinterParam1 { get; set; }

        [ObservableProperty]

        [JsonProperty("printerType")]
        public partial long PrinterType { get; set; }

        [ObservableProperty]

        [JsonProperty("radius")]
        public partial double Radius { get; set; }

        [ObservableProperty]

        [JsonProperty("radiusMove")]
        public partial long RadiusMove { get; set; }

        [ObservableProperty]

        [JsonProperty("repeat")]
        public partial long Repeat { get; set; }

        [ObservableProperty]

        [JsonProperty("slicer")]
        public partial string Slicer { get; set; } = string.Empty;

        [ObservableProperty]

        [JsonProperty("state")]
        public partial string State { get; set; } = string.Empty;

        [ObservableProperty]

        [JsonProperty("version")]
        public partial long Version { get; set; }

        [ObservableProperty]

        [JsonProperty("volumeTotal")]
        public partial double VolumeTotal { get; set; }

        [ObservableProperty]

        [JsonProperty("volumeUsage")]
        public partial List<double> VolumeUsage { get; set; } = [];

        [ObservableProperty]

        [JsonProperty("volumetric")]
        public partial bool Volumetric { get; set; }

        [ObservableProperty]

        [JsonProperty("xMax")]
        public partial double XMax { get; set; }

        [ObservableProperty]

        [JsonProperty("xMaxMove")]
        public partial double XMaxMove { get; set; }

        [ObservableProperty]

        [JsonProperty("xMaxView")]
        public partial double XMaxView { get; set; }

        [ObservableProperty]

        [JsonProperty("xMin")]
        public partial long XMin { get; set; }

        [ObservableProperty]

        [JsonProperty("xMinMove")]
        public partial long XMinMove { get; set; }

        [ObservableProperty]

        [JsonProperty("xMinView")]
        public partial double XMinView { get; set; }

        [ObservableProperty]

        [JsonProperty("yMax")]
        public partial double YMax { get; set; }

        [ObservableProperty]

        [JsonProperty("yMaxMove")]
        public partial long YMaxMove { get; set; }

        [ObservableProperty]

        [JsonProperty("yMaxView")]
        public partial double YMaxView { get; set; }

        [ObservableProperty]

        [JsonProperty("yMin")]
        public partial long YMin { get; set; }

        [ObservableProperty]

        [JsonProperty("yMinMove")]
        public partial long YMinMove { get; set; }

        [ObservableProperty]

        [JsonProperty("yMinView")]
        public partial double YMinView { get; set; }

        [ObservableProperty]

        [JsonProperty("zMax")]
        public partial double ZMax { get; set; }

        [ObservableProperty]

        [JsonProperty("zMin")]
        public partial long ZMin { get; set; }

        #region Interface, unused

        [ObservableProperty]

        [JsonProperty("created")]
        [NotifyPropertyChangedFor(nameof(TimeAddedGeneralized))]
        public partial double? TimeAdded { get; set; } = 0;

        partial void OnTimeAddedChanged(double? value)
        {
            if (value is not null)
                TimeAddedGeneralized = TimeBaseConvertHelper.FromUnixDoubleMiliseconds(value);
        }

        [ObservableProperty]

        public partial DateTime? TimeAddedGeneralized { get; set; }

        [ObservableProperty]

        [NotifyPropertyChangedFor(nameof(TimeInQueueGeneralized))]
        public partial double? TimeInQueue { get; set; } = 0;

        partial void OnTimeInQueueChanged(double? value)
        {
            if (value is not null)
                TimeInQueueGeneralized = TimeBaseConvertHelper.FromUnixDoubleMiliseconds(value);
        }

        [ObservableProperty]

        public partial DateTime? TimeInQueueGeneralized { get; set; }
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

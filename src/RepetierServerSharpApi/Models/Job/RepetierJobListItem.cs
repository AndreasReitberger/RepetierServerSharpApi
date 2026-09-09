using AndreasReitberger.API.Print3dServer.Core.Interfaces;
using AndreasReitberger.API.Print3dServer.Core.Utilities;
using System;
using System.Threading.Tasks;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierJobListItem : ObservableObject, IPrint3dJob
    {
        #region Properties

        [ObservableProperty]
        [JsonIgnore]
        public partial Guid Id { get; set; }

        [ObservableProperty]
        [JsonPropertyName("analysed")]
        public partial long Analysed { get; set; }

        [ObservableProperty]
        [JsonPropertyName("done")]
        public partial double? Done { get; set; }

        [ObservableProperty]
        [JsonPropertyName("extruderUsage")]
        public partial List<double> ExtruderUsage { get; set; } = [];

        [ObservableProperty]
        [JsonPropertyName("filamentTotal")]
        public partial double FilamentTotal { get; set; }

        [ObservableProperty]
        [JsonPropertyName("fits")]
        public partial bool Fits { get; set; }

        [ObservableProperty]
        [JsonPropertyName("gcodePatch")]
        public partial string GcodePatch { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("group")]
        public partial string Group { get; set; } = string.Empty;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(JobId))]
        [JsonPropertyName("id")]
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
        [JsonPropertyName("printTime")]
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
        [JsonPropertyName("lastPrintTime")]
        public partial double? LastPrintTime { get; set; }
        partial void OnLastPrintTimeChanged(double? value)
        {
            if (value is not null)
                LastPrintTimeGeneralized = TimeBaseConvertHelper.FromDoubleSeconds(value);
        }

        [ObservableProperty]
        public partial TimeSpan? LastPrintTimeGeneralized { get; set; }

        [ObservableProperty]
        [JsonPropertyName("layer")]
        public partial long Layer { get; set; }

        [ObservableProperty]
        [JsonPropertyName("length")]
        public partial long Length { get; set; }

        [ObservableProperty]
        [JsonPropertyName("lines")]
        public partial long Lines { get; set; }

        [ObservableProperty]
        [JsonPropertyName("materials")]
        public partial List<string> Materials { get; set; } = [];

        [ObservableProperty]
        [JsonPropertyName("name")]
        public partial string FileName { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("notes")]
        public partial string Notes { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("printed")]
        public partial long Printed { get; set; }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(PrintedTimeCompGeneralized))]
        [JsonPropertyName("printedTimeComp")]
        public partial long? PrintedTimeComp { get; set; }
        partial void OnPrintedTimeCompChanged(long? value)
        {
            if (value is not null)
                PrintedTimeCompGeneralized = TimeBaseConvertHelper.FromDoubleSeconds(value);
        }

        [ObservableProperty]
        public partial TimeSpan? PrintedTimeCompGeneralized { get; set; }

        [ObservableProperty]
        [JsonPropertyName("printerParam1")]
        public partial long PrinterParam1 { get; set; }

        [ObservableProperty]
        [JsonPropertyName("printerType")]
        public partial long PrinterType { get; set; }

        [ObservableProperty]
        [JsonPropertyName("radius")]
        public partial double Radius { get; set; }

        [ObservableProperty]
        [JsonPropertyName("radiusMove")]
        public partial long RadiusMove { get; set; }

        [ObservableProperty]
        [JsonPropertyName("repeat")]
        public partial long Repeat { get; set; }

        [ObservableProperty]
        [JsonPropertyName("slicer")]
        public partial string Slicer { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("state")]
        public partial string State { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("version")]
        public partial long Version { get; set; }

        [ObservableProperty]
        [JsonPropertyName("volumeTotal")]
        public partial double VolumeTotal { get; set; }

        [ObservableProperty]
        [JsonPropertyName("volumeUsage")]
        public partial List<double> VolumeUsage { get; set; } = [];

        [ObservableProperty]
        [JsonPropertyName("volumetric")]
        public partial bool Volumetric { get; set; }

        [ObservableProperty]
        [JsonPropertyName("xMax")]
        public partial double XMax { get; set; }

        [ObservableProperty]
        [JsonPropertyName("xMaxMove")]
        public partial double XMaxMove { get; set; }

        [ObservableProperty]
        [JsonPropertyName("xMaxView")]
        public partial double XMaxView { get; set; }

        [ObservableProperty]
        [JsonPropertyName("xMin")]
        public partial double XMin { get; set; }

        [ObservableProperty]
        [JsonPropertyName("xMinMove")]
        public partial double XMinMove { get; set; }

        [ObservableProperty]
        [JsonPropertyName("xMinView")]
        public partial double XMinView { get; set; }

        [ObservableProperty]
        [JsonPropertyName("yMax")]
        public partial double YMax { get; set; }

        [ObservableProperty]
        [JsonPropertyName("yMaxMove")]
        public partial double YMaxMove { get; set; }

        [ObservableProperty]
        [JsonPropertyName("yMaxView")]
        public partial double YMaxView { get; set; }

        [ObservableProperty]
        [JsonPropertyName("yMin")]
        public partial double YMin { get; set; }

        [ObservableProperty]
        [JsonPropertyName("yMinMove")]
        public partial double YMinMove { get; set; }

        [ObservableProperty]
        [JsonPropertyName("yMinView")]
        public partial double YMinView { get; set; }

        [ObservableProperty]
        [JsonPropertyName("zMax")]
        public partial double ZMax { get; set; }

        [ObservableProperty]
        [JsonPropertyName("zMin")]
        public partial double ZMin { get; set; }

        #region Interface, unused

        [ObservableProperty]
        [JsonPropertyName("created")]
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
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierJobListItem);

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

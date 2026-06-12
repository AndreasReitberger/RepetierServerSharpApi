using AndreasReitberger.API.Print3dServer.Core.Interfaces;
using AndreasReitberger.API.Print3dServer.Core.Utilities;
using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierJobListItem : ObservableObject, IPrint3dJob
    {
        #region Properties

        [ObservableProperty]
        [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore]
        public partial Guid Id { get; set; }

        [ObservableProperty]
        [JsonProperty("analysed"), JsonPropertyName("analysed")]
        public partial long Analysed { get; set; }

        [ObservableProperty]
        [JsonProperty("done"), JsonPropertyName("done")]
        public partial double? Done { get; set; }

        [ObservableProperty]
        [JsonProperty("extruderUsage"), JsonPropertyName("extruderUsage")]
        public partial List<double> ExtruderUsage { get; set; } = [];

        [ObservableProperty]
        [JsonProperty("filamentTotal"), JsonPropertyName("filamentTotal")]
        public partial double FilamentTotal { get; set; }

        [ObservableProperty]
        [JsonProperty("fits"), JsonPropertyName("fits")]
        public partial bool Fits { get; set; }

        [ObservableProperty]
        [JsonProperty("gcodePatch"), JsonPropertyName("gcodePatch")]
        public partial string GcodePatch { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("group"), JsonPropertyName("group")]
        public partial string Group { get; set; } = string.Empty;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(JobId))]
        [JsonProperty("id"), JsonPropertyName("id")]
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
        [JsonProperty("printTime"), JsonPropertyName("printTime")]
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
        [JsonProperty("lastPrintTime"), JsonPropertyName("lastPrintTime")]
        public partial double? LastPrintTime { get; set; }
        partial void OnLastPrintTimeChanged(double? value)
        {
            if (value is not null)
                LastPrintTimeGeneralized = TimeBaseConvertHelper.FromDoubleSeconds(value);
        }

        [ObservableProperty]
        public partial TimeSpan? LastPrintTimeGeneralized { get; set; }

        [ObservableProperty]
        [JsonProperty("layer"), JsonPropertyName("layer")]
        public partial long Layer { get; set; }

        [ObservableProperty]
        [JsonProperty("length"), JsonPropertyName("length")]
        public partial long Length { get; set; }

        [ObservableProperty]
        [JsonProperty("lines"), JsonPropertyName("lines")]
        public partial long Lines { get; set; }

        [ObservableProperty]
        [JsonProperty("materials"), JsonPropertyName("materials")]
        public partial List<string> Materials { get; set; } = [];

        [ObservableProperty]
        [JsonProperty("name"), JsonPropertyName("name")]
        public partial string FileName { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("notes"), JsonPropertyName("notes")]
        public partial string Notes { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("printed"), JsonPropertyName("printed")]
        public partial long Printed { get; set; }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(PrintedTimeCompGeneralized))]
        [JsonProperty("printedTimeComp"), JsonPropertyName("printedTimeComp")]
        public partial long? PrintedTimeComp { get; set; }
        partial void OnPrintedTimeCompChanged(long? value)
        {
            if (value is not null)
                PrintedTimeCompGeneralized = TimeBaseConvertHelper.FromDoubleSeconds(value);
        }

        [ObservableProperty]
        public partial TimeSpan? PrintedTimeCompGeneralized { get; set; }

        [ObservableProperty]
        [JsonProperty("printerParam1"), JsonPropertyName("printerParam1")]
        public partial long PrinterParam1 { get; set; }

        [ObservableProperty]
        [JsonProperty("printerType"), JsonPropertyName("printerType")]
        public partial long PrinterType { get; set; }

        [ObservableProperty]
        [JsonProperty("radius"), JsonPropertyName("radius")]
        public partial double Radius { get; set; }

        [ObservableProperty]
        [JsonProperty("radiusMove"), JsonPropertyName("radiusMove")]
        public partial long RadiusMove { get; set; }

        [ObservableProperty]
        [JsonProperty("repeat"), JsonPropertyName("repeat")]
        public partial long Repeat { get; set; }

        [ObservableProperty]
        [JsonProperty("slicer"), JsonPropertyName("slicer")]
        public partial string Slicer { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("state"), JsonPropertyName("state")]
        public partial string State { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("version"), JsonPropertyName("version")]
        public partial long Version { get; set; }

        [ObservableProperty]
        [JsonProperty("volumeTotal"), JsonPropertyName("volumeTotal")]
        public partial double VolumeTotal { get; set; }

        [ObservableProperty]
        [JsonProperty("volumeUsage"), JsonPropertyName("volumeUsage")]
        public partial List<double> VolumeUsage { get; set; } = [];

        [ObservableProperty]
        [JsonProperty("volumetric"), JsonPropertyName("volumetric")]
        public partial bool Volumetric { get; set; }

        [ObservableProperty]
        [JsonProperty("xMax"), JsonPropertyName("xMax")]
        public partial double XMax { get; set; }

        [ObservableProperty]
        [JsonProperty("xMaxMove"), JsonPropertyName("xMaxMove")]
        public partial double XMaxMove { get; set; }

        [ObservableProperty]
        [JsonProperty("xMaxView"), JsonPropertyName("xMaxView")]
        public partial double XMaxView { get; set; }

        [ObservableProperty]
        [JsonProperty("xMin"), JsonPropertyName("xMin")]
        public partial double XMin { get; set; }

        [ObservableProperty]
        [JsonProperty("xMinMove"), JsonPropertyName("xMinMove")]
        public partial double XMinMove { get; set; }

        [ObservableProperty]
        [JsonProperty("xMinView"), JsonPropertyName("xMinView")]
        public partial double XMinView { get; set; }

        [ObservableProperty]
        [JsonProperty("yMax"), JsonPropertyName("yMax")]
        public partial double YMax { get; set; }

        [ObservableProperty]
        [JsonProperty("yMaxMove"), JsonPropertyName("yMaxMove")]
        public partial double YMaxMove { get; set; }

        [ObservableProperty]
        [JsonProperty("yMaxView"), JsonPropertyName("yMaxView")]
        public partial double YMaxView { get; set; }

        [ObservableProperty]
        [JsonProperty("yMin"), JsonPropertyName("yMin")]
        public partial double YMin { get; set; }

        [ObservableProperty]
        [JsonProperty("yMinMove"), JsonPropertyName("yMinMove")]
        public partial double YMinMove { get; set; }

        [ObservableProperty]
        [JsonProperty("yMinView"), JsonPropertyName("yMinView")]
        public partial double YMinView { get; set; }

        [ObservableProperty]
        [JsonProperty("zMax"), JsonPropertyName("zMax")]
        public partial double ZMax { get; set; }

        [ObservableProperty]
        [JsonProperty("zMin"), JsonPropertyName("zMin")]
        public partial double ZMin { get; set; }

        #region Interface, unused

        [ObservableProperty]
        [JsonProperty("created"), JsonPropertyName("created")]
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

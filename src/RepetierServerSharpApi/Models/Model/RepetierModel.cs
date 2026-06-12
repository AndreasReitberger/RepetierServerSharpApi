using AndreasReitberger.API.Print3dServer.Core.Enums;
using AndreasReitberger.API.Print3dServer.Core.Interfaces;
using AndreasReitberger.API.Print3dServer.Core.Utilities;
using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierModel : ObservableObject, IGcode
    {
        #region Properties

        [ObservableProperty]
        [Newtonsoft.Json.JsonIgnore, System.Text.Json.Serialization.JsonIgnore]
        public partial Guid Id { get; set; } = Guid.NewGuid();

        [ObservableProperty]
        public partial GcodeTimeBaseTarget TimeBaseTarget { get; set; } = GcodeTimeBaseTarget.DoubleSeconds;

        [ObservableProperty]
        [JsonProperty("analysed"), JsonPropertyName("analysed")]
        public partial long Analysed { get; set; }

        [ObservableProperty]
        [JsonProperty("created"), JsonPropertyName("created")]
        [NotifyPropertyChangedFor(nameof(CreatedGeneralized))]
        public partial double? Created { get; set; } = 0;
        partial void OnCreatedChanged(double? value)
        {
            if (value is not null)
                CreatedGeneralized = TimeBaseConvertHelper.FromUnixDoubleMiliseconds(value);
        }

        [ObservableProperty]
        public partial DateTime? CreatedGeneralized { get; set; }

        [ObservableProperty]
        [JsonProperty("extruderUsage"), JsonPropertyName("extruderUsage")]
        public partial double[] ExtruderUsage { get; set; } = [];

        [ObservableProperty]
        [JsonProperty("filamentTotal"), JsonPropertyName("filamentTotal")]
        public partial double Filament { get; set; }

        [ObservableProperty]
        [JsonProperty("fits"), JsonPropertyName("fits")]
        public partial bool Fits { get; set; }

        [ObservableProperty]
        [JsonProperty("gcodePatch"), JsonPropertyName("gcodePatch")]
        public partial string GcodePatch { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("group"), JsonPropertyName("group")]
        public partial string Group { get; set; } = string.Empty;
        partial void OnGroupChanged(string value)
        {
            FilePath = value;
        }

        [ObservableProperty]
        [JsonProperty("id"), JsonPropertyName("id")]
        public partial long Identifier { get; set; }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(LastPrintTimeGeneralized))]
        [JsonProperty("lastPrintTime"), JsonPropertyName("lastPrintTime")]
        public partial double? LastPrintTime { get; set; }
        partial void OnLastPrintTimeChanged(double? value)
        {
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
        public partial string[] Materials { get; set; } = [];

        [ObservableProperty]
        [JsonProperty("name"), JsonPropertyName("name")]
        public partial string FileName { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonProperty("notes"), JsonPropertyName("notes")]
        public partial string Notes { get; set; } = string.Empty;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(PrintTimeGeneralized))]
        [JsonProperty("printTime"), JsonPropertyName("printTime")]
        public partial double PrintTime { get; set; }
        partial void OnPrintTimeChanged(double value)
        {
            PrintTimeGeneralized = TimeBaseConvertHelper.FromDoubleSeconds(value);
        }

        [ObservableProperty]
        public partial TimeSpan? PrintTimeGeneralized { get; set; }

        [ObservableProperty]
        [JsonProperty("printed"), JsonPropertyName("printed")]
        public partial long Printed { get; set; }

        [ObservableProperty]
        [JsonProperty("printerParam1"), JsonPropertyName("printerParam1")]
        public partial double PrinterParam1 { get; set; }

        [ObservableProperty]
        [JsonProperty("printerType"), JsonPropertyName("printerType")]
        public partial long PrinterType { get; set; }

        [ObservableProperty]
        [JsonProperty("radius"), JsonPropertyName("radius")]
        public partial double Radius { get; set; }

        [ObservableProperty]
        [JsonProperty("radiusMove"), JsonPropertyName("radiusMove")]
        public partial double RadiusMove { get; set; }

        [ObservableProperty]
        [JsonProperty("repeat"), JsonPropertyName("repeat")]
        public partial int Repeat { get; set; }

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
        public partial double Volume { get; set; }

        [ObservableProperty]
        [JsonProperty("volumeUsage"), JsonPropertyName("volumeUsage")]
        public partial double[] VolumeUsage { get; set; } = [];

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
        public partial double? Modified { get; set; }

        [ObservableProperty]
        public partial string FilePath { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string Permissions { get; set; } = string.Empty;

        [ObservableProperty]
        public partial long Size { get; set; }

        [ObservableProperty]
        public partial IGcodeMeta? Meta { get; set; }
        #endregion

        #region JsonIgnore

        [ObservableProperty]
        public partial bool IsVisible { get; set; }

        [ObservableProperty]
        public partial bool IsLoadingImage { get; set; } = false;

        [ObservableProperty]
        public partial byte[]? Image { get; set; } = [];

        [ObservableProperty]
        public partial byte[]? Thumbnail { get; set; } = [];

        [ObservableProperty]
        public partial GcodeImageType ImageType { get; set; } = GcodeImageType.Thumbnail;

        [ObservableProperty]
        public partial string PrinterName { get; set; } = string.Empty;

        #endregion

        #endregion

        #region Ctor
        public RepetierModel()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Methods
        public Task MoveToAsync(IPrint3dServerClient client, string targetPath, bool copy = false)
        {
            throw new NotImplementedException();
        }

        public Task MoveToQueueAsync(IPrint3dServerClient client, bool printIfReady = false)
        {
            throw new NotImplementedException();
        }

        public Task PrintAsync(IPrint3dServerClient client)
        {
            throw new NotImplementedException();
        }
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

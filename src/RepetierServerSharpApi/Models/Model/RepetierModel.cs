using AndreasReitberger.API.Print3dServer.Core.Enums;
using AndreasReitberger.API.Print3dServer.Core.Interfaces;
using AndreasReitberger.API.Print3dServer.Core.Utilities;
using System;
using System.Threading.Tasks;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierModel : ObservableObject, IGcode
    {
        #region Properties

        [ObservableProperty]
        [JsonIgnore]
        public partial Guid Id { get; set; } = Guid.NewGuid();

        [ObservableProperty]
        public partial GcodeTimeBaseTarget TimeBaseTarget { get; set; } = GcodeTimeBaseTarget.DoubleSeconds;

        [ObservableProperty]
        [JsonPropertyName("analysed")]
        public partial long Analysed { get; set; }

        [ObservableProperty]
        [JsonPropertyName("created")]
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
        [JsonPropertyName("extruderUsage")]
        public partial double[] ExtruderUsage { get; set; } = [];

        [ObservableProperty]
        [JsonPropertyName("filamentTotal")]
        public partial double Filament { get; set; }

        [ObservableProperty]
        [JsonPropertyName("fits")]
        public partial bool Fits { get; set; }

        [ObservableProperty]
        [JsonPropertyName("gcodePatch")]
        public partial string GcodePatch { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("group")]
        public partial string Group { get; set; } = string.Empty;
        partial void OnGroupChanged(string value)
        {
            FilePath = value;
        }

        [ObservableProperty]
        [JsonPropertyName("id")]
        public partial long Identifier { get; set; }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(LastPrintTimeGeneralized))]
        [JsonPropertyName("lastPrintTime")]
        public partial double? LastPrintTime { get; set; }
        partial void OnLastPrintTimeChanged(double? value)
        {
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
        public partial string[] Materials { get; set; } = [];

        [ObservableProperty]
        [JsonPropertyName("name")]
        public partial string FileName { get; set; } = string.Empty;

        [ObservableProperty]
        [JsonPropertyName("notes")]
        public partial string Notes { get; set; } = string.Empty;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(PrintTimeGeneralized))]
        [JsonPropertyName("printTime")]
        public partial double PrintTime { get; set; }
        partial void OnPrintTimeChanged(double value)
        {
            PrintTimeGeneralized = TimeBaseConvertHelper.FromDoubleSeconds(value);
        }

        [ObservableProperty]
        public partial TimeSpan? PrintTimeGeneralized { get; set; }

        [ObservableProperty]
        [JsonPropertyName("printed")]
        public partial long Printed { get; set; }

        [ObservableProperty]
        [JsonPropertyName("printerParam1")]
        public partial double PrinterParam1 { get; set; }

        [ObservableProperty]
        [JsonPropertyName("printerType")]
        public partial long PrinterType { get; set; }

        [ObservableProperty]
        [JsonPropertyName("radius")]
        public partial double Radius { get; set; }

        [ObservableProperty]
        [JsonPropertyName("radiusMove")]
        public partial double RadiusMove { get; set; }

        [ObservableProperty]
        [JsonPropertyName("repeat")]
        public partial int Repeat { get; set; }

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
        public partial double Volume { get; set; }

        [ObservableProperty]
        [JsonPropertyName("volumeUsage")]
        public partial double[] VolumeUsage { get; set; } = [];

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
        public override string ToString() => JsonSerializer.Serialize(this!, RepetierSourceGenerationContext.Default.RepetierModel);

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

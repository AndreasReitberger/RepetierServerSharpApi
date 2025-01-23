using AndreasReitberger.API.Print3dServer.Core.Enums;
using AndreasReitberger.API.Print3dServer.Core.Interfaces;
using AndreasReitberger.API.Print3dServer.Core.Utilities;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierModel : ObservableObject, IGcode
    {
        #region Properties

        [ObservableProperty]

        public partial Guid Id { get; set; }

        [ObservableProperty]

        public partial GcodeTimeBaseTarget TimeBaseTarget { get; set; } = GcodeTimeBaseTarget.DoubleSeconds;

        [ObservableProperty]

        [JsonProperty("analysed")]
        public partial long Analysed { get; set; }

        /*
        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("created")]
        long created;
        */

        [ObservableProperty]

        [JsonProperty("created")]
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

        [JsonProperty("extruderUsage")]
        public partial double[] ExtruderUsage { get; set; } = [];

        [ObservableProperty]

        [JsonProperty("filamentTotal")]
        public partial double Filament { get; set; }

        [ObservableProperty]

        [JsonProperty("fits")]
        public partial bool Fits { get; set; }

        [ObservableProperty]

        [JsonProperty("gcodePatch")]
        public partial string GcodePatch { get; set; } = string.Empty;

        [ObservableProperty]

        [JsonProperty("group")]
        public partial string Group { get; set; } = string.Empty;

        partial void OnGroupChanged(string value)
        {
            FilePath = value;
        }

        [ObservableProperty]

        [JsonProperty("id")]
        public partial long Identifier { get; set; }

        [ObservableProperty]

        [NotifyPropertyChangedFor(nameof(LastPrintTimeGeneralized))]
        [JsonProperty("lastPrintTime")]
        public partial double? LastPrintTime { get; set; }

        partial void OnLastPrintTimeChanged(double? value)
        {
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
        public partial string[] Materials { get; set; } = [];

        [ObservableProperty]

        [JsonProperty("name")]
        public partial string FileName { get; set; } = string.Empty;

        [ObservableProperty]

        [JsonProperty("notes")]
        public partial string Notes { get; set; } = string.Empty;

        [ObservableProperty]

        [NotifyPropertyChangedFor(nameof(PrintTimeGeneralized))]
        [JsonProperty("printTime")]
        public partial double PrintTime { get; set; }

        partial void OnPrintTimeChanged(double value)
        {
            PrintTimeGeneralized = TimeBaseConvertHelper.FromDoubleSeconds(value);
        }

        [ObservableProperty]

        public partial TimeSpan? PrintTimeGeneralized { get; set; }

        [ObservableProperty]

        [JsonProperty("printed")]
        public partial long Printed { get; set; }

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
        public partial double RadiusMove { get; set; }

        [ObservableProperty]

        [JsonProperty("repeat")]
        public partial int Repeat { get; set; }

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
        public partial double Volume { get; set; }

        [ObservableProperty]

        [JsonProperty("volumeUsage")]
        public partial double[] VolumeUsage { get; set; } = [];

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
        public partial long XMinView { get; set; }

        [ObservableProperty]

        [JsonProperty("yMax")]
        public partial double YMax { get; set; }

        [ObservableProperty]

        [JsonProperty("yMaxMove")]
        public partial double YMaxMove { get; set; }

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
        public partial long YMinView { get; set; }

        [ObservableProperty]

        [JsonProperty("zMax")]
        public partial double ZMax { get; set; }

        [ObservableProperty]

        [JsonProperty("zMin")]
        public partial long ZMin { get; set; }

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

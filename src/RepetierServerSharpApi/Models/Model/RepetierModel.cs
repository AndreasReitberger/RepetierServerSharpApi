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

        [ObservableProperty, JsonIgnore]
        Guid id;

        [ObservableProperty, JsonIgnore]
        GcodeTimeBaseTarget timeBaseTarget = GcodeTimeBaseTarget.DoubleSeconds;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("analysed")]
        long analysed;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("created")]
        long created;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("extruderUsage")]
        double[] extruderUsage = [];

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("filamentTotal")]
        double filament;

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
        [property: JsonProperty("id")]
        long identifier;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("lastPrintTime")]
        double lastPrintTime;

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
        string[] materials = [];

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("name")]
        string fileName = string.Empty;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("notes")]
        string notes = string.Empty;

        [ObservableProperty, JsonIgnore]
        [NotifyPropertyChangedFor(nameof(PrintTimeGeneralized))]
        [property: JsonProperty("printTime")]
        double printTime;
        partial void OnPrintTimeChanged(double value)
        {
            PrintTimeGeneralized = TimeBaseConvertHelper.FromDoubleSeconds(value);
        }

        [ObservableProperty, JsonIgnore]
        TimeSpan? printTimeGeneralized;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("printed")]
        long printed;

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
        double radiusMove;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("repeat")]
        int repeat;

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
        double volume;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("volumeUsage")]
        double[] volumeUsage = [];

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
        long xMinView;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("yMax")]
        double yMax;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("yMaxMove")]
        double yMaxMove;

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
        long yMinView;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("zMax")]
        double zMax;

        [ObservableProperty, JsonIgnore]
        [property: JsonProperty("zMin")]
        long zMin;

        #region Interface, unused

        [ObservableProperty, JsonIgnore]
        double modified;

        [ObservableProperty, JsonIgnore]
        string filePath = string.Empty;

        [ObservableProperty, JsonIgnore]
        string permissions = string.Empty;

        [ObservableProperty, JsonIgnore]
        long size;

        [ObservableProperty, JsonIgnore]
        IGcodeMeta? meta;
        #endregion

        #region JsonIgnore

        [ObservableProperty, JsonIgnore]
        bool isVisible;

        [ObservableProperty, JsonIgnore]
        bool isLoadingImage = false;

        [ObservableProperty, JsonIgnore]
        byte[] image = [];

        [ObservableProperty, JsonIgnore]
        byte[] thumbnail = [];

        [ObservableProperty, JsonIgnore]
        GcodeImageType imageType = GcodeImageType.Thumbnail;
            
        [ObservableProperty, JsonIgnore]
        string printerName = string.Empty;

        #endregion

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

        public object Clone() =>  MemberwiseClone();
        
        #endregion

    }
}

using AndreasReitberger.API.Print3dServer.Core.Enums;
using AndreasReitberger.API.Print3dServer.Core.Interfaces;
using AndreasReitberger.API.Print3dServer.Core.Utilities;
using Newtonsoft.Json;
using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierModel : ObservableObject, IGcode
    {
        #region Properties

        [ObservableProperty, JsonIgnore]
        [property: JsonIgnore]
        Guid id;

        [ObservableProperty, JsonIgnore]
        [property: JsonIgnore]
        GcodeTimeBaseTarget timeBaseTarget = GcodeTimeBaseTarget.DoubleSeconds;

        [ObservableProperty]
        [JsonProperty("analysed")]
        [property: JsonIgnore]
        long analysed;

        [ObservableProperty]
        [JsonProperty("created")]
        [property: JsonIgnore]
        long created;

        [ObservableProperty]
        [JsonProperty("extruderUsage")]
        [property: JsonIgnore]
        double[] extruderUsage;

        [ObservableProperty]
        [JsonProperty("filamentTotal")]
        [property: JsonIgnore]
        double filament;

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

        [ObservableProperty]
        [JsonProperty("id")]
        [property: JsonIgnore]
        long identifier;

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
        string[] materials;

        [ObservableProperty]
        [JsonProperty("name")]
        [property: JsonIgnore]
        string fileName;

        [ObservableProperty]
        [JsonProperty("notes")]
        [property: JsonIgnore]
        string notes;

        [ObservableProperty, JsonIgnore]
        [NotifyPropertyChangedFor(nameof(PrintTimeGeneralized))]
        [property: JsonProperty("printTime")]
        double printTime;
        partial void OnPrintTimeChanged(double value)
        {
            PrintTimeGeneralized = TimeBaseConvertHelper.FromDoubleSeconds(value);
        }

        [ObservableProperty]
        TimeSpan? printTimeGeneralized;

        [ObservableProperty]
        [JsonProperty("printed")]
        [property: JsonIgnore]
        long printed;

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
        double radiusMove;

        [ObservableProperty]
        [JsonProperty("repeat")]
        [property: JsonIgnore]
        int repeat;

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
        double volume;

        [ObservableProperty]
        [JsonProperty("volumeUsage")]
        [property: JsonIgnore]
        double[] volumeUsage;

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
        long xMinView;

        [ObservableProperty]
        [JsonProperty("yMax")]
        [property: JsonIgnore]
        double yMax;

        [ObservableProperty]
        [JsonProperty("yMaxMove")]
        [property: JsonIgnore]
        double yMaxMove;

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
        long yMinView;

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
        [property: JsonIgnore]
        double modified;

        [ObservableProperty, JsonIgnore]
        [property: JsonIgnore]
        string filePath;

        [ObservableProperty, JsonIgnore]
        [property: JsonIgnore]
        string permissions;

        [ObservableProperty, JsonIgnore]
        [property: JsonIgnore]
        long size;

        [ObservableProperty, JsonIgnore]
        [property: JsonIgnore]
        IGcodeMeta meta;
        #endregion

        #region JsonIgnore

        [ObservableProperty]
        [JsonIgnore]
        [property: JsonIgnore]
        bool isVisible;

        [ObservableProperty]
        [JsonIgnore]
        [property: JsonIgnore]
        bool isLoadingImage = false;

        [ObservableProperty]
        [JsonIgnore]
        [property: JsonIgnore]
        byte[] image = Array.Empty<byte>();

        [ObservableProperty]
        [JsonIgnore]
        [property: JsonIgnore]
        byte[] thumbnail = Array.Empty<byte>();

        [ObservableProperty]
        [JsonIgnore]
        [property: JsonIgnore]
        GcodeImageType imageType = GcodeImageType.Thumbnail;
            
        [ObservableProperty]
        [JsonIgnore]
        [property: JsonIgnore]
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

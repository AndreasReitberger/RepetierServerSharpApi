using AndreasReitberger.API.Print3dServer.Core.Enums;
using AndreasReitberger.API.Print3dServer.Core.Interfaces;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierModel : ObservableObject, IGcode
    {
        #region Properties

        [ObservableProperty, JsonIgnore]
        [property: JsonIgnore]
        Guid id;

        [ObservableProperty]
        [JsonProperty("analysed")]
        long analysed;

        [ObservableProperty]
        [JsonProperty("created")]
        long created;

        [ObservableProperty]
        [JsonProperty("extruderUsage")]
        double[] extruderUsage;

        [ObservableProperty]
        [JsonProperty("filamentTotal")]
        double filament;

        [ObservableProperty]
        [JsonProperty("fits")]
        bool fits;

        [ObservableProperty]
        [JsonProperty("gcodePatch")]
        string gcodePatch;

        [ObservableProperty]
        [JsonProperty("group")]
        string group;

        [ObservableProperty]
        [JsonProperty("id")]
        long identifier;

        [ObservableProperty]
        [JsonProperty("lastPrintTime")]
        double lastPrintTime;

        [ObservableProperty]
        [JsonProperty("layer")]
        long layer;

        [ObservableProperty]
        [JsonProperty("length")]
        long length;

        [ObservableProperty]
        [JsonProperty("lines")]
        long lines;

        [ObservableProperty]
        [JsonProperty("materials")]
        string[] materials;

        [ObservableProperty]
        [JsonProperty("name")]
        string fileName;

        [ObservableProperty]
        [JsonProperty("notes")]
        string notes;

        [ObservableProperty]
        [JsonProperty("printTime")]
        double printTime;

        [ObservableProperty]
        [JsonProperty("printed")]
        long printed;

        [ObservableProperty]
        [JsonProperty("printerParam1")]
        long printerParam1;

        [ObservableProperty]
        [JsonProperty("printerType")]
        long printerType;

        [ObservableProperty]
        [JsonProperty("radius")]
        double radius;

        [ObservableProperty]
        [JsonProperty("radiusMove")]
        double radiusMove;

        [ObservableProperty]
        [JsonProperty("repeat")]
        int repeat;

        [ObservableProperty]
        [JsonProperty("slicer")]
        string slicer;

        [ObservableProperty]
        [JsonProperty("state")]
        string state;

        [ObservableProperty]
        [JsonProperty("version")]
        long version;

        [ObservableProperty]
        [JsonProperty("volumeTotal")]
        double volume;

        [ObservableProperty]
        [JsonProperty("volumeUsage")]
        double[] volumeUsage;

        [ObservableProperty]
        [JsonProperty("volumetric")]
        bool volumetric;

        [ObservableProperty]
        [JsonProperty("xMax")]
        double xMax;

        [ObservableProperty]
        [JsonProperty("xMaxMove")]
        double xMaxMove;

        [ObservableProperty]
        [JsonProperty("xMaxView")]
        double xMaxView;

        [ObservableProperty]
        [JsonProperty("xMin")]
        long xMin;

        [ObservableProperty]
        [JsonProperty("xMinMove")]
        long xMinMove;

        [ObservableProperty]
        [JsonProperty("xMinView")]
        long xMinView;

        [ObservableProperty]
        [JsonProperty("yMax")]
        double yMax;

        [ObservableProperty]
        [JsonProperty("yMaxMove")]
        double yMaxMove;

        [ObservableProperty]
        [JsonProperty("yMaxView")]
        double yMaxView;

        [ObservableProperty]
        [JsonProperty("yMin")]
        long yMin;

        [ObservableProperty]
        [JsonProperty("yMinMove")]
        long yMinMove;

        [ObservableProperty]
        [JsonProperty("yMinView")]
        long yMinView;

        [ObservableProperty]
        [JsonProperty("zMax")]
        double zMax;

        [ObservableProperty]
        [JsonProperty("zMin")]
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
        bool isVisible;

        [ObservableProperty]
        [JsonIgnore]
        bool isLoadingImage = false;

        [ObservableProperty]
        [JsonIgnore]
        byte[] image = Array.Empty<byte>();

        [ObservableProperty]
        [JsonIgnore]
        byte[] thumbnail = Array.Empty<byte>();

        [ObservableProperty]
        [JsonIgnore]
        GcodeImageType imageType = GcodeImageType.Thumbnail;
            
        [ObservableProperty]
        [JsonIgnore]
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

using AndreasReitberger.Enum;
using Newtonsoft.Json;

namespace AndreasReitberger.Models
{
    public partial class RepetierModel
    {
        #region Properties
        [JsonIgnore]
        public bool IsVisible { get; set; }
        [JsonIgnore]
        public bool IsLoadingImage { get; set; } = false;
        [JsonIgnore]
        public byte[] Image { get; set; }
        [JsonIgnore]
        public byte[] Thumbnail { get; set; }
        [JsonIgnore]
        public RepetierImageType ImageType { get; set; } = RepetierImageType.Thumbnail;
        [JsonIgnore]
        public string PrinterName { get; set; } = string.Empty;


        [JsonProperty("analysed")]
        public long Analysed { get; set; }

        [JsonProperty("created")]
        public long Created { get; set; }

        [JsonProperty("extruderUsage")]
        public double[] ExtruderUsage { get; set; }

        [JsonProperty("filamentTotal")]
        public double FilamentTotal { get; set; }

        [JsonProperty("fits")]
        public bool Fits { get; set; }

        [JsonProperty("gcodePatch")]
        public string GcodePatch { get; set; }

        [JsonProperty("group")]
        public string Group { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("lastPrintTime")]
        public double LastPrintTime { get; set; }

        [JsonProperty("layer")]
        public long Layer { get; set; }

        [JsonProperty("length")]
        public long Length { get; set; }

        [JsonProperty("lines")]
        public long Lines { get; set; }

        [JsonProperty("materials")]
        public string[] Materials { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }

        [JsonProperty("printTime")]
        public double PrintTime { get; set; }

        [JsonProperty("printed")]
        public long Printed { get; set; }

        [JsonProperty("radius")]
        public double Radius { get; set; }

        [JsonProperty("radiusMove")]
        public double RadiusMove { get; set; }

        [JsonProperty("slicer")]
        public string Slicer { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("version")]
        public long Version { get; set; }

        [JsonProperty("volumeTotal")]
        public double VolumeTotal { get; set; }

        [JsonProperty("volumeUsage")]
        public double[] VolumeUsage { get; set; }

        [JsonProperty("volumetric")]
        public bool Volumetric { get; set; }

        [JsonProperty("xMax")]
        public double XMax { get; set; }

        [JsonProperty("xMaxMove")]
        public double XMaxMove { get; set; }

        [JsonProperty("xMin")]
        public long XMin { get; set; }

        [JsonProperty("xMinMove")]
        public long XMinMove { get; set; }

        [JsonProperty("yMax")]
        public double YMax { get; set; }

        [JsonProperty("yMaxMove")]
        public double YMaxMove { get; set; }

        [JsonProperty("yMin")]
        public long YMin { get; set; }

        [JsonProperty("yMinMove")]
        public long YMinMove { get; set; }

        [JsonProperty("zMax")]
        public double ZMax { get; set; }

        [JsonProperty("zMin")]
        public long ZMin { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion

    }
}

﻿using AndreasReitberger.API.Print3dServer.Core.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AndreasReitberger.API.Repetier.Models
{
    public partial class RepetierJobListItem : ObservableObject, IPrint3dJob
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
        [JsonProperty("done")]
        double? done;

        [ObservableProperty]
        [JsonProperty("extruderUsage")]
        List<double> extruderUsage;

        [ObservableProperty]
        [JsonProperty("filamentTotal")]
        double filamentTotal;

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
        partial void OnIdentifierChanged(long value)
        {
            jobId = value.ToString();
        }

        [ObservableProperty, JsonIgnore]
        [property: JsonIgnore]
        string jobId;

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
        List<string> materials;

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
        [JsonProperty("printedTimeComp")]
        long printedTimeComp;

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
        long radiusMove;

        [ObservableProperty]
        [JsonProperty("repeat")]
        long repeat;

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
        double volumeTotal;

        [ObservableProperty]
        [JsonProperty("volumeUsage")]
        List<double> volumeUsage;

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
        double xMinView;

        [ObservableProperty]
        [JsonProperty("yMax")]
        double yMax;

        [ObservableProperty]
        [JsonProperty("yMaxMove")]
        long yMaxMove;

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
        double yMinView;

        [ObservableProperty]
        [JsonProperty("zMax")]
        double zMax;

        [ObservableProperty]
        [JsonProperty("zMin")]
        long zMin;

        #region Interface, unused

        [ObservableProperty, JsonIgnore]
        [property: JsonIgnore]
        double? timeAdded = 0;

        [ObservableProperty, JsonIgnore]
        [property: JsonIgnore]
        double? timeInQueue = 0;
        #endregion

        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

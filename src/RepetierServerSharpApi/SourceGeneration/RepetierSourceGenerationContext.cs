using AndreasReitberger.API.Print3dServer.Core;
using AndreasReitberger.API.Print3dServer.Core.Events;
using AndreasReitberger.API.Print3dServer.Core.Exceptions;
using AndreasReitberger.API.Repetier.Models;
using AndreasReitberger.API.Repetier.TypeConverters;
using AndreasReitberger.API.REST.SourceGeneration;
using System;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.Repetier.SourceGeneration
{
    [JsonSerializable(typeof(RepetierModel))]
    [JsonSerializable(typeof(RepetierModelGroup))]
    [JsonSerializable(typeof(RepetierModelList))]
    [JsonSerializable(typeof(RepetierPrinter))]
    [JsonSerializable(typeof(RepetierPrinterFan))]
    [JsonSerializable(typeof(RepetierPrinterHeaterComponent))]
    [JsonSerializable(typeof(RepetierPrinterInfo))]
    [JsonSerializable(typeof(RepetierPrinterInfoRespone))]
    [JsonSerializable(typeof(RepetierPrinterListRespone))]
    [JsonSerializable(typeof(RepetierPrinterState))]
    [JsonSerializable(typeof(RepetierPrinterStateGlobal))]
    [JsonSerializable(typeof(RepetierPrinterToolhead))]
    // Can be removed once `Print3dCoreSourceGenerationContext` is available in the Core project and used as base class for this context
    [JsonSerializable(typeof(ActivePrinterChangedEventArgs))]
    [JsonSerializable(typeof(ActivePrintImageChangedEventArgs))]
    [JsonSerializable(typeof(FanChangedEventArgs))]
    [JsonSerializable(typeof(FansChangedEventArgs))]
    [JsonSerializable(typeof(GcodeGroupsChangedEventArgs))]
    [JsonSerializable(typeof(GcodesChangedEventArgs))]
    [JsonSerializable(typeof(HeaterChangedEventArgs))]
    [JsonSerializable(typeof(HeatersChangedEventArgs))]
    [JsonSerializable(typeof(IgnoredJsonResultsChangedEventArgs))]
    [JsonSerializable(typeof(IsPrintingStateChangedEventArgs))]
    [JsonSerializable(typeof(JobFinishedEventArgs))]
    [JsonSerializable(typeof(JobListChangedEventArgs))]
    [JsonSerializable(typeof(JobsChangedEventArgs))]
    [JsonSerializable(typeof(JobStartedEventArgs))]
    [JsonSerializable(typeof(JobStatusChangedEventArgs))]
    [JsonSerializable(typeof(JobStatusFinishedEventArgs))]
    [JsonSerializable(typeof(Print3dBaseEventArgs))]
    [JsonSerializable(typeof(PrintersChangedEventArgs))]
    [JsonSerializable(typeof(SensorsChangedEventArgs))]
    [JsonSerializable(typeof(TemperatureDataEventArgs))]
    [JsonSerializable(typeof(ToolheadChangedEventArgs))]
    [JsonSerializable(typeof(ToolheadsChangedEventArgs))]
    [JsonSerializable(typeof(WebCamConfigChangedEventArgs))]
    [JsonSerializable(typeof(WebCamConfigsChangedEventArgs))]
    [JsonSerializable(typeof(ServerNotReachableException))]
    [JsonSerializable(typeof(Print3dServerClient))]
    [JsonSourceGenerationOptions(WriteIndented = true, 
        Converters = new Type[] { 
            typeof(AuthenticationHeaderConverter),
            typeof(RepetierModelConverter),
            typeof(RepetierModelGroupConverter),
            typeof(RepetierJobConverter),
            typeof(RepetierToolheadConverter),
            typeof(RepetierHeaterConverter),
            typeof(RepetierTemperatureInfoConverter),
            typeof(RepetierPrintInfoConverter),
        }, ReferenceHandler =
#if DEBUG
        JsonKnownReferenceHandler.Preserve
#else
        JsonKnownReferenceHandler.IgnoreCycles
#endif
        )]
    public partial class RepetierSourceGenerationContext : RestSourceGenerationContext //Print3dCoreSourceGenerationContext
    {
    }
}

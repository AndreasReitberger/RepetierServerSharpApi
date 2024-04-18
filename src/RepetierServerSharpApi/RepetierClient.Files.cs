using AndreasReitberger.API.Print3dServer.Core.Enums;
using AndreasReitberger.API.Print3dServer.Core.Events;
using AndreasReitberger.API.Print3dServer.Core.Interfaces;
using AndreasReitberger.API.Repetier.Models;
using AndreasReitberger.API.Repetier.Structs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace AndreasReitberger.API.Repetier
{
    public partial class RepetierClient
    {
        #region Methods

        #region Files
        public override Task<ObservableCollection<IGcode>> GetFilesAsync() => GetModelsAsync(GetActivePrinterSlug(), GcodeImageType.Thumbnail, null);

        public async Task<ObservableCollection<IGcode>> GetModelsAsync(
            string PrinterName = "",
            GcodeImageType ImageType = GcodeImageType.Thumbnail,
            IProgress<int>? Prog = null)
        {
            try
            {
                ObservableCollection<IGcode> modelDatas = new();
                if (!IsReady)
                    return modelDatas;

                string currentPrinter = string.IsNullOrEmpty(PrinterName) ? GetActivePrinterSlug() : PrinterName;
                if (string.IsNullOrEmpty(currentPrinter)) return modelDatas;

                // Reporting
                Prog?.Report(0);
                RepetierModelList? models = await GetModelListInfoResponeAsync(currentPrinter).ConfigureAwait(false);
                if (models is not null)
                {
                    List<RepetierModel> modelList = models.Data;
                    if (modelList is not null)
                    {
                        ObservableCollection<IGcode> Models = new(modelList);
                        if (ImageType != GcodeImageType.None)
                        {
                            int total = Models.Count;
                            for (int i = 0; i < total; i++)
                            {
                                IGcode model = Models[i];
                                model.PrinterName = currentPrinter;
                                model.ImageType = ImageType;
                                // Load image depending on settings
                                switch (ImageType)
                                {
                                    // Blocks thread, however async download leads to bad requestes
                                    case GcodeImageType.Thumbnail:
                                        model.Thumbnail = await GetDynamicRenderImageAsync(model.Identifier, true).ConfigureAwait(false);
                                        break;
                                    case GcodeImageType.Image:
                                        model.Image = await GetDynamicRenderImageAsync(model.Identifier, false).ConfigureAwait(false);
                                        break;
                                    default:
                                        model.Thumbnail = await GetDynamicRenderImageAsync(model.Identifier, true).ConfigureAwait(false);
                                        model.Image = await GetDynamicRenderImageAsync(model.Identifier, false).ConfigureAwait(false);
                                        break;
                                }
                                if (Prog is not null)
                                {
                                    float progress = ((float)i / total) * 100f;
                                    if (i < total - 1)
                                    {
                                        Prog.Report(Convert.ToInt32(progress));
                                    }
                                    else
                                    {
                                        Prog.Report(100);
                                    }
                                }
                            }
                        }
                        else
                        {
                            Prog?.Report(100);
                        }
                        return Models;
                    }
                }

                Prog?.Report(100);

                return modelDatas;
            }
            catch (Exception exc)
            {
                Prog?.Report(100);
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return [];
            }
        }

        public async Task<Dictionary<long, byte[]>?> GetModelImagesAsync(ObservableCollection<IGcode> models, GcodeImageType imageType = GcodeImageType.Thumbnail)
        {
            string currentPrinter = GetActivePrinterSlug();
            if (string.IsNullOrEmpty(currentPrinter)) return null;
            Dictionary<long, byte[]> result = [];
            try
            {
                for (int i = 0; i < models.Count; i++)
                {
                    IGcode model = models[i];
                    byte[] image = [];
                    image = imageType switch
                    {
                        GcodeImageType.Thumbnail => await GetDynamicRenderImageAsync(model.Identifier, true).ConfigureAwait(false),
                        GcodeImageType.Image => await GetDynamicRenderImageAsync(model.Identifier, false).ConfigureAwait(false),
                        _ => throw new NotSupportedException($"The image type '{imageType}' is not supported here."),
                    };
                    result.Add(model.Identifier, image);
                }
                return result;
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return null;
            }
        }
        public async Task<bool> DeleteModelFromServerAsync(IGcode model)
        {
            string currentPrinter = GetActivePrinterSlug();
            if (string.IsNullOrEmpty(currentPrinter)) return false;

            try
            {
                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{currentPrinter}";
                IRestApiRequestRespone? result = await SendRestApiRequestAsync(
                       requestTargetUri: targetUri,
                       method: Method.Post,
                       command: "removeModel",
                       jsonObject: new { id = model.Identifier },
                       authHeaders: AuthHeaders
                       )
                    .ConfigureAwait(false);
                return GetQueryResult(result?.Result);
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return false;
            }
        }

        public async Task RefreshModelsAsync(GcodeImageType imageType = GcodeImageType.Thumbnail, IProgress<int>? prog = null)
        {
            try
            {
                ObservableCollection<IGcode> modelDatas = new();
                if (!IsReady || ActivePrinter == null)
                {
                    Files = modelDatas;
                    return;
                }
                Files = await GetModelsAsync(GetActivePrinterSlug(), imageType, prog).ConfigureAwait(false);
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                Files = new ObservableCollection<IGcode>();
            }
        }
        public async Task<bool> CopyModelToPrintQueueAsync(IGcode model, bool startPrintIfPossible = true)
        {
            string currentPrinter = GetActivePrinterSlug();
            if (string.IsNullOrEmpty(currentPrinter))
            {
                return false;
            }

            try
            {
                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{currentPrinter}";
                IRestApiRequestRespone? result = await SendRestApiRequestAsync(
                       requestTargetUri: targetUri,
                       method: Method.Post,
                       command: "removeModel",
                       jsonObject: new { id = model.Identifier, autostart = (startPrintIfPossible ? "true" : "false") },
                       authHeaders: AuthHeaders
                       )
                    .ConfigureAwait(false);
                /*
                RepetierApiRequestRespone result =
                    await SendRestApiRequestAsync(
                        RepetierCommandBase.printer, RepetierCommandFeature.api,
                        command: "copyModel", jsonData: $"{{\"id\":{model.Identifier}, \"autostart\":{(startPrintIfPossible ? "true" : "false")}}}",
                        printerName: currentPrinter)
                    .ConfigureAwait(false);
                */
                await RefreshJobListAsync().ConfigureAwait(false);
                return GetQueryResult(result?.Result, true);
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return false;
            }
        }

        public async Task UpdateFreeSpaceAsync()
        {
            IRestApiRequestRespone? result = null;
            try
            {
                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{GetActivePrinterSlug()}";
                result = await SendRestApiRequestAsync(
                   requestTargetUri: targetUri,
                   method: Method.Post,
                   command: "freeSpace",
                   authHeaders: AuthHeaders
                   )
                .ConfigureAwait(false);

                RepetierFreeSpaceRespone? space = GetObjectFromJson<RepetierFreeSpaceRespone>(result?.Result);
                if (space is not null)
                {
                    FreeDiskSpace = space.Free;
                    TotalDiskSpace = space.Capacity;
                    UsedDiskSpace = TotalDiskSpace - space.Available;
                }
            }
            catch (JsonException jecx)
            {
                OnError(new JsonConvertEventArgs()
                {
                    Exception = jecx,
                    OriginalString = result?.Result,
                    TargetType = nameof(RepetierFreeSpaceRespone),
                    Message = jecx.Message,
                });
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
            }
        }
        public Task RefreshDiskSpaceAsync() => UpdateFreeSpaceAsync();

        public override async Task<byte[]?> DownloadFileAsync(string relativeFilePath)
        {
            try
            {
                string uri = $"{FullWebAddress}/server/files/{relativeFilePath}";
                byte[]? file = await DownloadFileFromUriAsync(uri)
                    .ConfigureAwait(false)
                    ;
                return file;
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return null;
            }
        }
        #endregion

        #region Groups
        public async Task<bool> AddModelGroupAsync(IGcodeGroup? group) => await AddModelGroupAsync(group?.Name);

        public async Task<bool> AddModelGroupAsync(string? groupName)
        {
            string currentPrinter = GetActivePrinterSlug();
            if (string.IsNullOrEmpty(currentPrinter) || groupName is null) return false;

            try
            {
                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{currentPrinter}";
                object data = new
                {
                    groupName = groupName,
                };
                IRestApiRequestRespone? result = await SendRestApiRequestAsync(
                   requestTargetUri: targetUri,
                   method: Method.Post,
                   command: "addModelGroup",
                   jsonObject: data,
                   authHeaders: AuthHeaders
                   )
                .ConfigureAwait(false);
                /*
                RepetierApiRequestRespone result =
                    await SendRestApiRequestAsync(
                        RepetierCommandBase.printer, RepetierCommandFeature.api,
                        command: "addModelGroup", jsonData: string.Format("{{\"groupName\":\"{0}\"}}", groupName),
                        printerName: currentPrinter)
                    .ConfigureAwait(false);
                */
                return GetQueryResult(result?.Result);
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return false;
            }
        }
        public async Task<bool> AddModelGroupAsync(string printerName, IGcodeGroup group) => await AddModelGroupAsync(printerName, group.Name);
        public async Task<bool> AddModelGroupAsync(string printerName, string groupName)
        {
            try
            {
                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{printerName}";
                object data = new
                {
                    groupName = groupName,
                };
                IRestApiRequestRespone? result = await SendRestApiRequestAsync(
                   requestTargetUri: targetUri,
                   method: Method.Post,
                   command: "addModelGroup",
                   jsonObject: data,
                   authHeaders: AuthHeaders
                   )
                .ConfigureAwait(false);
                /*
                RepetierApiRequestRespone result =
                    await SendRestApiRequestAsync(
                        RepetierCommandBase.printer, RepetierCommandFeature.api,
                        command: "addModelGroup", jsonData: string.Format("{{\"groupName\":\"{0}\"}}", groupName),
                        printerName: printerName)
                    .ConfigureAwait(false);
                */
                return GetQueryResult(result?.Result);
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return false;
            }
        }

        public async Task<bool> RemoveModelGroupAsync(IGcodeGroup group) => await RemoveModelGroupAsync(group.Name);
        public async Task<bool> RemoveModelGroupAsync(string groupName)
        {
            string currentPrinter = GetActivePrinterSlug();
            if (string.IsNullOrEmpty(currentPrinter)) return false;

            try
            {
                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{currentPrinter}";
                object data = new
                {
                    groupName = groupName,
                };
                IRestApiRequestRespone? result = await SendRestApiRequestAsync(
                   requestTargetUri: targetUri,
                   method: Method.Post,
                   command: "delModelGroup",
                   jsonObject: data,
                   authHeaders: AuthHeaders
                   )
                .ConfigureAwait(false);
                /*
                RepetierApiRequestRespone result =
                    await SendRestApiRequestAsync(
                        RepetierCommandBase.printer, RepetierCommandFeature.api,
                        command: "delModelGroup", jsonData: string.Format("{{\"groupName\":\"{0}\"}}", groupName),
                        printerName: currentPrinter)
                    .ConfigureAwait(false);
                */
                return GetQueryResult(result?.Result);
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return false;
            }
        }

        public async Task<bool> RemoveModelGroupAsync(string printerName, IGcodeGroup group) => await RemoveModelGroupAsync(printerName, group.Name);
        public async Task<bool> RemoveModelGroupAsync(string printerName, string groupName)
        {
            try
            {
                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{printerName}";
                object data = new
                {
                    groupName = groupName,
                };
                IRestApiRequestRespone? result = await SendRestApiRequestAsync(
                   requestTargetUri: targetUri,
                   method: Method.Post,
                   command: "delModelGroup",
                   jsonObject: data,
                   authHeaders: AuthHeaders
                   )
                .ConfigureAwait(false);
                /*
                RepetierApiRequestRespone result =
                    await SendRestApiRequestAsync(
                        RepetierCommandBase.printer, RepetierCommandFeature.api,
                        command: "delModelGroup", jsonData: string.Format("{{\"groupName\":\"{0}\"}}", groupName),
                        printerName: printerName)
                    .ConfigureAwait(false);
                */
                return GetQueryResult(result?.Result);
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return false;
            }
        }

        public async Task<bool> MoveModelToGroupAsync(string groupName, IGcode file) => await MoveModelToGroupAsync(groupName, file.Identifier);
        public async Task<bool> MoveModelToGroupAsync(string groupName, long id)
        {
            string currentPrinter = GetActivePrinterSlug();
            if (string.IsNullOrEmpty(currentPrinter)) return false;

            try
            {
                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{currentPrinter}";
                object data = new
                {
                    groupName = groupName,
                    id = id,
                };
                IRestApiRequestRespone? result = await SendRestApiRequestAsync(
                   requestTargetUri: targetUri,
                   method: Method.Post,
                   command: "moveModelFileToGroup",
                   jsonObject: data,
                   authHeaders: AuthHeaders
                   )
                .ConfigureAwait(false);
                /*
                RepetierApiRequestRespone result =
                    await SendRestApiRequestAsync(
                        RepetierCommandBase.printer, RepetierCommandFeature.api,
                        command: "moveModelFileToGroup", jsonData: string.Format("{{\"groupName\":\"{0}\", \"id\":{1}}}", groupName, id),
                        printerName: currentPrinter
                        )
                    .ConfigureAwait(false);
                */
                return GetQueryResult(result?.Result);
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return false;
            }
        }
        public async Task<bool> MoveModelToGroupAsync(string printerName, IGcodeGroup group, IGcode file) => await MoveModelToGroupAsync(printerName, group.Name, file.Identifier);
        public async Task<bool> MoveModelToGroupAsync(string printerName, string groupName, long id)
        {
            try
            {
                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{GetActivePrinterSlug()}";
                IRestApiRequestRespone? result = await SendRestApiRequestAsync(
                       requestTargetUri: targetUri,
                       method: Method.Post,
                        command: "removeModel",
                       jsonObject: new { groupName = groupName, id = id },
                       authHeaders: AuthHeaders
                       )
                    .ConfigureAwait(false);
                /*
                RepetierApiRequestRespone result =
                    await SendRestApiRequestAsync(
                        RepetierCommandBase.printer, RepetierCommandFeature.api,
                        command: "moveModelFileToGroup", jsonData: string.Format("{{\"groupName\":\"{0}\", \"id\":{1}}}", groupName, id),
                        printerName: printerName
                        )
                    .ConfigureAwait(false);
                */
                return GetQueryResult(result?.Result);
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return false;
            }
        }

        public async Task<ObservableCollection<IGcodeGroup>> GetModelGroupsAsync()
        {
            IRestApiRequestRespone? result = null;
            ObservableCollection<IGcodeGroup> resultObject = new();

            string currentPrinter = GetActivePrinterSlug();
            if (string.IsNullOrEmpty(currentPrinter)) return resultObject;

            try
            {
                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{currentPrinter}";
                result = await SendRestApiRequestAsync(
                       requestTargetUri: targetUri,
                       method: Method.Post,
                       command: "listModelGroups",
                       jsonObject: null,
                       authHeaders: AuthHeaders
                       )
                    .ConfigureAwait(false);
                /*
                result = await SendRestApiRequestAsync(
                   commandBase: RepetierCommandBase.printer,
                   commandFeature: RepetierCommandFeature.api, 
                   command: "listModelGroups", 
                   printerName: currentPrinter)
                    .ConfigureAwait(false);
                */
                RepetierModelGroups? info = GetObjectFromJson<RepetierModelGroups>(result?.Result);
                return info is not null && info.GroupNames is not null ? new ObservableCollection<IGcodeGroup>(info.GroupNames.Select(g => new RepetierModelGroup() { Name = g })) : resultObject;
            }
            catch (JsonException jecx)
            {
                OnError(new JsonConvertEventArgs()
                {
                    Exception = jecx,
                    OriginalString = result?.Result,
                    TargetType = nameof(String),
                    Message = jecx.Message,
                });
                return [];
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return resultObject;
            }
        }
        public async Task RefreshModelGroupsAsync()
        {
            try
            {
                ObservableCollection<IGcodeGroup> groups = [];
                if (!IsReady || ActivePrinter == null)
                {
                    Groups = groups;
                    return;
                }

                string currentPrinter = ActivePrinter.Slug;
                if (string.IsNullOrEmpty(currentPrinter)) return;

                RepetierModelGroups? result = await GetModelGroupsAsync(currentPrinter).ConfigureAwait(false);
                if (result is not null)
                {
                    Groups = new ObservableCollection<IGcodeGroup>(result.GroupNames.Select(g => new RepetierModelGroup() { Name = g }));
                }
                else Groups = groups;

            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                Groups = [];
            }
        }
        #endregion

        #endregion
    }
}

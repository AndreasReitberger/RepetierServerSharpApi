using AndreasReitberger.API.Print3dServer.Core.Enums;
using AndreasReitberger.API.Print3dServer.Core.Interfaces;
using AndreasReitberger.API.Repetier.Models;
using AndreasReitberger.API.Repetier.Structs;
using AndreasReitberger.API.REST.Events;
using AndreasReitberger.API.REST.Interfaces;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndreasReitberger.API.Repetier
{
    public partial class RepetierClient
    {
        #region Methods

        #region Files
        public override Task<List<IGcode>> GetFilesAsync() => GetModelsAsync(GetActivePrinterSlug(), GcodeImageType.Thumbnail, null);

        public async Task<List<IGcode>> GetModelsAsync(
            string PrinterName = "",
            GcodeImageType ImageType = GcodeImageType.Thumbnail,
            IProgress<int>? Prog = null)
        {
            try
            {
                List<IGcode> modelDatas = [];
                if (!IsReady)
                    return modelDatas;

                string currentPrinter = string.IsNullOrEmpty(PrinterName) ? GetActivePrinterSlug() : PrinterName;
                if (string.IsNullOrEmpty(currentPrinter)) return modelDatas;

                // Reporting
                Prog?.Report(0);
                RepetierModelList? modelInfo = await GetModelListInfoResponeAsync(currentPrinter).ConfigureAwait(false);
                if (modelInfo is not null)
                {
                    List<RepetierModel> modelList = modelInfo.Data;
                    if (modelList is not null)
                    {
                        List<IGcode> models = [.. modelList];
                        if (ImageType != GcodeImageType.None)
                        {
                            int lastProgres = -1;
                            int total = models.Count;
                            for (int i = 0; i < total; i++)
                            {
                                IGcode model = models[i];
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
                                        if (Math.Round(progress, 0) > lastProgres)
                                        {
                                            int reportedProgress = Convert.ToInt32(progress);
                                            Prog.Report(reportedProgress);
                                            lastProgres = reportedProgress;
                                        }
                                    }
                                    else
                                    {
                                        Prog.Report(100);
                                    }
                                }
                                await Task.Delay(1);
                            }
                        }
                        else
                        {
                            Prog?.Report(100);
                        }
                        return models;
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

        async Task<RepetierModelList?> GetModelListInfoResponeAsync(string printerName)
        {
            IRestApiRequestRespone? result = null;
            try
            {
                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{printerName}";
                result = await SendRestApiRequestAsync(
                   requestTargetUri: targetUri,
                   method: Method.Post,
                   command: "listModels",
                   body: null,
                   authHeaders: AuthHeaders
                   )
                .ConfigureAwait(false);
                RepetierModelList? list = GetObjectFromJsonSystem<RepetierModelList>(result?.Result, DefaultJsonSerializerSettings);
                await UpdateFreeSpaceAsync().ConfigureAwait(false);

                return list;
            }
            catch (JsonException jecx)
            {
                OnError(new JsonConvertEventArgs()
                {
                    Exception = jecx,
                    OriginalString = result?.Result,
                    TargetType = nameof(RepetierModelList),
                    Message = jecx.Message,
                });
                return new RepetierModelList();
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return new RepetierModelList();
            }
        }

        public async Task<Dictionary<long, byte[]>?> GetModelImagesAsync(
            IList<IGcode> models, GcodeImageType imageType = GcodeImageType.Thumbnail, IProgress<int>? Prog = null
            )
        {
            string currentPrinter = GetActivePrinterSlug();
            if (string.IsNullOrEmpty(currentPrinter) || imageType == GcodeImageType.None) return null;
            Dictionary<long, byte[]> result = [];
            try
            {
                int lastProgres = -1;
                int total = models.Count;
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
                    // Update progress
                    if (Prog is not null)
                    {
                        float progress = ((float)i / total) * 100f;
                        if (i < total - 1)
                        {
                            if (Math.Round(progress, 0) > lastProgres)
                            {
                                int reportedProgress = Convert.ToInt32(progress);
                                Prog.Report(reportedProgress);
                                lastProgres = reportedProgress;
                            }
                        }
                        else
                        {
                            Prog.Report(100);
                        }
                    }
                }
                //Prog?.Report(100);
                return result;
            }
            catch (Exception exc)
            {
                Prog?.Report(100);
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return null;
            }
        }

        public async Task<IList<IGcode>?> UpdateModelImagesAsync(
            IList<IGcode> models, GcodeImageType imageType = GcodeImageType.Thumbnail, IProgress<int>? Prog = null
            )
        {
            string currentPrinter = GetActivePrinterSlug();
            if (string.IsNullOrEmpty(currentPrinter) || imageType == GcodeImageType.None) return null;
            IList<IGcode> result = models;
            try
            {
                int lastProgres = -1;
                int total = models.Count;
                for (int i = 0; i < models.Count; i++)
                {
                    IGcode model = models[i];
                    byte[] image = [];
                    switch (imageType)
                    {
                        case GcodeImageType.Thumbnail:
                            image = await GetDynamicRenderImageAsync(model.Identifier, true).ConfigureAwait(false);
                            model.Thumbnail = image;
                            break;
                        case GcodeImageType.Image:
                            image = await GetDynamicRenderImageAsync(model.Identifier, false).ConfigureAwait(false);
                            model.Image = image;
                            break;
                        default:
                            throw new NotSupportedException($"The image type '{imageType}' is not supported here.");
                    }
                    // Update progress
                    if (Prog is not null)
                    {
                        float progress = ((float)i / total) * 100f;
                        if (i < total - 1)
                        {
                            if (Math.Round(progress, 0) > lastProgres)
                            {
                                int reportedProgress = Convert.ToInt32(progress);
                                Prog.Report(reportedProgress);
                                lastProgres = reportedProgress;
                            }
                        }
                        else
                        {
                            Prog.Report(100);
                        }
                    }
                }
                //Prog?.Report(100);
                return result;
            }
            catch (Exception exc)
            {
                Prog?.Report(100);
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
                       body: new { id = model.Identifier },
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
                List<IGcode> modelDatas = [];
                if (!IsReady || ActivePrinter == null)
                {
                    Files = [.. modelDatas];
                    return;
                }
                List<IGcode> files = await GetModelsAsync(GetActivePrinterSlug(), imageType, prog).ConfigureAwait(false);
                Files = [.. files];
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                Files = [];
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
                       body: new { id = model.Identifier, autostart = (startPrintIfPossible ? "true" : "false") },
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

                RepetierFreeSpaceRespone? space = GetObjectFromJsonSystem<RepetierFreeSpaceRespone>(result?.Result, DefaultJsonSerializerSettings);
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

        /// <summary>
        /// Downloads the file by using a <c>HttpClient</c> instead of the rest api.
        /// </summary>
        /// <param name="fid">The file id to be downloaded</param>
        /// <returns>File as <c>byte[]</c></returns>
        public async Task<byte[]?> DownloadGcodeAsync(string fid)
        {
            try
            {
#if NET6_0_OR_GREATER
                ArgumentNullException.ThrowIfNull(fid, nameof(fid));
#endif
                // Example: http://IP/printer/model/PRINTER?a=download&id=FILEID
                string uri = $"{FullWebAddress}/printer/model/{ActivePrinter?.Name.Replace(" ", "_")}?a=download&id={fid}";
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

        /// <summary>
        /// Downloads the file by using a <c>HttpClient</c> instead of the rest api.
        /// </summary>
        /// <param name="gcode">The file to be downloaded</param>
        /// <returns>File as <c>byte[]</c></returns>
        public Task<byte[]?> DownloadGcodeAsync(IGcode gcode)
            => DownloadGcodeAsync(fid: $"{gcode.Identifier}");

        /// <summary>
        /// Downloads the gocde file as string by its file Id.
        /// </summary>
        /// <param name="fileId">The file Id to be downloaded</param>
        /// <param name="printerName">The printer name (if not provided, ActivePrinter is used)</param>
        /// <returns>File as <c>byte[]</c></returns>
        public async Task<string?> DownloadGcodeAsync(string fileId, string printerName = "")
        {
            IRestApiRequestRespone? result = null;
            string resultString = string.Empty;
            string? resultObject = null;

            string currentPrinter = string.IsNullOrEmpty(printerName) ? GetActivePrinterSlug() : printerName;
            if (string.IsNullOrEmpty(currentPrinter))
            {
                return resultObject;
            }
            try
            {
                if (!IsReady)
                {
                    return null;
                }
                // Example: http://IP/printer/model/PRINTER?a=download&id=FILEID
                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Model}/{currentPrinter}";
                result = await SendRestApiRequestAsync(
                       requestTargetUri: targetUri,
                       method: Method.Get,
                       command: "",
                       body: null,
                       authHeaders: AuthHeaders,
                       urlSegments: [new ("a", "download"), new ("id", $"{fileId}")])
                    .ConfigureAwait(false);
                return result?.Result;
            }
            catch (JsonException jecx)
            {
                OnError(new JsonConvertEventArgs()
                {
                    Exception = jecx,
                    OriginalString = resultString,
                    Message = jecx.Message,
                });
                return null;
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return null;
            }
        }

        /// <summary>
        /// Downloads the gocde file as string.
        /// </summary>
        /// <param name="gcode">The file to be downloaded</param>
        /// <param name="printerName">The printer name (if not provided, ActivePrinter is used)</param>
        /// <returns>File as <c>byte[]</c></returns>
        public Task<string?> DownloadGcodeAsync(IGcode gcode, string printerName = "")
            => DownloadGcodeAsync(fileId: $"{gcode.Identifier}", printerName: printerName);

        /// <summary>
        /// Downloads the gocde file as text and converts it with the provided encoding.
        /// </summary>
        /// <param name="gcode">The gcode file for the download</param>
        /// <param name="encoding">The encoding, default is <c>Encoding.Default</c></param>
        /// <param name="printerName">The printer name (if not provided, ActivePrinter is used)</param>
        /// <returns>File as <c>byte[]</c></returns>
        public async Task<byte[]?> DownloadGcodeAsync(IGcode gcode, Encoding? encoding = null, string printerName = "")
        {
            string? resultString = string.Empty;
            encoding ??= Encoding.Default;
            resultString = await DownloadGcodeAsync(gcode, printerName).ConfigureAwait(false);
            return resultString is not null ? encoding?.GetBytes(resultString) : null;
        }
        #endregion

        #region Groups

        [Obsolete("Use GetModelGroupsAsync() instead")]
        async Task<RepetierModelGroups?> GetModelGroupsFromPrinterAsync(string printerName)
        {
            IRestApiRequestRespone? result = null;
            try
            {
                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{printerName}";
                result = await SendRestApiRequestAsync(
                   requestTargetUri: targetUri,
                   method: Method.Post,
                   command: "listModelGroups",
                   body: null,
                   authHeaders: AuthHeaders
                   )
                .ConfigureAwait(false);
                return GetObjectFromJson<RepetierModelGroups>(result?.Result);
            }
            catch (JsonException jecx)
            {
                OnError(new JsonConvertEventArgs()
                {
                    Exception = jecx,
                    OriginalString = result?.Result,
                    TargetType = nameof(RepetierModelGroups),
                    Message = jecx.Message,
                });
                return new RepetierModelGroups();
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return new RepetierModelGroups();
            }
        }
        /// <summary>
        /// Gets the model groups from the server.
        /// </summary>
        /// <param name="path">Printer name</param>
        /// <returns>Available groups</returns>
        public override async Task<List<IGcodeGroup>> GetModelGroupsAsync(string path = "")
        {
            IRestApiRequestRespone? result = null;
            List<IGcodeGroup> resultObject = [];

            string currentPrinter = !string.IsNullOrEmpty(path) ? path : GetActivePrinterSlug();
            if (string.IsNullOrEmpty(currentPrinter)) return resultObject;

            try
            {
                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{currentPrinter}";
                result = await SendRestApiRequestAsync(
                       requestTargetUri: targetUri,
                       method: Method.Post,
                       command: "listModelGroups",
                       body: null,
                       authHeaders: AuthHeaders
                       )
                    .ConfigureAwait(false);
                RepetierModelGroups? info = GetObjectFromJsonSystem<RepetierModelGroups>(result?.Result, DefaultJsonSerializerSettings);
                return
                    info is not null && info.GroupNames is not null ?
                    [.. info.GroupNames.Select(g => new RepetierModelGroup() { Name = g })] : resultObject;
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
                List<IGcodeGroup> groups = [];
                if (!IsReady || ActivePrinter == null)
                {
                    Groups = [.. groups];
                    return;
                }

                string currentPrinter = ActivePrinter.Slug;
                if (string.IsNullOrEmpty(currentPrinter)) return;

                groups = await GetModelGroupsAsync(path: currentPrinter).ConfigureAwait(false);
                Groups = [.. groups];
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                Groups = [];
            }
        }
        public Task<bool> AddModelGroupAsync(IGcodeGroup? group) => AddModelGroupAsync(group?.Name);

        public async Task<bool> AddModelGroupAsync(string? groupName)
        {
            string currentPrinter = GetActivePrinterSlug();
            if (string.IsNullOrEmpty(currentPrinter) || groupName is null) return false;

            try
            {
                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{currentPrinter}";
                object data = new
                {
                    groupName,
                };
                IRestApiRequestRespone? result = await SendRestApiRequestAsync(
                   requestTargetUri: targetUri,
                   method: Method.Post,
                   command: "addModelGroup",
                   body: null,
                   authHeaders: AuthHeaders,
                   urlSegments: [new ("data", JsonConvert.SerializeObject(data))]
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
        public Task<bool> AddModelGroupAsync(string printerName, IGcodeGroup group) => AddModelGroupAsync(printerName, group.Name);
        public async Task<bool> AddModelGroupAsync(string printerName, string groupName)
        {
            try
            {
                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{printerName}";
                object data = new
                {
                    groupName,
                };
                IRestApiRequestRespone? result = await SendRestApiRequestAsync(
                   requestTargetUri: targetUri,
                   method: Method.Post,
                   command: "addModelGroup",
                   body: null,
                   authHeaders: AuthHeaders,
                   urlSegments: [new("data", JsonConvert.SerializeObject(data))]
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

        public Task<bool> RemoveModelGroupAsync(IGcodeGroup group) => RemoveModelGroupAsync(group.Name);
        public async Task<bool> RemoveModelGroupAsync(string groupName)
        {
            string currentPrinter = GetActivePrinterSlug();
            if (string.IsNullOrEmpty(currentPrinter)) return false;

            try
            {
                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{currentPrinter}";
                object data = new
                {
                    groupName,
                };
                IRestApiRequestRespone? result = await SendRestApiRequestAsync(
                   requestTargetUri: targetUri,
                   method: Method.Post,
                   command: "delModelGroup",
                   body: null,
                   authHeaders: AuthHeaders,
                   urlSegments: [new ("data", JsonConvert.SerializeObject(data))]
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

        public Task<bool> RemoveModelGroupAsync(string printerName, IGcodeGroup group) => RemoveModelGroupAsync(printerName, group.Name);
        public async Task<bool> RemoveModelGroupAsync(string printerName, string groupName)
        {
            try
            {
                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{printerName}";
                object data = new
                {
                     groupName,
                };
                IRestApiRequestRespone? result = await SendRestApiRequestAsync(
                   requestTargetUri: targetUri,
                   method: Method.Post,
                   command: "delModelGroup",
                   body: null,
                   authHeaders: AuthHeaders,
                   urlSegments: [new("data", JsonConvert.SerializeObject(data))]
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

        public Task<bool> MoveModelToGroupAsync(string groupName, IGcode file) => MoveModelToGroupAsync(groupName, file.Identifier);
        public async Task<bool> MoveModelToGroupAsync(string groupName, long id)
        {
            string currentPrinter = GetActivePrinterSlug();
            if (string.IsNullOrEmpty(currentPrinter)) return false;

            try
            {
                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{currentPrinter}";
                object data = new
                {
                    groupName,
                    id,
                };
                IRestApiRequestRespone? result = await SendRestApiRequestAsync(
                   requestTargetUri: targetUri,
                   method: Method.Post,
                   command: "moveModelFileToGroup",
                   body: null,
                   authHeaders: AuthHeaders,
                   urlSegments: [new ("data", JsonConvert.SerializeObject(data))]
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
        public Task<bool> MoveModelToGroupAsync(string printerName, IGcodeGroup group, IGcode file) => MoveModelToGroupAsync(printerName, group.Name, file.Identifier);
        public async Task<bool> MoveModelToGroupAsync(string printerName, string groupName, long id)
        {
            try
            {
                object data = new { groupName, id };
                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{GetActivePrinterSlug()}";
                IRestApiRequestRespone? result = await SendRestApiRequestAsync(
                       requestTargetUri: targetUri,
                       method: Method.Post,
                        command: "removeModel",
                       body: null,
                       authHeaders: AuthHeaders,
                       urlSegments: [new ("data", JsonConvert.SerializeObject(data))]
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

        #endregion

        #endregion
    }
}

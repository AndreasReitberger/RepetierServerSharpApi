using AndreasReitberger.API.Print3dServer.Core.Events;
using AndreasReitberger.API.Print3dServer.Core.Interfaces;
using AndreasReitberger.API.Repetier.Models;
using AndreasReitberger.API.Repetier.Structs;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace AndreasReitberger.API.Repetier
{
    public partial class RepetierClient
    {
        #region Methods

        public override async Task<List<IPrinter3d>> GetPrintersAsync()
        {
            IRestApiRequestRespone? result = null;
            try
            {
                List<IPrinter3d> repetierPrinterList = [];
                if (!IsReady)
                    return repetierPrinterList;

                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.List}";
                result = await SendRestApiRequestAsync(
                       requestTargetUri: targetUri,
                       method: Method.Post,
                       command: "",
                       jsonObject: null,
                       authHeaders: AuthHeaders
                       )
                    .ConfigureAwait(false);
                /*
                result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer,
                    RepetierCommandFeature.list)
                    .ConfigureAwait(false);
                */
                RepetierPrinterListRespone? respone = GetObjectFromJson<RepetierPrinterListRespone>(result?.Result);
                if (respone is not null)
                {
                    repetierPrinterList = new List<IPrinter3d>(respone.Printers);
                    foreach (RepetierPrinter? printer in repetierPrinterList.Cast<RepetierPrinter>())
                    {
                        if (printer is not null)
                        {
                            if (printer?.JobId > 0)
                            {
                                IPrinter3d? prevPrinter = Printers?.FirstOrDefault(p => p.Slug == printer.Slug);
                                if (prevPrinter is null) continue;
                                // Avoid unnecessary calls if the image or the job hasn't changed
                                if (prevPrinter?.ActiveJobId != printer?.ActiveJobId || prevPrinter?.CurrentPrintImage?.Length <= 0)
                                {
                                    if (printer is not null) printer.CurrentPrintImage = await GetDynamicRenderImageByJobIdAsync(printer.JobId, false).ConfigureAwait(false);
                                }
                                else
                                {
                                    if (printer is not null) printer.CurrentPrintImage = prevPrinter?.CurrentPrintImage ?? [];
                                }
                            }
                            else if (printer is not null) printer.CurrentPrintImage = [];
                        }
                    }
                    Printers = [.. repetierPrinterList];
                }
                return repetierPrinterList;
            }
            catch (JsonException jecx)
            {
                OnError(new JsonConvertEventArgs()
                {
                    Exception = jecx,
                    OriginalString = result?.Result,
                    TargetType = nameof(RepetierPrinter),
                    Message = jecx.Message,
                });
                return [];
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return [];
            }
        }
        public async Task RefreshPrinterListAsync()
        {
            try
            {
                List<IPrinter3d> printers = [];
                if (!IsReady)
                {
                    Printers = [.. printers];
                    return;
                }

                List<IPrinter3d> result = await GetPrintersAsync().ConfigureAwait(false);
                if (result is not null)
                {
                    Printers = [.. result];
                }
                else
                {
                    Printers = [.. printers];
                }
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                Printers = [];
            }
        }

        public async Task<RepetierPrinterConfig?> GetPrinterConfigAsync(string printerName = "")
        {
            RepetierPrinterConfig? resultObject = null;

            string currentPrinter = string.IsNullOrEmpty(printerName) ? GetActivePrinterSlug() : printerName;
            if (string.IsNullOrEmpty(currentPrinter)) return resultObject;

            IRestApiRequestRespone? result = null;
            try
            {
                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{currentPrinter}";
                result = await SendRestApiRequestAsync(
                       requestTargetUri: targetUri,
                       method: Method.Post,
                       command: "getPrinterConfig",
                       jsonObject: new { printer = currentPrinter },
                       authHeaders: AuthHeaders
                       )
                    .ConfigureAwait(false);
                /*
                result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api,
                    command: "getPrinterConfig", jsonData: string.Format("{{\"printer\": \"{0}\"}}", currentPrinter),
                    printerName: currentPrinter)
                    .ConfigureAwait(false);
                */
                RepetierPrinterConfig? config = GetObjectFromJson<RepetierPrinterConfig>(result?.Result);
                if (config is not null)
                {
                    Config = resultObject = config;
                }
                return resultObject;
            }
            catch (JsonException jecx)
            {
                OnError(new JsonConvertEventArgs()
                {
                    Exception = jecx,
                    OriginalString = result?.Result,
                    Message = jecx.Message,
                });
                return resultObject;
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return resultObject;
            }
        }
        public async Task RefreshPrinterConfigAsync()
        {
            try
            {
                RepetierPrinterConfig? result = await GetPrinterConfigAsync().ConfigureAwait(false);
                if (result is not null)
                {
                    Config = result;
                }
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
            }
        }

        public async Task<bool> SetPrinterConfigAsync(RepetierPrinterConfig newConfig, string printerName = "")
        {
            string currentPrinter = string.IsNullOrEmpty(printerName) ? GetActivePrinterSlug() : printerName;
            if (string.IsNullOrEmpty(currentPrinter))
            {
                return false;
            }

            IRestApiRequestRespone? result = null;
            try
            {
                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{currentPrinter}";
                result = await SendRestApiRequestAsync(
                       requestTargetUri: targetUri,
                       method: Method.Post,
                       command: "setPrinterConfig",
                       jsonObject: newConfig,
                       authHeaders: AuthHeaders
                       )
                    .ConfigureAwait(false);
                /*
                result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api,
                    command: "setPrinterConfig", jsonData: newConfig,
                    printerName: currentPrinter
                    ).ConfigureAwait(false);
                */
                return GetQueryResult(result?.Result, true);
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return false;
            }
        }
        #endregion
    }
}

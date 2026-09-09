using AndreasReitberger.API.Print3dServer.Core.Events;
using AndreasReitberger.API.Repetier.Models;
using AndreasReitberger.API.Repetier.SourceGeneration;
using AndreasReitberger.API.REST.Events;
using AndreasReitberger.Shared.Core.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Websocket.Client;

namespace AndreasReitberger.API.Repetier
{
    public partial class RepetierClient
    {

        #region WebSocket

        public new Task StartListeningAsync(bool stopActiveListening = false, string[]? commandsOnConnect = null) => StartListeningAsync(WebSocketTargetUri, stopActiveListening, () => Task.Run(async () =>
        {
            List<Task> tasks =
            [
                RefreshPrinterStateAsync(),
                RefreshCurrentPrintInfosAsync(),
            ];
            await Task.WhenAll(tasks).ConfigureAwait(false);
        }), commandsOnConnect: commandsOnConnect);

        protected void Client_WebSocketMessageReceived(object? sender, WebsocketEventArgs e)
        {
            try
            {
                if (e == null || string.IsNullOrEmpty(e.Message))
                    return;
                string text = e.Message;
                if (text.Contains("login", StringComparison.CurrentCultureIgnoreCase))
                {
                    //var login = GetObjectFromJson<RepetierLoginRequiredResult>(text, NewtonsoftJsonSerializerSettings);
                    //var login = GetObjectFromJson<RepetierLoginResult>(text, NewtonsoftJsonSerializerSettings);
                }
                if (text.Contains("session", StringComparison.CurrentCultureIgnoreCase))
                {
                    //Session = GetObjectFromJson<EventSession>(text, NewtonsoftJsonSerializerSettings);
                    Session = JsonConvertHelper.ToObject<EventSession>(text, context: RepetierSourceGenerationContext.Default);
                }
                else if (text.Contains("event", StringComparison.CurrentCultureIgnoreCase))
                {
                    RepetierEventContainer? repetierEvent = JsonConvertHelper.ToObject<RepetierEventContainer>(text, context: RepetierSourceGenerationContext.Default);
                    if (repetierEvent is not null)
                    {
                        string name = string.Empty;
                        string? jsonBody = string.Empty;
                        foreach (RepetierEventData obj in repetierEvent.Data)
                        {
                            name = obj.EventName;
                            jsonBody = obj.Data?.ToString();
                            switch (name)
                            {
                                case "userCredentials":
                                    RepetierLoginResult? login = JsonConvertHelper.ToObject<RepetierLoginResult>(jsonBody!, context: RepetierSourceGenerationContext.Default);
                                    if (login is not null)
                                    {
                                        OnLoginResultReceived(new RepetierLoginRequiredEventArgs()
                                        {
                                            ResultData = login,
                                            LoginSucceeded = true,
                                            CallbackId = PingCounter,
                                            SessionId = SessionId,
                                            Message = obj.Printer,
                                        });
                                    }
                                    break;
                                case "temp":
                                    EventTempData? eventTempData = JsonConvertHelper.ToObject<EventTempData>(jsonBody!, context: RepetierSourceGenerationContext.Default);
                                    if (eventTempData is not null)
                                    {
                                        if (obj.Printer == ActivePrinter?.Slug)
                                        {
                                            switch (eventTempData.EventId)
                                            {
                                                case 0:
                                                    ActivePrinter?.Extruder1Temperature = eventTempData.TemperatureTarget;
                                                    ActiveToolhead?.TempRead = eventTempData.TemperatureTarget;
                                                    ActiveToolhead?.TempSet = eventTempData.TemperatureSet;
                                                    break;
                                                case 1000:
                                                    ActivePrinter?.HeatedBedTemperature = eventTempData.TemperatureTarget;
                                                    ActiveHeatedBed?.TempRead = eventTempData.TemperatureTarget;
                                                    ActiveHeatedBed?.TempSet = eventTempData.TemperatureSet;
                                                    break;
                                                default:
                                                    break;
                                            }
                                        }
                                        OnTemperatureDataReceived(new TemperatureDataEventArgs()
                                        {
                                            TemperatureInfo = eventTempData,
                                            CallbackId = PingCounter,
                                            SessionId = SessionId,
                                            Printer = obj.Printer,
                                        });
                                    }
                                    break;
                                case "jobStarted":
                                    EventJobStartedData? eventJobStarted = JsonConvertHelper.ToObject<EventJobStartedData>(jsonBody!, context: RepetierSourceGenerationContext.Default);
                                    OnJobStarted(new RepetierJobStartedEventArgs()
                                    {
                                        Job = eventJobStarted,
                                        CallbackId = PingCounter,
                                        SessionId = SessionId,
                                        Printer = obj.Printer,
                                    });
                                    break;
                                case "jobsChanged":
                                    EventJobChangedData? eventJobsChanged = JsonConvertHelper.ToObject<EventJobChangedData>(jsonBody!, context: RepetierSourceGenerationContext.Default);
                                    if (eventJobsChanged is not null)
                                    {
                                        /*
                                        OnJobsChangedEvent(new RepetierJobsChangedEventArgs()
                                        {
                                            Data = eventJobsChanged,
                                            CallbackId = PingCounter,
                                            SessionId = SessionId,
                                            Printer = obj.Printer,
                                        });
                                        */
                                        OnJobListChangedEvent(new JobListChangedEventArgs()
                                        {
                                            NewJobList = [],
                                            CallbackId = PingCounter,
                                            SessionId = SessionId,
                                            Printer = obj.Printer,
                                        });
                                    }
                                    break;
                                case "jobDeactivated":
                                case "jobFinished":
                                    EventJobFinishedData? eventJobFinished = JsonConvertHelper.ToObject<EventJobFinishedData>(jsonBody!, context: RepetierSourceGenerationContext.Default);
                                    if (eventJobFinished is not null)
                                    {
                                        /*
                                        OnJobFinished(new RepetierJobFinishedEventArgs()
                                        {
                                            Job = eventJobFinished,
                                            CallbackId = PingCounter,
                                            SessionId = SessionId,
                                            Printer = obj.Printer,
                                        });
                                        */
                                        OnJobFinished(new JobFinishedEventArgs()
                                        {
                                            Job = new RepetierJobListItem() { PrintTime = eventJobFinished.Duration },
                                            CallbackId = PingCounter,
                                            SessionId = SessionId,
                                            Printer = obj.Printer,
                                        });
                                    }
                                    break;
                                case "messagesChanged":
                                    EventMessageChangedData? eventMessageChanged = JsonConvertHelper.ToObject<EventMessageChangedData>(jsonBody!, context: RepetierSourceGenerationContext.Default);
                                    if (eventMessageChanged is not null)
                                    {
                                        OnMessagesChangedEvent(new RepetierMessagesChangedEventArgs()
                                        {
                                            RepetierMessage = eventMessageChanged,
                                            CallbackId = PingCounter,
                                            SessionId = SessionId,
                                            Printer = obj.Printer,
                                        });
                                    }
                                    break;
                                case "hardwareInfo":
                                    EventHardwareInfoChangedData? eventHardwareInfoChanged = JsonConvertHelper.ToObject<EventHardwareInfoChangedData>(jsonBody!, context: RepetierSourceGenerationContext.Default);
                                    OnHardwareInfoChangedEvent(new RepetierHardwareInfoChangedEventArgs()
                                    {
                                        Info = eventHardwareInfoChanged,
                                        CallbackId = PingCounter,
                                        SessionId = SessionId,
                                        Printer = obj.Printer,
                                    });
                                    break;
                                case "wifiChanged":
                                    EventWifiChangedData? eventWifiChanged = JsonConvertHelper.ToObject<EventWifiChangedData>(jsonBody!, context: RepetierSourceGenerationContext.Default);
                                    OnWifiChangedEvent(new RepetierWifiChangedEventArgs()
                                    {
                                        Data = eventWifiChanged,
                                        CallbackId = PingCounter,
                                        SessionId = SessionId,
                                        Printer = obj.Printer,
                                    });
                                    break;
                                case "gcodeInfoUpdated":
                                    EventGcodeInfoUpdatedData? eventGcodeInfoUpdatedChanged = JsonConvertHelper.ToObject<EventGcodeInfoUpdatedData>(jsonBody!, context: RepetierSourceGenerationContext.Default);
                                    break;
                                case "layerChanged":
                                    RepetierLayerChangedEvent? eventLayerChanged = JsonConvertHelper.ToObject<RepetierLayerChangedEvent>(jsonBody!, context: RepetierSourceGenerationContext.Default);
                                    break;
                                case "updatePrinterState":
                                    RepetierPrinterState? updatePrinterState = JsonConvertHelper.ToObject<RepetierPrinterState>(jsonBody!, context: RepetierSourceGenerationContext.Default);
                                    break;
                                case "timelapseChanged":
#if DEBUG
                                    Console.WriteLine($"No Json object found for '{name}' => '{jsonBody!}");
#endif
                                    break;
                                case "newRenderImage":
#if DEBUG
                                    Console.WriteLine($"No Json object found for '{name}' => '{jsonBody!}");
#endif
                                    break;
                                case "printerListChanged":
#if DEBUG
                                    Console.WriteLine($"No Json object found for '{name}' => '{jsonBody!}");
#endif
                                    break;
                                case "printqueueChanged":
#if DEBUG
                                    Console.WriteLine($"No Json object found for '{name}' => '{jsonBody!}");
#endif
                                    break;
                                case "workerFinished":
#if DEBUG
                                    Console.WriteLine($"No Json object found for '{name}' => '{jsonBody!}");
#endif
                                    break;
                                case "config":
#if DEBUG
                                    Console.WriteLine($"No Json object found for '{name}' => '{jsonBody!}");
#endif
                                    break;
                                case "state":
#if DEBUG
                                    Console.WriteLine($"No Json object found for '{name}' => '{jsonBody!}");
#endif
                                    break;
                                // Bodyless events, with no additional data
                                case "addErrorLogLine":
                                case "timer30":
                                case "timer60":
                                case "timer300":
                                case "timer1800":
                                case "printJobAdded":
                                case "prepareJob":
                                case "prepareJobFinished":
                                case "lastPrintsChanged":
                                case "modelGroupListChanged":
                                    break;
                                // For unknown events log the needed information to create a class

                                case "dispatcherCount":
                                case "recoverChanged":
                                case "log":
                                default:
#if DEBUG
                                    Console.WriteLine($"No Json object found for '{name}' => '{jsonBody!}");
#endif
                                    ConcurrentDictionary<string, string> loggedResults = new(IgnoredJsonResults);
                                    if (!loggedResults.ContainsKey(name) && !string.IsNullOrEmpty(jsonBody!))
                                    {
                                        // Log unused json results for further releases
#if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                                        loggedResults.TryAdd(name, jsonBody!);
#else
                                        loggedResults.Add(name, jsonBody!);
#endif
                                        IgnoredJsonResults = loggedResults;
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
            catch (JsonException jecx)
            {
                OnError(new JsonConvertEventArgs()
                {
                    Exception = jecx,
                    OriginalString = e.Message,
                    Message = jecx.Message,
                });
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
            }
        }

#if NET_WS
        new void WebSocket_MessageReceived(object sender, MessageReceivedEventArgs msg)
#else
        new void WebSocket_MessageReceived(ResponseMessage? msg)
#endif
        {
            try
            {
                if (msg?.Text == null || string.IsNullOrEmpty(msg.Text))
                    return;
                base.WebSocket_MessageReceived(msg);
                string text = msg.Text;
                if (text.Contains("login", StringComparison.CurrentCultureIgnoreCase))
                {
                    //var login = GetObjectFromJson<RepetierLoginRequiredResult>(text, NewtonsoftJsonSerializerSettings);
                    //var login = GetObjectFromJson<RepetierLoginResult>(text, NewtonsoftJsonSerializerSettings);
                }
                if (text.Contains("session", StringComparison.CurrentCultureIgnoreCase))
                {
                    //Session = GetObjectFromJson<EventSession>(text, NewtonsoftJsonSerializerSettings);
                    Session = JsonConvertHelper.ToObject<EventSession>(text, context: RepetierSourceGenerationContext.Default);
                }
                else if (text.Contains("event", StringComparison.CurrentCultureIgnoreCase))
                {
                    RepetierEventContainer? repetierEvent = JsonConvertHelper.ToObject<RepetierEventContainer>(text, context: RepetierSourceGenerationContext.Default);
                    if (repetierEvent is not null)
                    {
                        string name = string.Empty;
                        string? jsonBody = string.Empty;
                        foreach (RepetierEventData obj in repetierEvent.Data)
                        {
                            name = obj.EventName;
                            jsonBody = obj.Data?.ToString();
                            switch (name)
                            {
                                case "userCredentials":
                                    RepetierLoginResult? login = JsonConvertHelper.ToObject<RepetierLoginResult>(jsonBody!, context: RepetierSourceGenerationContext.Default);
                                    if (login is not null)
                                    {
                                        OnLoginResultReceived(new RepetierLoginRequiredEventArgs()
                                        {
                                            ResultData = login,
                                            LoginSucceeded = true,
                                            CallbackId = PingCounter,
                                            SessionId = SessionId,
                                            Message = obj.Printer,
                                        });
                                    }
                                    break;
                                case "temp":
                                    EventTempData? eventTempData = JsonConvertHelper.ToObject<EventTempData>(jsonBody!, context: RepetierSourceGenerationContext.Default);
                                    if (eventTempData is not null)
                                    {
                                        OnTemperatureDataReceived(new TemperatureDataEventArgs()
                                        {
                                            TemperatureInfo = eventTempData,
                                            CallbackId = PingCounter,
                                            SessionId = SessionId,
                                            Printer = obj.Printer,
                                        });
                                    }
                                    break;
                                case "jobStarted":
                                    EventJobStartedData? eventJobStarted = JsonConvertHelper.ToObject<EventJobStartedData>(jsonBody!, context: RepetierSourceGenerationContext.Default);
                                    OnJobStarted(new RepetierJobStartedEventArgs()
                                    {
                                        Job = eventJobStarted,
                                        CallbackId = PingCounter,
                                        SessionId = SessionId,
                                        Printer = obj.Printer,
                                    });
                                    break;
                                case "jobsChanged":
                                    EventJobChangedData? eventJobsChanged = JsonConvertHelper.ToObject<EventJobChangedData>(jsonBody!, context: RepetierSourceGenerationContext.Default);
                                    if (eventJobsChanged is not null)
                                    {
                                        /*
                                        OnJobsChangedEvent(new RepetierJobsChangedEventArgs()
                                        {
                                            Data = eventJobsChanged,
                                            CallbackId = PingCounter,
                                            SessionId = SessionId,
                                            Printer = obj.Printer,
                                        });
                                        */
                                        OnJobListChangedEvent(new JobListChangedEventArgs()
                                        {
                                            NewJobList = [],
                                            CallbackId = PingCounter,
                                            SessionId = SessionId,
                                            Printer = obj.Printer,
                                        });
                                    }
                                    break;
                                case "jobDeactivated":
                                case "jobFinished":
                                    EventJobFinishedData? eventJobFinished = JsonConvertHelper.ToObject<EventJobFinishedData>(jsonBody!, context: RepetierSourceGenerationContext.Default);
                                    if (eventJobFinished is not null)
                                    {
                                        /*
                                        OnJobFinished(new RepetierJobFinishedEventArgs()
                                        {
                                            Job = eventJobFinished,
                                            CallbackId = PingCounter,
                                            SessionId = SessionId,
                                            Printer = obj.Printer,
                                        });
                                        */
                                        OnJobFinished(new JobFinishedEventArgs()
                                        {
                                            Job = new RepetierJobListItem() { PrintTime = eventJobFinished.Duration },
                                            CallbackId = PingCounter,
                                            SessionId = SessionId,
                                            Printer = obj.Printer,
                                        });
                                    }
                                    break;
                                case "messagesChanged":
                                    EventMessageChangedData? eventMessageChanged = JsonConvertHelper.ToObject<EventMessageChangedData>(jsonBody!, context: RepetierSourceGenerationContext.Default);
                                    if (eventMessageChanged is not null)
                                    {
                                        OnMessagesChangedEvent(new RepetierMessagesChangedEventArgs()
                                        {
                                            RepetierMessage = eventMessageChanged,
                                            CallbackId = PingCounter,
                                            SessionId = SessionId,
                                            Printer = obj.Printer,
                                        });
                                    }
                                    break;
                                case "hardwareInfo":
                                    EventHardwareInfoChangedData? eventHardwareInfoChanged = JsonConvertHelper.ToObject<EventHardwareInfoChangedData>(jsonBody!, context: RepetierSourceGenerationContext.Default);
                                    OnHardwareInfoChangedEvent(new RepetierHardwareInfoChangedEventArgs()
                                    {
                                        Info = eventHardwareInfoChanged,
                                        CallbackId = PingCounter,
                                        SessionId = SessionId,
                                        Printer = obj.Printer,
                                    });
                                    break;
                                case "wifiChanged":
                                    EventWifiChangedData? eventWifiChanged = JsonConvertHelper.ToObject<EventWifiChangedData>(jsonBody!, context: RepetierSourceGenerationContext.Default);
                                    OnWifiChangedEvent(new RepetierWifiChangedEventArgs()
                                    {
                                        Data = eventWifiChanged,
                                        CallbackId = PingCounter,
                                        SessionId = SessionId,
                                        Printer = obj.Printer,
                                    });
                                    break;
                                case "gcodeInfoUpdated":
                                    EventGcodeInfoUpdatedData? eventGcodeInfoUpdatedChanged = JsonConvertHelper.ToObject<EventGcodeInfoUpdatedData>(jsonBody!, context: RepetierSourceGenerationContext.Default);
                                    break;
                                case "layerChanged":
                                    RepetierLayerChangedEvent? eventLayerChanged = JsonConvertHelper.ToObject<RepetierLayerChangedEvent>(jsonBody!, context: RepetierSourceGenerationContext.Default);
                                    break;
                                case "updatePrinterState":
                                    RepetierPrinterState? updatePrinterState = JsonConvertHelper.ToObject<RepetierPrinterState>(jsonBody!, context: RepetierSourceGenerationContext.Default);
                                    break;
                                case "timelapseChanged":
#if DEBUG
                                    Console.WriteLine($"No Json object found for '{name}' => '{jsonBody!}");
#endif
                                    break;
                                case "newRenderImage":
#if DEBUG
                                    Console.WriteLine($"No Json object found for '{name}' => '{jsonBody!}");
#endif
                                    break;
                                case "printerListChanged":
#if DEBUG
                                    Console.WriteLine($"No Json object found for '{name}' => '{jsonBody!}");
#endif
                                    break;
                                case "printqueueChanged":
#if DEBUG
                                    Console.WriteLine($"No Json object found for '{name}' => '{jsonBody!}");
#endif
                                    break;
                                case "workerFinished":
#if DEBUG
                                    Console.WriteLine($"No Json object found for '{name}' => '{jsonBody!}");
#endif
                                    break;
                                case "config":
#if DEBUG
                                    Console.WriteLine($"No Json object found for '{name}' => '{jsonBody!}");
#endif
                                    break;
                                case "state":
#if DEBUG
                                    Console.WriteLine($"No Json object found for '{name}' => '{jsonBody!}");
#endif
                                    break;
                                // Bodyless events, with no additional data
                                case "addErrorLogLine":
                                case "timer30":
                                case "timer60":
                                case "timer300":
                                case "timer1800":
                                case "printJobAdded":
                                case "prepareJob":
                                case "prepareJobFinished":
                                case "lastPrintsChanged":
                                case "modelGroupListChanged":
                                    break;
                                // For unknown events log the needed information to create a class

                                case "dispatcherCount":
                                case "recoverChanged":
                                case "log":
                                default:
#if DEBUG
                                    Console.WriteLine($"No Json object found for '{name}' => '{jsonBody!}");
#endif
                                    ConcurrentDictionary<string, string> loggedResults = new(IgnoredJsonResults);
                                    if (!loggedResults.ContainsKey(name) && !string.IsNullOrEmpty(jsonBody!))
                                    {
                                        // Log unused json results for further releases
#if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                                        loggedResults.TryAdd(name, jsonBody!);
#else
                                        loggedResults.Add(name, jsonBody!);
#endif
                                        IgnoredJsonResults = loggedResults;
                                    }
                                    break;
                            }
                        }
                    }
                }
                /* Done in base method already
                OnWebSocketMessageReceived(new RepetierWebsocketEventArgs()
                {
                    CallbackId = PingCounter,
                    Message = text,
                    SessionId = SessionId,
                });
                */
            }
            catch (JsonException jecx)
            {
                OnError(new JsonConvertEventArgs()
                {
                    Exception = jecx,
                    OriginalString = msg?.Text,
                    Message = jecx.Message,
                });
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
            }
        }

        #endregion

    }
}

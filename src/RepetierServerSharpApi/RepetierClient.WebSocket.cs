using AndreasReitberger.API.Print3dServer.Core.Events;
using AndreasReitberger.API.Print3dServer.Core.Interfaces;
using AndreasReitberger.API.Repetier.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using Websocket.Client;

namespace AndreasReitberger.API.Repetier
{
    public partial class RepetierClient
    {

        #region WebSocket

        protected void Client_WebSocketMessageReceived(object? sender, WebsocketEventArgs e)
        {
            try
            {
                if (e == null || string.IsNullOrEmpty(e.Message))
                    return;
                string text = e.Message;
                if (text.ToLower().Contains("login"))
                {
                    //var login = GetObjectFromJson<RepetierLoginRequiredResult>(text, NewtonsoftJsonSerializerSettings);
                    //var login = GetObjectFromJson<RepetierLoginResult>(text, NewtonsoftJsonSerializerSettings);
                }
                if (text.ToLower().Contains("session"))
                {
                    //Session = GetObjectFromJson<EventSession>(text, NewtonsoftJsonSerializerSettings);
                    Session = GetObjectFromJson<EventSession>(text);
                }
                else if (text.ToLower().Contains("event"))
                {
                    RepetierEventContainer? repetierEvent = GetObjectFromJson<RepetierEventContainer>(text, NewtonsoftJsonSerializerSettings);
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
                                    RepetierLoginResult? login = GetObjectFromJson<RepetierLoginResult>(jsonBody);
                                    if (login is not null)
                                    {
                                        OnLoginResultReceived(new RepetierLoginRequiredEventArgs()
                                        {
                                            ResultData = login,
                                            LoginSucceeded = true,
                                            CallbackId = PingCounter,
                                            SessionId = SessionId,
                                            Printer = obj.Printer,
                                        });
                                    }
                                    break;
                                case "temp":
                                    EventTempData? eventTempData = GetObjectFromJson<EventTempData>(jsonBody);
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
                                    EventJobStartedData? eventJobStarted = GetObjectFromJson<EventJobStartedData>(jsonBody);
                                    OnJobStarted(new RepetierJobStartedEventArgs()
                                    {
                                        Job = eventJobStarted,
                                        CallbackId = PingCounter,
                                        SessionId = SessionId,
                                        Printer = obj.Printer,
                                    });
                                    break;
                                case "jobsChanged":
                                    EventJobChangedData? eventJobsChanged = GetObjectFromJson<EventJobChangedData>(jsonBody);
                                    if (eventJobsChanged is not null)
                                    {
                                        OnJobsChangedEvent(new RepetierJobsChangedEventArgs()
                                        {
                                            Data = eventJobsChanged,
                                            CallbackId = PingCounter,
                                            SessionId = SessionId,
                                            Printer = obj.Printer,
                                        });
                                    }
                                    break;
                                case "jobDeactivated":
                                case "jobFinished":
                                    EventJobFinishedData? eventJobFinished = GetObjectFromJson<EventJobFinishedData>(jsonBody);
                                    if (eventJobFinished is not null)
                                    {
                                        OnJobFinished(new RepetierJobFinishedEventArgs()
                                        {
                                            Job = eventJobFinished,
                                            CallbackId = PingCounter,
                                            SessionId = SessionId,
                                            Printer = obj.Printer,
                                        });
                                    }
                                    break;
                                case "messagesChanged":
                                    EventMessageChangedData? eventMessageChanged = GetObjectFromJson<EventMessageChangedData>(jsonBody);
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
                                    EventHardwareInfoChangedData? eventHardwareInfoChanged = GetObjectFromJson<EventHardwareInfoChangedData>(jsonBody);
                                    OnHardwareInfoChangedEvent(new RepetierHardwareInfoChangedEventArgs()
                                    {
                                        Info = eventHardwareInfoChanged,
                                        CallbackId = PingCounter,
                                        SessionId = SessionId,
                                        Printer = obj.Printer,
                                    });
                                    break;
                                case "wifiChanged":
                                    EventWifiChangedData? eventWifiChanged = GetObjectFromJson<EventWifiChangedData>(jsonBody);
                                    OnWifiChangedEvent(new RepetierWifiChangedEventArgs()
                                    {
                                        Data = eventWifiChanged,
                                        CallbackId = PingCounter,
                                        SessionId = SessionId,
                                        Printer = obj.Printer,
                                    });
                                    break;
                                case "gcodeInfoUpdated":
                                    EventGcodeInfoUpdatedData? eventGcodeInfoUpdatedChanged = GetObjectFromJson<EventGcodeInfoUpdatedData>(jsonBody);
                                    break;
                                case "layerChanged":
                                    RepetierLayerChangedEvent? eventLayerChanged = GetObjectFromJson<RepetierLayerChangedEvent>(jsonBody);
                                    break;
                                case "updatePrinterState":
                                    RepetierPrinterState? updatePrinterState = GetObjectFromJson<RepetierPrinterState>(jsonBody);
                                    break;
                                case "timelapseChanged":
#if DEBUG
                                    Console.WriteLine($"No Json object found for '{name}' => '{jsonBody}");
#endif
                                    break;
                                case "newRenderImage":
#if DEBUG
                                    Console.WriteLine($"No Json object found for '{name}' => '{jsonBody}");
#endif
                                    break;
                                case "printerListChanged":
#if DEBUG
                                    Console.WriteLine($"No Json object found for '{name}' => '{jsonBody}");
#endif
                                    break;
                                case "printqueueChanged":
#if DEBUG
                                    Console.WriteLine($"No Json object found for '{name}' => '{jsonBody}");
#endif
                                    break;
                                case "workerFinished":
#if DEBUG
                                    Console.WriteLine($"No Json object found for '{name}' => '{jsonBody}");
#endif
                                    break;
                                case "config":
#if DEBUG
                                    Console.WriteLine($"No Json object found for '{name}' => '{jsonBody}");
#endif
                                    break;
                                case "state":
#if DEBUG
                                    Console.WriteLine($"No Json object found for '{name}' => '{jsonBody}");
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
                                    Console.WriteLine($"No Json object found for '{name}' => '{jsonBody}");
#endif
                                    ConcurrentDictionary<string, string> loggedResults = new(IgnoredJsonResults);
                                    if (!loggedResults.ContainsKey(name) && !string.IsNullOrEmpty(jsonBody))
                                    {
                                        // Log unused json results for further releases
#if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                                        loggedResults.TryAdd(name, jsonBody);
#else
                                        loggedResults.Add(name, jsonBody);
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
                if (text.ToLower().Contains("login"))
                {
                    //var login = GetObjectFromJson<RepetierLoginRequiredResult>(text, NewtonsoftJsonSerializerSettings);
                    //var login = GetObjectFromJson<RepetierLoginResult>(text, NewtonsoftJsonSerializerSettings);
                }
                if (text.ToLower().Contains("session"))
                {
                    //Session = GetObjectFromJson<EventSession>(text, NewtonsoftJsonSerializerSettings);
                    Session = GetObjectFromJson<EventSession>(text);
                }
                else if (text.ToLower().Contains("event"))
                {
                    RepetierEventContainer? repetierEvent = GetObjectFromJson<RepetierEventContainer>(text, NewtonsoftJsonSerializerSettings);
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
                                    RepetierLoginResult? login = GetObjectFromJson<RepetierLoginResult>(jsonBody);
                                    if (login is not null)
                                    {
                                        OnLoginResultReceived(new RepetierLoginRequiredEventArgs()
                                        {
                                            ResultData = login,
                                            LoginSucceeded = true,
                                            CallbackId = PingCounter,
                                            SessionId = SessionId,
                                            Printer = obj.Printer,
                                        });
                                    }
                                    break;
                                case "temp":
                                    EventTempData? eventTempData = GetObjectFromJson<EventTempData>(jsonBody);
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
                                    EventJobStartedData? eventJobStarted = GetObjectFromJson<EventJobStartedData>(jsonBody);
                                    OnJobStarted(new RepetierJobStartedEventArgs()
                                    {
                                        Job = eventJobStarted,
                                        CallbackId = PingCounter,
                                        SessionId = SessionId,
                                        Printer = obj.Printer,
                                    });
                                    break;
                                case "jobsChanged":
                                    EventJobChangedData? eventJobsChanged = GetObjectFromJson<EventJobChangedData>(jsonBody);
                                    if (eventJobsChanged is not null)
                                    {
                                        OnJobsChangedEvent(new RepetierJobsChangedEventArgs()
                                        {
                                            Data = eventJobsChanged,
                                            CallbackId = PingCounter,
                                            SessionId = SessionId,
                                            Printer = obj.Printer,
                                        });
                                    }
                                    break;
                                case "jobDeactivated":
                                case "jobFinished":
                                    EventJobFinishedData? eventJobFinished = GetObjectFromJson<EventJobFinishedData>(jsonBody);
                                    if (eventJobFinished is not null)
                                    {
                                        OnJobFinished(new RepetierJobFinishedEventArgs()
                                        {
                                            Job = eventJobFinished,
                                            CallbackId = PingCounter,
                                            SessionId = SessionId,
                                            Printer = obj.Printer,
                                        });
                                    }
                                    break;
                                case "messagesChanged":
                                    EventMessageChangedData? eventMessageChanged = GetObjectFromJson<EventMessageChangedData>(jsonBody);
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
                                    EventHardwareInfoChangedData? eventHardwareInfoChanged = GetObjectFromJson<EventHardwareInfoChangedData>(jsonBody);
                                    OnHardwareInfoChangedEvent(new RepetierHardwareInfoChangedEventArgs()
                                    {
                                        Info = eventHardwareInfoChanged,
                                        CallbackId = PingCounter,
                                        SessionId = SessionId,
                                        Printer = obj.Printer,
                                    });
                                    break;
                                case "wifiChanged":
                                    EventWifiChangedData? eventWifiChanged = GetObjectFromJson<EventWifiChangedData>(jsonBody);
                                    OnWifiChangedEvent(new RepetierWifiChangedEventArgs()
                                    {
                                        Data = eventWifiChanged,
                                        CallbackId = PingCounter,
                                        SessionId = SessionId,
                                        Printer = obj.Printer,
                                    });
                                    break;
                                case "gcodeInfoUpdated":
                                    EventGcodeInfoUpdatedData? eventGcodeInfoUpdatedChanged = GetObjectFromJson<EventGcodeInfoUpdatedData>(jsonBody);
                                    break;
                                case "layerChanged":
                                    RepetierLayerChangedEvent? eventLayerChanged = GetObjectFromJson<RepetierLayerChangedEvent>(jsonBody);
                                    break;
                                case "updatePrinterState":
                                    RepetierPrinterState? updatePrinterState = GetObjectFromJson<RepetierPrinterState>(jsonBody);
                                    break;
                                case "timelapseChanged":
#if DEBUG
                                    Console.WriteLine($"No Json object found for '{name}' => '{jsonBody}");
#endif
                                    break;
                                case "newRenderImage":
#if DEBUG
                                    Console.WriteLine($"No Json object found for '{name}' => '{jsonBody}");
#endif
                                    break;
                                case "printerListChanged":
#if DEBUG
                                    Console.WriteLine($"No Json object found for '{name}' => '{jsonBody}");
#endif
                                    break;
                                case "printqueueChanged":
#if DEBUG
                                    Console.WriteLine($"No Json object found for '{name}' => '{jsonBody}");
#endif
                                    break;
                                case "workerFinished":
#if DEBUG
                                    Console.WriteLine($"No Json object found for '{name}' => '{jsonBody}");
#endif
                                    break;
                                case "config":
#if DEBUG
                                    Console.WriteLine($"No Json object found for '{name}' => '{jsonBody}");
#endif
                                    break;
                                case "state":
#if DEBUG
                                    Console.WriteLine($"No Json object found for '{name}' => '{jsonBody}");
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
                                    Console.WriteLine($"No Json object found for '{name}' => '{jsonBody}");
#endif
                                    ConcurrentDictionary<string, string> loggedResults = new(IgnoredJsonResults);
                                    if (!loggedResults.ContainsKey(name) && !string.IsNullOrEmpty(jsonBody))
                                    {
                                        // Log unused json results for further releases
#if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                                        loggedResults.TryAdd(name, jsonBody);
#else
                                        loggedResults.Add(name, jsonBody);
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

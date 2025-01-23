using AndreasReitberger.API.Print3dServer.Core;
using AndreasReitberger.API.Print3dServer.Core.Enums;
using AndreasReitberger.API.Print3dServer.Core.Events;
using AndreasReitberger.API.Print3dServer.Core.Interfaces;
using AndreasReitberger.API.Repetier.Enum;
using AndreasReitberger.API.Repetier.Models;
using AndreasReitberger.API.Repetier.Structs;
using AndreasReitberger.API.REST.Events;
using AndreasReitberger.API.REST.Interfaces;
using AndreasReitberger.Core.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AndreasReitberger.API.Repetier
{
    public partial class RepetierClient : Print3dServerClient, IPrint3dServerClient
    {
        #region Instance

        static RepetierClient? _instance = null;
        static readonly object Lock = new();
        public new static RepetierClient Instance
        {
            get
            {
                lock (Lock)
                {
                    _instance ??= new RepetierClient();
                }
                return _instance;
            }
            set
            {
                if (_instance == value) return;
                lock (Lock)
                {
                    _instance = value;
                }
            }
        }

        #endregion

        #region Properties

        #region Connection

        [ObservableProperty]
        [JsonIgnore, System.Text.Json.Serialization.JsonIgnore, XmlIgnore]
        public partial EventSession? Session { get; set; }

        partial void OnSessionChanged(EventSession? value)
        {
            SessionId = Session?.Session ?? string.Empty;
            OnSessionChangedEvent(new SessionChangedEventArgs()
            {
                CallbackId = Session?.CallbackId ?? -1,
                Session = value?.Session,
                SessionId = value?.Session
            });
        }

        #endregion

        #region General

        [ObservableProperty]
        [JsonIgnore, System.Text.Json.Serialization.JsonIgnore, XmlIgnore]
        public partial RepetierAvailableUpdateInfo? Update { get; set; }

        #endregion

        #region ExternalCommands
        [ObservableProperty]
        [JsonIgnore, System.Text.Json.Serialization.JsonIgnore, XmlIgnore]
        public partial ObservableCollection<ExternalCommand> ExternalCommands { get; set; } = [];

        #endregion

        #region Messages
        [ObservableProperty]
        [JsonIgnore, System.Text.Json.Serialization.JsonIgnore, XmlIgnore]
        public partial ObservableCollection<RepetierMessage> Messages { get; set; } = [];

        partial void OnMessagesChanged(ObservableCollection<RepetierMessage> value)
        {
            OnMessagesChangedEvent(new RepetierMessagesChangedEventArgs()
            {
                RepetierMessages = [.. value],
                SessionId = SessionId,
                CallbackId = -1,
                Printer = GetActivePrinterSlug(),
            });
        }

        #endregion

        #region WebCalls
        [ObservableProperty]
        [JsonIgnore, System.Text.Json.Serialization.JsonIgnore, XmlIgnore]
        public partial ObservableCollection<RepetierWebCallAction> WebCallActions { get; set; } = [];

        partial void OnWebCallActionsChanged(ObservableCollection<RepetierWebCallAction> value)
        {
            OnWebCallActionsChangedEvent(new RepetierWebCallActionsChangedEventArgs()
            {
                NewWebCallActions = [.. value],
                SessionId = SessionId,
                CallbackId = -1,
                Printer = GetActivePrinterSlug(),
            });
        }

        #endregion

        #region GPIO
        [ObservableProperty]
        [JsonIgnore, System.Text.Json.Serialization.JsonIgnore, XmlIgnore]
        public partial ObservableCollection<RepetierGpioListItem> GPIOList { get; set; } = [];

        #endregion

        #region State & Config
        [ObservableProperty]
        [JsonIgnore, System.Text.Json.Serialization.JsonIgnore, XmlIgnore]
        public partial RepetierPrinterConfig? Config { get; set; }

        partial void OnConfigChanged(RepetierPrinterConfig? value)
        {
            OnRepetierPrinterConfigChangedEvent(new RepetierPrinterConfigChangedEventArgs()
            {
                NewConfiguration = value,
                SessionId = SessionId,
                CallbackId = -1,
                Printer = GetActivePrinterSlug(),
            });
            UpdatePrinterConfig(value);
        }

        [ObservableProperty]
        [JsonIgnore, System.Text.Json.Serialization.JsonIgnore, XmlIgnore]
        public partial RepetierPrinterState? State { get; set; }

        partial void OnStateChanged(RepetierPrinterState? value)
        {
            OnRepetierPrinterStateChangedEvent(new RepetierPrinterStateChangedEventArgs()
            {
                NewPrinterState = value,
                SessionId = SessionId,
                CallbackId = -1,
                Printer = GetActivePrinterSlug(),
            });
            UpdatePrinterState(value);
        }

        [ObservableProperty]
        [JsonIgnore, System.Text.Json.Serialization.JsonIgnore, XmlIgnore]
        public partial ObservableCollection<RepetierCurrentPrintInfo> ActivePrintInfos { get; set; } = [];

        partial void OnActivePrintInfosChanged(ObservableCollection<RepetierCurrentPrintInfo> value)
        {
            OnPrintInfosChangedEvent(new RepetierActivePrintInfosChangedEventArgs()
            {
                SessionId = SessionId,
                NewActivePrintInfos = [.. value],
                Printer = GetActivePrinterSlug(),
            });
        }

        #endregion

        #endregion

        #region Constructor
        public RepetierClient()
        {
            Id = Guid.NewGuid();
            LoadDefaults();
            UpdateRestClientInstance();
        }

        public RepetierClient(string serverAddress, string api, int port = 3344, bool isSecure = false) //: base(serverAddress, api, port = 3344, isSecure = false)
        {
            Id = Guid.NewGuid();
            LoadDefaults();
            InitInstance(serverAddress, port, api, isSecure);
            UpdateRestClientInstance();
        }

        public RepetierClient(string serverAddress, int port = 3344, bool isSecure = false) //: base(serverAddress, port = 3344, isSecure = false)
        {
            Id = Guid.NewGuid();
            LoadDefaults();
            InitInstance(serverAddress, port, "", isSecure);
            UpdateRestClientInstance();
        }
        #endregion

        #region Destructor
        ~RepetierClient()
        {
            /* Done in Dtor of Print3dServerClient
            if (WebSocket is not null && WebSocket.IsRunning)
            {
                WebSocket.Stop(System.Net.WebSockets.WebSocketCloseStatus.NormalClosure, $"{nameof(RepetierClient)} was disposed...");
            }
            */
            WebSocketMessageReceived -= Client_WebSocketMessageReceived;
        }
        #endregion

        #region Init

        public static void UpdateSingleInstance(RepetierClient Inst)
        {
            try
            {
                Instance = Inst;
            }
            catch (Exception)
            {
                //OnError(new UnhandledExceptionEventArgs(exc, false));
            }
        }
        public new void InitInstance(string serverAddress, int port = 3344, string api = "", bool isSecure = false)
        {
            try
            {
                ServerAddress = serverAddress;
                ApiKey = api;
                Port = port;
                IsSecure = isSecure;
                //WebSocketTargetUri = GetWebSocketTargetUri();

                Instance = this;

                if (Instance is not null)
                {
                    Instance.UpdateInstance = false;
                    Instance.IsInitialized = true;
                }
                UpdateInstance = false;
                IsInitialized = true;
            }
            catch (Exception exc)
            {
                //UpdateInstance = true;
                OnError(new UnhandledExceptionEventArgs(exc, false));
                IsInitialized = false;
            }
        }
        #endregion

        #region Methods

        #region Private

        #region Misc
        void LoadDefaults()
        {
            PingInterval = 5;
            Target = Print3dServerTarget.RepetierServer;
            ApiKeyRegexPattern = RegexHelper.RepetierServerProApiKey;
            WebSocketTarget = "/socket/";
            WebCamTarget = "/printer/cammjpg/";
            WebSocketMessageReceived -= Client_WebSocketMessageReceived;
            WebSocketMessageReceived += Client_WebSocketMessageReceived;
        }
        #endregion

        #region Download

        public async Task<byte[]> GetDynamicRenderImageAsync(long modelId, bool thumbnail, int timeout = 20000, string? targetUri = "dyn/render_image")
        {
            try
            {
                byte[] resultObject = [];
                string currentPrinter = GetActivePrinterSlug();
                if (string.IsNullOrEmpty(currentPrinter)) return resultObject;

                // http://repetierserver.local/dyn/render_image?q=models&id=158&slug=Prusa_i3_MK3S&t=m
                // https://www.repetier-server.com/manuals/programming/API/index.html
                if (RestClient == null)
                {
                    UpdateRestClientInstance();
                }
                RestRequest request = new(targetUri ??= "dyn/render_image")
                {
                    AlwaysMultipartFormData = true,
                    RequestFormat = DataFormat.None,
                    //RequestFormat = DataFormat.Json,
                    Method = Method.Get,
                    Timeout = TimeSpan.FromMilliseconds(timeout),
                };
                if (!string.IsNullOrEmpty(ApiKey))
                {
                    request.AddHeader("X-Api-Key", $"{ApiKey}");
                }
                //request.AddHeader("Content-Type", "image/png");
                request.AddParameter("q", "models");
                request.AddParameter("id", modelId);
                request.AddParameter("slug", currentPrinter);
                request.AddParameter("t", thumbnail ? "s" : "l");
                request.AddParameter("apikey", ApiKey, ParameterType.QueryString);

                Uri? fullUrl = RestClient?.BuildUri(request);
                if (fullUrl is null) return resultObject;

                HttpClient ??= new();
                byte[]? bytes = await HttpClient.GetByteArrayAsync(fullUrl);
                return bytes ?? resultObject;
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return [];
            }
        }

        public async Task<byte[]> GetDynamicRenderImageByJobIdAsync(long jobId, bool thumbnail, int timeout = 20000, string? targetUri = "dyn/render_image")
        {
            try
            {
                byte[] resultObject = [];
                string currentPrinter = GetActivePrinterSlug();
                if (string.IsNullOrEmpty(currentPrinter)) return resultObject;

                // http://repetierserver.local/dyn/render_image?q=models&id=158&slug=Prusa_i3_MK3S&t=m
                // https://www.repetier-server.com/manuals/programming/API/index.html
                if (RestClient == null)
                {
                    UpdateRestClientInstance();
                }
                RestRequest request = new(targetUri ??= "dyn/render_image")
                {
                    AlwaysMultipartFormData = true,
                    RequestFormat = DataFormat.None,
                    //RequestFormat = DataFormat.Json,
                    Method = Method.Get,
                    Timeout = TimeSpan.FromMilliseconds(timeout),
                };
                if (!string.IsNullOrEmpty(ApiKey))
                {
                    request.AddHeader("X-Api-Key", $"{ApiKey}");
                }
                //request.AddHeader("Content-Type", "image/png");
                request.AddParameter("q", "jobs");
                request.AddParameter("id", jobId);
                request.AddParameter("slug", currentPrinter);
                request.AddParameter("t", thumbnail ? "s" : "l");
                request.AddParameter("apikey", ApiKey, ParameterType.QueryString);

                Uri? fullUrl = RestClient?.BuildUri(request);
                if (fullUrl is null) return resultObject;

                HttpClient ??= new();
                byte[]? bytes = await HttpClient.GetByteArrayAsync(fullUrl);
                return bytes ?? resultObject;
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return [];
            }
        }
        #endregion

        #region StateUpdates
        void UpdatePrinterState(RepetierPrinterState? newState)
        {
            try
            {
                if (newState == null) return;

                ActiveToolheadIndex = (int)newState.ActiveExtruder;
                NumberOfToolHeads = (int)newState.NumExtruder;

                Toolheads ??= new();
                foreach (var ext in newState.Extruder.Select((x, i) => new { Value = x, Index = i }))
                {
                    Toolheads.AddOrUpdate(ext.Index, ext.Value, (key, oldValue) => oldValue = ext.Value);
                }
                IsMultiExtruder = Toolheads?.Count > 1;
                ActiveToolhead = Toolheads?[ActiveToolheadIndex];

                HeatedBeds = new();
                foreach (var ext in newState.HeatedBeds.Select((x, i) => new { Value = x, Index = i }))
                {
                    HeatedBeds.AddOrUpdate(ext.Index, ext.Value, (key, oldValue) => oldValue = ext.Value);
                }
                HasHeatedBed = HeatedBeds?.Count > 0;
                ActiveHeatedBed = HeatedBeds?.FirstOrDefault().Value;

                HeatedChambers = new();
                foreach (var ext in newState.HeatedChambers.Select((x, i) => new { Value = x, Index = i }))
                {
                    HeatedChambers.AddOrUpdate(ext.Index, ext.Value, (key, oldValue) => oldValue = ext.Value);
                }
                HasHeatedBed = HeatedChambers?.Count > 0;
                ActiveHeatedChamber = HeatedChambers?.FirstOrDefault().Value;

                ConcurrentDictionary<string, IPrint3dFan> fans = new();
                for (int i = 0; i < newState.Fans.Count; i++)
                {
                    fans.TryAdd($"{i}", newState.Fans[i]);
                }
                Fans = fans;
                HasFan = Fans?.Count > 0;

                X = newState.X;
                Y = newState.Y;
                Z = newState.Z;

                XHomed = newState.HasXHome;
                YHomed = newState.HasYHome;
                ZHomed = newState.HasZHome;

                ShutdownAfterPrint = newState.ShutdownAfterPrint;

            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
            }
        }
        void UpdatePrinterConfig(RepetierPrinterConfig? newConfig)
        {
            try
            {
                if (newConfig is null) return;
                if (newConfig.Webcams is not null)
                {
                    WebCams = [.. newConfig.Webcams];
                    HasWebCam = WebCams is not null && WebCams.Count > 0;
                    if (HasWebCam)
                        SelectedWebCam = WebCams?.FirstOrDefault();
                }
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
            }
        }
        void UpdateActivePrintInfo(IPrint3dJobStatus? newPrintInfo)
        {
            try
            {
                if (newPrintInfo is not null)
                {
                    if (newPrintInfo is RepetierCurrentPrintInfo info)
                    {
                        IsConnectedPrinterOnline = info.Online > 0;
                    }
                    IsPrinting = !string.IsNullOrEmpty(newPrintInfo.JobId);
                    IsPaused = newPrintInfo.State == Print3dJobState.Paused;
                }
                else
                {
                    IsConnectedPrinterOnline = false;
                    IsPrinting = false;
                    IsPaused = false;
                }
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
            }
        }

        #endregion

        #region Jobs
        async Task<RepetierJobListRespone?> GetJobListResponeAsync(string printerName)
        {
            IRestApiRequestRespone? result = null;
            RepetierJobListRespone? resultObject = null; //new();
            try
            {
                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{printerName}";
                result = await SendRestApiRequestAsync(
                   requestTargetUri: targetUri,
                   method: Method.Post,
                   command: "listJobs",
                   jsonObject: null,
                   authHeaders: AuthHeaders
                   )
                .ConfigureAwait(false);
                /*
                result = await SendRestApiRequestAsync(
                   commandBase: RepetierCommandBase.printer,
                   commandFeature: RepetierCommandFeature.api,
                   command: "listJobs",
                   printerName: printerName)
                    .ConfigureAwait(false);
                */
                return GetObjectFromJson<RepetierJobListRespone>(result?.Result);
            }
            catch (JsonException jecx)
            {
                OnError(new JsonConvertEventArgs()
                {
                    Exception = jecx,
                    OriginalString = result?.Result,
                    TargetType = nameof(RepetierJobListRespone),
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
        #endregion

        #region WebCallActions
        async Task<RepetierWebCallList?> GetWebCallListAsync(string printerName)
        {
            IRestApiRequestRespone? result = null;
            try
            {
                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{printerName}";
                result = await SendRestApiRequestAsync(
                   requestTargetUri: targetUri,
                   method: Method.Post,
                   command: "webCallsList",
                   jsonObject: null,
                   authHeaders: AuthHeaders
                   )
                .ConfigureAwait(false);
                /*
                result = await SendRestApiRequestAsync(
                   commandBase: RepetierCommandBase.printer,
                   commandFeature: RepetierCommandFeature.api,
                   command: "webCallsList",
                   printerName: printerName
                   )
                    .ConfigureAwait(false);
                */
                return GetObjectFromJson<RepetierWebCallList>(result?.Result);
            }
            catch (JsonException jecx)
            {
                OnError(new JsonConvertEventArgs()
                {
                    Exception = jecx,
                    OriginalString = result?.Result,
                    TargetType = nameof(RepetierWebCallList),
                    Message = jecx.Message,
                });
                return new RepetierWebCallList();
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return null;
            }
        }
        #endregion

        #endregion

        #region Public

        #region Proxy
        [Obsolete("Use `SetProxy` instead")]
        public void SetProxyOld(bool secure, string address, int port, bool enable = true)
        {
            EnableProxy = enable;
            ProxyUserUsesDefaultCredentials = true;
            ProxyAddress = address;
            ProxyPort = port;
            ProxyUser = string.Empty;
            ProxyPassword = null;
            SecureProxyConnection = secure;
            UpdateRestClientInstance();
        }

        [Obsolete("Use `SetProxy` instead")]
        public void SetProxOldy(bool secure, string address, int port, string user = "", string? password = null, bool enable = true)
        {
            EnableProxy = enable;
            ProxyUserUsesDefaultCredentials = false;
            ProxyAddress = address;
            ProxyPort = port;
            ProxyUser = user;
            ProxyPassword = password;
            SecureProxyConnection = secure;
            UpdateRestClientInstance();
        }
        #endregion

        #region Refresh
        public new Task StartListeningAsync(bool stopActiveListening = false, string[]? commandsOnConnect = null) => StartListeningAsync(WebSocketTargetUri, stopActiveListening, () => Task.Run(async () =>
        {
            List<Task> tasks =
            [
                RefreshPrinterStateAsync(),
                RefreshCurrentPrintInfosAsync(),
            ];
            await Task.WhenAll(tasks).ConfigureAwait(false);
        }), commandsOnConnect: commandsOnConnect);
        public new Task RefreshAllAsync() => RefreshAllAsync(GcodeImageType.Thumbnail);
        public async Task RefreshAllAsync(GcodeImageType imageType = GcodeImageType.Thumbnail)
        {
            try
            {
                await base.RefreshAllAsync().ConfigureAwait(false);
                // Avoid multiple calls
                if (IsRefreshing) return;
                IsRefreshing = true;
                //await RefreshPrinterListAsync();
                List<Task> task =
                [
                    CheckForServerUpdateAsync(),
                    RefreshDiskSpaceAsync(),
                    RefreshModelGroupsAsync(),
                    RefreshModelsAsync(imageType),
                    RefreshPrinterStateAsync(),
                    RefreshPrinterConfigAsync(),
                    RefreshCurrentPrintInfosAsync(),
                    RefreshExternalCommandsAsync(),
                    RefreshMessagesAsync(),
                    RefreshWebCallsAsync(),
                    RefreshGPIOListAsync(),
                    RefreshJobListAsync(),
                ];
                await Task.WhenAll(task).ConfigureAwait(false);
                if (!InitialDataFetched)
                    InitialDataFetched = true;
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
            }
            IsRefreshing = false;
        }
        #endregion

        #region Login

        /*
        public async Task LoginAsync(string UserName, SecureString Password, string SessionId, bool remember = true)
        {
            try
            {

                if (string.IsNullOrEmpty(SessionId)) SessionId = this.SessionId;
                if (string.IsNullOrEmpty(SessionId) || !IsListeningToWebsocket)
                    throw new Exception($"Current session is null! Please start the Listener first to establish a WebSocket connection!");

                // Password is MD5(sessionId + MD5(login + password))
                string encryptedPassword = EncryptPassword(UserName, Password, SessionId);
                var cmd = new
                {
                    login = UserName,
                    password = encryptedPassword,
                    rememberMe = remember,
                };

                string command = 
                    $"{{\"action\":\"login\",\"data\":{{\"login\":\"{UserName}\",\"password\":\"{encryptedPassword}\",\"rememberMe\":{(remember ? "true" : "false")}}},\"printer\":\"{GetActivePrinterSlug()}\",\"callback_id\":{99}}}";


                if (WebSocket.State == WebSocketState.Open)
                {
                    WebSocket.Send(command);
                }
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
            }
        }
        */
        public void Login(string userName, SecureString password, string sessionId, bool remember = true)
        {
            if (string.IsNullOrEmpty(sessionId)) sessionId = this.SessionId;
            if (string.IsNullOrEmpty(sessionId) || !IsListeningToWebsocket)
                throw new Exception($"Current session is null! Please start the Listener first to establish a WebSocket connection!");

            // Password is MD5(sessionId + MD5(login + password))
            string encryptedPassword = EncryptPassword(userName, password, sessionId);
            string command =
                $"{{\"action\":\"login\",\"data\":{{\"login\":\"{userName}\",\"password\":\"{encryptedPassword}\",\"rememberMe\":{(remember ? "true" : "false")}}},\"printer\":\"{GetActivePrinterSlug()}\",\"callback_id\":{99}}}";
            _ = SendWebSocketCommandAsync(command);
        }

        public async Task LoginAsync(string userName, SecureString password, string sessionId, bool remember = true)
        {
            if (string.IsNullOrEmpty(sessionId)) sessionId = this.SessionId;
            if (string.IsNullOrEmpty(sessionId) || !IsListeningToWebsocket)
                throw new Exception($"Current session is null! Please start the Listener first to establish a WebSocket connection!");

            // Password is MD5(sessionId + MD5(login + password))
            string encryptedPassword = EncryptPassword(userName, password, sessionId);
            string command =
                $"{{\"action\":\"login\",\"data\":{{\"login\":\"{userName}\",\"password\":\"{encryptedPassword}\",\"rememberMe\":{(remember ? "true" : "false")}}},\"printer\":\"{GetActivePrinterSlug()}\",\"callback_id\":{99}}}";
            await SendWebSocketCommandAsync(command);
        }

        public async Task LogoutAsync()
        {
            if (string.IsNullOrEmpty(SessionId) || !IsListeningToWebsocket)
                throw new Exception($"Current session is null! Please start the Listener first to establish a WebSocket connection!");
            //_ = await SendRestApiRequestAsync("", "logout").ConfigureAwait(false);

            string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{GetActivePrinterSlug()}";
            _ = await SendRestApiRequestAsync(
               requestTargetUri: targetUri,
               method: Method.Post,
               command: "logout",
               jsonObject: null,
               authHeaders: AuthHeaders
               )
            .ConfigureAwait(false);
            /*
            _ = await SendRestApiRequestAsync(RepetierCommandBase.printer, RepetierCommandFeature.api, command: "logout")
                .ConfigureAwait(false);
            */
        }

        public void Logout()
        {
            if (string.IsNullOrEmpty(SessionId) || !IsListeningToWebsocket)
                throw new Exception($"Current session is null! Please start the Listener first to establish a WebSocket connection!");

            string command =
                $"{{\"action\":\"logout\",\"data\":{{}},\"printer\":\"{GetActivePrinterSlug()}\",\"callback_id\":{PingCounter++}}}";
            _ = SendWebSocketCommandAsync(command);
        }

        public async Task LogoutViaWebSocketCommandAsync()
        {
            if (string.IsNullOrEmpty(SessionId) || !IsListeningToWebsocket)
                throw new Exception($"Current session is null! Please start the Listener first to establish a WebSocket connection!");

            string command =
                $"{{\"action\":\"logout\",\"data\":{{}},\"printer\":\"{GetActivePrinterSlug()}\",\"callback_id\":{PingCounter++}}}";
            await SendWebSocketCommandAsync(command);
        }


        string EncryptPassword(string userName, SecureString password, string sessionId)
        {
            // Password is MD5(sessionId + MD5(login + password))
            // Source: https://www.godo.dev/tutorials/csharp-md5/
            using MD5 md5 = MD5.Create();
            string credentials = $"{userName}{SecureStringHelper.ConvertToString(password)}";
            // Hash credentials first
            md5.ComputeHash(Encoding.UTF8.GetBytes(credentials));
            List<byte> inputBuffer = [.. Encoding.UTF8.GetBytes(sessionId)];

            if (md5?.Hash is null) return "";

            string hexHash = BitConverter.ToString(md5.Hash).Replace("-", string.Empty).ToLowerInvariant();
            inputBuffer.AddRange(Encoding.UTF8.GetBytes(hexHash));

            md5.ComputeHash([.. inputBuffer]);

            // Get hash result after compute it  
            byte[] hashedCredentials = md5
                .Hash;

            StringBuilder strBuilder = new();
            for (int i = 0; i < hashedCredentials.Length; i++)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilder.Append(hashedCredentials[i].ToString("x2"));
            }
            return strBuilder.ToString();
        }
        #endregion

        #region ActivePrinter
        public override async Task SetPrinterActiveAsync(int index = -1, bool refreshPrinterList = true)
        {
            try
            {
                if (refreshPrinterList)
                {
                    await RefreshPrinterListAsync().ConfigureAwait(false);
                }

                if (Printers.Count > index && index >= 0)
                {
                    ActivePrinter = Printers[index];
                }
                else
                {
                    // If no index is provided, or it's out of bound, the first online printer is used
                    ActivePrinter = Printers.FirstOrDefault(printer => printer.IsOnline);
                    // If no online printers is found, however there is at least one printer configured, use this one
                    if (ActivePrinter == null && Printers.Count > 0)
                    {
                        ActivePrinter = Printers[0];
                    }
                }
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
            }
        }

        public override async Task SetPrinterActiveAsync(string slug, bool refreshPrinterList = true)
        {
            try
            {
                if (refreshPrinterList)
                    await RefreshPrinterListAsync().ConfigureAwait(false);
                IPrinter3d? printer = Printers.FirstOrDefault(prt => prt.Slug == slug);
                if (printer is not null)
                    ActivePrinter = printer;
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
            }
        }

        #endregion

        #region CheckOnline

        public async Task<bool> CheckOnlineWithApiCallAsync(int timeout = 10000)
        {
            IsConnecting = true;
            bool isReachable = false;
            try
            {
                if (IsReady)
                {
                    // Send an empty command to check the respone
                    string pingCommand = "{}";
                    string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Ping}/{GetActivePrinterSlug()}";
                    IRestApiRequestRespone? result = await SendRestApiRequestAsync(
                           requestTargetUri: targetUri,
                           method: Method.Post,
                           command: pingCommand,
                           jsonObject: null,
                           authHeaders: AuthHeaders,
                           cts: new(timeout)
                           )
                        .ConfigureAwait(false);
                    /*
                    RepetierApiRequestRespone respone = await SendRestApiRequestAsync(
                       commandBase: RepetierCommandBase.printer,
                       commandFeature: RepetierCommandFeature.ping,
                       command: pingCommand,
                       cts: new(timeout)
                       )
                    .ConfigureAwait(false);
                    */
                    if (result is not null)
                    {
                        isReachable = result.IsOnline;
                    }
                }
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
            }
            IsConnecting = false;
            IsOnline = isReachable;
            return isReachable;
        }

        public new async Task CheckOnlineAsync(int timeout = 10000)
        {
            CancellationTokenSource cts = new(timeout);
            await CheckOnlineAsync(cts).ConfigureAwait(false);
            cts?.Dispose();
        }

        public Task CheckOnlineAsync(CancellationTokenSource cts) => CheckOnlineAsync($"{RepetierCommands.Base}/{RepetierCommands.Api}/{GetActivePrinterSlug()}", AuthHeaders, "{}", cts);
        public Task<bool> CheckIfApiIsValidAsync(int timeout = 10000) => CheckIfApiIsValidAsync($"{RepetierCommands.Base}/{RepetierCommands.Api}/{GetActivePrinterSlug()}", AuthHeaders, "{}", timeout);
        public Task CheckServerIfApiIsValidAsync(int timeout = 10000) => CheckIfApiIsValidAsync(timeout);

        #endregion

        #region Updates
        public async Task CheckForServerUpdateAsync()
        {
            IRestApiRequestRespone? result = null;
            try
            {
                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{GetActivePrinterSlug()}";
                result = await SendRestApiRequestAsync(
                       requestTargetUri: targetUri,
                       method: Method.Post,
                       command: "checkForUpdates",
                       jsonObject: null,
                       authHeaders: AuthHeaders
                       )
                    .ConfigureAwait(false);
                /*
                result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api, command: "checkForUpdates").ConfigureAwait(false);
                */
                if (result is not null)
                {
                    if (GetQueryResult(result.Result))
                    {
                        Update = await GetAvailableServerUpdateAsync().ConfigureAwait(false);
                        if (Update == null) return;

                        Version current = new(Update.CurrentVersion);
                        Version update = new(Update.VersionName);
                        UpdateAvailable = update > current;
                    }
                }
            }
            catch (JsonException jecx)
            {
                OnError(new JsonConvertEventArgs()
                {
                    Exception = jecx,
                    OriginalString = result?.Result,
                    Message = jecx.Message,
                });
            }
            catch (Exception exc)
            {
                UpdateAvailable = false;
                OnError(new UnhandledExceptionEventArgs(exc, false));
            }
        }
        public async Task AutoUpdateAsync()
        {
            IRestApiRequestRespone? result = null;
            try
            {
                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{GetActivePrinterSlug()}";
                result = await SendRestApiRequestAsync(
                       requestTargetUri: targetUri,
                       method: Method.Post,
                       command: "autoupdate",
                       jsonObject: null,
                       authHeaders: AuthHeaders
                       )
                    .ConfigureAwait(false);
                /*
                result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api, command: "autoupdate")
                    .ConfigureAwait(false);
                */
                if (result is not null)
                {
                    if (GetQueryResult(result.Result))
                    {
                        Update = await GetAvailableServerUpdateAsync().ConfigureAwait(false);
                        if (Update == null) return;

                        Version current = new(Update.CurrentVersion);
                        Version update = new(Update.VersionName);
                        UpdateAvailable = update > current;
                    }
                }
            }
            catch (JsonException jecx)
            {
                OnError(new JsonConvertEventArgs()
                {
                    Exception = jecx,
                    OriginalString = result?.Result,
                    Message = jecx.Message,
                });
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
            }
        }
        public async Task<RepetierAvailableUpdateInfo?> GetAvailableServerUpdateAsync()
        {
            IRestApiRequestRespone? result = null;
            RepetierAvailableUpdateInfo resultObject = new();
            try
            {
                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{GetActivePrinterSlug()}";
                result = await SendRestApiRequestAsync(
                       requestTargetUri: targetUri,
                       method: Method.Post,
                       command: "updateAvailable",
                       jsonObject: null,
                       authHeaders: AuthHeaders
                       )
                    .ConfigureAwait(false);
                /*
                result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api, command: "updateAvailable")
                    .ConfigureAwait(false);
                */
                return GetObjectFromJson<RepetierAvailableUpdateInfo>(result?.Result);
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
        #endregion

        #region License

        public async Task<RepetierLicenseInfo?> GetLicenseDataAsync()
        {
            IRestApiRequestRespone? result = null;
            try
            {
                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{GetActivePrinterSlug()}";
                result = await SendRestApiRequestAsync(
                       requestTargetUri: targetUri,
                       method: Method.Post,
                       command: "getLicenceData",
                       jsonObject: null,
                       authHeaders: AuthHeaders
                       )
                    .ConfigureAwait(false);
                /*
                result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api, command: "getLicenceData")
                    .ConfigureAwait(false);
                */
                if (result is not null)
                {
                    return GetObjectFromJson<RepetierLicenseInfo>(result?.Result);
                }
                else
                    return null;
            }
            catch (JsonException jecx)
            {
                OnError(new JsonConvertEventArgs()
                {
                    Exception = jecx,
                    OriginalString = result?.Result,
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
        #endregion

        #region DetectChanges
        public bool CheckIfConfigurationHasChanged(object temp)
        {
            try
            {
                if (temp is not RepetierClient tempServer) return false;
                else
                {
                    return
                    !(ServerAddress == tempServer.ServerAddress &&
                        Port == tempServer.Port &&
                        ApiKey == tempServer.ApiKey &&
                        IsSecure == tempServer.IsSecure
                        )
                    ;
                }
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return false;
            }
        }
        #endregion

        #region GCode

        public async Task<RepetierErrorCodes> SendGcodeFileAsync(string printerName, string filePath)
        {
            try
            {
                return await SendAndMoveGcodeFileAsync(printerName, filePath).ConfigureAwait(false);
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return RepetierErrorCodes.EXCEPTION;
            }
        }

        public async Task<RepetierErrorCodes> SendAndMoveGcodeFileAsync(string printerName, string filePath, string group = "#", int timeout = 25000)
        {
            // https://www.repetier-server.com/using-simplify-3d-repetier-server/
            try
            {
                FileInfo info = new(filePath);
                if (!info.Exists) return RepetierErrorCodes.FILE_NOT_FOUND;

                if (RestClient == null)
                {
                    UpdateRestClientInstance();
                }
                RestRequest request = new(string.Format("/printer/model/{2}", ServerAddress, Port, printerName.Replace(" ", "_")))
                {
                    Method = Method.Post,
                    Timeout = TimeSpan.FromMilliseconds(timeout),
                    AlwaysMultipartFormData = true,
                };

                request.AddHeader("x-api-key", ApiKey);
                request.AddFile(Path.GetFileNameWithoutExtension(filePath), filePath);

                CancellationTokenSource cts = new(timeout);
                if (RestClient is not null)
                {
                    RestResponse? respone = await RestClient.ExecuteAsync(request, cts.Token).ConfigureAwait(false);

                    if (respone.StatusCode == HttpStatusCode.OK)
                    {
                        if (group != "#")
                        {
                            List<IGcode> list = await GetModelsAsync(printerName).ConfigureAwait(false);
                            string fileName = info.Name.Replace(Path.GetExtension(filePath), string.Empty);
                            IGcode? model = list.FirstOrDefault(m => m.FileName == fileName);
                            if (model is not null)
                            {
                                bool result = await MoveModelToGroupAsync(printerName, group, model.Identifier).ConfigureAwait(false);
                            }
                        }
                        return RepetierErrorCodes.SUCCESS;
                    }
                    else return RepetierErrorCodes.FAILED;
                }
                else return RepetierErrorCodes.MISS_CONFIGURATION;
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return RepetierErrorCodes.EXCEPTION;
            }
        }

        #endregion

        #region Jobs
        public async Task<ObservableCollection<IPrint3dJob>> GetJobListAsync()
        {
            ObservableCollection<IPrint3dJob> resultObject = [];

            string currentPrinter = GetActivePrinterSlug();
            if (string.IsNullOrEmpty(currentPrinter)) return resultObject;

            try
            {
                RepetierJobListRespone? info = await GetJobListResponeAsync(currentPrinter).ConfigureAwait(false);
                if (info is not null && info.Data is not null)
                    return new ObservableCollection<IPrint3dJob>(info.Data);
                else
                    return resultObject;
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return resultObject;
            }
        }
        public async Task RefreshJobListAsync()
        {
            try
            {
                ObservableCollection<IPrint3dJob> jobList = [];
                if (!IsReady || ActivePrinter == null)
                {
                    Jobs = jobList;
                    return;
                }

                string currentPrinter = ActivePrinter.Slug;
                if (string.IsNullOrEmpty(currentPrinter)) return;

                RepetierJobListRespone? result = await GetJobListResponeAsync(currentPrinter).ConfigureAwait(false);
                Jobs = result is not null ? new(result.Data) : jobList;

            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                Jobs = new();
            }
        }

        public async Task<bool> StartJobAsync(long id)
        {
            try
            {
                string currentPrinter = GetActivePrinterSlug();
                if (string.IsNullOrEmpty(currentPrinter))
                {
                    return false;
                }
                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{currentPrinter}";
                IRestApiRequestRespone? result = await SendRestApiRequestAsync(
                       requestTargetUri: targetUri,
                       method: Method.Post,
                       command: "startJob",
                       jsonObject: new { id = id },
                       authHeaders: AuthHeaders
                       )
                    .ConfigureAwait(false);
                /*
                RepetierApiRequestRespone result =
                    await SendRestApiRequestAsync(
                        RepetierCommandBase.printer, RepetierCommandFeature.api,
                        command: "startJob", jsonData: string.Format("{{\"id\":{0}}}", id),
                        printerName: currentPrinter)
                    .ConfigureAwait(false);
                */
                return GetQueryResult(result?.Result, true);
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return false;
            }
        }
        public async Task<bool> StartJobAsync(string id)
        {
            try
            {
                string currentPrinter = GetActivePrinterSlug();
                if (string.IsNullOrEmpty(currentPrinter))
                {
                    return false;
                }
                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{currentPrinter}";
                IRestApiRequestRespone? result = await SendRestApiRequestAsync(
                       requestTargetUri: targetUri,
                       method: Method.Post,
                       command: "startJob",
                       jsonObject: new { id = id },
                       authHeaders: AuthHeaders
                       )
                    .ConfigureAwait(false);
                /*
                RepetierApiRequestRespone result =
                    await SendRestApiRequestAsync(
                        RepetierCommandBase.printer, RepetierCommandFeature.api,
                        command: "startJob", jsonData: string.Format("{{\"id\":{0}}}", id),
                        printerName: currentPrinter)
                    .ConfigureAwait(false);
                */
                return GetQueryResult(result?.Result, true);
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return false;
            }
        }

        public Task<bool> StartJobAsync(IGcode model) => StartJobAsync(model.Identifier);
        public Task<bool> StartJobAsync(RepetierJobListItem jobItem) => StartJobAsync(jobItem.Identifier);
        public Task<bool> StartJobAsync(IPrint3dJob jobItem) => StartJobAsync(jobItem.JobId);

        public async Task<bool> RemoveJobAsync(long jobId)
        {
            try
            {
                string currentPrinter = GetActivePrinterSlug();
                if (string.IsNullOrEmpty(currentPrinter))
                {
                    return false;
                }
                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{currentPrinter}";
                IRestApiRequestRespone? result = await SendRestApiRequestAsync(
                       requestTargetUri: targetUri,
                       method: Method.Post,
                       command: "removeJob",
                       jsonObject: new { id = jobId },
                       authHeaders: AuthHeaders
                       )
                    .ConfigureAwait(false);
                /*
                RepetierApiRequestRespone result =
                    await SendRestApiRequestAsync(
                        RepetierCommandBase.printer, RepetierCommandFeature.api,
                        command: "removeJob", jsonData: string.Format("{{\"id\":{0}}}", jobId),
                        printerName: currentPrinter)
                    .ConfigureAwait(false);
                */
                await RefreshJobListAsync().ConfigureAwait(false);
                return GetQueryResult(result?.Result);
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return false;
            }
        }
        public async Task<bool> RemoveJobAsync(string jobId)
        {
            try
            {
                string currentPrinter = GetActivePrinterSlug();
                if (string.IsNullOrEmpty(currentPrinter))
                {
                    return false;
                }
                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{currentPrinter}";
                IRestApiRequestRespone? result = await SendRestApiRequestAsync(
                       requestTargetUri: targetUri,
                       method: Method.Post,
                       command: "removeJob",
                       jsonObject: new { id = jobId },
                       authHeaders: AuthHeaders
                       )
                    .ConfigureAwait(false);
                /*
                RepetierApiRequestRespone result =
                    await SendRestApiRequestAsync(
                        RepetierCommandBase.printer, RepetierCommandFeature.api,
                        command: "removeJob", jsonData: string.Format("{{\"id\":{0}}}", jobId),
                        printerName: currentPrinter)
                    .ConfigureAwait(false);
                */
                await RefreshJobListAsync().ConfigureAwait(false);
                return GetQueryResult(result?.Result);
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return false;
            }
        }
        public Task<bool> RemoveJobAsync(RepetierCurrentPrintInfo job) => RemoveJobAsync(job.JobIdLong);
        public Task<bool> RemoveJobAsync(RepetierJobListItem job) => RemoveJobAsync(job.Identifier);
        public Task<bool> RemoveJobAsync(IPrint3dJob job) => RemoveJobAsync(job.JobId);

        public async Task<bool> ContinueJobAsync(string printerName = "")
        {
            try
            {
                string currentPrinter = GetActivePrinterSlug();
                if (!string.IsNullOrEmpty(printerName))
                {
                    currentPrinter = printerName; // Override current selected printer, if needed
                }
                if (string.IsNullOrEmpty(currentPrinter))
                {
                    return false;
                }
                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{currentPrinter}";
                IRestApiRequestRespone? result = await SendRestApiRequestAsync(
                       requestTargetUri: targetUri,
                       method: Method.Post,
                       command: "continueJob",
                       jsonObject: null,
                       authHeaders: AuthHeaders
                       )
                    .ConfigureAwait(false);
                /*
                RepetierApiRequestRespone result =
                    await SendRestApiRequestAsync(RepetierCommandBase.printer, RepetierCommandFeature.api, command: "continueJob", printerName: currentPrinter)
                    .ConfigureAwait(false);
                */
                return GetQueryResult(result?.Result, true);

            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return false;
            }
        }

        public async Task<bool> PauseJobAsync(string printerName = "")
        {
            try
            {
                //bool result = await SendGcodeCommandAsync("@pause", printerName).ConfigureAwait(false);
                bool result = await SendGcodeAsync("@pause", printerName).ConfigureAwait(false);
                return result;
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return false;
            }
        }

        public async Task<bool> StopJobAsync(string printerName = "")
        {
            try
            {
                string currentPrinter = GetActivePrinterSlug();
                if (!string.IsNullOrEmpty(printerName))
                {
                    currentPrinter = printerName; // Override current selected printer, if needed
                }
                if (string.IsNullOrEmpty(currentPrinter))
                {
                    return false;
                }

                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{currentPrinter}";
                IRestApiRequestRespone? result = await SendRestApiRequestAsync(
                       requestTargetUri: targetUri,
                       method: Method.Post,
                       command: "stopJob",
                       jsonObject: null,
                       authHeaders: AuthHeaders
                       )
                    .ConfigureAwait(false);
                /*
                RepetierApiRequestRespone result =
                    await SendRestApiRequestAsync(
                        RepetierCommandBase.printer, RepetierCommandFeature.api,
                        command: "stopJob", printerName: currentPrinter)
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

        public async Task<bool> SetShutdownAfterPrintAsync(bool shutdown)
        {
            try
            {
                string currentPrinter = GetActivePrinterSlug();
                if (string.IsNullOrEmpty(currentPrinter))
                {
                    return false;
                }
                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{currentPrinter}";
                IRestApiRequestRespone? result = await SendRestApiRequestAsync(
                       requestTargetUri: targetUri,
                       method: Method.Post,
                       command: "setShutdownAfterPrint",
                       jsonObject: new { shutdown = shutdown ? "true" : "false" },
                       authHeaders: AuthHeaders
                       )
                    .ConfigureAwait(false);
                /*
                RepetierApiRequestRespone result =
                    await SendRestApiRequestAsync(
                        RepetierCommandBase.printer, RepetierCommandFeature.api,
                        command: "setShutdownAfterPrint", jsonData: $"{{\"shutdown\":{(shutdown ? "true" : "false")}}}",
                        printerName: currentPrinter)
                    .ConfigureAwait(false);
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

        #region PrinterState
        public async Task<Dictionary<string, RepetierPrinterState>?> GetStatesAsync(string printerName = "")
        {
            IRestApiRequestRespone? result = null;
            string resultString = string.Empty;
            Dictionary<string, RepetierPrinterState> resultObject = [];

            string currentPrinter = string.IsNullOrEmpty(printerName) ? GetActivePrinterSlug() : printerName;
            if (string.IsNullOrEmpty(currentPrinter))
            {
                return resultObject;
            }

            try
            {
                if (!IsReady)
                {
                    return [];
                }
                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{currentPrinter}";
                result = await SendRestApiRequestAsync(
                       requestTargetUri: targetUri,
                       method: Method.Post,
                       command: "stateList",
                       jsonObject: null,
                       authHeaders: AuthHeaders
                       )
                    .ConfigureAwait(false);
                /*
                result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api,
                    command: "stateList", printerName: currentPrinter)
                    .ConfigureAwait(false);
                */
                Dictionary<string, RepetierPrinterState>? state = GetObjectFromJson<Dictionary<string, RepetierPrinterState>>(result?.Result);
                if (state is not null && IsPrinterSlugSelected(currentPrinter))
                {
                    State = state.FirstOrDefault(keypair => keypair.Key == ActivePrinter?.Slug).Value ?? state.FirstOrDefault().Value;
                }
                return state;
            }
            catch (JsonException jecx)
            {
                OnError(new JsonConvertEventArgs()
                {
                    Exception = jecx,
                    OriginalString = resultString,
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
        public async Task RefreshPrinterStateAsync()
        {
            try
            {
                if (!IsReady || ActivePrinter == null)
                {
                    return;
                }
                Dictionary<string, RepetierPrinterState>? result = await GetStatesAsync().ConfigureAwait(false);
                if (result is not null && result?.Count > 0)
                {
                    State = result.FirstOrDefault(keypair => keypair.Key == ActivePrinter?.Slug).Value ?? result.FirstOrDefault().Value;
                }
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
            }
        }

        public async Task<RepetierPrinterState?> GetStateForPrinterAsync(string printerName)
        {
            Dictionary<string, RepetierPrinterState>? states = await GetStatesAsync().ConfigureAwait(false);
            return states?.FirstOrDefault(keypair => keypair.Key == printerName).Value;
        }

        #endregion

        #region CurrentPrintInfo
        public async Task<RepetierCurrentPrintInfo?> GetCurrentPrintInfoAsync()
        {
            RepetierCurrentPrintInfo? resultObject = null;

            string currentPrinter = GetActivePrinterSlug();
            if (string.IsNullOrEmpty(currentPrinter)) return resultObject;

            try
            {
                ObservableCollection<RepetierCurrentPrintInfo> listResult = await GetCurrentPrintInfosAsync().ConfigureAwait(false);
                if (listResult is not null)
                {
                    resultObject = listResult.FirstOrDefault(jobInfo => jobInfo.Slug == currentPrinter);
                }
                return resultObject;
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return resultObject;
            }
        }
        public async Task<ObservableCollection<RepetierCurrentPrintInfo>> GetCurrentPrintInfosAsync(string printerName = "")
        {
            ObservableCollection<RepetierCurrentPrintInfo> resultObject = new();

            string currentPrinter = string.IsNullOrEmpty(printerName) ? GetActivePrinterSlug() : printerName;
            if (string.IsNullOrEmpty(currentPrinter)) return resultObject;

            IRestApiRequestRespone? result = null;
            try
            {
                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{currentPrinter}";
                result = await SendRestApiRequestAsync(
                       requestTargetUri: targetUri,
                       method: Method.Post,
                       command: "listPrinter",
                       jsonObject: null,
                       authHeaders: AuthHeaders
                       )
                    .ConfigureAwait(false);
                /*
                result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api, command: "listPrinter", printerName: currentPrinter)
                    .ConfigureAwait(false);
                */
                RepetierCurrentPrintInfo[]? info = GetObjectFromJson<RepetierCurrentPrintInfo[]>(result?.Result);
                if (info is not null)
                {
                    resultObject = new ObservableCollection<RepetierCurrentPrintInfo>(info);
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
        public async Task RefreshCurrentPrintInfosAsync()
        {
            try
            {
                ObservableCollection<RepetierCurrentPrintInfo> result = await GetCurrentPrintInfosAsync().ConfigureAwait(false);
                ActiveJobs = new(result);

                RepetierCurrentPrintInfo? job = ActiveJobs
                    .Cast<RepetierCurrentPrintInfo>()
                    .FirstOrDefault(info => info.Slug == GetActivePrinterSlug());
                bool updatePrintImage = false;
                if (job?.JobId != ActiveJob?.JobId)
                {
                    updatePrintImage = true;
                }
                else
                {
                    if (CurrentPrintImage is null || CurrentPrintImage.Length == 0)
                    {
                        updatePrintImage = true;
                    }
                }
                ActiveJob = job;
                if (updatePrintImage)
                {
                    if (ActiveJob is RepetierCurrentPrintInfo info)
                    {
                        CurrentPrintImage = info?.JobIdLong > 0
                            ? await GetDynamicRenderImageByJobIdAsync(info.JobIdLong, false).ConfigureAwait(false)
                            : [];
                    }
                }
                UpdateActivePrintInfo(ActiveJob);
                /*
                ActivePrintInfos = result ?? new ObservableCollection<RepetierCurrentPrintInfo>();
                ActivePrintInfo = ActivePrintInfos.FirstOrDefault(info => info.Slug == GetActivePrinterSlug());
                CurrentPrintImage = ActivePrintInfo?.JobIdLong > 0
                    ? await GetDynamicRenderImageByJobIdAsync(ActivePrintInfo.JobIdLong, false).ConfigureAwait(false)
                    : [];
                */
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                ActiveJobs = new();
                //ActivePrintInfos = new ObservableCollection<RepetierCurrentPrintInfo>();
            }
        }

        public async Task<RepetierCurrentPrintInfo?> GetCurrentPrintInfoForPrinterAsync(string printerName)
        {
            RepetierCurrentPrintInfo? resultObject = null;
            try
            {
                ObservableCollection<RepetierCurrentPrintInfo> listResult = await GetCurrentPrintInfosAsync().ConfigureAwait(false);
                if (listResult is not null)
                {
                    resultObject = listResult.FirstOrDefault(jobInfo => jobInfo.Slug == printerName);
                }
                return resultObject;
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return resultObject;
            }
        }

        #endregion

        #region Control Commands

        #region Active Extruder

        public async Task SetExtruderActiveAsync(int extruder)
        {
            try
            {
                string cmd = $"T{extruder}\\nG92 E0";
                _ = await SendGcodeAsync(command: "send", data: new { cmd = cmd }).ConfigureAwait(false);
                //_ = await SendGcodeCommandAsync(cmd).ConfigureAwait(false);
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
            }
        }

        #endregion

        #region Movement
        public async Task<bool> HomeAsync(bool x, bool y, bool z)
        {
            try
            {
                bool result;
                if (x && y && z)
                {
                    result = await SendGcodeAsync(command: "send", data: new { cmd = "G28" }).ConfigureAwait(false);
                    //result = await SendGcodeCommandAsync("G28").ConfigureAwait(false);
                }
                else
                {
                    string cmd = string.Format("G28{0}{1}{2}", x ? " X0 " : "", y ? " Y0 " : "", z ? " Z0 " : "");
                    result = await SendGcodeAsync(command: "send", data: new { cmd = cmd }).ConfigureAwait(false);
                    //result = await SendGcodeCommandAsync(cmd).ConfigureAwait(false);
                }
                return result;
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
            }
            return false;
        }

        public async Task MoveAxesAsync(
            double speed = 100,
            double x = double.PositiveInfinity,
            double y = double.PositiveInfinity,
            double z = double.PositiveInfinity,
            double e = double.PositiveInfinity,
            bool relative = false)
        {
            try
            {
                string currentPrinter = GetActivePrinterSlug();
                if (string.IsNullOrEmpty(currentPrinter)) return;

                if (Config == null)
                    await GetPrinterConfigAsync().ConfigureAwait(false);
                if (State == null)
                    await RefreshPrinterStateAsync().ConfigureAwait(false);

                RepetierPrinterConfigMovement? shape = Config?.Movement;
                var newX = MathHelper.Clamp(relative ? State?.X ?? 0 + x : x, shape?.XMin ?? 0, shape?.XMax ?? 0);
                var newY = MathHelper.Clamp(relative ? State?.Y ?? 0 + y : y, shape?.YMin ?? 0, shape?.YMax ?? 0);
                var newZ = MathHelper.Clamp(relative ? State?.Z ?? 0 + z : z, shape?.ZMin ?? 0, shape?.ZMax ?? 0);

                string data = $"{{\"speed\":{speed}" +
                    string.Format(",\"relative\":{0}", relative ? "true" : "false") +
                    (double.IsInfinity(x) ? "" : $",\"x\":{newX}") +
                    (double.IsInfinity(y) ? "" : $",\"y\":{newY}") +
                    (double.IsInfinity(z) ? "" : $",\"z\":{newZ}") +
                    (double.IsInfinity(e) ? "" : $",\"e\":{e}") +
                    $"}}";

                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{currentPrinter}";
                IRestApiRequestRespone? result = await SendRestApiRequestAsync(
                       requestTargetUri: targetUri,
                       method: Method.Post,
                       command: "move",
                       jsonObject: data,
                       authHeaders: AuthHeaders
                       )
                    .ConfigureAwait(false);
                /*
                RepetierApiRequestRespone result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api,
                    command: "move", jsonData: data,
                    printerName: currentPrinter
                    ).ConfigureAwait(false);
                */
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
            }
        }

        #endregion

        #region Temperatures

        public async Task<bool> SetExtruderTemperatureAsync(int extruder, int temperature)
        {
            try
            {
                string currentPrinter = GetActivePrinterSlug();
                if (string.IsNullOrEmpty(currentPrinter)) return false;

                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{currentPrinter}";
                return await SetExtruderTemperatureAsync("setExtruderTemperature", new { temperature, extruder }, targetUri).ConfigureAwait(false);
                /*
                IRestApiRequestRespone? result = await SendRestApiRequestAsync(
                       requestTargetUri: targetUri,
                       method: Method.Post,
                       command: "setExtruderTemperature",
                       jsonObject: new { temperature, extruder },
                       authHeaders: AuthHeaders
                       )
                    .ConfigureAwait(false);
                */
                /*
                RepetierApiRequestRespone result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api,
                    command: "setExtruderTemperature",
                    jsonData: string.Format("{{\"temperature\":{0}, \"extruder\":{1}}}", temperature, extruder),
                    printerName: currentPrinter
                    ).ConfigureAwait(false);
                */
                //return GetQueryResult(result?.Result, true);
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return false;
            }
        }
        public async Task<bool> SetBedTemperatureAsync(int bedId, int temperature)
        {
            try
            {
                string currentPrinter = GetActivePrinterSlug();
                if (string.IsNullOrEmpty(currentPrinter)) return false;

                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{currentPrinter}";
                return await SetBedTemperatureAsync("setBedTemperature", new { temperature, bedId }, targetUri).ConfigureAwait(false);
                /*
                IRestApiRequestRespone? result = await SendRestApiRequestAsync(
                       requestTargetUri: targetUri,
                       method: Method.Post,
                       command: "setBedTemperature",
                       jsonObject: new { temperature, bedId },
                       authHeaders: AuthHeaders
                       )
                    .ConfigureAwait(false);
                */
                /*
                RepetierApiRequestRespone result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api,
                    command: "setBedTemperature",
                    jsonData: string.Format("{{\"temperature\":{0}, \"bedId\":{1}}}", temperature, bedId),
                    printerName: currentPrinter
                    ).ConfigureAwait(false);
                */
                //return GetQueryResult(result?.Result, true);
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return false;
            }
        }
        public async Task<bool> SetChamberTemperatureAsync(int chamberId, int temperature)
        {
            try
            {
                string currentPrinter = GetActivePrinterSlug();
                if (string.IsNullOrEmpty(currentPrinter)) return false;

                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{currentPrinter}";
                return await SetChamberTemperatureAsync("setChamberTemperature", new { temperature, chamberId }, targetUri).ConfigureAwait(false);
                /*
                IRestApiRequestRespone? result = await SendRestApiRequestAsync(
                       requestTargetUri: targetUri,
                       method: Method.Post,
                       command: "setChamberTemperature",
                       jsonObject: new { temperature, chamberId },
                       authHeaders: AuthHeaders
                       )
                    .ConfigureAwait(false);
                */
                /*
                RepetierApiRequestRespone result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api,
                    command: "setChamberTemperature",
                    jsonData: string.Format("{{\"temperature\":{0}, \"chamberId\":{1}}}", temperature, chamberId),
                    printerName: currentPrinter
                    ).ConfigureAwait(false);
                */
                //return GetQueryResult(result?.Result, true);
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return false;
            }
        }

        #endregion

        #region Fan

        public async Task<bool> SetFanSpeedAsync(int fanId, int speed, bool inPercent)
        {
            try
            {
                string currentPrinter = GetActivePrinterSlug();
                if (string.IsNullOrEmpty(currentPrinter)) return false;

                int SetSpeed = speed;
                if (!inPercent)
                {
                    // Avoid invalid ranges
                    if (speed > 255)
                        SetSpeed = 255;
                    else if (speed < 0)
                        SetSpeed = 0;
                }
                else
                {
                    SetSpeed = Convert.ToInt32(((float)speed) * 255f / 100f);
                }

                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{currentPrinter}";
                return await SetFanSpeedAsync("setFanSpeed", new { speed, fanId }, targetUri).ConfigureAwait(false);
                /*
                IRestApiRequestRespone? result = await SendRestApiRequestAsync(
                       requestTargetUri: targetUri,
                       method: Method.Post,
                       command: "setFanSpeed",
                       jsonObject: new { speed, fanId },
                       authHeaders: AuthHeaders
                       )
                    .ConfigureAwait(false);
                */
                /*
                RepetierApiRequestRespone result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api,
                    command: "setFanSpeed",
                    jsonData: string.Format("{{\"speed\":{0}, \"fanid\":{1}}}", SetSpeed, fanId),
                    printerName: currentPrinter
                    ).ConfigureAwait(false);
                */
                //return GetQueryResult(result?.Result);
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return false;
            }
        }

        #endregion

        #endregion

        #region External Commands
        public async Task<ObservableCollection<ExternalCommand>> GetExternalCommandsAsync()
        {
            IRestApiRequestRespone? result = null;
            ObservableCollection<ExternalCommand> resultObject = new();

            try
            {
                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{GetActivePrinterSlug()}";
                result = await SendRestApiRequestAsync(
                       requestTargetUri: targetUri,
                       method: Method.Post,
                       command: "listExternalCommands",
                       jsonObject: null,
                       authHeaders: AuthHeaders
                       )
                    .ConfigureAwait(false);
                /*
                result = await SendRestApiRequestAsync(
                   commandBase: RepetierCommandBase.printer,
                   commandFeature: RepetierCommandFeature.api,
                   command: "listExternalCommands")
                    .ConfigureAwait(false);
                */
                ExternalCommand[]? cmds = GetObjectFromJson<ExternalCommand[]>(result?.Result);
                return new ObservableCollection<ExternalCommand>(cmds ?? [new ExternalCommand()]);
            }
            catch (JsonException jecx)
            {
                OnError(new JsonConvertEventArgs()
                {
                    Exception = jecx,
                    OriginalString = result?.Result,
                    TargetType = nameof(ExternalCommand),
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
        public async Task<bool> RunExternalCommandAsync(ExternalCommand command)
        {
            try
            {
                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{GetActivePrinterSlug()}";
                IRestApiRequestRespone? result = await SendRestApiRequestAsync(
                       requestTargetUri: targetUri,
                       method: Method.Post,
                       command: "runExternalCommand",
                       jsonObject: new { id = command.Id },
                       authHeaders: AuthHeaders
                       )
                    .ConfigureAwait(false);
                /*
                RepetierApiRequestRespone result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api,
                    command: "runExternalCommand", 
                    jsonData: string.Format("{{\"id\":{0}}}", command.Id)
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
        public async Task RefreshExternalCommandsAsync()
        {
            try
            {
                var commands = new ObservableCollection<ExternalCommand>();
                if (!IsReady || ActivePrinter == null)
                {
                    ExternalCommands = commands;
                    return;
                }

                ObservableCollection<ExternalCommand> result = await GetExternalCommandsAsync().ConfigureAwait(false);
                if (result is not null)
                {
                    ExternalCommands = result;
                }
                else ExternalCommands = commands;

            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                ExternalCommands = new ObservableCollection<ExternalCommand>();
            }
        }

        #endregion

        #region Messages
        public async Task<bool> RemoveMessageAsync(RepetierMessage message, bool unPause = false)
        {
            try
            {
                string currentPrinter = GetActivePrinterSlug();
                if (string.IsNullOrEmpty(currentPrinter)) return false;

                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{currentPrinter}";
                IRestApiRequestRespone? result = await SendRestApiRequestAsync(
                       requestTargetUri: targetUri,
                       method: Method.Post,
                       command: "removeMessage",
                       jsonObject: new { id = message.Id, a = unPause ? "unpause" : "" },
                       authHeaders: AuthHeaders
                       )
                    .ConfigureAwait(false);
                /*
                RepetierApiRequestRespone result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api,
                    command: "removeMessage",
                    jsonData: string.Format("{{\"id\":{0}, \"a\":\"{1}\"}}", message.Id, unPause ? "unpause" : ""),
                    printerName: currentPrinter
                    ).ConfigureAwait(false);
                */
                return GetQueryResult(result?.Result);
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return false;
            }
        }
        public async Task<ObservableCollection<RepetierMessage>> GetMessagesAsync(string printerName = "")
        {
            IRestApiRequestRespone? result = null;
            ObservableCollection<RepetierMessage> resultObject = new();

            string currentPrinter = string.IsNullOrEmpty(printerName) ? GetActivePrinterSlug() : printerName;
            if (string.IsNullOrEmpty(currentPrinter)) return resultObject;

            try
            {
                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{currentPrinter}";
                result = await SendRestApiRequestAsync(
                       requestTargetUri: targetUri,
                       method: Method.Post,
                       command: "messages",
                       jsonObject: null,
                       authHeaders: AuthHeaders
                       )
                    .ConfigureAwait(false);
                /*
                result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api,
                    command: "messages", printerName: currentPrinter)
                    .ConfigureAwait(false);
                */
                RepetierMessage[]? info = GetObjectFromJson<RepetierMessage[]>(result?.Result);
                if (info is not null)
                    resultObject = new ObservableCollection<RepetierMessage>(info);
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
        public async Task RefreshMessagesAsync()
        {
            try
            {
                ObservableCollection<RepetierMessage> temp = new();
                ObservableCollection<RepetierMessage> result = await GetMessagesAsync().ConfigureAwait(false);
                if (result is not null)
                {
                    Messages = result;
                }
                else
                    Messages = temp;

            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                Messages = new ObservableCollection<RepetierMessage>();
            }
        }
        #endregion

        #region Commands while Printing
        public async Task<bool> SetFlowMultiplierAsync(int multiplier)
        {
            try
            {
                string currentPrinter = GetActivePrinterSlug();
                if (string.IsNullOrEmpty(currentPrinter)) return false;

                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{currentPrinter}";
                IRestApiRequestRespone? result = await SendRestApiRequestAsync(
                       requestTargetUri: targetUri,
                       method: Method.Post,
                       command: "setFlowMultiply",
                       jsonObject: new { speed = multiplier },
                       authHeaders: AuthHeaders
                       )
                    .ConfigureAwait(false);
                /*
                RepetierApiRequestRespone result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api,
                    command: "setFlowMultiply", jsonData: string.Format("{{\"speed\":{0}}}", multiplier),
                    printerName: currentPrinter
                    ).ConfigureAwait(false);
                */
                return GetQueryResult(result?.Result);
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return false;
            }
        }

        public async Task<bool> SetSpeedMultiplierAsync(int speed)
        {
            try
            {
                string currentPrinter = GetActivePrinterSlug();
                if (string.IsNullOrEmpty(currentPrinter)) return false;

                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{currentPrinter}";
                IRestApiRequestRespone? result = await SendRestApiRequestAsync(
                       requestTargetUri: targetUri,
                       method: Method.Post,
                       command: "setSpeedMultiply",
                       jsonObject: new { speed },
                       authHeaders: AuthHeaders
                       )
                    .ConfigureAwait(false);
                /*
                RepetierApiRequestRespone result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api,
                    command: "setSpeedMultiply", jsonData: string.Format("{{\"speed\":{0}}}", speed),
                    printerName: currentPrinter
                    ).ConfigureAwait(false);
                */
                return GetQueryResult(result?.Result);
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return false;
            }
        }

        public async Task<bool> SendEmergencyStop()
        {
            try
            {
                string currentPrinter = GetActivePrinterSlug();
                if (string.IsNullOrEmpty(currentPrinter)) return false;

                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{currentPrinter}";
                IRestApiRequestRespone? result = await SendRestApiRequestAsync(
                       requestTargetUri: targetUri,
                       method: Method.Post,
                       command: "emergencyStop",
                       jsonObject: null,
                       authHeaders: AuthHeaders
                       )
                    .ConfigureAwait(false);
                /*
                RepetierApiRequestRespone result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api, command: "emergencyStop", printerName: currentPrinter)
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

        #endregion

        #region Gcode Scripts
        public async Task<RepetierGcodeScript?> GetGcodeScriptAsync(string scriptName)
        {
            IRestApiRequestRespone? result = null;
            RepetierGcodeScript? resultObject = null;
            try
            {
                string currentPrinter = GetActivePrinterSlug();
                if (string.IsNullOrEmpty(currentPrinter)) return resultObject;

                object cmd = new { name = scriptName };
                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{currentPrinter}";
                result = await SendRestApiRequestAsync(
                       requestTargetUri: targetUri,
                       method: Method.Post,
                       command: "getScript",
                       jsonObject: cmd,
                       authHeaders: AuthHeaders
                       )
                    .ConfigureAwait(false);
                /*
                result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api,
                    command: "getScript", jsonData: cmd,
                    printerName: currentPrinter
                    ).ConfigureAwait(false);
                */
                return GetObjectFromJson<RepetierGcodeScript>(result?.Result);
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
        public Task<bool> SetGcodeScriptAsync(RepetierGcodeScript script) => SetGcodeScriptAsync(script.Name, script.Script);
        public async Task<bool> SetGcodeScriptAsync(string scriptName, string script)
        {
            try
            {
                string currentPrinter = GetActivePrinterSlug();
                if (string.IsNullOrEmpty(currentPrinter)) return false;

                object cmd = new { name = scriptName, script = script };
                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{currentPrinter}";
                IRestApiRequestRespone? result = await SendRestApiRequestAsync(
                       requestTargetUri: targetUri,
                       method: Method.Post,
                       command: "setScript",
                       jsonObject: cmd,
                       authHeaders: AuthHeaders
                       )
                    .ConfigureAwait(false);
                /*
                RepetierApiRequestRespone result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api,
                    command: "setScript", jsonData: cmd,
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

        public async Task<bool> RunGcodeScriptAsync(string scriptName)
        {
            try
            {
                string currentPrinter = GetActivePrinterSlug();
                if (string.IsNullOrEmpty(currentPrinter)) return false;

                object cmd = new { script = scriptName };
                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{currentPrinter}";
                IRestApiRequestRespone? result = await SendRestApiRequestAsync(
                       requestTargetUri: targetUri,
                       method: Method.Post,
                       command: "runScript",
                       jsonObject: cmd,
                       authHeaders: AuthHeaders
                       )
                    .ConfigureAwait(false);
                /*
                RepetierApiRequestRespone result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api,
                    command: "runScript", jsonData: cmd,
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
        #endregion

        #region WebCalls

        public async Task<ObservableCollection<RepetierWebCallAction>> GetWebCallActionsAsync()
        {
            ObservableCollection<RepetierWebCallAction> resultObject = [];
            try
            {
                string currentPrinter = GetActivePrinterSlug();
                if (string.IsNullOrEmpty(currentPrinter)) return resultObject;

                RepetierWebCallList? script = await GetWebCallListAsync(currentPrinter).ConfigureAwait(false);
                if (script is not null && script.List is not null)
                {
                    resultObject = new ObservableCollection<RepetierWebCallAction>(script.List);
                }
                return resultObject;
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return resultObject;
            }
        }

        public async Task<bool> ExecuteWebCallAsync(RepetierWebCallAction action, int timeout = 1500)
        {
            try
            {
                string currentPrinter = GetActivePrinterSlug();
                if (string.IsNullOrEmpty(currentPrinter)) return false;

                string cmd = string.Format("{{\"name\":\"{0}\",\"params\":{1}}}", action.Name, JsonConvert.SerializeObject(new string[] { action.Question }));
                CancellationTokenSource cts = new(timeout);

                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{currentPrinter}";
                IRestApiRequestRespone? result = await SendRestApiRequestAsync(
                       requestTargetUri: targetUri,
                       method: Method.Post,
                       command: "webCallExecute",
                       jsonObject: new { name = action.Name, @params = new string[] { action.Question } },
                       authHeaders: AuthHeaders
                       )
                    .ConfigureAwait(false);
                /*
                RepetierApiRequestRespone result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api,
                    command: "webCallExecute", jsonData: cmd, cts: cts,
                    printerName: currentPrinter
                    ).ConfigureAwait(false);
                */
                return GetQueryResult(result?.Result);
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return false;
            }
        }
        public async Task<bool> DeleteWebCallFromServerAsync(RepetierWebCallAction action)
        {
            try
            {
                string currentPrinter = GetActivePrinterSlug();
                if (string.IsNullOrEmpty(currentPrinter)) return false;
                /**/
                object cmd = new
                {
                    name = action.Name,
                    printer = action.Slug,
                };

                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{currentPrinter}";
                IRestApiRequestRespone? result = await SendRestApiRequestAsync(
                       requestTargetUri: targetUri,
                       method: Method.Post,
                       command: "webCallRemove",
                       jsonObject: cmd,
                       authHeaders: AuthHeaders
                       )
                    .ConfigureAwait(false);
                /*
                RepetierApiRequestRespone result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api,
                    command: "webCallRemove", jsonData: cmd,
                    printerName: currentPrinter
                    ).ConfigureAwait(false);
                */
                return GetQueryResult(result?.Result);
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return false;
            }
        }

        public async Task RefreshWebCallsAsync()
        {
            try
            {
                ObservableCollection<RepetierWebCallAction> result = await GetWebCallActionsAsync().ConfigureAwait(false);
                WebCallActions = result ?? new ObservableCollection<RepetierWebCallAction>();
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                WebCallActions = new ObservableCollection<RepetierWebCallAction>();
            }
        }

        #endregion

        #region Projects
        public async Task<byte[]?> DownloadFileFromUriAsync(string path, int timeout = 10000, Dictionary<string, object>? additionalParameters = null)
        {
            try
            {
                if (RestClient == null)
                {
                    UpdateRestClientInstance();
                }
                RestRequest request = new(path);
                request.AddParameter("apikey", ApiKey);
                request.RequestFormat = DataFormat.Json;
                request.Method = Method.Get;
                request.Timeout = TimeSpan.FromMilliseconds(timeout);

                if (additionalParameters is not null)
                {
                    foreach (KeyValuePair<string, object> parameter in additionalParameters)
                    {
                        request.AddParameter(parameter.Key, parameter.Value.ToString());
                    }
                }
                Uri? fullUrl = RestClient?.BuildUri(request);
                return await DownloadFileFromUriAsync(fullUrl).ConfigureAwait(false);
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return null;
            }
        }
        public async Task<byte[]?> DownloadFileFromUriAsync(Uri? uri)
        {
            try
            {
                if (uri is null) return null;
                HttpClient ??= new();
                byte[] data = await HttpClient.GetByteArrayAsync(uri);
                return data;
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return null;
            }
        }

        public async Task<byte[]?> GetProjectImageAsync(Guid server, Guid project, string? preview, string action = "mthumb", int size = 1, int timeout = 10000)
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "v", size }
                };
                string path = string.IsNullOrEmpty(preview) ?
                    $"project/{server}/{action}/{project}/" :
                    $"project/{server}/{action}/{project}/{preview}/"
                    ;
                return await DownloadFileFromUriAsync(path, timeout, parameters).ConfigureAwait(false);
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return [];
            }
        }
        public Task<byte[]?> GetProjectImageAsync(Guid Server, RepetierProjectItem projectItem, string action = "mthumb", int size = 1, int timeout = 10000)
            => GetProjectImageAsync(Server, projectItem.Project?.Uuid ?? Guid.Empty, projectItem.Project?.Preview, action, size, timeout);

        public async Task<RepetierProjectsServerListRespone?> GetProjectsListServerAsync(string printerName = "")
        {
            IRestApiRequestRespone? result = null;
            try
            {
                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{GetActivePrinterSlug()}";
                result = await SendRestApiRequestAsync(
                       requestTargetUri: targetUri,
                       method: Method.Post,
                       command: "projectsListServer",
                       jsonObject: null,
                       authHeaders: AuthHeaders
                       )
                    .ConfigureAwait(false);
                /*
                result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api, command: "projectsListServer", printerName: printerName)
                    .ConfigureAwait(false);
                */
                return GetObjectFromJson<RepetierProjectsServerListRespone>(result?.Result);
            }
            catch (JsonException jecx)
            {
                OnError(new JsonConvertEventArgs()
                {
                    Exception = jecx,
                    OriginalString = result?.Result,
                    Message = jecx.Message,
                });
                return new RepetierProjectsServerListRespone();
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return new RepetierProjectsServerListRespone();
            }
        }

        public async Task<RepetierProjectsFolderRespone?> GetProjectsGetFolderAsync(Guid serverUuid, int index = 1, string printerName = "")
        {
            IRestApiRequestRespone? result = null;
            try
            {
                object data = new
                {
                    serveruuid = serverUuid,
                    idx = index,
                };

                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{printerName}";
                result = await SendRestApiRequestAsync(
                       requestTargetUri: targetUri,
                       method: Method.Post,
                       command: "projectsGetFolder",
                       jsonObject: data,
                       authHeaders: AuthHeaders
                       )
                    .ConfigureAwait(false);
                /*
                result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api,
                    command: "projectsGetFolder", jsonData: data,
                    printerName: printerName
                    ).ConfigureAwait(false);
                */
                return GetObjectFromJson<RepetierProjectsFolderRespone>(result?.Result);
            }
            catch (JsonException jecx)
            {
                OnError(new JsonConvertEventArgs()
                {
                    Exception = jecx,
                    OriginalString = result?.Result,
                    Message = jecx.Message,
                });
                return new RepetierProjectsFolderRespone();
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return new RepetierProjectsFolderRespone();
            }
        }

        public async Task<ObservableCollection<RepetierProjectItem>> GetProjectItemsAsync(Guid serverUuid, int index = 1, string printerName = "")
        {
            RepetierProjectsFolderRespone? result;
            ObservableCollection<RepetierProjectItem> items = [];
            try
            {
                object data = new
                {
                    serveruuid = serverUuid,
                    idx = index,
                };

                result = await GetProjectsGetFolderAsync(serverUuid, index, printerName).ConfigureAwait(false);
                if (result is not null && result.Folder is not null)
                {
                    foreach (RepetierProjectSubFolder item in result.Folder.Folders)
                        items.Add(new RepetierProjectItem()
                        {
                            Index = item.Idx,
                            Path = item.Name,
                            Folder = item,
                            Project = null,
                        });
                    foreach (RepetierProject item in result.Folder.Projects)
                        items.Add(new RepetierProjectItem()
                        {
                            Index = item.Folder,
                            Path = item.Name,
                            Folder = null,
                            Project = item,
                        });

                    // Avoid multiple requests
                    byte[]? emptyProject = await DownloadFileFromUriAsync("img/emptyproject.png").ConfigureAwait(false);
                    byte[]? folder = await DownloadFileFromUriAsync("img/folder_m.png").ConfigureAwait(false);

                    foreach (RepetierProjectItem project in items)
                    {
                        if (!project.IsFolder && project.Project is not null)
                        {
                            if (!string.IsNullOrEmpty(project.Project.Preview))
                                // Load image from server
                                project.PreviewImage = await GetProjectImageAsync(serverUuid, project).ConfigureAwait(false);
                            else
                                // Static image from the server
                                project.PreviewImage = emptyProject;
                        }
                        else if (project.Folder is not null)
                        {
                            // Static image from the server
                            project.PreviewImage = folder;
                        }

                    }

                    return items;
                }
                else
                {
                    return [];
                }
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return [];
            }
        }

        public async Task<RepetierProjectsProjectRespone?> GetProjectsGetProjectAsync(Guid serverUuid, Guid projectUuid, string printerName = "")
        {
            IRestApiRequestRespone? result = null;
            try
            {
                object data = new
                {
                    serveruuid = serverUuid,
                    uuid = projectUuid,
                };
                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{printerName}";
                result = await SendRestApiRequestAsync(
                       requestTargetUri: targetUri,
                       method: Method.Post,
                       command: "projectsGetFolder",
                       jsonObject: data,
                       authHeaders: AuthHeaders
                       )
                    .ConfigureAwait(false);
                /*
                result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api,
                    command: "projectsGetProject", jsonData: data,
                    printerName: printerName
                    ).ConfigureAwait(false);
                */
                return GetObjectFromJson<RepetierProjectsProjectRespone>(result?.Result);
            }
            catch (JsonException jecx)
            {
                OnError(new JsonConvertEventArgs()
                {
                    Exception = jecx,
                    OriginalString = result?.Result,
                    Message = jecx.Message,
                });
                return new RepetierProjectsProjectRespone();
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return new RepetierProjectsProjectRespone();
            }
        }

        public async Task<bool> UpdateProjectsGetProjectAsync(Guid serverUuid, RepetierProjectsProject project, string printerName = "")
        {
            try
            {
                // Is done automatically by the server
                //Project.Version++;
                object data = new
                {
                    serveruuid = serverUuid,
                    project = project,
                };
                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{printerName}";
                IRestApiRequestRespone? result = await SendRestApiRequestAsync(
                       requestTargetUri: targetUri,
                       method: Method.Post,
                       command: "projectsUpdateProject",
                       jsonObject: data,
                       authHeaders: AuthHeaders
                       )
                    .ConfigureAwait(false);
                /*
                RepetierApiRequestRespone result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api,
                    command: "projectsUpdateProject", jsonData: data,
                    printerName: printerName
                    ).ConfigureAwait(false);
                */
                return GetQueryResult(result?.Result);
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return false;
            }
        }

        public Uri? GetProjectFileUri(Guid serverUuid, RepetierProjectFile file, string action = "view")
        {
            try
            {
                string uriString = string.Empty;
                switch (file.Type)
                {
                    case RepetierProjectFileType.Model:
                    case RepetierProjectFileType.Other:
                        uriString = $"{FullWebAddress}/project/{serverUuid}/{action}/{file.ProjectUuid}/{file.Name}/?apikey={ApiKey}";
                        break;

                    case RepetierProjectFileType.Image:
                        uriString = $"{FullWebAddress}/project/{serverUuid}/image/{file.ProjectUuid}/{file.Name}/?apikey={ApiKey}&v=19";
                        break;
                    default:
                        break;
                }
                return new Uri(uriString);
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return null;
            }
        }

        public async Task<bool> DeleteProjectFileFromServerAsync(Guid serverUuid, RepetierProjectFile file, string printerName = "")
        {
            try
            {
                object data = new
                {
                    serveruuid = serverUuid,
                    uuid = file.ProjectUuid,
                    name = file.Name,
                };

                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{printerName}";
                IRestApiRequestRespone? result = await SendRestApiRequestAsync(
                       requestTargetUri: targetUri,
                       method: Method.Post,
                       command: "projectsDeleteFile",
                       jsonObject: data,
                       authHeaders: AuthHeaders
                       )
                    .ConfigureAwait(false);
                /*
                RepetierApiRequestRespone result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api,
                    command: "projectsDeleteFile", jsonData: data,
                    printerName: printerName
                    ).ConfigureAwait(false);
                */
                return GetQueryResult(result?.Result);
            }

            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return false;
            }
        }

        public async Task<bool> DeleteCommentFromServerAsync(Guid serverUuid, Guid projectUuid, RepetierProjectsProjectComment comment, string printerName = "")
        {
            try
            {
                object data = new
                {
                    serveruuid = serverUuid,
                    projectuuid = projectUuid,
                    username = comment.User,
                    comment = comment.Comment,
                };
                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{printerName}";
                IRestApiRequestRespone? result = await SendRestApiRequestAsync(
                       requestTargetUri: targetUri,
                       method: Method.Post,
                       command: "projectDelComment",
                       jsonObject: data,
                       authHeaders: AuthHeaders
                       )
                    .ConfigureAwait(false);
                /*
                RepetierApiRequestRespone result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api,
                    command: "projectDelComment", jsonData: data,
                    printerName: printerName
                    ).ConfigureAwait(false);
                */
                return GetQueryResult(result?.Result);
            }

            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return false;
            }
        }

        #endregion

        #region History
        async Task<RepetierHistoryListRespone?> GetHistoryListResponeAsync(
            string printerNameForHistory, string serverUuid = "", int limit = 50, int page = 0, int start = 0, bool allPrinter = false)
        {
            IRestApiRequestRespone? result = null;
            try
            {
                string currentPrinter = GetActivePrinterSlug();

                object data = new
                {
                    allPrinter = allPrinter,
                    limit = limit,
                    page = page,
                    slug = printerNameForHistory,
                    start = page > 0 && start == 0 ? limit * page : start,
                    uuid = serverUuid,
                };

                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{currentPrinter}";
                result = await SendRestApiRequestAsync(
                       requestTargetUri: targetUri,
                       method: Method.Post,
                       command: "historyList",
                       jsonObject: data,
                       authHeaders: AuthHeaders
                       )
                    .ConfigureAwait(false);
                /*
                result = await SendRestApiRequestAsync(
                   commandBase: RepetierCommandBase.printer,
                   commandFeature: RepetierCommandFeature.api,
                   jsonData: data,
                   command: "historyList",
                   printerName: currentPrinter
                   )
                    .ConfigureAwait(false);
                */
                return GetObjectFromJson<RepetierHistoryListRespone>(result?.Result);
            }
            catch (JsonException jecx)
            {
                OnError(new JsonConvertEventArgs()
                {
                    Exception = jecx,
                    OriginalString = result?.Result,
                    Message = jecx.Message,
                });
                return new RepetierHistoryListRespone();
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return new RepetierHistoryListRespone();
            }
        }

        public async Task<ObservableCollection<RepetierHistoryListItem>> GetHistoryListAsync(
            string printerNameForHistory, string serverUuid = "", int limit = 50, int page = 0, int start = 0, bool allPrinter = false)
        {
            try
            {
                string currentPrinter = GetActivePrinterSlug();
                RepetierHistoryListRespone? historyList =
                    await GetHistoryListResponeAsync(printerNameForHistory, serverUuid, limit, page, start, allPrinter)
                    .ConfigureAwait(false);
                return [.. historyList?.List];
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return [];
            }
        }

        async Task<RepetierHistorySummaryRespone?> GetHistorySummaryAsync(string printerNameForHistory, int year, bool allPrinter = false)
        {
            IRestApiRequestRespone? result = null;
            try
            {
                string currentPrinter = GetActivePrinterSlug();
                object data = new
                {
                    allPrinter = allPrinter,
                    slug = printerNameForHistory,
                    year = year,
                };

                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{currentPrinter}";
                result = await SendRestApiRequestAsync(
                       requestTargetUri: targetUri,
                       method: Method.Post,
                       command: "historySummary",
                       jsonObject: data,
                       authHeaders: AuthHeaders
                       )
                    .ConfigureAwait(false);
                /*
                result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api,
                    command: "historySummary", jsonData: data,
                    printerName: currentPrinter
                    ).ConfigureAwait(false);
                */
                return GetObjectFromJson<RepetierHistorySummaryRespone>(result?.Result);
            }
            catch (JsonException jecx)
            {
                OnError(new JsonConvertEventArgs()
                {
                    Exception = jecx,
                    OriginalString = result?.Result,
                    Message = jecx.Message,
                });
                return new RepetierHistorySummaryRespone();
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return new RepetierHistorySummaryRespone();
            }
        }
        public async Task<byte[]?> GetHistoryReportAsync(long reportId, string printerName = "")
        {
            try
            {
                if (string.IsNullOrEmpty(printerName))
                    printerName = GetActivePrinterSlug();
                byte[]? report = await DownloadFileFromUriAsync($"{FullWebAddress}/printer/export/{printerName}?a=history_report&id={reportId}&apikey={ApiKey}")
                    .ConfigureAwait(false)
                    ;
                return report;
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return [];
            }
        }
        public async Task<ObservableCollection<RepetierHistorySummaryItem>?> GetHistorySummaryItemsAsync(string printerNameForHistory, int year, bool allPrinter = false)
        {
            try
            {
                RepetierHistorySummaryRespone? list = await GetHistorySummaryAsync(printerNameForHistory, year, allPrinter).ConfigureAwait(false);
                return [.. list?.Summaries];
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return [];
            }
        }

        public async Task<bool> DeleteHistoryListItemAsync(RepetierHistoryListItem item, string printerName = "")
        {
            try
            {
                if (string.IsNullOrEmpty(printerName))
                    printerName = GetActivePrinterSlug();

                object data = new
                {
                    id = item.Id,
                };
                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{printerName}";
                IRestApiRequestRespone? result = await SendRestApiRequestAsync(
                       requestTargetUri: targetUri,
                       method: Method.Post,
                       command: "historyDeleteEntry",
                       jsonObject: data,
                       authHeaders: AuthHeaders
                       )
                    .ConfigureAwait(false);
                /*
                RepetierApiRequestRespone result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api,
                    command: "historyDeleteEntry", jsonData: data,
                    printerName: printerName
                    ).ConfigureAwait(false);
                */
                return GetQueryResult(result?.Result);
            }

            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return false;
            }
        }
        #endregion

        #region GPIO 
        async Task<RepetierGpioListRespone?> GetGPIOListResponeAsync()
        {
            IRestApiRequestRespone? result = null;
            try
            {
                string currentPrinter = GetActivePrinterSlug();

                string targetUri = $"{RepetierCommands.Base}/{RepetierCommands.Api}/{currentPrinter}";
                result = await SendRestApiRequestAsync(
                       requestTargetUri: targetUri,
                       method: Method.Post,
                       command: "GPIOGetList",
                       jsonObject: null,
                       authHeaders: AuthHeaders
                       )
                    .ConfigureAwait(false);
                /*
                result = await SendRestApiRequestAsync(
                   commandBase: RepetierCommandBase.printer,
                   commandFeature: RepetierCommandFeature.api,
                   command: "GPIOGetList",
                   printerName: currentPrinter)
                    .ConfigureAwait(false);
                */

                return GetObjectFromJson<RepetierGpioListRespone>(result?.Result);
            }
            catch (JsonException jecx)
            {
                OnError(new JsonConvertEventArgs()
                {
                    Exception = jecx,
                    OriginalString = result?.Result,
                    TargetType = nameof(RepetierGpioListRespone),
                    Message = jecx.Message,
                });
                return new RepetierGpioListRespone();
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return new RepetierGpioListRespone();
            }
        }
        public async Task<ObservableCollection<RepetierGpioListItem>> GetGPIOListAsync()
        {
            try
            {
                RepetierGpioListRespone? list = await GetGPIOListResponeAsync().ConfigureAwait(false);
                if (list?.List is not null)
                    return [.. list.List];
                else
                    return [];
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return [];
            }
        }

        public async Task RefreshGPIOListAsync()
        {
            try
            {
                ObservableCollection<RepetierGpioListItem> result = await GetGPIOListAsync().ConfigureAwait(false);
                GPIOList = result ?? new ObservableCollection<RepetierGpioListItem>();
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                GPIOList = new ObservableCollection<RepetierGpioListItem>();
            }
        }

        #endregion

        #endregion

        #endregion

        #region Overrides
        public override string ToString()
        {
            try
            {
                return FullWebAddress;
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return string.Empty;
            }
        }
        public override bool Equals(object? obj)
        {
            if (obj is not RepetierClient item)
                return false;
            return Id.Equals(item.Id);
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        #endregion
    }
}

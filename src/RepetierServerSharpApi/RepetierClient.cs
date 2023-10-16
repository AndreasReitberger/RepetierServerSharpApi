using AndreasReitberger.API.Print3dServer.Core;
using AndreasReitberger.API.Print3dServer.Core.Enums;
using AndreasReitberger.API.Print3dServer.Core.Interfaces;
using AndreasReitberger.API.Repetier.Enum;
using AndreasReitberger.API.Repetier.Events;
using AndreasReitberger.API.Repetier.Models;
using AndreasReitberger.Core.Enums;
using AndreasReitberger.Core.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WebSocket4Net;
using ErrorEventArgs = SuperSocket.ClientEngine.ErrorEventArgs;

namespace AndreasReitberger.API.Repetier
{
    public partial class RepetierClient : Print3dServerClient, IPrint3dServerClient, IDisposable, ICloneable //, IRestApiClient
    {
        #region Variables
        RestClient restClient;
        HttpClient httpClient;
        int _retries = 0;
        #endregion

        #region Basic
        [ObservableProperty]
        Print3dServerTarget target = Print3dServerTarget.RepetierServer;

        [ObservableProperty]
        Guid id = Guid.Empty;

        #endregion

        #region Instance
        static RepetierClient _instance = null;
        static readonly object Lock = new();
        public static RepetierClient Instance
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

        [ObservableProperty]
        bool isActive = false;

        [ObservableProperty]
        bool updateInstance = false;
        partial void OnUpdateInstanceChanged(bool value)
        {
            if (value)
            {
                InitInstance(ServerAddress, Port, ApiKey, IsSecure);
            }
        }

        [ObservableProperty]
        bool isInitialized = false;

        #endregion

        #region RefreshTimer
        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        Timer timer;


        [ObservableProperty]
        int refreshInterval = 3;
        partial void OnRefreshIntervalChanged(int value)
        {
            if (IsListening)
            {
                StartListening(stopActiveListening: true);
            }
        }

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        bool isListening = false;
        partial void OnIsListeningChanged(bool value)
        {
            OnListeningChangedEvent(new RepetierEventListeningChangedEventArgs()
            {
                SessonId = SessionId,
                IsListening = value,
                IsListeningToWebSocket = IsListeningToWebsocket,
            });
        }

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        bool initialDataFetched = false;

        #endregion

        #region Properties

        #region Debug
        [ObservableProperty]
        [property: JsonIgnore, System.Text.Json.Serialization.JsonIgnore, XmlIgnore]
        ConcurrentDictionary<string, string> ignoredJsonResults = new();
        partial void OnIgnoredJsonResultsChanged(ConcurrentDictionary<string, string> value)
        {
            OnRepetierIgnoredJsonResultsChanged(new RepetierIgnoredJsonResultsChangedEventArgs()
            {
                NewIgnoredJsonResults = value,
            });
        }
        #endregion

        #region Connection

        [ObservableProperty]
        Dictionary<string, IAuthenticationHeader> authHeaders = new();

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        EventSession session;
        partial void OnSessionChanged(EventSession value)
        {
            SessionId = Session?.Session;
            OnSessionChangedEvent(new RepetierEventSessionChangedEventArgs()
            {
                CallbackId = Session?.CallbackId ?? -1,
                Sesson = value,
                SessonId = value?.Session
            });           
        }

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        string sessionId = string.Empty;

        [ObservableProperty]
        string serverName = string.Empty;

        [ObservableProperty]
        string checkOnlineTargetUri = string.Empty;

        [ObservableProperty]
        string serverAddress = string.Empty;
        partial void OnServerAddressChanged(string value)
        {
            UpdateRestClientInstance();
        }


        [ObservableProperty]
        bool loginRequired = false;

        [ObservableProperty]
        bool isSecure = false;
        partial void OnIsSecureChanged(bool value)
        {
            UpdateRestClientInstance();
        }

        [ObservableProperty]
        string apiKey = string.Empty;

        [ObservableProperty]
        string apiKeyRegexPattern = string.Empty;

        [ObservableProperty]
        string apiVersion = string.Empty;

        [ObservableProperty]
        int port = 3344;
        partial void OnPortChanged(int value)
        {
            UpdateRestClientInstance();
        }

        [ObservableProperty]
        int defaultTimeout = 10000;

        [ObservableProperty]
        bool overrideValidationRules = false;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        bool isOnline = false;
        partial void OnIsOnlineChanged(bool value)
        {
            if (value)
            {
                OnServerWentOnline(new RepetierEventArgs()
                {
                    SessonId = SessionId,
                });
            }
            else
            {
                OnServerWentOffline(new RepetierEventArgs()
                {
                    SessonId = SessionId,
                });
            }
        }

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        bool isConnecting = false;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        bool authenticationFailed = false;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        bool isRefreshing = false;

        [ObservableProperty]
        //[property: JsonIgnore, XmlIgnore]
        int retriesWhenOffline = 2;

        #endregion

        #region General
        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        bool updateAvailable = false;
        partial void OnUpdateAvailableChanged(bool value)
        {
            if (value)
            {
                OnServerUpdateAvailable(new RepetierEventArgs()
                {
                    SessonId = SessionId,
                });
            }
        }

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        RepetierAvailableUpdateInfo update;

        #endregion

        #region Proxy
        [ObservableProperty]
        bool enableProxy = false;
        partial void OnEnableProxyChanged(bool value)
        {
            UpdateRestClientInstance();
        }

        [ObservableProperty]
        bool proxyUserUsesDefaultCredentials = true;
        partial void OnProxyUserUsesDefaultCredentialsChanged(bool value)
        {
            UpdateRestClientInstance();
        }

        [ObservableProperty]
        bool secureProxyConnection = true;
        partial void OnSecureProxyConnectionChanged(bool value)
        {
            UpdateRestClientInstance();
        }

        [ObservableProperty]
        string proxyAddress = string.Empty;
        partial void OnProxyAddressChanged(string value)
        {
            UpdateRestClientInstance();
        }

        [ObservableProperty]
        int proxyPort = 443;
        partial void OnProxyPortChanged(int value)
        {
            UpdateRestClientInstance();
        }

        [ObservableProperty]
        string proxyUser = string.Empty;
        partial void OnProxyUserChanged(string value)
        {
            UpdateRestClientInstance();
        }

        [ObservableProperty]
        SecureString proxyPassword;
        partial void OnProxyPasswordChanged(SecureString value)
        {
            UpdateRestClientInstance();
        }
        #endregion

        #region DiskSpace
        [ObservableProperty]
        long freeDiskSpace = 0;

        [ObservableProperty]
        long usedDiskSpace = 0;

        [JsonIgnore, XmlIgnore]
        [ObservableProperty, Obsolete("Use UsedDiskSpace instead")]
        [property: Obsolete("Use UsedDiskSpace instead")]
        long availableDiskSpace = 0;

        [ObservableProperty]
        long totalDiskSpace = 0;

        #endregion

        #region PrinterStateInformation

        #region ConfigurationInfo

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        [property: Obsolete("Use ActiveToolHead instead")]
        long activeExtruder = 0;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        long activeToolHead = 0;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        [property: Obsolete("Use NumberOfToolHeads instead")]
        long numberOfExtruders = 0;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        long numberOfToolHeads = 0;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        bool isDualExtruder = false;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        bool hasHeatedBed = false;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        bool hasHeatedChamber = false;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        bool hasFan = false;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        bool hasWebCam = false;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        RepetierPrinterConfigWebcam selectedWebCam;

        #endregion

        #region PrinterState
        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        bool isPrinting = false;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        bool isPaused = false;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        bool isConnectedPrinterOnline = false;

        #endregion

        #region Temperatures

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        double temperatureExtruderMain = 0;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        double temperatureExtruderSecondary = 0;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        double temperatureHeatedBedMain = 0;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        double temperatureHeatedChamberMain = 0;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        double speedFactor = 0;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        double speedFactorTarget = 0;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        double flowFactor = 0;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        double flowFactorTarget = 0;

        #endregion

        #region Fans
        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        int speedFanMain = 0;

        #endregion

        #endregion

        #region Printers
        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        IPrinter3d activePrinter;
        partial void OnActivePrinterChanging(IPrinter3d value)
        {
            OnActivePrinterChangedEvent(new RepetierActivePrinterChangedEventArgs()
            {
                SessonId = SessionId,
                NewPrinter = value,
                OldPrinter = ActivePrinter,
                Printer = GetActivePrinterSlug(),
            });
        }

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        ObservableCollection<IPrinter3d> printers = new();
        partial void OnPrintersChanged(ObservableCollection<IPrinter3d> value)
        {
            if (value?.Count > 0 && ActivePrinter == null)
            {
                ActivePrinter = value.FirstOrDefault();
            }
        }

        #endregion

        #region Models
        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        ObservableCollection<IGcodeGroup> groups = new();
        partial void OnGroupsChanged(ObservableCollection<IGcodeGroup> value)
        {
            OnRepetierModelGroupsChangedEvent(new RepetierModelGroupsChangedEventArgs()
            {
                NewModelGroups = value,
                SessonId = SessionId,
                CallbackId = -1,
                Printer = GetActivePrinterSlug(),
            });
        }

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        ObservableCollection<IGcode> files = new();
        partial void OnFilesChanged(ObservableCollection<IGcode> value)
        {
            OnRepetierModelsChangedEvent(new RepetierModelsChangedEventArgs()
            {
                NewModels = value,
                SessonId = SessionId,
                CallbackId = -1,
                Printer = GetActivePrinterSlug(),
            });
        }

        #endregion

        #region Jobs
        [ObservableProperty]
        [property: JsonIgnore, System.Text.Json.Serialization.JsonIgnore, XmlIgnore]
        byte[] currentPrintImage = Array.Empty<byte>();
        partial void OnCurrentPrintImageChanging(byte[] value) {
            OnRepetierCurrentPrintImageChanged(new RepetierCurrentPrintImageChangedEventArgs() {
                NewImage = value,
                PreviousImage = CurrentPrintImage,
                SessonId = SessionId,
                CallbackId = -1,
            });
        }

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        ObservableCollection<IPrint3dJob> jobs = new();
        partial void OnJobsChanged(ObservableCollection<IPrint3dJob> value)
        {
            OnRepetierJobListChangedEvent(new RepetierJobListChangedEventArgs()
            {
                NewJobList = value,
                SessonId = SessionId,
                CallbackId = -1,
                Printer = GetActivePrinterSlug(),
            });
        }

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        IPrint3dJobStatus jobStatus;

        #endregion

        #region ExternalCommands
        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        ObservableCollection<ExternalCommand> externalCommands = new();

        #endregion

        #region Messages
        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        ObservableCollection<RepetierMessage> messages = new();
        partial void OnMessagesChanged(ObservableCollection<RepetierMessage> value)
        {
            OnMessagesChangedEvent(new RepetierMessagesChangedEventArgs()
                {
                    RepetierMessages = value,
                    SessonId = SessionId,
                    CallbackId = -1,
                    Printer = GetActivePrinterSlug(),
                });
        }

        #endregion

        #region WebCalls
        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        ObservableCollection<RepetierWebCallAction> webCallActions = new();
        partial void OnWebCallActionsChanged(ObservableCollection<RepetierWebCallAction> value)
        {
            OnWebCallActionsChangedEvent(new RepetierWebCallActionsChangedEventArgs()
            {
                NewWebCallActions = value,
                SessonId = SessionId,
                CallbackId = -1,
                Printer = GetActivePrinterSlug(),
            });
        }

        #endregion

        #region GPIO
        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        ObservableCollection<RepetierGpioListItem> gPIOList = new();

        #endregion

        #region State & Config
        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        RepetierPrinterConfig config;
        partial void OnConfigChanged(RepetierPrinterConfig value)
        {
           OnRepetierPrinterConfigChangedEvent(new RepetierPrinterConfigChangedEventArgs()
            {
                NewConfiguration = value,
                SessonId = SessionId,
                CallbackId = -1,
                Printer = GetActivePrinterSlug(),
            });
            UpdatePrinterConfig(value); 
        }

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        RepetierPrinterState state;
        partial void OnStateChanged(RepetierPrinterState value)
        {
            OnRepetierPrinterStateChangedEvent(new RepetierPrinterStateChangedEventArgs()
            {
                NewPrinterState = value,
                SessonId = SessionId,
                CallbackId = -1,
                Printer = GetActivePrinterSlug(),
            });
            UpdatePrinterState(value);
        }

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        RepetierCurrentPrintInfo activePrintInfo;
        partial void OnActivePrintInfoChanged(RepetierCurrentPrintInfo value)
        {
            OnPrintInfoChangedEvent(new RepetierActivePrintInfoChangedEventArgs()
            {
                SessonId = SessionId,
                NewActivePrintInfo = value,
                Printer = GetActivePrinterSlug(),
            });
            UpdateActivePrintInfo(value);
        }

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        ObservableCollection<RepetierCurrentPrintInfo> activePrintInfos = new();
        partial void OnActivePrintInfosChanged(ObservableCollection<RepetierCurrentPrintInfo> value)
        {
            OnPrintInfosChangedEvent(new RepetierActivePrintInfosChangedEventArgs()
            {
                SessonId = SessionId,
                NewActivePrintInfos = value,
                Printer = GetActivePrinterSlug(),
            });
        }

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        ObservableCollection<RepetierPrinterHeaterComponent> extruders = new();

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        ObservableCollection<RepetierPrinterHeaterComponent> heatedBeds = new();

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        ObservableCollection<RepetierPrinterHeaterComponent> heatedChambers = new();

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        ObservableCollection<RepetierPrinterFan> fans = new();

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        ObservableCollection<RepetierPrinterConfigWebcam> webCams = new();

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        bool shutdownAfterPrint = false;

        #endregion

        #region Position
        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        [property: Obsolete("Use X instead")]
        long curX = 0;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        [property: Obsolete("Use Y instead")]
        long curY = 0;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        [property: Obsolete("Use Z instead")]
        long curZ = 0;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        double x = 0;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        double y = 0;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        double z = 0;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        int layer = 0;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        int layers = 0;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        bool yHomed = false;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        bool zHomed = false;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        bool xHomed = false;

        #endregion

        #region ReadOnly

        public string FullWebAddress => $"{(IsSecure ? "https" : "http")}://{ServerAddress}:{Port}";

        public bool IsReady
        {
            get
            {
                return 
                    !string.IsNullOrEmpty(ServerAddress) && Port > 0 && //  !string.IsNullOrEmpty(API)) &&
                    (
                        // Address
                        (Regex.IsMatch(ServerAddress, RegexHelper.IPv4AddressRegex) || Regex.IsMatch(ServerAddress, RegexHelper.IPv6AddressRegex) || Regex.IsMatch(ServerAddress, RegexHelper.Fqdn)) &&
                        // API-Key (also allow empty key if the user performs a login instead
                        (string.IsNullOrEmpty(ApiKey) ? true : Regex.IsMatch(ApiKey, RegexHelper.RepetierServerProApiKey))
                    ||
                        // Or validation rules are overriden
                        OverrideValidationRules
                    );
            }
        }
        #endregion

        #endregion

        #region WebSocket
        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        WebSocket webSocket;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        Timer pingTimer;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        int pingCounter = 0;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        int refreshCounter = 0;

        [ObservableProperty]
        string pingCommand;

        [ObservableProperty]
        string webSocketTargetUri;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        bool isListeningToWebsocket = false;
        partial void OnIsListeningToWebsocketChanged(bool value)
        {
            OnListeningChangedEvent(new RepetierEventListeningChangedEventArgs()
            {
                SessonId = SessionId,
                IsListening = IsListening,
                IsListeningToWebSocket = value,
            });
        }
        #endregion

        #region Constructor
        public RepetierClient()
        {
            Id = Guid.NewGuid();
            UpdateRestClientInstance();
        }

        public RepetierClient(string serverAddress, string api, int port = 3344, bool isSecure = false)
        {
            Id = Guid.NewGuid();
            InitInstance(serverAddress, port, api, isSecure);
            UpdateRestClientInstance();
        }

        public RepetierClient(string serverAddress, int port = 3344, bool isSecure = false)
        {
            Id = Guid.NewGuid();
            InitInstance(serverAddress, port, "", isSecure);
            UpdateRestClientInstance();
        }
        #endregion

        #region Destructor
        ~RepetierClient()
        {
            if (WebSocket != null)
            {
                /* SharpWebSocket
                if (WebSocket.ReadyState == WebSocketState.Open)
                    WebSocket.Close();
                WebSocket = null;
                */
            }
        }
        #endregion

        #region Init
        public void InitInstance()
        {
            try
            {
                // https://www.repetier-server.com/manuals/programming/API/index.html
                Instance = this;
                if (Instance != null)
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
        public void InitInstance(string serverAddress, int port = 3344, string api = "", bool isSecure = false)
        {
            try
            {
                ServerAddress = serverAddress;
                ApiKey = api;
                Port = port;
                IsSecure = isSecure;

                Instance = this;

                if (Instance != null)
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

        #region WebSocket
        void PingServer()
        {
            try
            {
                if (WebSocket != null)
                    if (WebSocket.State == WebSocketState.Open)
                    {
                        string pingCommand = $"{{\"action\":\"ping\",\"data\":{{\"source\":\"{"App"}\"}},\"printer\":\"{GetActivePrinterSlug()}\",\"callback_id\":{PingCounter}}}";
                        WebSocket.Send(pingCommand);
                    }
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
            }

        }

        public void ConnectWebSocket()
        {
            try
            {
                //if (!IsReady) return;
                if (!string.IsNullOrEmpty(FullWebAddress) && (
                    Regex.IsMatch(FullWebAddress, RegexHelper.IPv4AddressRegex) ||
                    Regex.IsMatch(FullWebAddress, RegexHelper.IPv6AddressRegex) ||
                    Regex.IsMatch(FullWebAddress, RegexHelper.Fqdn))) return;
                //if (!IsReady || IsListeningToWebsocket) return;

                DisconnectWebSocket();

                string target = $"{(IsSecure ? "wss" : "ws")}://{ServerAddress}:{Port}/socket/{(!string.IsNullOrEmpty(ApiKey) ? $"?apikey={ApiKey}" : "")}";
                WebSocket = new WebSocket(target)
                {
                    EnableAutoSendPing = false
                };

                if (IsSecure)
                {
                    // https://github.com/sta/websocket-sharp/issues/219#issuecomment-453535816
                    SslProtocols sslProtocolHack = (SslProtocols)(SslProtocolsHack.Tls12 | SslProtocolsHack.Tls11 | SslProtocolsHack.Tls);
                    //Avoid TlsHandshakeFailure
                    if (WebSocket.Security.EnabledSslProtocols != sslProtocolHack)
                    {
                        WebSocket.Security.EnabledSslProtocols = sslProtocolHack;
                    }
                }

                WebSocket.MessageReceived += WebSocket_MessageReceived;
                //WebSocket.DataReceived += WebSocket_DataReceived;
                WebSocket.Opened += WebSocket_Opened;
                WebSocket.Closed += WebSocket_Closed;
                WebSocket.Error += WebSocket_Error;

#if NETSTANDARD
                WebSocket.OpenAsync();
#else
                WebSocket.Open();
#endif

            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
            }
        }
        public void DisconnectWebSocket()
        {
            try
            {
                if (WebSocket != null)
                {
                    if (WebSocket.State == WebSocketState.Open)
                    {
#if NETSTANDARD
                        WebSocket.CloseAsync();
#else
                        WebSocket.Close();
#endif
                    }
                    StopPingTimer();

                    WebSocket.MessageReceived -= WebSocket_MessageReceived;
                    WebSocket.Opened -= WebSocket_Opened;
                    WebSocket.Closed -= WebSocket_Closed;
                    WebSocket.Error -= WebSocket_Error;

                    WebSocket = null;
                }
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
            }
        }

#if NET5_0_OR_GREATER || NETSTANDARD
        public async Task ConnectWebSocketAsync()
        {
            try
            {
                //if (!IsReady) return;
                if (!string.IsNullOrEmpty(FullWebAddress) && (
                    Regex.IsMatch(FullWebAddress, RegexHelper.IPv4AddressRegex) ||
                    Regex.IsMatch(FullWebAddress, RegexHelper.IPv6AddressRegex) ||
                    Regex.IsMatch(FullWebAddress, RegexHelper.Fqdn)))
                {
                    return;
                }
                //if (!IsReady || IsListeningToWebsocket) return;
                await DisconnectWebSocketAsync();
                string target = $"{(IsSecure ? "wss" : "ws")}://{ServerAddress}:{Port}/socket/{(!string.IsNullOrEmpty(ApiKey) ? $"?apikey={ApiKey}" : "")}";
                WebSocket = new WebSocket(target)
                {
                    EnableAutoSendPing = false,

                };

                if (IsSecure)
                {
                    // https://github.com/sta/websocket-sharp/issues/219#issuecomment-453535816
                    SslProtocols sslProtocolHack = (SslProtocols)(SslProtocolsHack.Tls12 | SslProtocolsHack.Tls11 | SslProtocolsHack.Tls);
                    //Avoid TlsHandshakeFailure
                    if (WebSocket.Security.EnabledSslProtocols != sslProtocolHack)
                    {
                        WebSocket.Security.EnabledSslProtocols = sslProtocolHack;
                    }
                }

                WebSocket.MessageReceived += WebSocket_MessageReceived;
                //WebSocket.DataReceived += WebSocket_DataReceived;
                WebSocket.Opened += WebSocket_Opened;
                WebSocket.Closed += WebSocket_Closed;
                WebSocket.Error += WebSocket_Error;

                await WebSocket.OpenAsync();
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
            }
        }
        public async Task DisconnectWebSocketAsync()
        {
            try
            {
                if (WebSocket != null)
                {
                    if (WebSocket.State == WebSocketState.Open)
                        await WebSocket.CloseAsync();
                    StopPingTimer();

                    WebSocket.MessageReceived -= WebSocket_MessageReceived;
                    //WebSocket.DataReceived -= WebSocket_DataReceived;
                    WebSocket.Opened -= WebSocket_Opened;
                    WebSocket.Closed -= WebSocket_Closed;
                    WebSocket.Error -= WebSocket_Error;

                    WebSocket = null;
                }
                //WebSocket = null;
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
            }
        }
#endif

        void WebSocket_Error(object sender, ErrorEventArgs e)
        {
            try
            {
                IsListeningToWebsocket = false;
                OnWebSocketError(e);
                OnError(e);
            }
            catch(Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
            }
            finally
            {
                DisconnectWebSocket();
            }
        }

        void WebSocket_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            try
            {
                if (e.Message == null || string.IsNullOrEmpty(e.Message))
                    return;
                if (e.Message.ToLower().Contains("login"))
                {
                    //var login = GetObjectFromJson<RepetierLoginRequiredResult>(e.Message, JsonSerializerSettings);
                    //var login = GetObjectFromJson<RepetierLoginResult>(e.Message, JsonSerializerSettings);
                }
                if (e.Message.ToLower().Contains("session"))
                {
                    //Session = GetObjectFromJson<EventSession>(e.Message, JsonSerializerSettings);
                    Session = GetObjectFromJson<EventSession>(e.Message);
                }
                else if (e.Message.ToLower().Contains("event"))
                {
                    RepetierEventContainer repetierEvent = GetObjectFromJson<RepetierEventContainer>(e.Message, JsonSerializerSettings);
                    if (repetierEvent != null)
                    {
                        string name = string.Empty;
                        string jsonBody = string.Empty;
                        foreach (RepetierEventData obj in repetierEvent.Data)
                        {
                            name = obj.EventName;
                            jsonBody = obj.Data?.ToString();
                            switch (name)
                            {
                                case "userCredentials":
                                    RepetierLoginResult login = GetObjectFromJson<RepetierLoginResult>(jsonBody);
                                    if (login != null)
                                    {
                                        OnLoginResultReceived(new RepetierLoginRequiredEventArgs()
                                        {
                                            ResultData = login,
                                            LoginSucceeded = true,
                                            CallbackId = PingCounter,
                                            SessonId = SessionId,
                                            Printer = obj.Printer,
                                        });
                                    }
                                    break;
                                case "temp":
                                    EventTempData eventTempData = GetObjectFromJson<EventTempData>(jsonBody);
                                    if (eventTempData != null)
                                    {
                                        OnTempDataReceived(new RepetierTempDataEventArgs()
                                        {
                                            TemperatureData = eventTempData,
                                            CallbackId = PingCounter,
                                            SessonId = SessionId,
                                            Printer = obj.Printer,
                                        });
                                    }
                                    break;
                                case "jobStarted":
                                    EventJobStartedData eventJobStarted = GetObjectFromJson<EventJobStartedData>(jsonBody);
                                    OnJobStarted(new RepetierJobStartedEventArgs()
                                    {
                                        Job = eventJobStarted,
                                        CallbackId = PingCounter,
                                        SessonId = SessionId,
                                        Printer = obj.Printer,
                                    });
                                    break;
                                case "jobsChanged":
                                    EventJobChangedData eventJobsChanged = GetObjectFromJson<EventJobChangedData>(jsonBody);
                                    if (eventJobsChanged != null)
                                    {
                                        OnJobsChangedEvent(new RepetierJobsChangedEventArgs()
                                        {
                                            Data = eventJobsChanged,
                                            CallbackId = PingCounter,
                                            SessonId = SessionId,
                                            Printer = obj.Printer,
                                        });
                                    }
                                    break;
                                case "jobDeactivated":
                                case "jobFinished":
                                    EventJobFinishedData eventJobFinished = GetObjectFromJson<EventJobFinishedData>(jsonBody);
                                    if (eventJobFinished != null)
                                    {
                                        OnJobFinished(new RepetierJobFinishedEventArgs()
                                        {
                                            Job = eventJobFinished,
                                            CallbackId = PingCounter,
                                            SessonId = SessionId,
                                            Printer = obj.Printer,
                                        });
                                    }
                                    break;
                                case "messagesChanged":
                                    EventMessageChangedData eventMessageChanged = GetObjectFromJson<EventMessageChangedData>(jsonBody);
                                    if (eventMessageChanged != null)
                                    {
                                        OnMessagesChangedEvent(new RepetierMessagesChangedEventArgs()
                                        {
                                            RepetierMessage = eventMessageChanged,
                                            CallbackId = PingCounter,
                                            SessonId = SessionId,
                                            Printer = obj.Printer,
                                        });
                                    }
                                    break;
                                case "hardwareInfo":
                                    EventHardwareInfoChangedData eventHardwareInfoChanged = GetObjectFromJson<EventHardwareInfoChangedData>(jsonBody);
                                    OnHardwareInfoChangedEvent(new RepetierHardwareInfoChangedEventArgs()
                                    {
                                        Info = eventHardwareInfoChanged,
                                        CallbackId = PingCounter,
                                        SessonId = SessionId,
                                        Printer = obj.Printer,
                                    });
                                    break;
                                case "wifiChanged":
                                    EventWifiChangedData eventWifiChanged = GetObjectFromJson<EventWifiChangedData>(jsonBody);
                                    OnWifiChangedEvent(new RepetierWifiChangedEventArgs()
                                    {
                                        Data = eventWifiChanged,
                                        CallbackId = PingCounter,
                                        SessonId = SessionId,
                                        Printer = obj.Printer,
                                    });
                                    break;
                                case "gcodeInfoUpdated":
                                    EventGcodeInfoUpdatedData eventGcodeInfoUpdatedChanged = GetObjectFromJson<EventGcodeInfoUpdatedData>(jsonBody);
                                    break;
                                case "layerChanged":
                                    RepetierLayerChangedEvent eventLayerChanged = GetObjectFromJson<RepetierLayerChangedEvent>(jsonBody);
                                    break;
                                case "updatePrinterState":
                                    RepetierPrinterState updatePrinterState = GetObjectFromJson<RepetierPrinterState>(jsonBody);
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
                                    if (!loggedResults.ContainsKey(name))
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
                OnWebSocketMessageReceived(new RepetierWebsocketEventArgs()
                {
                    CallbackId = PingCounter,
                    Message = e.Message,
                    SessonId = SessionId,
                });
            }
            catch (JsonException jecx)
            {
                OnError(new RepetierJsonConvertEventArgs()
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

        [Obsolete]
        void WebSocket_MessageReceivedOld(object sender, MessageReceivedEventArgs e)
        {
            try
            {
                if (e.Message == null || string.IsNullOrEmpty(e.Message))
                    return;
                if (e.Message.ToLower().Contains("login"))
                {
                    //var login = GetObjectFromJson<RepetierLoginRequiredResult>(e.Message, JsonSerializerSettings);
                    //var login = GetObjectFromJson<RepetierLoginResult>(e.Message, JsonSerializerSettings);
                }
                if (e.Message.ToLower().Contains("session"))
                {
                    //Session = GetObjectFromJson<EventSession>(e.Message, JsonSerializerSettings);
                    Session = GetObjectFromJson<EventSession>(e.Message);
                }
                else if (e.Message.ToLower().Contains("event"))
                {
                    RepetierEventContainer repetierEvent = GetObjectFromJson<RepetierEventContainer>(e.Message, JsonSerializerSettings);
                    if (repetierEvent != null)
                    {
                        foreach (RepetierEventData obj in repetierEvent.Data)
                        {
                            string jsonString = obj.Data.ToString();
                            if (obj.EventName == "userCredentials")
                            {
                                //EventUserCredentialsData eventUserCredentials = JsonConvert.DeserializeObject<EventUserCredentialsData>(jsonString);
                                //var login = JsonConvert.DeserializeObject<RepetierLoginResult>(jsonString, JsonSerializerSettings);
                                RepetierLoginResult login = GetObjectFromJson<RepetierLoginResult>(jsonString);
                                if (login != null)
                                {
                                    OnLoginResultReceived(new RepetierLoginRequiredEventArgs()
                                    {
                                        ResultData = login,
                                        LoginSucceeded = true,
                                        CallbackId = PingCounter,
                                        SessonId = SessionId,
                                        Printer = obj.Printer,
                                    });
                                }
                            }
                            else if (obj.EventName == "temp")
                            {
                                //EventTempData eventTempData = JsonConvert.DeserializeObject<EventTempData>(jsonString, JsonSerializerSettings);
                                EventTempData eventTempData = GetObjectFromJson<EventTempData>(jsonString);
                                if (eventTempData != null)
                                    OnTempDataReceived(new RepetierTempDataEventArgs()
                                    {
                                        TemperatureData = eventTempData,
                                        CallbackId = PingCounter,
                                        SessonId = SessionId,
                                        Printer = obj.Printer,
                                    });

                            }
                            else if (obj.EventName == "jobStarted")
                            {
                                //EventJobStartedData eventJobStarted = JsonConvert.DeserializeObject<EventJobStartedData>(jsonString, JsonSerializerSettings);
                                EventJobStartedData eventJobStarted = GetObjectFromJson<EventJobStartedData>(jsonString);
                            }
                            else if (obj.EventName == "jobsChanged")
                            {
                                // Gets triggered when a model has been deleted
                                //EventJobChangedData eventJobsChanged = JsonConvert.DeserializeObject<EventJobChangedData>(jsonString, JsonSerializerSettings);
                                EventJobChangedData eventJobsChanged = GetObjectFromJson<EventJobChangedData>(jsonString);
                                if (eventJobsChanged != null)
                                    OnJobsChangedEvent(new RepetierJobsChangedEventArgs()
                                    {
                                        Data = eventJobsChanged,
                                        CallbackId = PingCounter,
                                        SessonId = SessionId,
                                        Printer = obj.Printer,
                                    });
                            }
                            else if (obj.EventName == "jobFinished")
                            {
                                //EventJobFinishedData eventJobFinished = JsonConvert.DeserializeObject<EventJobFinishedData>(jsonString, JsonSerializerSettings);
                                EventJobFinishedData eventJobFinished = GetObjectFromJson<EventJobFinishedData>(jsonString);
                                if (eventJobFinished != null)
                                    OnJobFinished(new RepetierJobFinishedEventArgs()
                                    {
                                        Job = eventJobFinished,
                                        CallbackId = PingCounter,
                                        SessonId = SessionId,
                                        Printer = obj.Printer,
                                    });
                            }
                            else if (obj.EventName == "messagesChanged")
                            {
                                //EventMessageChangedData eventMessageChanged = JsonConvert.DeserializeObject<EventMessageChangedData>(jsonString, JsonSerializerSettings);
                                EventMessageChangedData eventMessageChanged = GetObjectFromJson<EventMessageChangedData>(jsonString);
                                if (eventMessageChanged != null)
                                    OnMessagesChangedEvent(new RepetierMessagesChangedEventArgs()
                                    {
                                        RepetierMessage = eventMessageChanged,
                                        CallbackId = PingCounter,
                                        SessonId = SessionId,
                                        Printer = obj.Printer,
                                    });
                            }
                            else if (obj.EventName == "prepareJob")
                            {
                                // No information provided in "Data"
                            }
                            else if (obj.EventName == "timer30" || obj.EventName == "timer60" || obj.EventName == "timer300")
                            {

                            }
                            else if (obj.EventName == "hardwareInfo")
                            {
                                //EventHardwareInfoChangedData eventHardwareInfoChanged = JsonConvert.DeserializeObject<EventHardwareInfoChangedData>(jsonString, JsonSerializerSettings);
                                EventHardwareInfoChangedData eventHardwareInfoChanged = GetObjectFromJson<EventHardwareInfoChangedData>(jsonString);
                            }
                            else if (obj.EventName == "wifiChanged")
                            {
                                EventWifiChangedData eventWifiChanged = GetObjectFromJson<EventWifiChangedData>(jsonString);
                            }
                            else if (obj.EventName == "modelGroupListChanged")
                            {

                            }
                            else if (obj.EventName == "modelGroupListChanged")
                            {
                                // no data available here
                            }
                            else if (obj.EventName == "gcodeInfoUpdated")
                            {
                                EventGcodeInfoUpdatedData eventGcodeInfoUpdatedChanged = GetObjectFromJson<EventGcodeInfoUpdatedData>(jsonString);
                            }
                            else if (obj.EventName == "dispatcherCount")
                            {

                            }
                            else if (obj.EventName == "printerListChanged")
                            {
                                // 2
                            }
                            else if (obj.EventName == "recoverChanged")
                            {

                            }
                            else if (obj.EventName == "state")
                            {

                            }
                            else if (obj.EventName == "printqueueChanged")
                            {
                                // {"slug": "Prusa_i3_MK3S1"}
                            }
                            else if (obj.EventName == "config")
                            {

                            }
                            else if (obj.EventName == "log")
                            {

                            }
                            else if (obj.EventName == "workerFinished")
                            {

                            }
                            else
                            {

                            }
                        }
                    }
                }
                OnWebSocketMessageReceived(new RepetierWebsocketEventArgs()
                {
                    CallbackId = PingCounter,
                    Message = e.Message,
                    SessonId = SessionId,
                });
            }
            catch (JsonException jecx)
            {
                OnError(new RepetierJsonConvertEventArgs()
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

        void WebSocket_Closed(object sender, EventArgs e)
        {
            try
            {
                IsListeningToWebsocket = false;
                StopPingTimer();
                OnWebSocketDisconnected(new RepetierEventArgs()
                {
                    Message = $"WebSocket connection to {WebSocket} closed. Connection state while closing was '{(IsOnline ? "online" : "offline")}'",
                    Printer = GetActivePrinterSlug(),
                });
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
            }
        }

        void WebSocket_Opened(object sender, EventArgs e)
        {
            try
            {
                // Trigger ping to get session id
                string pingCommand = $"{{\"action\":\"ping\",\"data\":{{\"source\":\"{"App"}\"}},\"printer\":\"{GetActivePrinterSlug()}\",\"callback_id\":{PingCounter}}}";
                WebSocket?.Send(pingCommand);

                PingTimer = new Timer((action) => PingServer(), null, 0, 2500);

                IsListeningToWebsocket = true;
                OnWebSocketConnected(new RepetierEventArgs()
                {
                    Message = $"WebSocket connection to {WebSocket} established. Connection state while opening was '{(IsOnline ? "online" : "offline")}'",
                    Printer = GetActivePrinterSlug(),
                });
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
            }
        }

        void WebSocket_DataReceived(object sender, DataReceivedEventArgs e)
        {
            try
            {
                OnWebSocketDataReceived(new RepetierWebsocketEventArgs()
                {
                    CallbackId = PingCounter,
                    Data = e.Data,
                    SessonId = SessionId,
                });
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
            }
        }

#endregion

        #region Methods

        #region Private

        #region ValidateResult

        bool GetQueryResult(string result, bool emptyResultIsValid = false)
        {
            try
            {
                if ((string.IsNullOrEmpty(result) || result == "{}") && emptyResultIsValid)
                    return true;
                RepetierActionResult actionResult = GetObjectFromJson<RepetierActionResult>(result);
                return actionResult?.Ok ?? false;
            }
            catch (JsonException jecx)
            {
                OnError(new RepetierJsonConvertEventArgs()
                {
                    Exception = jecx,
                    OriginalString = result,
                    Message = jecx.Message,
                });
                return false;
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return false;
            }
        }

        RepetierApiRequestRespone ValidateRespone(RestResponse respone, Uri targetUri)
        {
            RepetierApiRequestRespone apiRsponeResult = new() { IsOnline = IsOnline };
            try
            {
                if ((
                    respone.StatusCode == HttpStatusCode.OK || respone.StatusCode == HttpStatusCode.NoContent) &&
                    respone.ResponseStatus == ResponseStatus.Completed)
                {
                    apiRsponeResult.IsOnline = true;
                    AuthenticationFailed = false;
                    apiRsponeResult.Result = respone.Content;
                    apiRsponeResult.Succeeded = true;
                    apiRsponeResult.EventArgs = new RepetierRestEventArgs()
                    {
                        Status = respone.ResponseStatus.ToString(),
                        Exception = respone.ErrorException,
                        Message = respone.ErrorMessage,
                        Uri = targetUri,
                    };
                }
                else if (respone.StatusCode == HttpStatusCode.NonAuthoritativeInformation
                    || respone.StatusCode == HttpStatusCode.Forbidden
                    || respone.StatusCode == HttpStatusCode.Unauthorized
                    )
                {
                    apiRsponeResult.IsOnline = true;
                    apiRsponeResult.HasAuthenticationError = true;
                    apiRsponeResult.EventArgs = new RepetierRestEventArgs()
                    {
                        Status = respone.ResponseStatus.ToString(),
                        Exception = respone.ErrorException,
                        Message = respone.ErrorMessage,
                        Uri = targetUri,
                    };
                }
                else if (respone.StatusCode == HttpStatusCode.Conflict)
                {
                    apiRsponeResult.IsOnline = true;
                    apiRsponeResult.HasAuthenticationError = false;
                    apiRsponeResult.EventArgs = new RepetierRestEventArgs()
                    {
                        Status = respone.ResponseStatus.ToString(),
                        Exception = respone.ErrorException,
                        Message = respone.ErrorMessage,
                        Uri = targetUri,
                    };
                }
                else
                {
                    OnRestApiError(new RepetierRestEventArgs()
                    {
                        Status = respone.ResponseStatus.ToString(),
                        Exception = respone.ErrorException,
                        Message = respone.ErrorMessage,
                        Uri = targetUri,
                    });
                }
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
            }
            return apiRsponeResult;
        }
        #endregion

        #region ValidateActivePrinter
        string GetActivePrinterSlug()
        {
            try
            {
                if (!IsReady || ActivePrinter == null)
                {
                    return string.Empty;
                }
                return ActivePrinter.Slug;
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return string.Empty;
            }
        }
        bool IsPrinterSlugSelected(string PrinterSlug)
        {
            try
            {
                return PrinterSlug == GetActivePrinterSlug();
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return false;
            }
        }
        #endregion

        #region RestApi
        
        // New web api: https://prgdoc.repetier-server.com/v1/docs/index.html#/en/web-api/direct?id=gcode
        async Task<RepetierApiRequestRespone> SendRestApiRequestAsync(
            RepetierCommandBase commandBase,
            RepetierCommandFeature commandFeature,
            string command = "",
            //string jsonDataString = "",
            object jsonData = null,
            string printerName = "",
            CancellationTokenSource cts = default,
            string requestTargetUri = "")
        {
            RepetierApiRequestRespone apiRsponeResult = new() { IsOnline = IsOnline };
            if (!IsOnline) return apiRsponeResult;
            try
            {
                if (string.IsNullOrEmpty(printerName))
                {
                    printerName = "";
                }
                if (cts == default)
                {
                    cts = new(DefaultTimeout);
                }
                if (restClient == null)
                {
                    UpdateRestClientInstance();
                }
                RestRequest request = new(
                    string.IsNullOrEmpty(requestTargetUri) ?
                    !string.IsNullOrEmpty(printerName) ? $"{commandBase}/{commandFeature}/{{slug}}" : $"{commandBase}/{commandFeature}" :
                    requestTargetUri)
                {
                    RequestFormat = DataFormat.Json,
                    Method = Method.Post
                };

                if (!string.IsNullOrEmpty(printerName))
                {
                    request.AddUrlSegment("slug", printerName);
                }
                request.AddParameter("a", command, ParameterType.QueryString);

                string jsonDataString = "";
                if(jsonData is string jsonString)
                {
                    jsonDataString = jsonString;
                }
                else if(jsonData is object jsonObject)
                {
                    jsonDataString = JsonConvert.SerializeObject(jsonObject);
                }
                
                request.AddParameter("data", jsonDataString, ParameterType.QueryString);
                if (!string.IsNullOrEmpty(ApiKey))
                {
                    request.AddParameter("apikey", ApiKey, ParameterType.QueryString);
                }
                else if (!string.IsNullOrEmpty(SessionId))
                {
                    request.AddParameter("sess", SessionId, ParameterType.QueryString);
                }

                Uri fullUri = restClient.BuildUri(request);
                try
                {
                    RestResponse respone = await restClient.ExecuteAsync(request, cts.Token).ConfigureAwait(false);
                    apiRsponeResult = ValidateRespone(respone, fullUri);
                }
                catch (TaskCanceledException texp)
                {
                    if (!IsOnline)
                    {
                        OnError(new UnhandledExceptionEventArgs(texp, false));
                    }
                    // Throws exception on timeout, not actually an error but indicates if the server is reachable.
                }
                catch (HttpRequestException hexp)
                {
                    // Throws exception on timeout, not actually an error but indicates if the server is not reachable.
                    if (!IsOnline)
                    {
                        OnError(new UnhandledExceptionEventArgs(hexp, false));
                    }
                }
                catch (TimeoutException toexp)
                {
                    // Throws exception on timeout, not actually an error but indicates if the server is not reachable.
                    if (!IsOnline)
                    {
                        OnError(new UnhandledExceptionEventArgs(toexp, false));
                    }
                }
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
            }
            return apiRsponeResult;
        }


        async Task<RepetierApiRequestRespone> SendOnlineCheckRestApiRequestAsync(
            RepetierCommandBase commandBase,
            RepetierCommandFeature commandFeature,
            CancellationTokenSource cts,
            string command,
            string requestTargetUri = ""
            )
        {
            RepetierApiRequestRespone apiRsponeResult = new() { IsOnline = false };
            try
            {
                if (restClient == null)
                {
                    UpdateRestClientInstance();
                }
                RestRequest request = new(
                    string.IsNullOrEmpty(requestTargetUri) ?
                    $"{commandBase}/{commandFeature}" :
                    requestTargetUri)
                {
                    RequestFormat = DataFormat.Json,
                    Method = Method.Post
                };
                _ = request.AddParameter("a", command, ParameterType.QueryString);
                if (!string.IsNullOrEmpty(ApiKey))
                {
                    _ = request.AddParameter("apikey", ApiKey, ParameterType.QueryString);
                }
                else if (!string.IsNullOrEmpty(SessionId))
                {
                    _ = request.AddParameter("sess", SessionId, ParameterType.QueryString);
                }

                Uri fullUri = restClient.BuildUri(request);
                try
                {
                    RestResponse respone = await restClient.ExecuteAsync(request, cts.Token).ConfigureAwait(false);
                    apiRsponeResult = ValidateRespone(respone, fullUri);
                }
                catch (TaskCanceledException)
                {
                    // Throws exception on timeout, not actually an error but indicates if the server is not reachable.
                }
                catch (HttpRequestException)
                {
                    // Throws exception on timeout, not actually an error but indicates if the server is not reachable.
                }
                catch (TimeoutException)
                {
                    // Throws exception on timeout, not actually an error but indicates if the server is not reachable.
                }

            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
            }
            return apiRsponeResult;
        }

        #endregion

        #region Download
        public async Task<byte[]> GetDynamicRenderImageAsync(long modelId, bool thumbnail, int timeout = 20000)
        {
            try
            {
                byte[] resultObject = new byte[0];

                string currentPrinter = GetActivePrinterSlug();
                if (string.IsNullOrEmpty(currentPrinter)) return resultObject;

                // http://repetierserver.local/dyn/render_image?q=models&id=158&slug=Prusa_i3_MK3S&t=m

                // https://www.repetier-server.com/manuals/programming/API/index.html
                if (restClient == null)
                {
                    UpdateRestClientInstance();
                }
                RestRequest request = new("dyn/render_image")
                {
                    AlwaysMultipartFormData = true,
                    RequestFormat = DataFormat.Json,
                    Method = Method.Get,
                    Timeout = -1,
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

                Uri fullUrl = restClient.BuildUri(request);
                
                // Workaround, because the RestClient returns bad requests
                using WebClient client = new();
                byte[] bytes = await client.DownloadDataTaskAsync(fullUrl);
                return bytes;

                /*
                CancellationTokenSource cts = new(timeout);
                byte[] respone = await restClient.DownloadDataAsync(request, cts.Token).ConfigureAwait(false);
                if(respone?.Length == 0)
                {

                }
                return respone;
                */
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return new byte[0];
            }
        }

        public async Task<byte[]> GetDynamicRenderImageByJobIdAsync(long jobId, bool thumbnail, int timeout = 20000)
        {
            try
            {
                byte[] resultObject = new byte[0];

                string currentPrinter = GetActivePrinterSlug();
                if (string.IsNullOrEmpty(currentPrinter)) return resultObject;

                if (restClient == null)
                {
                    UpdateRestClientInstance();
                }
                RestRequest request = new("dyn/render_image")
                {
                    RequestFormat = DataFormat.None,
                    Method = Method.Get,
                    Timeout = timeout
                };

                request.AddParameter("q", "jobs");
                request.AddParameter("id", jobId);
                request.AddParameter("slug", currentPrinter);
                request.AddParameter("t", thumbnail ? "s" : "l");
                request.AddParameter("apikey", ApiKey, ParameterType.QueryString);

                Uri fullUrl = restClient.BuildUri(request);

                // Workaround, because the RestClient returns bad requests
                using WebClient client = new();
                byte[] bytes = await client.DownloadDataTaskAsync(fullUrl);
                if (bytes?.Length == 0)
                {

                }
                return bytes;

                /*
                CancellationTokenSource cts = new(timeout);
                byte[] respone = await restClient.DownloadDataAsync(request, cts.Token).ConfigureAwait(false);
                return respone;
                */
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return new byte[0];
            }
        }
        #endregion

        #region StateUpdates
        void UpdatePrinterState(RepetierPrinterState newState)
        {
            try
            {
                if (newState == null) return;

                ActiveExtruder = newState.ActiveExtruder;
                NumberOfExtruders = newState.NumExtruder;

                Extruders = new ObservableCollection<RepetierPrinterHeaterComponent>(newState.Extruder);
                IsDualExtruder = Extruders != null && Extruders.Count > 1;

                HeatedBeds = new ObservableCollection<RepetierPrinterHeaterComponent>(newState.HeatedBeds);
                HasHeatedBed = HeatedBeds != null && HeatedBeds.Count > 0;

                HeatedChambers = new ObservableCollection<RepetierPrinterHeaterComponent>(newState.HeatedChambers);
                HasHeatedBed = HeatedChambers != null && HeatedChambers.Count > 0;

                Fans = new ObservableCollection<RepetierPrinterFan>(newState.Fans);
                HasFan = Fans != null && Fans.Count > 0;

                CurX = newState.X;
                CurY = newState.Y;
                CurZ = newState.Z;

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
        void UpdatePrinterConfig(RepetierPrinterConfig newConfig)
        {
            try
            {
                if (newConfig == null) return;
                if (newConfig.Webcams != null)
                {
                    WebCams = new ObservableCollection<RepetierPrinterConfigWebcam>(newConfig.Webcams);
                    HasWebCam = WebCams != null && WebCams.Count > 0;
                    if (HasWebCam)
                        SelectedWebCam = WebCams[0];
                }

            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
            }
        }
        void UpdateActivePrintInfo(RepetierCurrentPrintInfo newPrintInfo)
        {
            try
            {
                if (newPrintInfo != null)
                {
                    IsConnectedPrinterOnline = newPrintInfo.Online > 0;
                    IsPrinting = newPrintInfo.Jobid > 0;
                    IsPaused = newPrintInfo.Paused;
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

        #region Models
        async Task<RepetierModelList> GetModelListInfoResponeAsync(string printerName)
        {
            RepetierApiRequestRespone result = new();
            try
            {
                result = await SendRestApiRequestAsync(
                   commandBase: RepetierCommandBase.printer,
                   commandFeature: RepetierCommandFeature.api,
                   command: "listModels",
                   printerName: printerName)
                    .ConfigureAwait(false);

                RepetierModelList list = GetObjectFromJson<RepetierModelList>(result.Result);
                await UpdateFreeSpaceAsync().ConfigureAwait(false);

                return list;
            }
            catch (JsonException jecx)
            {
                OnError(new RepetierJsonConvertEventArgs()
                {
                    Exception = jecx,
                    OriginalString = result.Result,
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
#endregion

        #region ModelGroups
        async Task<RepetierModelGroups> GetModelGroupsAsync(string printerName)
        {
            RepetierApiRequestRespone result = new();
            try
            {
                result = await SendRestApiRequestAsync(
                   commandBase: RepetierCommandBase.printer,
                   commandFeature: RepetierCommandFeature.api,
                   command: "listModelGroups",
                   printerName: printerName)
                    .ConfigureAwait(false);

                return GetObjectFromJson<RepetierModelGroups>(result.Result);
            }
            catch (JsonException jecx)
            {
                OnError(new RepetierJsonConvertEventArgs()
                {
                    Exception = jecx,
                    OriginalString = result.Result,
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

        #endregion

        #region Jobs
        async Task<RepetierJobListRespone> GetJobListResponeAsync(string printerName)
        {
            RepetierApiRequestRespone result = new();
            RepetierJobListRespone resultObject = null; //new();
            try
            {
                result = await SendRestApiRequestAsync(
                   commandBase: RepetierCommandBase.printer,
                   commandFeature: RepetierCommandFeature.api,
                   command: "listJobs",
                   printerName: printerName)
                    .ConfigureAwait(false);

                return GetObjectFromJson<RepetierJobListRespone>(result.Result);
            }
            catch (JsonException jecx)
            {
                OnError(new RepetierJsonConvertEventArgs()
                {
                    Exception = jecx,
                    OriginalString = result.Result,
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
        async Task<RepetierWebCallList> GetWebCallListAsync(string printerName)
        {
            RepetierApiRequestRespone result = new();
            try
            {
                result = await SendRestApiRequestAsync(
                   commandBase: RepetierCommandBase.printer,
                   commandFeature: RepetierCommandFeature.api,
                   command: "webCallsList",
                   printerName: printerName
                   )
                    .ConfigureAwait(false);

                return GetObjectFromJson<RepetierWebCallList>(result.Result);
            }
            catch (JsonException jecx)
            {
                OnError(new RepetierJsonConvertEventArgs()
                {
                    Exception = jecx,
                    OriginalString = result.Result,
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

        #region Proxy
        Uri GetProxyUri() => ProxyAddress.StartsWith("http://") || ProxyAddress.StartsWith("https://") ? new Uri($"{ProxyAddress}:{ProxyPort}") : new Uri($"{(SecureProxyConnection ? "https" : "http")}://{ProxyAddress}:{ProxyPort}");
        
        WebProxy GetCurrentProxy()
        {
            WebProxy proxy = new()
            {
                Address = GetProxyUri(),
                BypassProxyOnLocal = false,
                UseDefaultCredentials = ProxyUserUsesDefaultCredentials,
            };
            if (ProxyUserUsesDefaultCredentials && !string.IsNullOrEmpty(ProxyUser))
            {
                proxy.Credentials = new NetworkCredential(ProxyUser, ProxyPassword);
            }
            else
            {
                proxy.UseDefaultCredentials = ProxyUserUsesDefaultCredentials;
            }
            return proxy;
        }
        void UpdateRestClientInstance()
        {
            if (string.IsNullOrEmpty(ServerAddress))
            {
                return;
            }
            if (EnableProxy && !string.IsNullOrEmpty(ProxyAddress))
            {
                RestClientOptions options = new(FullWebAddress)
                {
                    ThrowOnAnyError = true,
                    MaxTimeout = 10000,
                };
                HttpClientHandler httpHandler = new()
                {
                    UseProxy = true,
                    Proxy = GetCurrentProxy(),
                    AllowAutoRedirect = true,
                };

                httpClient = new(handler: httpHandler, disposeHandler: true);
                restClient = new(httpClient: httpClient, options: options);
            }
            else
            {
                httpClient = null;
                restClient = new(baseUrl: FullWebAddress);
            }
        }
        #endregion

        #region Timers
        void StopPingTimer()
        {
            if (PingTimer != null)
            {
                try
                {
                    PingTimer?.Change(Timeout.Infinite, Timeout.Infinite);
                    PingTimer = null;
                    IsListeningToWebsocket = false;
                }
                catch (ObjectDisposedException)
                {
                    //PingTimer = null;
                }
            }
        }
        void StopTimer()
        {
            if (Timer != null)
            {
                try
                {
                    Timer?.Change(Timeout.Infinite, Timeout.Infinite);
                    Timer = null;
                    IsListening = false;
                }
                catch (ObjectDisposedException)
                {
                    //PingTimer = null;
                }
            }
        }
        #endregion

        #endregion

        #region Public

        #region Proxy
        public void SetProxy(bool secure, string address, int port, bool enable = true)
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
        public void SetProxy(bool secure, string address, int port, string user = "", SecureString password = null, bool enable = true)
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
        public void StartListening(bool stopActiveListening = false)
        {
            if (IsListening)// avoid multiple sessions
            {
                if (stopActiveListening)
                {
                    StopListening();
                }
                else
                {
                    return; // StopListening();
                }
            }
            ConnectWebSocket();
            Timer = new Timer(async (action) =>
            {
                // Do not check the online state ever tick
                if (RefreshCounter > 5)
                {
                    RefreshCounter = 0;
                    await CheckOnlineAsync(3500).ConfigureAwait(false);
                }
                else RefreshCounter++;
                if (IsOnline)
                {
                    List<Task> tasks = new()
                    {
                        //CheckServerOnlineAsync(),
                        RefreshPrinterStateAsync(),
                        RefreshCurrentPrintInfosAsync(),
                    };
                    await Task.WhenAll(tasks).ConfigureAwait(false);
                }
                else if (IsListening)
                {
                    StopListening();
                }
            }, null, 0, RefreshInterval * 1000);
            IsListening = true;
        }
        public void StopListening()
        {
            CancelCurrentRequests();
            StopPingTimer();
            StopTimer();

            if (IsListeningToWebsocket)
                DisconnectWebSocket();
            IsListening = false;
        }

#if NET5_0_OR_GREATER || NETSTANDARD
        public async Task StartListeningAsync(bool stopActiveListening = false)
        {
            if (IsListening)// avoid multiple sessions
            {
                if (stopActiveListening)
                {
                    await StopListeningAsync();
                }
                else
                {
                    return; // StopListening();
                }
            }
            await ConnectWebSocketAsync().ConfigureAwait(false);
            Timer = new Timer(async (action) =>
            {
                // Do not check the online state ever tick
                if (RefreshCounter > 5)
                {
                    RefreshCounter = 0;
                    await CheckOnlineAsync(3500).ConfigureAwait(false);
                }
                else RefreshCounter++;
                if (IsOnline)
                {
                    List<Task> tasks = new()
                    {
                        RefreshPrinterStateAsync(),
                        RefreshCurrentPrintInfosAsync(),
                    };
                    await Task.WhenAll(tasks).ConfigureAwait(false);               
                }
                else if (IsListening)
                {
                    await StopListeningAsync(); // StopListening();
                }
            }, null, 0, RefreshInterval * 1000);
            IsListening = true;
        }
        public async Task StopListeningAsync()
        {
            CancelCurrentRequests();
            StopPingTimer();
            StopTimer();

            if (IsListeningToWebsocket)
            {
                await DisconnectWebSocketAsync().ConfigureAwait(false);
            }
            IsListening = false;
        }
#endif
        public async Task RefreshAllAsync(GcodeImageType imageType = GcodeImageType.Thumbnail)
        {
            try
            {
                // Avoid multiple calls
                if (IsRefreshing) return;
                IsRefreshing = true;
                //await RefreshPrinterListAsync();
                List<Task> task = new()
                {
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
                };
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
            SendWebSocketCommand(command);
        }

        public async Task LogoutAsync()
        {
            if (string.IsNullOrEmpty(SessionId) || !IsListeningToWebsocket)
                throw new Exception($"Current session is null! Please start the Listener first to establish a WebSocket connection!");
            //_ = await SendRestApiRequestAsync("", "logout").ConfigureAwait(false);
            _ = await SendRestApiRequestAsync(RepetierCommandBase.printer, RepetierCommandFeature.api, command: "logout")
                .ConfigureAwait(false);
        }

        public void Logout()
        {
            if (string.IsNullOrEmpty(SessionId) || !IsListeningToWebsocket)
                throw new Exception($"Current session is null! Please start the Listener first to establish a WebSocket connection!");

            string command =
                $"{{\"action\":\"logout\",\"data\":{{}},\"printer\":\"{GetActivePrinterSlug()}\",\"callback_id\":{PingCounter++}}}";
            SendWebSocketCommand(command);
        }


        string EncryptPassword(string userName, SecureString password, string sessionId)
        {
            // Password is MD5(sessionId + MD5(login + password))
            // Source: https://www.godo.dev/tutorials/csharp-md5/
            using MD5 md5 = MD5.Create();
            string credentials = $"{userName}{SecureStringHelper.ConvertToString(password)}";
            // Hash credentials first
            md5.ComputeHash(Encoding.UTF8.GetBytes(credentials));
            List<byte> inputBuffer = Encoding.UTF8.GetBytes(sessionId).ToList();

            string hexHash = BitConverter.ToString(md5.Hash).Replace("-", string.Empty).ToLowerInvariant();
            inputBuffer.AddRange(Encoding.UTF8.GetBytes(hexHash));

            md5.ComputeHash(inputBuffer.ToArray());

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
        public async Task SetPrinterActiveAsync(int index = -1, bool refreshPrinterList = true)
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
        public async Task SetPrinterActiveAsync(string slug, bool refreshPrinterList = true)
        {
            try
            {
                if (refreshPrinterList)
                    await RefreshPrinterListAsync().ConfigureAwait(false);
                IPrinter3d printer = Printers.FirstOrDefault(prt => prt.Slug == slug);
                if (printer != null)
                    ActivePrinter = printer;
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
            }
        }
        #endregion

        #region Cancel
        public void CancelCurrentRequests()
        {
            try
            {
                if (httpClient != null)
                {
                    httpClient.CancelPendingRequests();
                    UpdateRestClientInstance();
                }
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
            }
        }
        #endregion

        #region WebCam
        public string GetWebCamUri(int camIndex = 0, RepetierWebcamType type = RepetierWebcamType.Dynamic)
        {
            try
            {
                string currentPrinter = GetActivePrinterSlug();
                if (string.IsNullOrEmpty(currentPrinter)) return string.Empty;

                return $"{FullWebAddress}/printer/{(type == RepetierWebcamType.Dynamic ? "cammjpg" : "camjpg")}/{currentPrinter}?cam={camIndex}&apikey={ApiKey}";
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return "";
            }
        }
        public async Task<string> GetWebCamUriAsync(int camIndex = 0, RepetierWebcamType type = RepetierWebcamType.Dynamic)
        {
            try
            {
                string currentPrinter = GetActivePrinterSlug();
                if (string.IsNullOrEmpty(currentPrinter)) return string.Empty;

                await RefreshPrinterConfigAsync();
                return $"{FullWebAddress}/printer/{(type == RepetierWebcamType.Dynamic ? "cammjpg" : "camjpg")}/{currentPrinter}?cam={camIndex}&apikey={ApiKey}";
                /*
                RepetierPrinterConfigWebcam webcamConfig = WebCams?.FirstOrDefault(config => config.Pos == camIndex);
                if(webcamConfig != null)
                {
                    Uri webcamUri = type == RepetierWebcamType.Dynamic ? webcamConfig.DynamicUrl : webcamConfig.StaticUrl;
                    return $"{FullWebAddress}/printer/camjpg/{currentPrinter}?cam={camIndex}&apikey={API}";
                }
                else
                {
                    return $"{FullWebAddress}/printer/cammjpg/{currentPrinter}?cam={camIndex}&apikey={API}";
                }
                */
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return "";
            }
        }
        public async Task<RepetierPrinterConfigWebcam> GetWebCamConfigAsync(int camIndex = 0, bool refreshConfigs = true)
        {
            try
            {
                string currentPrinter = GetActivePrinterSlug();
                if (string.IsNullOrEmpty(currentPrinter)) return null;

                if (refreshConfigs)
                {
                    await RefreshPrinterConfigAsync();
                }
                
                return WebCams?.FirstOrDefault(webCam => webCam.Pos == camIndex);
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return null;
            }
        }
        public async Task<RepetierPrinterConfigWebcam> GetWebCamConfigAsync(string slug, int camIndex = 0)
        {
            try
            {
                RepetierPrinterConfig config = await GetPrinterConfigAsync(slug);             
                return config?.Webcams?.FirstOrDefault(webCam => webCam.Pos == camIndex);
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return null;
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

                    RepetierApiRequestRespone respone = await SendRestApiRequestAsync(
                       commandBase: RepetierCommandBase.printer,
                       commandFeature: RepetierCommandFeature.ping,
                       command: pingCommand,
                       cts: new(timeout)
                       )
                    .ConfigureAwait(false);
                    if (respone != null)
                    {
                        isReachable = respone.IsOnline;
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

        public async Task CheckOnlineAsync(int timeout = 10000)
        {
            CancellationTokenSource cts = new(timeout);
            await CheckOnlineAsync(cts).ConfigureAwait(false);
        }

        public async Task CheckOnlineAsync(CancellationTokenSource cts)
        {
            if (IsConnecting) return; // Avoid multiple calls
            IsConnecting = true;
            bool isReachable = false;
            try
            {
                string uriString = FullWebAddress;
                try
                {
                    // Send a blank api request in order to check if the server is reachable
                    RepetierApiRequestRespone respone = await SendOnlineCheckRestApiRequestAsync(
                       commandBase: RepetierCommandBase.printer,
                       commandFeature: RepetierCommandFeature.ping,
                       cts: cts,
                       command: "{}")
                    .ConfigureAwait(false);

                    isReachable = respone?.IsOnline == true;
                }
                catch (InvalidOperationException iexc)
                {
                    OnError(new UnhandledExceptionEventArgs(iexc, false));
                }
                catch (HttpRequestException rexc)
                {
                    OnError(new UnhandledExceptionEventArgs(rexc, false));
                }
                catch (TaskCanceledException)
                {
                    // Throws an exception on timeout, not actually an error
                }
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
            }
            IsConnecting = false;
            // Avoid offline message for short connection loss
            if (!IsOnline || isReachable || _retries > RetriesWhenOffline)
            {
                // Do not check if the previous state was already offline
                _retries = 0;
                IsOnline = isReachable;
            }
            else
            {
                // Retry with shorter timeout to see if the connection loss is real
                _retries++;
                cts = new(3500);
                await CheckOnlineAsync(cts).ConfigureAwait(false);
            }
        }

        public async Task<bool> CheckIfApiIsValidAsync(int timeout = 10000)
        {
            try
            {
                if (IsOnline)
                {
                    // Send an empty command to check the respone
                    string pingCommand = "{}";
                    var respone = await SendRestApiRequestAsync(
                        RepetierCommandBase.printer, RepetierCommandFeature.api, command: "ping", jsonData: pingCommand,
                        cts: new(timeout))
                        .ConfigureAwait(false);
                    if (respone.HasAuthenticationError)
                    {
                        AuthenticationFailed = true;
                        OnRestApiAuthenticationError(respone.EventArgs);
                    }
                    else
                    {
                        AuthenticationFailed = false;
                        OnRestApiAuthenticationSucceeded(respone.EventArgs);
                    }
                    return AuthenticationFailed;
                }
                else
                    return false;
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return false;
            }
        }

        Task SendRestApiRequestAsync(string v1, string v2, string pingCommand, int timeout)
        {
            throw new NotImplementedException();
        }

        public async Task CheckServerIfApiIsValidAsync(int timeout = 10000)
        {
            await CheckIfApiIsValidAsync(timeout).ConfigureAwait(false);
        }
        #endregion

        #region WebSocket
        public void SendWebSocketCommand(string command)
        {
            try
            {
                //string infoCommand = $"{{\"jsonrpc\":\"2.0\",\"method\":\"server.info\",\"params\":{{}},\"id\":1}}";
                if (WebSocket?.State == WebSocketState.Open)
                {
                    WebSocket.Send(command);
                }
            }
            catch (Exception exc)
            {
                OnWebSocketError(new ErrorEventArgs(exc));
            }
        }
#endregion

        #region Updates
        public async Task CheckForServerUpdateAsync()
        {
            RepetierApiRequestRespone result = new();
            try
            {
                result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api, command: "checkForUpdates").ConfigureAwait(false);
                if (result != null)
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
                OnError(new RepetierJsonConvertEventArgs()
                {
                    Exception = jecx,
                    OriginalString = result.Result,
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
            RepetierApiRequestRespone result = new();
            try
            {
                result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api, command: "autoupdate")
                    .ConfigureAwait(false);
                if (result != null)
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
                OnError(new RepetierJsonConvertEventArgs()
                {
                    Exception = jecx,
                    OriginalString = result.Result,
                    Message = jecx.Message,
                });
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
            }
        }
        public async Task<RepetierAvailableUpdateInfo> GetAvailableServerUpdateAsync()
        {
            RepetierApiRequestRespone result = new();
            RepetierAvailableUpdateInfo resultObject = new();
            try
            {
                result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api, command: "updateAvailable")
                    .ConfigureAwait(false);
                return GetObjectFromJson<RepetierAvailableUpdateInfo>(result.Result);
            }
            catch (JsonException jecx)
            {
                OnError(new RepetierJsonConvertEventArgs()
                {
                    Exception = jecx,
                    OriginalString = result.Result,
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

        public async Task<RepetierLicenseInfo> GetLicenseDataAsync()
        {
            RepetierApiRequestRespone result = new();
            try
            {
                result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api, command: "getLicenceData")
                    .ConfigureAwait(false);
                if (result != null)
                {
                    return GetObjectFromJson<RepetierLicenseInfo>(result.Result);
                }
                else
                    return null;
            }
            catch (JsonException jecx)
            {
                OnError(new RepetierJsonConvertEventArgs()
                {
                    Exception = jecx,
                    OriginalString = result.Result,
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

        public async Task<RepetierErrorCodes> SendGcodeAsync(string printerName, string filePath)
        {
            try
            {
                return await SendAndMoveGcodeAsync(printerName, filePath).ConfigureAwait(false);
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return RepetierErrorCodes.EXCEPTION;
            }
        }

        public async Task<RepetierErrorCodes> SendAndMoveGcodeAsync(string printerName, string filePath, string group = "#", int timeout = 25000)
        {
            // https://www.repetier-server.com/using-simplify-3d-repetier-server/
            try
            {
                FileInfo info = new(filePath);
                if (!info.Exists) return RepetierErrorCodes.FILE_NOT_FOUND;

                if (restClient == null)
                {
                    UpdateRestClientInstance();
                }
                RestRequest request = new(string.Format("/printer/model/{2}", ServerAddress, Port, printerName.Replace(" ", "_")))
                {
                    Method = Method.Post,
                    Timeout = timeout,
                    AlwaysMultipartFormData = true,
                };

                request.AddHeader("x-api-key", ApiKey);
                request.AddFile(Path.GetFileNameWithoutExtension(filePath), filePath);
                
                CancellationTokenSource cts = new(timeout);
                RestResponse respone = await restClient.ExecuteAsync(request, cts.Token).ConfigureAwait(false);
                
                if (respone.StatusCode == HttpStatusCode.OK)
                {
                    if (group != "#")
                    {
                        ObservableCollection<IGcode> res = await GetModelsAsync(printerName).ConfigureAwait(false);
                        List<IGcode> list = new(res);

                        string fileName = info.Name.Replace(Path.GetExtension(filePath), string.Empty);
                        IGcode model = list.FirstOrDefault(m => m.FileName == fileName);
                        bool result = await MoveModelToGroupAsync(printerName, group, model.Identifier).ConfigureAwait(false);
                    }
                    return RepetierErrorCodes.SUCCESS;
                }
                else return RepetierErrorCodes.FAILED;
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return RepetierErrorCodes.EXCEPTION;
            }
        }

        #endregion

        #region Models

        public async Task<ObservableCollection<IGcode>> GetModelsAsync(
            string PrinterName = "",
            GcodeImageType ImageType = GcodeImageType.Thumbnail,
            IProgress<int> Prog = null)
        {
            try
            {
                ObservableCollection<IGcode> modelDatas = new();
                if (!IsReady)
                    return modelDatas;

                string currentPrinter = string.IsNullOrEmpty(PrinterName) ? GetActivePrinterSlug() : PrinterName;
                if (string.IsNullOrEmpty(currentPrinter)) return modelDatas;

                // Reporting
                if (Prog != null)
                {
                    Prog.Report(0);
                }
                RepetierModelList models = await GetModelListInfoResponeAsync(currentPrinter).ConfigureAwait(false);
                if (models != null)
                {
                    List<RepetierModel> modelList = models.Data;
                    if (modelList != null)
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

                                if (Prog != null)
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
                        else if (Prog != null)
                        {
                            Prog.Report(100);
                        }
                        return Models;
                    }
                }

                if (Prog != null)
                    Prog.Report(100);

                return modelDatas;
            }
            catch (Exception exc)
            {
                if (Prog != null)
                    Prog.Report(100);

                OnError(new UnhandledExceptionEventArgs(exc, false));
                return new ObservableCollection<IGcode>();
            }
        }
        public async Task<Dictionary<long, byte[]>> GetModelImagesAsync(ObservableCollection<RepetierModel> models, RepetierImageType imageType = RepetierImageType.Thumbnail)
        {
            string currentPrinter = GetActivePrinterSlug();
            if (string.IsNullOrEmpty(currentPrinter)) return null;

            Dictionary<long, byte[]> result = new();
            try
            {

                //foreach (RepetierModel model in Models)
                for (int i = 0; i < models.Count; i++)
                {
                    RepetierModel model = models[i];
                    byte[] image = new byte[0];
                    switch (imageType)
                    {
                        case RepetierImageType.Thumbnail:
                            image = await GetDynamicRenderImageAsync(model.Identifier, true).ConfigureAwait(false);
                            break;
                        case RepetierImageType.Image:
                            image = await GetDynamicRenderImageAsync(model.Identifier, false).ConfigureAwait(false);
                            break;
                        default:
                            throw new NotSupportedException($"The image type '{imageType}' is not supported here.");
                            //break;
                    }
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
        public async Task<bool> DeleteModelFromServerAsync(RepetierModel model)
        {
            string currentPrinter = GetActivePrinterSlug();
            if (string.IsNullOrEmpty(currentPrinter)) return false;

            try
            {
                RepetierApiRequestRespone result =
                    await SendRestApiRequestAsync(RepetierCommandBase.printer, RepetierCommandFeature.api,
                    command: "removeModel", jsonData: string.Format("{{\"id\":{0}}}", model.Id), printerName: currentPrinter)
                    .ConfigureAwait(false);
                return GetQueryResult(result.Result);
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return false;
            }
        }

        public async Task RefreshModelsAsync(GcodeImageType imageType = GcodeImageType.Thumbnail, IProgress<int> prog = null)
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
                RepetierApiRequestRespone result =
                    await SendRestApiRequestAsync(
                        RepetierCommandBase.printer, RepetierCommandFeature.api,
                        command: "copyModel", jsonData: $"{{\"id\":{model.Identifier}, \"autostart\":{(startPrintIfPossible ? "true" : "false")}}}",
                        printerName: currentPrinter)
                    .ConfigureAwait(false);
                await RefreshJobListAsync().ConfigureAwait(false);
                return GetQueryResult(result.Result, true);
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return false;
            }
        }

        public async Task UpdateFreeSpaceAsync()
        {
            RepetierApiRequestRespone result = new();
            try
            {
                result = await SendRestApiRequestAsync(
                   commandBase: RepetierCommandBase.printer,
                   commandFeature: RepetierCommandFeature.api,
                   command: "freeSpace")
                    .ConfigureAwait(false);

                RepetierFreeSpaceRespone space = GetObjectFromJson<RepetierFreeSpaceRespone>(result.Result);
                if (space != null)
                {
                    FreeDiskSpace = space.Free;
                    TotalDiskSpace = space.Capacity;
                    AvailableDiskSpace = space.Available;
                }
            }
            catch (JsonException jecx)
            {
                OnError(new RepetierJsonConvertEventArgs()
                {
                    Exception = jecx,
                    OriginalString = result.Result,
                    TargetType = nameof(RepetierFreeSpaceRespone),
                    Message = jecx.Message,
                });
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
            }
        }
        public async Task RefreshDiskSpaceAsync()
        {
            await UpdateFreeSpaceAsync().ConfigureAwait(false);
        }

#endregion

        #region ModelGroups
        public async Task<bool> AddModelGroupAsync(string groupName)
        {
            string currentPrinter = GetActivePrinterSlug();
            if (string.IsNullOrEmpty(currentPrinter)) return false;

            try
            {
                RepetierApiRequestRespone result =
                    await SendRestApiRequestAsync(
                        RepetierCommandBase.printer, RepetierCommandFeature.api,
                        command: "addModelGroup", jsonData: string.Format("{{\"groupName\":\"{0}\"}}", groupName),
                        printerName: currentPrinter)
                    .ConfigureAwait(false);
                return GetQueryResult(result.Result);
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return false;
            }
        }
        public async Task<bool> AddModelGroupAsync(string printerName, string groupName)
        {
            try
            {
                RepetierApiRequestRespone result =
                    await SendRestApiRequestAsync(
                        RepetierCommandBase.printer, RepetierCommandFeature.api,
                        command: "addModelGroup", jsonData: string.Format("{{\"groupName\":\"{0}\"}}", groupName),
                        printerName: printerName)
                    .ConfigureAwait(false);
                return GetQueryResult(result.Result);
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return false;
            }
        }

        public async Task<bool> RemoveModelGroupAsync(string groupName)
        {
            string currentPrinter = GetActivePrinterSlug();
            if (string.IsNullOrEmpty(currentPrinter)) return false;

            try
            {
                RepetierApiRequestRespone result =
                    await SendRestApiRequestAsync(
                        RepetierCommandBase.printer, RepetierCommandFeature.api,
                        command: "delModelGroup", jsonData: string.Format("{{\"groupName\":\"{0}\"}}", groupName),
                        printerName: currentPrinter)
                    .ConfigureAwait(false);
                return GetQueryResult(result.Result);
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return false;
            }
        }
        public async Task<bool> RemoveModelGroupAsync(string printerName, string groupName)
        {
            try
            {
                RepetierApiRequestRespone result =
                    await SendRestApiRequestAsync(
                        RepetierCommandBase.printer, RepetierCommandFeature.api,
                        command: "delModelGroup", jsonData: string.Format("{{\"groupName\":\"{0}\"}}", groupName),
                        printerName: printerName)
                    .ConfigureAwait(false);
                return GetQueryResult(result.Result);
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return false;
            }
        }

        public async Task<bool> MoveModelToGroupAsync(string groupName, long id)
        {
            string currentPrinter = GetActivePrinterSlug();
            if (string.IsNullOrEmpty(currentPrinter)) return false;

            try
            {
                RepetierApiRequestRespone result =
                    await SendRestApiRequestAsync(
                        RepetierCommandBase.printer, RepetierCommandFeature.api,
                        command: "moveModelFileToGroup", jsonData: string.Format("{{\"groupName\":\"{0}\", \"id\":{1}}}", groupName, id),
                        printerName: currentPrinter
                        )
                    .ConfigureAwait(false);
                return GetQueryResult(result.Result);
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return false;
            }
        }
        public async Task<bool> MoveModelToGroupAsync(string printerName, string groupName, long id)
        {
            try
            {
                RepetierApiRequestRespone result =
                    await SendRestApiRequestAsync(
                        RepetierCommandBase.printer, RepetierCommandFeature.api,
                        command: "moveModelFileToGroup", jsonData: string.Format("{{\"groupName\":\"{0}\", \"id\":{1}}}", groupName, id),
                        printerName: printerName
                        )
                    .ConfigureAwait(false);
                return GetQueryResult(result.Result);
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return false;
            }
        }

        public async Task<ObservableCollection<IGcodeGroup>> GetModelGroupsAsync()
        {
            RepetierApiRequestRespone result = new();
            ObservableCollection<IGcodeGroup> resultObject = new();

            string currentPrinter = GetActivePrinterSlug();
            if (string.IsNullOrEmpty(currentPrinter)) return resultObject;

            try
            {
                result = await SendRestApiRequestAsync(
                   commandBase: RepetierCommandBase.printer,
                   commandFeature: RepetierCommandFeature.api, 
                   command: "listModelGroups", 
                   printerName: currentPrinter)
                    .ConfigureAwait(false);

                RepetierModelGroups info = GetObjectFromJson<RepetierModelGroups>(result.Result);
                return info != null && info.GroupNames != null ? new ObservableCollection<IGcodeGroup>(info.GroupNames.Select(g => new RepetierModelGroup() { Name = g})) : resultObject;
            }
            catch (JsonException jecx)
            {
                OnError(new RepetierJsonConvertEventArgs()
                {
                    Exception = jecx,
                    OriginalString = result.Result,
                    TargetType = nameof(String),
                    Message = jecx.Message,
                });
                return new ObservableCollection<IGcodeGroup>();
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
                ObservableCollection<IGcodeGroup> groups = new();
                if (!IsReady || ActivePrinter == null)
                {
                    Groups = groups;
                    return;
                }

                string currentPrinter = ActivePrinter.Slug;
                if (string.IsNullOrEmpty(currentPrinter)) return;

                RepetierModelGroups result = await GetModelGroupsAsync(currentPrinter).ConfigureAwait(false);
                if (result != null)
                {
                    Groups = new ObservableCollection<IGcodeGroup>(result.GroupNames?.Select(g => new RepetierModelGroup() { Name = g }));
                }
                else Groups = groups;

            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                Groups = new ObservableCollection<IGcodeGroup>();
            }
        }
        #endregion

        #region Jobs
        //public async Task<ObservableCollection<RepetierJobListItem>> GetJobListAsync()
        public async Task<ObservableCollection<IPrint3dJob>> GetJobListAsync()
        {
            ObservableCollection<IPrint3dJob> resultObject = new();

            string currentPrinter = GetActivePrinterSlug();
            if (string.IsNullOrEmpty(currentPrinter)) return resultObject;

            try
            {
                RepetierJobListRespone info = await GetJobListResponeAsync(currentPrinter).ConfigureAwait(false);
                if (info != null && info.Data != null)
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
                ObservableCollection<IPrint3dJob> jobList = new();
                if (!IsReady || ActivePrinter == null)
                {
                    Jobs = jobList;
                    return;
                }

                string currentPrinter = ActivePrinter.Slug;
                if (string.IsNullOrEmpty(currentPrinter)) return;

                RepetierJobListRespone result = await GetJobListResponeAsync(currentPrinter).ConfigureAwait(false);
                Jobs = result != null ? new(result.Data) : jobList;

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

                RepetierApiRequestRespone result =
                    await SendRestApiRequestAsync(
                        RepetierCommandBase.printer, RepetierCommandFeature.api,
                        command: "startJob", jsonData: string.Format("{{\"id\":{0}}}", id),
                        printerName: currentPrinter)
                    .ConfigureAwait(false);
                return GetQueryResult(result.Result, true);
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

                RepetierApiRequestRespone result =
                    await SendRestApiRequestAsync(
                        RepetierCommandBase.printer, RepetierCommandFeature.api,
                        command: "startJob", jsonData: string.Format("{{\"id\":{0}}}", id),
                        printerName: currentPrinter)
                    .ConfigureAwait(false);
                return GetQueryResult(result.Result, true);
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

                RepetierApiRequestRespone result =
                    await SendRestApiRequestAsync(
                        RepetierCommandBase.printer, RepetierCommandFeature.api,
                        command: "removeJob", jsonData: string.Format("{{\"id\":{0}}}", jobId),
                        printerName: currentPrinter)
                    .ConfigureAwait(false);
                await RefreshJobListAsync().ConfigureAwait(false);
                return GetQueryResult(result.Result);
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

                RepetierApiRequestRespone result =
                    await SendRestApiRequestAsync(
                        RepetierCommandBase.printer, RepetierCommandFeature.api,
                        command: "removeJob", jsonData: string.Format("{{\"id\":{0}}}", jobId),
                        printerName: currentPrinter)
                    .ConfigureAwait(false);
                await RefreshJobListAsync().ConfigureAwait(false);
                return GetQueryResult(result.Result);
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return false;
            }
        }
        public Task<bool> RemoveJobAsync(RepetierCurrentPrintInfo job) => RemoveJobAsync(job.Jobid);

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

                RepetierApiRequestRespone result =
                    await SendRestApiRequestAsync(RepetierCommandBase.printer, RepetierCommandFeature.api, command: "continueJob", printerName: currentPrinter)
                    .ConfigureAwait(false);
                return GetQueryResult(result.Result, true);
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
                bool result = await SendGcodeCommandAsync("@pause", printerName).ConfigureAwait(false);
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

                RepetierApiRequestRespone result =
                    await SendRestApiRequestAsync(
                        RepetierCommandBase.printer, RepetierCommandFeature.api,
                        command: "stopJob", printerName: currentPrinter)
                    .ConfigureAwait(false);

                await RefreshJobListAsync().ConfigureAwait(false);
                return GetQueryResult(result.Result, true);
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

                RepetierApiRequestRespone result =
                    await SendRestApiRequestAsync(
                        RepetierCommandBase.printer, RepetierCommandFeature.api,
                        command: "setShutdownAfterPrint", jsonData: $"{{\"shutdown\":{(shutdown ? "true" : "false")}}}",
                        printerName: currentPrinter)
                    .ConfigureAwait(false);
                return GetQueryResult(result.Result, true);
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return false;
            }
        }
        #endregion

        #region PrinterState
        [Obsolete("Switch to `GetStatesAsync`instead.")]
        internal async Task<RepetierPrinterStateRespone> GetStateObjectAsync(string printerName = "")
        {
            RepetierApiRequestRespone result;
            string resultString = string.Empty;
            RepetierPrinterStateRespone resultObject = null;

            string currentPrinter = string.IsNullOrEmpty(printerName) ? GetActivePrinterSlug() : printerName;
            if (string.IsNullOrEmpty(currentPrinter))
            {
                return resultObject;
            }

            try
            {
                if (!IsReady)
                {
                    return new RepetierPrinterStateRespone();
                }

                result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api,
                    command: "stateList", printerName: currentPrinter)
                    .ConfigureAwait(false);

                currentPrinter = currentPrinter.Replace(" ", "_");
                resultString = result.Result.Replace(currentPrinter, "Printer");

                RepetierPrinterStateRespone state = GetObjectFromJson<RepetierPrinterStateRespone>(result.Result);
                if (state != null && IsPrinterSlugSelected(currentPrinter))
                {
                    State = state.Printer;
                }
                return state;
            }
            catch (JsonException jecx)
            {
                OnError(new RepetierJsonConvertEventArgs()
                {
                    Exception = jecx,
                    OriginalString = resultString,
                    Message = jecx.Message,
                });
                return new RepetierPrinterStateRespone();
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return new RepetierPrinterStateRespone();
            }
        }
        public async Task<Dictionary<string, RepetierPrinterState>> GetStatesAsync(string printerName = "")
        {
            RepetierApiRequestRespone result;
            string resultString = string.Empty;
            Dictionary<string, RepetierPrinterState> resultObject = new();

            string currentPrinter = string.IsNullOrEmpty(printerName) ? GetActivePrinterSlug() : printerName;
            if (string.IsNullOrEmpty(currentPrinter))
            {
                return resultObject;
            }

            try
            {
                if (!IsReady)
                {
                    return new Dictionary<string, RepetierPrinterState>();
                }

                result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api,
                    command: "stateList", printerName: currentPrinter)
                    .ConfigureAwait(false);

                //currentPrinter = currentPrinter.Replace(" ", "_");
                //resultString = result.Result.Replace(currentPrinter, "Printer");

                Dictionary<string, RepetierPrinterState> state = GetObjectFromJson<Dictionary<string, RepetierPrinterState>>(result.Result);
                if (state != null && IsPrinterSlugSelected(currentPrinter))
                {
                    //State = state.Printer;
                    //State = state.FirstOrDefault().Value;
                    State = state.FirstOrDefault(keypair => keypair.Key == ActivePrinter?.Slug).Value ?? state.FirstOrDefault().Value;
                }
                return state;
            }
            catch (JsonException jecx)
            {
                OnError(new RepetierJsonConvertEventArgs()
                {
                    Exception = jecx,
                    OriginalString = resultString,
                    Message = jecx.Message,
                });
                return new Dictionary<string, RepetierPrinterState>();
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return new Dictionary<string, RepetierPrinterState>();
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
                Dictionary<string, RepetierPrinterState> result = await GetStatesAsync().ConfigureAwait(false);              
                if (result != null && result?.Count > 0)
                {
                    State = result.FirstOrDefault(keypair => keypair.Key == ActivePrinter?.Slug).Value ?? result.FirstOrDefault().Value;
                }
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
            }
        }

        public async Task<RepetierPrinterState> GetStateForPrinterAsync(string printerName)
        {
            Dictionary<string, RepetierPrinterState> states = await GetStatesAsync().ConfigureAwait(false);
            return states?.FirstOrDefault(keypair => keypair.Key == printerName).Value;
        }
        [Obsolete("Use `GetStateForPrinterAsync` instead")]
        public async Task<RepetierPrinterStateRespone> GetStateObjectForPrinterAsync(string printerName)
        {
            return await GetStateObjectAsync(printerName).ConfigureAwait(false);
        }


        #endregion

        #region Printers
        public async Task<ObservableCollection<IPrinter3d>> GetPrintersAsync()
        {
            RepetierApiRequestRespone result = new();
            try
            {
                ObservableCollection<IPrinter3d> repetierPrinterList = new();
                if (!IsReady)
                    return repetierPrinterList;

                result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer,
                    RepetierCommandFeature.list)
                    .ConfigureAwait(false);

                RepetierPrinterListRespone respone = GetObjectFromJson<RepetierPrinterListRespone>(result.Result);
                if (respone != null)
                {                   
                    repetierPrinterList = new ObservableCollection<IPrinter3d>(respone.Printers);
                    foreach (RepetierPrinter printer in repetierPrinterList.Cast<RepetierPrinter>())
                    {
                        if (printer?.JobId > 0)
                        {
                            IPrinter3d prevPrinter = Printers?.FirstOrDefault(p => p.Slug == printer.Slug);
                            if (prevPrinter is null) continue;
                            // Avoid unnecessary calls if the image or the job hasn't changed
                            if (prevPrinter?.ActiveJobId != printer?.ActiveJobId || prevPrinter?.CurrentPrintImage?.Length <= 0)
                            {
                                printer.CurrentPrintImage = await GetDynamicRenderImageByJobIdAsync(printer.JobId, false).ConfigureAwait(false);
                            }
                            else
                            {
                                printer.CurrentPrintImage = prevPrinter.CurrentPrintImage;
                            }
                        }
                        else printer.CurrentPrintImage = Array.Empty<byte>();
                    }
                    Printers = repetierPrinterList;
                }

                return repetierPrinterList;
            }
            catch (JsonException jecx)
            {
                OnError(new RepetierJsonConvertEventArgs()
                {
                    Exception = jecx,
                    OriginalString = result.Result,
                    TargetType = nameof(RepetierPrinter),
                    Message = jecx.Message,
                });
                return new ObservableCollection<IPrinter3d>();
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return new ObservableCollection<IPrinter3d>();
            }
        }
        public async Task RefreshPrinterListAsync()
        {
            try
            {
                ObservableCollection<IPrinter3d> printers = new();
                if (!IsReady)
                {
                    Printers = printers;
                    return;
                }

                ObservableCollection<IPrinter3d> result = await GetPrintersAsync().ConfigureAwait(false);
                if (result != null)
                {
                    Printers = result;
                }
                else
                {
                    Printers = printers;
                }
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                Printers = new ObservableCollection<IPrinter3d>();
            }
        }
        #endregion

        #region CurrentPrintInfo
        public async Task<RepetierCurrentPrintInfo> GetCurrentPrintInfoAsync()
        {
            RepetierCurrentPrintInfo resultObject = null;

            string currentPrinter = GetActivePrinterSlug();
            if (string.IsNullOrEmpty(currentPrinter)) return resultObject;

            try
            {
                ObservableCollection<RepetierCurrentPrintInfo> listResult = await GetCurrentPrintInfosAsync().ConfigureAwait(false);
                if (listResult != null)
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

            RepetierApiRequestRespone result = new();
            try
            {
                result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api, command: "listPrinter", printerName: currentPrinter)
                    .ConfigureAwait(false);

                RepetierCurrentPrintInfo[] info = GetObjectFromJson<RepetierCurrentPrintInfo[]>(result.Result);
                if (info != null)
                {
                    resultObject = new ObservableCollection<RepetierCurrentPrintInfo>(info);
                }
                return resultObject;
            }
            catch (JsonException jecx)
            {
                OnError(new RepetierJsonConvertEventArgs()
                {
                    Exception = jecx,
                    OriginalString = result.Result,
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
                var result = await GetCurrentPrintInfosAsync().ConfigureAwait(false);
                ActivePrintInfos = result ?? new ObservableCollection<RepetierCurrentPrintInfo>();
                ActivePrintInfo = ActivePrintInfos.FirstOrDefault(info => info.Slug == GetActivePrinterSlug());
                CurrentPrintImage = ActivePrintInfo?.Jobid > 0
                    ? await GetDynamicRenderImageByJobIdAsync(ActivePrintInfo.Jobid, false).ConfigureAwait(false)
                    : Array.Empty<byte>();
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                ActivePrintInfos = new ObservableCollection<RepetierCurrentPrintInfo>();
            }

        }

        public async Task<RepetierCurrentPrintInfo> GetCurrentPrintInfoForPrinterAsync(string printerName)
        {
            RepetierCurrentPrintInfo resultObject = null;

            try
            {
                ObservableCollection<RepetierCurrentPrintInfo> listResult = await GetCurrentPrintInfosAsync().ConfigureAwait(false);
                if (listResult != null)
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

        #region PrinterConfiguration
        public async Task<RepetierPrinterConfig> GetPrinterConfigAsync(string printerName = "")
        {
            RepetierPrinterConfig resultObject = null;

            string currentPrinter = string.IsNullOrEmpty(printerName) ? GetActivePrinterSlug() : printerName;
            if (string.IsNullOrEmpty(currentPrinter)) return resultObject;

            RepetierApiRequestRespone result = new();
            try
            {
                result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api,
                    command: "getPrinterConfig", jsonData: string.Format("{{\"printer\": \"{0}\"}}", currentPrinter),
                    printerName: currentPrinter)
                    .ConfigureAwait(false);

                RepetierPrinterConfig config = GetObjectFromJson<RepetierPrinterConfig>(result.Result);
                if (config != null)
                {
                    Config = resultObject = config;
                }
                return resultObject;
            }
            catch (JsonException jecx)
            {
                OnError(new RepetierJsonConvertEventArgs()
                {
                    Exception = jecx,
                    OriginalString = result.Result,
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
                RepetierPrinterConfig result = await GetPrinterConfigAsync().ConfigureAwait(false);
                if (result != null)
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

            RepetierApiRequestRespone result;
            try
            {
                result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api,
                    command: "setPrinterConfig", jsonData: newConfig,
                    printerName: currentPrinter
                    ).ConfigureAwait(false);
                return GetQueryResult(result?.Result, true);
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return false;
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
                _ = await SendGcodeCommandAsync(cmd).ConfigureAwait(false);

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
                    result = await SendGcodeCommandAsync("G28").ConfigureAwait(false);
                }
                else
                {
                    string cmd = string.Format("G28{0}{1}{2}", x ? " X0 " : "", y ? " Y0 " : "", z ? " Z0 " : "");
                    result = await SendGcodeCommandAsync(cmd).ConfigureAwait(false);
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

                var shape = Config.Movement;
                var newX = MathHelper.Clamp(relative ? State.X + x : x, shape.XMin, shape.XMax);
                var newY = MathHelper.Clamp(relative ? State.Y + y : y, shape.YMin, shape.YMax);
                var newZ = MathHelper.Clamp(relative ? State.Z + z : z, shape.ZMin, shape.ZMax);

                string data = $"{{\"speed\":{speed}" +
                    string.Format(",\"relative\":{0}", relative ? "true" : "false") +
                    (double.IsInfinity(x) ? "" : $",\"x\":{newX}") +
                    (double.IsInfinity(y) ? "" : $",\"y\":{newY}") +
                    (double.IsInfinity(z) ? "" : $",\"z\":{newZ}") +
                    (double.IsInfinity(e) ? "" : $",\"e\":{e}") +
                    $"}}";

                RepetierApiRequestRespone result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api,
                    command: "move", jsonData: data,
                    printerName: currentPrinter
                    ).ConfigureAwait(false);
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

                RepetierApiRequestRespone result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api,
                    command: "setExtruderTemperature",
                    jsonData: string.Format("{{\"temperature\":{0}, \"extruder\":{1}}}", temperature, extruder),
                    printerName: currentPrinter
                    ).ConfigureAwait(false);
                return GetQueryResult(result.Result, true);
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

                RepetierApiRequestRespone result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api,
                    command: "setBedTemperature",
                    jsonData: string.Format("{{\"temperature\":{0}, \"bedId\":{1}}}", temperature, bedId),
                    printerName: currentPrinter
                    ).ConfigureAwait(false);
                return GetQueryResult(result.Result, true);
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

                RepetierApiRequestRespone result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api,
                    command: "setChamberTemperature",
                    jsonData: string.Format("{{\"temperature\":{0}, \"chamberId\":{1}}}", temperature, chamberId),
                    printerName: currentPrinter
                    ).ConfigureAwait(false);
                return GetQueryResult(result.Result, true);
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

                RepetierApiRequestRespone result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api,
                    command: "setFanSpeed",
                    jsonData: string.Format("{{\"speed\":{0}, \"fanid\":{1}}}", SetSpeed, fanId),
                    printerName: currentPrinter
                    ).ConfigureAwait(false);
                return GetQueryResult(result.Result);
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
            RepetierApiRequestRespone result = new();
            ObservableCollection<ExternalCommand> resultObject = new();

            try
            {
                result = await SendRestApiRequestAsync(
                   commandBase: RepetierCommandBase.printer,
                   commandFeature: RepetierCommandFeature.api,
                   command: "listExternalCommands")
                    .ConfigureAwait(false);

                ExternalCommand[] cmds = GetObjectFromJson<ExternalCommand[]>(result.Result);
                return new ObservableCollection<ExternalCommand>(cmds ?? new ExternalCommand[] { new ExternalCommand() });
            }
            catch (JsonException jecx)
            {
                OnError(new RepetierJsonConvertEventArgs()
                {
                    Exception = jecx,
                    OriginalString = result.Result,
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
                RepetierApiRequestRespone result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api,
                    command: "runExternalCommand", 
                    jsonData: string.Format("{{\"id\":{0}}}", command.Id)
                    )
                    .ConfigureAwait(false);
                return GetQueryResult(result.Result);
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
                if (result != null)
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

                RepetierApiRequestRespone result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api,
                    command: "removeMessage",
                    jsonData: string.Format("{{\"id\":{0}, \"a\":\"{1}\"}}", message.Id, unPause ? "unpause" : ""),
                    printerName: currentPrinter
                    ).ConfigureAwait(false);
                return GetQueryResult(result.Result);
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return false;
            }
        }
        public async Task<ObservableCollection<RepetierMessage>> GetMessagesAsync(string printerName = "")
        {
            RepetierApiRequestRespone result = new();
            ObservableCollection<RepetierMessage> resultObject = new();

            string currentPrinter = string.IsNullOrEmpty(printerName) ? GetActivePrinterSlug() : printerName;
            if (string.IsNullOrEmpty(currentPrinter)) return resultObject;

            try
            {
                result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api,
                    command: "messages", printerName: currentPrinter)
                    .ConfigureAwait(false);
                RepetierMessage[] info = GetObjectFromJson<RepetierMessage[]>(result.Result);
                if (info != null)
                    resultObject = new ObservableCollection<RepetierMessage>(info);
                return resultObject;
            }
            catch (JsonException jecx)
            {
                OnError(new RepetierJsonConvertEventArgs()
                {
                    Exception = jecx,
                    OriginalString = result.Result,
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
                if (result != null)
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

                RepetierApiRequestRespone result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api,
                    command: "setFlowMultiply", jsonData: string.Format("{{\"speed\":{0}}}", multiplier),
                    printerName: currentPrinter
                    ).ConfigureAwait(false);
                return GetQueryResult(result.Result);
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

                RepetierApiRequestRespone result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api,
                    command: "setSpeedMultiply", jsonData: string.Format("{{\"speed\":{0}}}", speed),
                    printerName: currentPrinter
                    ).ConfigureAwait(false);
                return GetQueryResult(result.Result);
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

                RepetierApiRequestRespone result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api, command: "emergencyStop", printerName: currentPrinter)
                    .ConfigureAwait(false);
                return GetQueryResult(result.Result);
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return false;
            }
        }

        #endregion

        #region Gcode Commands
        public async Task<bool> SendGcodeCommandAsync(string command, string printerName = "")
        {
            try
            {
                string currentPrinter = GetActivePrinterSlug();
                if (!string.IsNullOrEmpty(printerName)) currentPrinter = printerName; // Override current selected printer, if needed
                if (string.IsNullOrEmpty(currentPrinter)) return false;

                //object cmd = new { cmd = command };
                RepetierApiRequestRespone result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api,
                    command: "send", jsonData: string.Format("{{\"cmd\":\"{0}\"}}", command),
                    printerName: currentPrinter
                    ).ConfigureAwait(false);
                return GetQueryResult(result.Result, true);
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return false;
            }
        }
        #endregion

        #region Gcode Scripts
        public async Task<RepetierGcodeScript> GetGcodeScriptAsync(string scriptName)
        {
            RepetierApiRequestRespone result = new();
            RepetierGcodeScript resultObject = null;
            try
            {
                string currentPrinter = GetActivePrinterSlug();
                if (string.IsNullOrEmpty(currentPrinter)) return resultObject;

                object cmd = new { name = scriptName };
                result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api,
                    command: "getScript", jsonData: cmd,
                    printerName: currentPrinter
                    ).ConfigureAwait(false);

                return GetObjectFromJson<RepetierGcodeScript>(result.Result);
            }
            catch (JsonException jecx)
            {
                OnError(new RepetierJsonConvertEventArgs()
                {
                    Exception = jecx,
                    OriginalString = result.Result,
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
        public async Task<bool> SetGcodeScriptAsync(RepetierGcodeScript script)
        {
            try
            {
                bool result = await SetGcodeScriptAsync(script.Name, script.Script).ConfigureAwait(false);
                return result;
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return false;
            }
        }
        public async Task<bool> SetGcodeScriptAsync(string scriptName, string script)
        {
            try
            {
                string currentPrinter = GetActivePrinterSlug();
                if (string.IsNullOrEmpty(currentPrinter)) return false;

                object cmd = new { name = scriptName, script = script };
                RepetierApiRequestRespone result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api,
                    command: "setScript", jsonData: cmd,
                    printerName: currentPrinter)
                    .ConfigureAwait(false);
                return GetQueryResult(result.Result);
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
                RepetierApiRequestRespone result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api,
                    command: "runScript", jsonData: cmd,
                    printerName: currentPrinter)
                    .ConfigureAwait(false);
                return GetQueryResult(result.Result);
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
            ObservableCollection<RepetierWebCallAction> resultObject = new();
            try
            {
                string currentPrinter = GetActivePrinterSlug();
                if (string.IsNullOrEmpty(currentPrinter)) return resultObject;

                RepetierWebCallList script = await GetWebCallListAsync(currentPrinter).ConfigureAwait(false);
                if (script != null && script.List != null)
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
                
                RepetierApiRequestRespone result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api,
                    command: "webCallExecute", jsonData: cmd, cts: cts,
                    printerName: currentPrinter
                    ).ConfigureAwait(false);
                return GetQueryResult(result.Result);
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

                RepetierApiRequestRespone result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api,
                    command: "webCallRemove", jsonData: cmd,
                    printerName: currentPrinter
                    ).ConfigureAwait(false);
                return GetQueryResult(result.Result);
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
        public async Task<byte[]> DownloadFileFromUriAsync(string path, int timeout = 10000, Dictionary<string, object> additionalParameters = null)
        {
            try
            {
                if (restClient == null)
                {
                    UpdateRestClientInstance();
                }
                RestRequest request = new(path);
                request.AddParameter("apikey", ApiKey);
                request.RequestFormat = DataFormat.Json;
                request.Method = Method.Get;
                request.Timeout = timeout;

                if(additionalParameters != null)
                {
                    foreach (KeyValuePair<string, object> parameter in additionalParameters)
                    {
                        request.AddParameter(parameter.Key, parameter.Value.ToString());
                    }
                }

                Uri fullUrl = restClient.BuildUri(request);

                // Workaround, because the RestClient returns bad requests
                return await DownloadFileFromUriAsync(fullUrl).ConfigureAwait(false);
                
                /*
                CancellationTokenSource cts = new(timeout);
                byte[] respone = await restClient.DownloadDataAsync(request, cts.Token)
                    .ConfigureAwait(false)
                    ;

                return respone;
                */
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return null;
            }
        }
        public async Task<byte[]> DownloadFileFromUriAsync(Uri uri)
        {
            try
            {
                // Workaround, because the RestClient returns bad requests
                using WebClient client = new();
                byte[] bytes = await client.DownloadDataTaskAsync(uri);
                return bytes;
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return null;
            }
        }

        public async Task<byte[]> GetProjectImageAsync(Guid server, Guid project, string preview, string action = "mthumb", int size = 1, int timeout = 10000)
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    { "v", size }
                };
                string path = $"project/{server}/{action}/{project}/{preview}/";
                return await DownloadFileFromUriAsync(path, timeout, parameters).ConfigureAwait(false);
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return new byte[0];
            }
        }
        public async Task<byte[]> GetProjectImageAsync(Guid Server, RepetierProjectItem projectItem, string action = "mthumb", int size = 1, int timeout = 10000)
        {
            try
            {
                return await GetProjectImageAsync(Server, projectItem.Project.Uuid, projectItem.Project.Preview, action, size, timeout)
                    .ConfigureAwait(false)
                    ;
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return new byte[0];
            }
        }

        public async Task<RepetierProjectsServerListRespone> GetProjectsListServerAsync(string printerName = "")
        {
            RepetierApiRequestRespone result = new();
            try
            {
                result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api, command: "projectsListServer", printerName: printerName)
                    .ConfigureAwait(false);

                return GetObjectFromJson<RepetierProjectsServerListRespone>(result.Result);
            }
            catch (JsonException jecx)
            {
                OnError(new RepetierJsonConvertEventArgs()
                {
                    Exception = jecx,
                    OriginalString = result.Result,
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

        public async Task<RepetierProjectsFolderRespone> GetProjectsGetFolderAsync(Guid serverUuid, int index = 1, string printerName = "")
        {
            RepetierApiRequestRespone result = new();
            try
            {
                object data = new
                {
                    serveruuid = serverUuid,
                    idx = index,
                };

                result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api,
                    command: "projectsGetFolder", jsonData: data,
                    printerName: printerName
                    ).ConfigureAwait(false);
                return GetObjectFromJson<RepetierProjectsFolderRespone>(result.Result);
            }
            catch (JsonException jecx)
            {
                OnError(new RepetierJsonConvertEventArgs()
                {
                    Exception = jecx,
                    OriginalString = result.Result,
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
            RepetierProjectsFolderRespone result;
            ObservableCollection<RepetierProjectItem> items = new();
            try
            {
                object data = new
                {
                    serveruuid = serverUuid,
                    idx = index,
                };

                result = await GetProjectsGetFolderAsync(serverUuid, index, printerName).ConfigureAwait(false);
                if (result != null && result.Folder != null)
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
                    byte[] emptyProject = await DownloadFileFromUriAsync("img/emptyproject.png").ConfigureAwait(false);
                    byte[] folder = await DownloadFileFromUriAsync("img/folder_m.png").ConfigureAwait(false);

                    foreach (RepetierProjectItem project in items)
                    {
                        if (!project.IsFolder && project.Project != null)
                        {
                            if (!string.IsNullOrEmpty(project.Project.Preview))
                                // Load image from server
                                project.PreviewImage = await GetProjectImageAsync(serverUuid, project).ConfigureAwait(false);
                            else
                                // Static image from the server
                                project.PreviewImage = emptyProject;
                        }
                        else if (project.Folder != null)
                        {
                            // Static image from the server
                            project.PreviewImage = folder;
                        }

                    }

                    return items;
                }
                else
                {
                    return new ObservableCollection<RepetierProjectItem>();
                }
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return new ObservableCollection<RepetierProjectItem>();
            }
        }
        
        public async Task<RepetierProjectsProjectRespone> GetProjectsGetProjectAsync(Guid serverUuid, Guid projectUuid, string printerName = "")
        {
            RepetierApiRequestRespone result = new();
            try
            {
                object data = new
                {
                    serveruuid = serverUuid,
                    uuid = projectUuid,
                };

                result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api,
                    command: "projectsGetProject", jsonData: data,
                    printerName: printerName
                    ).ConfigureAwait(false);
                return GetObjectFromJson<RepetierProjectsProjectRespone>(result.Result);
            }
            catch (JsonException jecx)
            {
                OnError(new RepetierJsonConvertEventArgs()
                {
                    Exception = jecx,
                    OriginalString = result.Result,
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

                RepetierApiRequestRespone result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api,
                    command: "projectsUpdateProject", jsonData: data,
                    printerName: printerName
                    ).ConfigureAwait(false);
                return GetQueryResult(result.Result);
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return false;
            }
        }

        public Uri GetProjectFileUri(Guid serverUuid, RepetierProjectFile file, string action = "view")
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

                RepetierApiRequestRespone result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api,
                    command: "projectsDeleteFile", jsonData: data,
                    printerName: printerName
                    ).ConfigureAwait(false);
                return GetQueryResult(result.Result);
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

                RepetierApiRequestRespone result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api,
                    command: "projectDelComment", jsonData: data,
                    printerName: printerName
                    ).ConfigureAwait(false);
                return GetQueryResult(result.Result);
            }

            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return false;
            }
        }

        #endregion

        #region History
        async Task<RepetierHistoryListRespone> GetHistoryListResponeAsync(
            string printerNameForHistory, string serverUuid = "", int limit = 50, int page = 0, int start = 0, bool allPrinter = false)
        {
            RepetierApiRequestRespone result = new();
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

                //string dataString = $"{{\"allPrinter\":\"{(AllPrinter ? "true" : "false")}\",\"limit\":{Limit},\"page\":{Page},\"slug\":\"{PrinterNameForHistory}\",\"start\":{Start},\"uuid\":\"{ServerUuid}\"}}";
                result = await SendRestApiRequestAsync(
                   commandBase: RepetierCommandBase.printer,
                   commandFeature: RepetierCommandFeature.api,
                   jsonData: data,
                   command: "historyList",
                   printerName: currentPrinter
                   )
                    .ConfigureAwait(false);

                return GetObjectFromJson<RepetierHistoryListRespone>(result.Result);
            }
            catch (JsonException jecx)
            {
                OnError(new RepetierJsonConvertEventArgs()
                {
                    Exception = jecx,
                    OriginalString = result.Result,
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

                RepetierHistoryListRespone historyList = 
                    await GetHistoryListResponeAsync(printerNameForHistory, serverUuid, limit, page, start, allPrinter)
                    .ConfigureAwait(false);
                return historyList != null
                    ? new ObservableCollection<RepetierHistoryListItem>(historyList.List)
                    : new ObservableCollection<RepetierHistoryListItem>();
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return new ObservableCollection<RepetierHistoryListItem>();
            }
        }

        async Task<RepetierHistorySummaryRespone> GetHistorySummaryAsync(string printerNameForHistory, int year, bool allPrinter = false)
        {
            RepetierApiRequestRespone result = new();
            try
            {
                string currentPrinter = GetActivePrinterSlug();

                object data = new
                {
                    allPrinter = allPrinter,
                    slug = printerNameForHistory,
                    year = year,
                };

                result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api,
                    command: "historySummary", jsonData: data,
                    printerName: currentPrinter
                    ).ConfigureAwait(false);
                return GetObjectFromJson<RepetierHistorySummaryRespone>(result.Result);
            }
            catch (JsonException jecx)
            {
                OnError(new RepetierJsonConvertEventArgs()
                {
                    Exception = jecx,
                    OriginalString = result.Result,
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
        public async Task<byte[]> GetHistoryReportAsync(long reportId, string printerName = "")
        {
            try
            {
                if (string.IsNullOrEmpty(printerName))
                    printerName = GetActivePrinterSlug();
                byte[] report = await DownloadFileFromUriAsync($"{FullWebAddress}/printer/export/{printerName}?a=history_report&id={reportId}&apikey={ApiKey}")
                    .ConfigureAwait(false)
                    ;
                return report;
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return new byte[0];
            }
        }
        public async Task<ObservableCollection<RepetierHistorySummaryItem>> GetHistorySummaryItemsAsync(string printerNameForHistory, int year, bool allPrinter = false)
        {
            try
            {
                RepetierHistorySummaryRespone list = await GetHistorySummaryAsync(printerNameForHistory, year, allPrinter).ConfigureAwait(false);
                return list != null && list.Summaries.Count > 0
                    ? new ObservableCollection<RepetierHistorySummaryItem>(list.Summaries)
                    : new ObservableCollection<RepetierHistorySummaryItem>();
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return new ObservableCollection<RepetierHistorySummaryItem>();
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

                RepetierApiRequestRespone result = await SendRestApiRequestAsync(
                    RepetierCommandBase.printer, RepetierCommandFeature.api,
                    command: "historyDeleteEntry", jsonData: data,
                    printerName: printerName
                    ).ConfigureAwait(false);
                return GetQueryResult(result.Result);
            }

            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return false;
            }
        }
#endregion

        #region GPIO 
        async Task<RepetierGpioListRespone> GetGPIOListResponeAsync()
        {
            RepetierApiRequestRespone result = new();
            try
            {
                string currentPrinter = GetActivePrinterSlug();

                result = await SendRestApiRequestAsync(
                   commandBase: RepetierCommandBase.printer,
                   commandFeature: RepetierCommandFeature.api,
                   command: "GPIOGetList",
                   printerName: currentPrinter)
                    .ConfigureAwait(false);

                return GetObjectFromJson<RepetierGpioListRespone>(result.Result);
            }
            catch (JsonException jecx)
            {
                OnError(new RepetierJsonConvertEventArgs()
                {
                    Exception = jecx,
                    OriginalString = result.Result,
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
                RepetierGpioListRespone list = await GetGPIOListResponeAsync().ConfigureAwait(false);
                return list != null ? new ObservableCollection<RepetierGpioListItem>(list.List) : new ObservableCollection<RepetierGpioListItem>();
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return new ObservableCollection<RepetierGpioListItem>();
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
        public override bool Equals(object obj)
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
                StopListening();
                DisconnectWebSocket();
            }
        }
        #endregion

        #region Clone

        public object Clone()
        {
            return MemberwiseClone();
        }
        #endregion
    }
}

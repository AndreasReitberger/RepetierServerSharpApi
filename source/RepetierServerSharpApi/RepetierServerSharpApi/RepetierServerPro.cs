using AndreasReitberger.Core.Utilities;
using AndreasReitberger.Enum;
using AndreasReitberger.Interfaces;
using AndreasReitberger.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
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

namespace AndreasReitberger
{
    public class RepetierServerPro : IPrintServerClient
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Variables
        static HttpClient client = new();
        int _retries = 0;
        #endregion

        #region Id
        [JsonProperty(nameof(Id))]
        Guid _id = Guid.Empty;
        [JsonIgnore]
        public Guid Id
        {
            get => _id;
            set
            {
                if (_id == value) return;
                _id = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Instance
        static RepetierServerPro _instance = null;
        static readonly object Lock = new();
        public static RepetierServerPro Instance
        {
            get
            {
                lock (Lock)
                {
                    if (_instance == null)
                        _instance = new RepetierServerPro();
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

        bool _isActive = false;
        public bool IsActive
        {
            get => _isActive;
            set
            {
                if (_isActive == value)
                    return;
                _isActive = value;
                OnPropertyChanged();
            }
        }

        bool _updateInstance = false;
        public bool UpdateInstance
        {
            get => _updateInstance;
            set
            {
                if (_updateInstance == value)
                    return;
                _updateInstance = value;
                // Update the instance to the latest settings
                if (_updateInstance)
                    InitInstance(this.ServerAddress, this.Port, this.API, this.IsSecure);

                OnPropertyChanged();
            }
        }

        bool _isInitialized = false;
        public bool IsInitialized
        {
            get => _isInitialized;
            set
            {
                if (_isInitialized == value)
                    return;
                _isInitialized = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region RefreshTimer
        [JsonIgnore]
        [XmlIgnore]
        Timer _timer;
        [JsonIgnore]
        [XmlIgnore]
        public Timer Timer
        {
            get => _timer;
            set
            {
                if (_timer == value) return;
                _timer = value;
                OnPropertyChanged();
            }
        }

        [JsonProperty(nameof(RefreshInterval))]
        int _refreshInterval = 3;
        [JsonIgnore]
        public int RefreshInterval
        {
            get => _refreshInterval;
            set
            {
                if (_refreshInterval == value) return;
                _refreshInterval = value;
                if (IsListening)
                {
                    StopListening();
                    StartListening();
                }
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [XmlIgnore]
        bool _isListening = false;
        [JsonIgnore]
        [XmlIgnore]
        public bool IsListening
        {
            get => _isListening;
            set
            {
                if (_isListening == value) return;
                _isListening = value;
                OnListeningChanged(new RepetierEventListeningChangedEventArgs()
                {
                    SessonId = SessionId,
                    IsListening = value,
                    IsListeningToWebSocket = IsListeningToWebsocket,
                });
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [XmlIgnore]
        bool _initialDataFetched = false;
        [JsonIgnore]
        [XmlIgnore]
        public bool InitialDataFetched
        {
            get => _initialDataFetched;
            set
            {
                if (_initialDataFetched == value) return;
                _initialDataFetched = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Properties

        #region Connection

        [JsonIgnore]
        [XmlIgnore]
        EventSession _session;
        [JsonIgnore]
        [XmlIgnore]
        public EventSession Session
        {
            get => _session;
            set
            {
                if (_session == value) return;
                _session = value;
                if (_session != null)
                {
                    SessionId = Session.Session;
                    OnSessionChanged(new RepetierEventSessionChangedEventArgs()
                    {
                        CallbackId = Session.CallbackId,
                        Sesson = value,
                        SessonId = value.Session
                    });
                }
                OnPropertyChanged();

            }
        }

        [JsonIgnore]
        [XmlIgnore]
        HttpMessageHandler _httpHandler;
        /*
        HttpMessageHandler _httpHandler = new HttpClientHandler()
        {
            UseProxy = false,
            UseDefaultCredentials = true,
            AllowAutoRedirect = true,
        };
        */
        [JsonIgnore]
        [XmlIgnore]
        public HttpMessageHandler HttpHandler
        {
            get => _httpHandler;
            set
            {
                if (_httpHandler == value) return;
                _httpHandler = value;
                UpdateWebClientInstance();
                OnPropertyChanged();

            }
        }

        [JsonIgnore]
        [XmlIgnore]
        string _sessionId = string.Empty;
        [JsonIgnore]
        [XmlIgnore]
        public string SessionId
        {
            get => _sessionId;
            set
            {
                if (_sessionId == value) return;
                _sessionId = value;
                OnPropertyChanged();
            }
        }

        [JsonProperty(nameof(ServerAddress))]
        string _address = string.Empty;
        [JsonIgnore]
        public string ServerAddress
        {
            get => _address;
            set
            {
                if (_address == value) return;
                _address = value;
                OnPropertyChanged();
            }
        }

        [JsonProperty(nameof(IsSecure))]
        bool _isSecure = false;
        [JsonIgnore]
        public bool IsSecure
        {
            get => _isSecure;
            set
            {
                if (_isSecure == value) return;
                _isSecure = value;
                OnPropertyChanged();
            }
        }

        [JsonProperty(nameof(API))]
        string _api = string.Empty;
        [JsonIgnore]
        public string API
        {
            get => _api;
            set
            {
                if (_api == value) return;
                _api = value;
                OnPropertyChanged();
            }
        }

        [JsonProperty(nameof(Port))]
        int _port = 3344;
        [JsonIgnore]
        public int Port
        {
            get => _port;
            set
            {
                if (_port != value)
                {
                    _port = value;
                    OnPropertyChanged();
                }
            }
        }
        /*
        [JsonProperty(nameof(Proxy))]
        WebProxy _proxy;
        [JsonIgnore]
        public WebProxy Proxy
        {
            get => _proxy;
            set
            {
                if (_proxy == value) return;             
                _proxy = value;
                OnPropertyChanged();                
            }
        }
        */

        [JsonProperty(nameof(OverrideValidationRules))]
        [XmlAttribute(nameof(OverrideValidationRules))]
        bool _overrideValidationRules = false;
        [JsonIgnore]
        [XmlIgnore]
        public bool OverrideValidationRules
        {
            get => _overrideValidationRules;
            set
            {
                if (_overrideValidationRules == value)
                    return;
                _overrideValidationRules = value;
                OnPropertyChanged();
            }
        }

        [JsonProperty(nameof(IsOnline))]
        [XmlAttribute(nameof(IsOnline))]
        bool _isOnline = false;
        [JsonIgnore]
        [XmlIgnore]
        public bool IsOnline
        {
            get => _isOnline;
            set
            {
                if (_isOnline == value) return;
                _isOnline = value;
                // Notify subscribres 
                if (IsOnline)
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
                OnPropertyChanged();
            }
        }

        [JsonProperty(nameof(IsConnecting))]
        [XmlAttribute(nameof(IsConnecting))]
        bool _isConnecting = false;
        [JsonIgnore]
        [XmlIgnore]
        public bool IsConnecting
        {
            get => _isConnecting;
            set
            {
                if (_isConnecting == value) return;
                _isConnecting = value;
                OnPropertyChanged();
            }
        }

        [JsonProperty(nameof(AuthenticationFailed))]
        [XmlAttribute(nameof(AuthenticationFailed))]
        bool _authenticationFailed = false;
        [JsonIgnore]
        [XmlIgnore]
        public bool AuthenticationFailed
        {
            get => _authenticationFailed;
            set
            {
                if (_authenticationFailed == value) return;
                _authenticationFailed = value;
                OnPropertyChanged();
            }
        }

        [JsonProperty(nameof(IsRefreshing))]
        [XmlAttribute(nameof(IsRefreshing))]
        bool _isRefreshing = false;
        [JsonIgnore]
        [XmlIgnore]
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set
            {
                if (_isRefreshing == value) return;
                _isRefreshing = value;
                OnPropertyChanged();
            }
        }

        [JsonProperty(nameof(RetriesWhenOffline))]
        [XmlAttribute(nameof(RetriesWhenOffline))]
        int _retriesWhenOffline = 2;
        [JsonIgnore]
        [XmlIgnore]
        public int RetriesWhenOffline
        {
            get => _retriesWhenOffline;
            set
            {
                if (_retriesWhenOffline == value) return;
                _retriesWhenOffline = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region General
        [JsonIgnore]
        [XmlIgnore]
        bool _updateAvailable = false;
        [JsonIgnore]
        [XmlIgnore]
        public bool UpdateAvailable
        {
            get => _updateAvailable;
            private set
            {
                if (_updateAvailable == value) return;
                _updateAvailable = value;
                if (_updateAvailable)
                    // Notify on update available
                    OnServerUpdateAvailable(new RepetierEventArgs()
                    {
                        SessonId = this.SessionId,
                    });
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [XmlIgnore]
        RepetierAvailableUpdateInfo _update;
        [JsonIgnore]
        [XmlIgnore]
        public RepetierAvailableUpdateInfo Update
        {
            get => _update;
            private set
            {
                if (_update == value) return;
                _update = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Proxy
        [JsonProperty(nameof(EnableProxy))]
        [XmlAttribute(nameof(EnableProxy))]
        bool _enableProxy = false;
        [JsonIgnore]
        [XmlIgnore]
        public bool EnableProxy
        {
            get => _enableProxy;
            set
            {
                if (_enableProxy == value) return;
                _enableProxy = value;
                OnPropertyChanged();
                UpdateWebClientInstance();
            }
        }

        [JsonProperty(nameof(ProxyUseDefaultCredentials))]
        [XmlAttribute(nameof(ProxyUseDefaultCredentials))]
        bool _proxyUseDefaultCredentials = true;
        [JsonIgnore]
        [XmlIgnore]
        public bool ProxyUseDefaultCredentials
        {
            get => _proxyUseDefaultCredentials;
            set
            {
                if (_proxyUseDefaultCredentials == value) return;
                _proxyUseDefaultCredentials = value;
                OnPropertyChanged();
                UpdateWebClientInstance();
            }
        }

        [JsonProperty(nameof(SecureProxyConnection))]
        [XmlAttribute(nameof(SecureProxyConnection))]
        bool _secureProxyConnection = true;
        [JsonIgnore]
        [XmlIgnore]
        public bool SecureProxyConnection
        {
            get => _secureProxyConnection;
            private set
            {
                if (_secureProxyConnection == value) return;
                _secureProxyConnection = value;
                OnPropertyChanged();
            }
        }

        [JsonProperty(nameof(ProxyAddress))]
        [XmlAttribute(nameof(ProxyAddress))]
        string _proxyAddress = string.Empty;
        [JsonIgnore]
        [XmlIgnore]
        public string ProxyAddress
        {
            get => _proxyAddress;
            private set
            {
                if (_proxyAddress == value) return;
                _proxyAddress = value;
                OnPropertyChanged();
            }
        }

        [JsonProperty(nameof(ProxyPort))]
        [XmlAttribute(nameof(ProxyPort))]
        int _proxyPort = 443;
        [JsonIgnore]
        [XmlIgnore]
        public int ProxyPort
        {
            get => _proxyPort;
            private set
            {
                if (_proxyPort == value) return;
                _proxyPort = value;
                OnPropertyChanged();
            }
        }

        [JsonProperty(nameof(ProxyUser))]
        [XmlAttribute(nameof(ProxyUser))]
        string _proxyUser = string.Empty;
        [JsonIgnore]
        [XmlIgnore]
        public string ProxyUser
        {
            get => _proxyUser;
            private set
            {
                if (_proxyUser == value) return;
                _proxyUser = value;
                OnPropertyChanged();
            }
        }

        [JsonProperty(nameof(ProxyPassword))]
        [XmlAttribute(nameof(ProxyPassword))]
        SecureString _proxyPassword;
        [JsonIgnore]
        [XmlIgnore]
        public SecureString ProxyPassword
        {
            get => _proxyPassword;
            private set
            {
                if (_proxyPassword == value) return;
                _proxyPassword = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region DiskSpace
        [JsonProperty(nameof(FreeDiskSpace))]
        [XmlAttribute(nameof(FreeDiskSpace))]
        long _freeDiskspace = 0;
        [JsonIgnore]
        [XmlIgnore]
        public long FreeDiskSpace
        {
            get => _freeDiskspace;
            set
            {
                if (_freeDiskspace == value) return;
                _freeDiskspace = value;
                OnPropertyChanged();

            }
        }

        [JsonProperty(nameof(AvailableDiskSpace))]
        [XmlAttribute(nameof(AvailableDiskSpace))]
        long _availableDiskSpace = 0;
        [JsonIgnore]
        [XmlIgnore]
        public long AvailableDiskSpace
        {
            get => _availableDiskSpace;
            set
            {
                if (_availableDiskSpace == value) return;
                _availableDiskSpace = value;
                OnPropertyChanged();
            }
        }

        [JsonProperty(nameof(TotalDiskSpace))]
        [XmlAttribute(nameof(TotalDiskSpace))]
        long _totalDiskSpace = 0;
        [JsonIgnore]
        [XmlIgnore]
        public long TotalDiskSpace
        {
            get => _totalDiskSpace;
            set
            {
                if (_totalDiskSpace == value) return;
                _totalDiskSpace = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region PrinterStateInformation

        #region ConfigurationInfo

        [JsonIgnore]
        [XmlIgnore]
        long _activeExtruder = 0;
        [JsonIgnore]
        [XmlIgnore]
        public long ActiveExtruder
        {
            get => _activeExtruder;
            set
            {
                if (_activeExtruder == value) return;
                _activeExtruder = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [XmlIgnore]
        long _numberOfExtruders = 0;
        [JsonIgnore]
        [XmlIgnore]
        public long NumberOfExtruders
        {
            get => _numberOfExtruders;
            set
            {
                if (_numberOfExtruders == value) return;
                _numberOfExtruders = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [XmlIgnore]
        bool _isDualExtruder = false;
        [JsonIgnore]
        [XmlIgnore]
        public bool IsDualExtruder
        {
            get => _isDualExtruder;
            set
            {
                if (_isDualExtruder == value) return;
                _isDualExtruder = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [XmlIgnore]
        bool _hasHeatedBed = false;
        [JsonIgnore]
        [XmlIgnore]
        public bool HasHeatedBed
        {
            get => _hasHeatedBed;
            set
            {
                if (_hasHeatedBed == value) return;
                _hasHeatedBed = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [XmlIgnore]
        bool _hasHeatedChamber = false;
        [JsonIgnore]
        [XmlIgnore]
        public bool HasHeatedChamber
        {
            get => _hasHeatedChamber;
            set
            {
                if (_hasHeatedChamber == value) return;
                _hasHeatedChamber = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [XmlIgnore]
        bool _hasFan = false;
        [JsonIgnore]
        [XmlIgnore]
        public bool HasFan
        {
            get => _hasFan;
            set
            {
                if (_hasFan == value) return;
                _hasFan = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [XmlIgnore]
        bool _hasWebCam = false;
        [JsonIgnore]
        [XmlIgnore]
        public bool HasWebCam
        {
            get => _hasWebCam;
            set
            {
                if (_hasWebCam == value) return;
                _hasWebCam = value;
                OnPropertyChanged();
            }
        }
        [JsonIgnore]
        [XmlIgnore]
        RepetierPrinterConfigWebcam _selectedWebCam;
        [JsonIgnore]
        [XmlIgnore]
        public RepetierPrinterConfigWebcam SelectedWebCam
        {
            get => _selectedWebCam;
            set
            {
                if (_selectedWebCam == value) return;
                _selectedWebCam = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region PrinterState
        [JsonIgnore]
        [XmlIgnore]
        bool _isPrinting = false;
        [JsonIgnore]
        [XmlIgnore]
        public bool IsPrinting
        {
            get => _isPrinting;
            set
            {
                if (_isPrinting == value) return;
                _isPrinting = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [XmlIgnore]
        bool _isPaused = false;
        [JsonIgnore]
        [XmlIgnore]
        public bool IsPaused
        {
            get => _isPaused;
            set
            {
                if (_isPaused == value) return;
                _isPaused = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [XmlIgnore]
        bool _isConnectedPrinterOnline = false;
        [JsonIgnore]
        [XmlIgnore]
        public bool IsConnectedPrinterOnline
        {
            get => _isConnectedPrinterOnline;
            set
            {
                if (_isConnectedPrinterOnline == value) return;
                _isConnectedPrinterOnline = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Temperatures

        [JsonIgnore]
        [XmlIgnore]
        double _temperatureExtruderMain = 0;
        [JsonIgnore]
        [XmlIgnore]
        public double TemperatureExtruderMain
        {
            get => _temperatureExtruderMain;
            set
            {
                if (_temperatureExtruderMain == value) return;
                _temperatureExtruderMain = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [XmlIgnore]
        double _temperatureExtruderSecondary = 0;
        [JsonIgnore]
        [XmlIgnore]
        public double TemperatureExtruderSecondary
        {
            get => _temperatureExtruderSecondary;
            set
            {
                if (_temperatureExtruderSecondary == value) return;
                _temperatureExtruderSecondary = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [XmlIgnore]
        double _temperatureHeatedBedMain = 0;
        [JsonIgnore]
        [XmlIgnore]
        public double TemperatureHeatedBedMain
        {
            get => _temperatureHeatedBedMain;
            set
            {
                if (_temperatureHeatedBedMain == value) return;
                _temperatureHeatedBedMain = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [XmlIgnore]
        double _temperatureHeatedChamberMain = 0;
        [JsonIgnore]
        [XmlIgnore]
        public double TemperatureHeatedChamberMain
        {
            get => _temperatureHeatedChamberMain;
            set
            {
                if (_temperatureHeatedChamberMain == value) return;
                _temperatureHeatedChamberMain = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Fans
        [JsonIgnore]
        [XmlIgnore]
        int _speedFanMain = 0;
        [JsonIgnore]
        [XmlIgnore]
        public int SpeedFanMain
        {
            get => _speedFanMain;
            set
            {
                if (_speedFanMain == value) return;
                _speedFanMain = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #endregion

        #region Printers
        [JsonIgnore]
        [XmlIgnore]
        RepetierPrinter _activePrinter;
        [JsonIgnore]
        [XmlIgnore]
        public RepetierPrinter ActivePrinter
        {
            get => _activePrinter;
            set
            {
                if (_activePrinter == value) return;
                OnActivePrinterChanged(new RepetierActivePrinterChangedEventArgs()
                {
                    SessonId = SessionId,
                    NewPrinter = value,
                    OldPrinter = _activePrinter,
                    Printer = GetActivePrinterSlug(),
                });
                _activePrinter = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [XmlIgnore]
        ObservableCollection<RepetierPrinter> _printers = new();
        [JsonIgnore]
        [XmlIgnore]
        public ObservableCollection<RepetierPrinter> Printers
        {
            get => _printers;
            set
            {
                if (_printers == value) return;
                _printers = value;
                if (_printers != null && _printers.Count > 0)
                {
                    if (ActivePrinter == null)
                        ActivePrinter = _printers[0];
                }
                OnPropertyChanged();
            }
        }
        #endregion

        #region Models
        [JsonIgnore]
        [XmlIgnore]
        ObservableCollection<string> _modelGroups = new();
        [JsonIgnore]
        [XmlIgnore]
        public ObservableCollection<string> ModelGroups
        {
            get => _modelGroups;
            set
            {
                if (_modelGroups == value) return;
                _modelGroups = value;
                OnRepetierModelGroupsChanged(new RepetierModelGroupsChangedEventArgs()
                {
                    NewModelGroups = value,
                    SessonId = SessionId,
                    CallbackId = -1,
                    Printer = GetActivePrinterSlug(),
                });
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [XmlIgnore]
        ObservableCollection<RepetierModel> _models = new();
        [JsonIgnore]
        [XmlIgnore]
        public ObservableCollection<RepetierModel> Models
        {
            get => _models;
            set
            {
                if (_models == value) return;
                _models = value;
                OnRepetierModelsChanged(new RepetierModelsChangedEventArgs()
                {
                    NewModels = value,
                    SessonId = SessionId,
                    CallbackId = -1,
                    Printer = GetActivePrinterSlug(),
                });
                OnPropertyChanged();
            }
        }

        #endregion

        #region Jobs
        [JsonIgnore]
        [XmlIgnore]
        ObservableCollection<RepetierJobListItem> _jobList = new();
        [JsonIgnore]
        [XmlIgnore]
        public ObservableCollection<RepetierJobListItem> JobList
        {
            get => _jobList;
            set
            {
                if (_jobList == value) return;
                _jobList = value;
                OnRepetierJobListChanged(new RepetierJobListChangedEventArgs()
                {
                    NewJobList = value,
                    SessonId = SessionId,
                    CallbackId = -1,
                    Printer = GetActivePrinterSlug(),
                });
                OnPropertyChanged();
            }
        }

        #endregion

        #region ExternalCommands
        [JsonIgnore]
        [XmlIgnore]
        ObservableCollection<ExternalCommand> _externalCommands = new();
        [JsonIgnore]
        [XmlIgnore]
        public ObservableCollection<ExternalCommand> ExternalCommands
        {
            get => _externalCommands;
            set
            {
                if (_externalCommands == value) return;
                _externalCommands = value;
                /*
                OnRepetierModelsChanged(new RepetierModelsChangedEventArgs()
                {
                    NewModels = value,
                    SessonId = SessionId,
                    CallbackId = -1,
                    Printer = GetActivePrinterSlug(),
                });
                */
                OnPropertyChanged();
            }
        }
        #endregion

        #region Messages
        [JsonIgnore]
        [XmlIgnore]
        ObservableCollection<RepetierMessage> _messages = new();
        [JsonIgnore]
        [XmlIgnore]
        public ObservableCollection<RepetierMessage> Messages
        {
            get => _messages;
            set
            {
                if (_messages == value) return;
                _messages = value;
                OnMessagesChanged(new RepetierMessagesChangedEventArgs()
                {
                    RepetierMessages = value,
                    SessonId = SessionId,
                    CallbackId = -1,
                    Printer = GetActivePrinterSlug(),
                });
                OnPropertyChanged();
            }
        }
        #endregion

        #region WebCalls
        [JsonIgnore]
        [XmlIgnore]
        ObservableCollection<RepetierWebCallAction> _webCallActions = new();
        [JsonIgnore]
        [XmlIgnore]
        public ObservableCollection<RepetierWebCallAction> WebCallActions
        {
            get => _webCallActions;
            set
            {
                if (_webCallActions == value) return;
                _webCallActions = value;
                OnWebCallActionsChanged(new RepetierWebCallActionsChangedEventArgs()
                {
                    NewWebCallActions = value,
                    SessonId = SessionId,
                    CallbackId = -1,
                    Printer = GetActivePrinterSlug(),
                });

                OnPropertyChanged();
            }
        }
        #endregion

        #region GPIO
        [JsonIgnore]
        [XmlIgnore]
        ObservableCollection<RepetierGpioListItem> _GPIOList = new();
        [JsonIgnore]
        [XmlIgnore]
        public ObservableCollection<RepetierGpioListItem> GPIOList
        {
            get => _GPIOList;
            set
            {
                if (_GPIOList == value) return;
                _GPIOList = value;
                /*
                OnWebCallActionsChanged(new RepetierWebCallActionsChangedEventArgs()
                {
                    NewWebCallActions = value,
                    SessonId = SessionId,
                    CallbackId = -1,
                    Printer = GetActivePrinterSlug(),
                });
                */
                OnPropertyChanged();
            }
        }
        #endregion

        #region State & Config
        [JsonIgnore]
        [XmlIgnore]
        RepetierPrinterConfig _config;
        [JsonIgnore]
        [XmlIgnore]
        public RepetierPrinterConfig Config
        {
            get => _config;
            set
            {
                if (_config == value) return;
                _config = value;
                OnRepetierPrinterConfigChanged(new RepetierPrinterConfigChangedEventArgs()
                {
                    NewConfiguration = value,
                    SessonId = SessionId,
                    CallbackId = -1,
                    Printer = GetActivePrinterSlug(),
                });
                UpdatePrinterConfig(value);
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [XmlIgnore]
        RepetierPrinterState _state;
        [JsonIgnore]
        [XmlIgnore]
        public RepetierPrinterState State
        {
            get => _state;
            set
            {
                if (_state == value) return;
                _state = value;
                OnRepetierPrinterStateChanged(new RepetierPrinterStateChangedEventArgs()
                {
                    NewPrinterState = value,
                    SessonId = SessionId,
                    CallbackId = -1,
                    Printer = GetActivePrinterSlug(),
                });
                UpdatePrinterState(value);
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [XmlIgnore]
        RepetierCurrentPrintInfo _activePrintInfo;
        [JsonIgnore]
        [XmlIgnore]
        public RepetierCurrentPrintInfo ActivePrintInfo
        {
            get => _activePrintInfo;
            set
            {
                if (_activePrintInfo == value) return;
                _activePrintInfo = value;
                OnPrintInfoChanged(new RepetierActivePrintInfoChangedEventArgs()
                {
                    SessonId = SessionId,
                    NewActivePrintInfo = value,
                    Printer = GetActivePrinterSlug(),
                });
                UpdateActivePrintInfo(value);
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [XmlIgnore]
        ObservableCollection<RepetierCurrentPrintInfo> _activePrintInfos = new();
        [JsonIgnore]
        [XmlIgnore]
        public ObservableCollection<RepetierCurrentPrintInfo> ActivePrintInfos
        {
            get => _activePrintInfos;
            set
            {
                if (_activePrintInfos == value) return;
                _activePrintInfos = value;
                OnPrintInfosChanged(new RepetierActivePrintInfosChangedEventArgs()
                {
                    SessonId = SessionId,
                    NewActivePrintInfos = value,
                    Printer = GetActivePrinterSlug(),
                });
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [XmlIgnore]
        ObservableCollection<RepetierPrinterExtruder> _extruders = new();
        [JsonIgnore]
        [XmlIgnore]
        public ObservableCollection<RepetierPrinterExtruder> Extruders
        {
            get => _extruders;
            set
            {
                if (_extruders == value) return;
                _extruders = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [XmlIgnore]
        ObservableCollection<RepetierPrinterHeatbed> _heatedBeds = new();
        [JsonIgnore]
        [XmlIgnore]
        public ObservableCollection<RepetierPrinterHeatbed> HeatedBeds
        {
            get => _heatedBeds;
            set
            {
                if (_heatedBeds == value) return;
                _heatedBeds = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [XmlIgnore]
        ObservableCollection<RepetierPrinterHeatchamber> _heatedChambers = new();
        [JsonIgnore]
        [XmlIgnore]
        public ObservableCollection<RepetierPrinterHeatchamber> HeatedChambers
        {
            get => _heatedChambers;
            set
            {
                if (_heatedChambers == value) return;
                _heatedChambers = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [XmlIgnore]
        ObservableCollection<RepetierPrinterFan> _fans = new();
        [JsonIgnore]
        [XmlIgnore]
        public ObservableCollection<RepetierPrinterFan> Fans
        {
            get => _fans;
            set
            {
                if (_fans == value) return;
                _fans = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [XmlIgnore]
        ObservableCollection<RepetierPrinterConfigWebcam> _webCams = new();
        [JsonIgnore]
        [XmlIgnore]
        public ObservableCollection<RepetierPrinterConfigWebcam> WebCams
        {
            get => _webCams;
            set
            {
                if (_webCams == value) return;
                _webCams = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Position
        [JsonIgnore]
        [XmlIgnore]
        long _curX = 0;
        [JsonIgnore]
        [XmlIgnore]
        public long CurX
        {
            get => _curX;
            set
            {
                if (_curX == value) return;
                _curX = value;
                OnPropertyChanged();
            }
        }
        [JsonIgnore]
        [XmlIgnore]
        long _curY = 0;
        [JsonIgnore]
        [XmlIgnore]
        public long CurY
        {
            get => _curY;
            set
            {
                if (_curY == value) return;
                _curY = value;
                OnPropertyChanged();
            }
        }
        [JsonIgnore]
        [XmlIgnore]
        long _curZ = 0;
        [JsonIgnore]
        [XmlIgnore]
        public long CurZ
        {
            get => _curZ;
            set
            {
                if (_curZ == value) return;
                _curZ = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [XmlIgnore]
        bool _yHomed = false;
        [JsonIgnore]
        [XmlIgnore]
        public bool YHomed
        {
            get => _yHomed;
            set
            {
                if (_yHomed == value) return;
                _yHomed = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [XmlIgnore]
        bool _zHomed = false;
        [JsonIgnore]
        [XmlIgnore]
        public bool ZHomed
        {
            get => _zHomed;
            set
            {
                if (_zHomed == value) return;
                _zHomed = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [XmlIgnore]
        bool _xHomed = false;
        [JsonIgnore]
        [XmlIgnore]
        public bool XHomed
        {
            get => _xHomed;
            set
            {
                if (_xHomed == value) return;
                _xHomed = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region ReadOnly

        public string FullWebAddress
        {
            get => $"{(IsSecure ? "https" : "http")}://{ServerAddress}:{Port}";
        }

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
                        (string.IsNullOrEmpty(API) ? true : Regex.IsMatch(API, RegexHelper.RepetierServerProApiKey))
                    ||
                        // Or validation rules are overriden
                        OverrideValidationRules
                    );
            }
        }
        #endregion

        #endregion

        #region WebSocket
        [JsonIgnore]
        [XmlIgnore]
        WebSocket _webSocket;
        [JsonIgnore]
        [XmlIgnore]
        public WebSocket WebSocket
        {
            get => _webSocket;
            set
            {
                if (_webSocket == value) return;
                _webSocket = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [XmlIgnore]
        Timer _pingTimer;
        [JsonIgnore]
        [XmlIgnore]
        public Timer PingTimer
        {
            get => _pingTimer;
            set
            {
                if (_pingTimer == value) return;
                _pingTimer = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [XmlIgnore]
        int _pingCounter = 0;
        [JsonIgnore]
        [XmlIgnore]
        public int PingCounter
        {
            get => _pingCounter;
            set
            {
                if (_pingCounter == value) return;
                _pingCounter = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [XmlIgnore]
        int _refreshCounter = 0;
        [JsonIgnore]
        [XmlIgnore]
        public int RefreshCounter
        {
            get => _refreshCounter;
            set
            {
                if (_refreshCounter == value) return;
                _refreshCounter = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [XmlIgnore]
        bool _isListeningToWebSocket = false;
        [JsonIgnore]
        [XmlIgnore]
        public bool IsListeningToWebsocket
        {
            get => _isListeningToWebSocket;
            set
            {
                if (_isListeningToWebSocket == value) return;
                _isListeningToWebSocket = value;
                OnListeningChanged(new RepetierEventListeningChangedEventArgs()
                {
                    SessonId = SessionId,
                    IsListening = IsListening,
                    IsListeningToWebSocket = value,
                });
                OnPropertyChanged();
            }
        }
        #endregion

        #region Constructor
        public RepetierServerPro()
        {
            Id = Guid.NewGuid();
            UpdateWebClientInstance();
        }

        public RepetierServerPro(string serverAddress, string api, int port = 3344, bool isSecure = false)
        {
            Id = Guid.NewGuid();
            InitInstance(serverAddress, port, api, isSecure);
            UpdateWebClientInstance();
        }

        public RepetierServerPro(string serverAddress, int port = 3344, bool isSecure = false)
        {
            Id = Guid.NewGuid();
            InitInstance(serverAddress, port, "", isSecure);
            UpdateWebClientInstance();
        }
        #endregion

        #region Destructor
        ~RepetierServerPro()
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
        public static void UpdateSingleInstance(RepetierServerPro Inst)
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
                API = api;
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

        #region EventHandlerss

        #region WebSocket

        public event EventHandler<RepetierEventArgs> WebSocketConnected;
        protected virtual void OnWebSocketConnected(RepetierEventArgs e)
        {
            WebSocketConnected?.Invoke(this, e);
        }

        public event EventHandler<RepetierEventArgs> WebSocketDisconnected;
        protected virtual void OnWebSocketDisconnected(RepetierEventArgs e)
        {
            WebSocketDisconnected?.Invoke(this, e);
        }

        public event EventHandler<ErrorEventArgs> WebSocketError;
        protected virtual void OnWebSocketError(ErrorEventArgs e)
        {
            WebSocketError?.Invoke(this, e);
        }

        public event EventHandler<RepetierEventArgs> WebSocketDataReceived;
        protected virtual void OnWebSocketDataReceived(RepetierEventArgs e)
        {
            WebSocketDataReceived?.Invoke(this, e);
        }

        public event EventHandler<RepetierLoginRequiredEventArgs> LoginResultReceived;
        protected virtual void OnLoginResultReceived(RepetierLoginRequiredEventArgs e)
        {
            LoginResultReceived?.Invoke(this, e);
        }

        #endregion

        #region ServerConnectionState

        public event EventHandler<RepetierEventArgs> ServerWentOffline;
        protected virtual void OnServerWentOffline(RepetierEventArgs e)
        {
            ServerWentOffline?.Invoke(this, e);
        }

        public event EventHandler<RepetierEventArgs> ServerWentOnline;
        protected virtual void OnServerWentOnline(RepetierEventArgs e)
        {
            ServerWentOnline?.Invoke(this, e);
        }

        public event EventHandler<RepetierEventArgs> ServerUpdateAvailable;
        protected virtual void OnServerUpdateAvailable(RepetierEventArgs e)
        {
            ServerUpdateAvailable?.Invoke(this, e);
        }
        #endregion

        #region Errors

        public event EventHandler Error;
        protected virtual void OnError()
        {
            Error?.Invoke(this, EventArgs.Empty);
        }
        protected virtual void OnError(ErrorEventArgs e)
        {
            Error?.Invoke(this, e);
        }
        protected virtual void OnError(UnhandledExceptionEventArgs e)
        {
            Error?.Invoke(this, e);
        }
        protected virtual void OnError(RepetierJsonConvertEventArgs e)
        {
            Error?.Invoke(this, e);
        }
        public event EventHandler<RepetierRestEventArgs> RestApiError;
        protected virtual void OnRestApiError(RepetierRestEventArgs e)
        {
            RestApiError?.Invoke(this, e);
        }

        public event EventHandler<RepetierRestEventArgs> RestApiAuthenticationError;
        protected virtual void OnRestApiAuthenticationError(RepetierRestEventArgs e)
        {
            RestApiAuthenticationError?.Invoke(this, e);
        }
        public event EventHandler<RepetierRestEventArgs> RestApiAuthenticationSucceeded;
        protected virtual void OnRestApiAuthenticationSucceeded(RepetierRestEventArgs e)
        {
            RestApiAuthenticationSucceeded?.Invoke(this, e);
        }

        public event EventHandler<RepetierJsonConvertEventArgs> RestJsonConvertError;
        protected virtual void OnRestJsonConvertError(RepetierJsonConvertEventArgs e)
        {
            RestJsonConvertError?.Invoke(this, e);
        }

        #endregion

        #region ServerStateChanges

        public event EventHandler<RepetierEventListeningChangedEventArgs> ListeningChanged;
        protected virtual void OnListeningChanged(RepetierEventListeningChangedEventArgs e)
        {
            ListeningChanged?.Invoke(this, e);
        }

        public event EventHandler<RepetierEventSessionChangedEventArgs> SessionChanged;
        protected virtual void OnSessionChanged(RepetierEventSessionChangedEventArgs e)
        {
            SessionChanged?.Invoke(this, e);
        }

        public event EventHandler<RepetierMessagesChangedEventArgs> MessagesChanged;
        protected virtual void OnMessagesChanged(RepetierMessagesChangedEventArgs e)
        {
            MessagesChanged?.Invoke(this, e);
        }

        public event EventHandler<RepetierActivePrintInfosChangedEventArgs> PrintInfosChanged;
        protected virtual void OnPrintInfosChanged(RepetierActivePrintInfosChangedEventArgs e)
        {
            PrintInfosChanged?.Invoke(this, e);
        }

        public event EventHandler<RepetierActivePrintInfoChangedEventArgs> PrintInfoChanged;
        protected virtual void OnPrintInfoChanged(RepetierActivePrintInfoChangedEventArgs e)
        {
            PrintInfoChanged?.Invoke(this, e);
        }

        public event EventHandler<RepetierJobsChangedEventArgs> JobsChanged;
        protected virtual void OnJobsChanged(RepetierJobsChangedEventArgs e)
        {
            JobsChanged?.Invoke(this, e);
        }

        public event EventHandler<RepetierJobFinishedEventArgs> JobFinished;
        protected virtual void OnJobFinished(RepetierJobFinishedEventArgs e)
        {
            JobFinished?.Invoke(this, e);
        }

        public event EventHandler<RepetierTempDataEventArgs> TempDataReceived;
        protected virtual void OnTempDataReceived(RepetierTempDataEventArgs e)
        {
            TempDataReceived?.Invoke(this, e);
        }

        public event EventHandler<RepetierPrinterConfigChangedEventArgs> RepetierPrinterConfigChanged;
        protected virtual void OnRepetierPrinterConfigChanged(RepetierPrinterConfigChangedEventArgs e)
        {
            RepetierPrinterConfigChanged?.Invoke(this, e);
        }

        public event EventHandler<RepetierPrinterStateChangedEventArgs> RepetierPrinterStateChanged;
        protected virtual void OnRepetierPrinterStateChanged(RepetierPrinterStateChangedEventArgs e)
        {
            RepetierPrinterStateChanged?.Invoke(this, e);
        }

        public event EventHandler<RepetierModelsChangedEventArgs> RepetierModelsChanged;
        protected virtual void OnRepetierModelsChanged(RepetierModelsChangedEventArgs e)
        {
            RepetierModelsChanged?.Invoke(this, e);
        }

        public event EventHandler<RepetierModelGroupsChangedEventArgs> RepetierModelGroupsChanged;
        protected virtual void OnRepetierModelGroupsChanged(RepetierModelGroupsChangedEventArgs e)
        {
            RepetierModelGroupsChanged?.Invoke(this, e);
        }

        public event EventHandler<RepetierJobListChangedEventArgs> RepetierJobListChanged;
        protected virtual void OnRepetierJobListChanged(RepetierJobListChangedEventArgs e)
        {
            RepetierJobListChanged?.Invoke(this, e);
        }

        public event EventHandler<RepetierActivePrinterChangedEventArgs> ActivePrinterChanged;
        protected virtual void OnActivePrinterChanged(RepetierActivePrinterChangedEventArgs e)
        {
            ActivePrinterChanged?.Invoke(this, e);
        }

        public event EventHandler<RepetierWebCallActionsChangedEventArgs> WebCallActionsChanged;
        protected virtual void OnWebCallActionsChanged(RepetierWebCallActionsChangedEventArgs e)
        {
            WebCallActionsChanged?.Invoke(this, e);
        }
        #endregion

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

                string target = $"{(IsSecure ? "wss" : "ws")}://{ServerAddress}:{Port}/socket/{(!string.IsNullOrEmpty(API) ? $"?apikey={API}" : "")}";
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

        private void WebSocket_Error(object sender, ErrorEventArgs e)
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

        private void WebSocket_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            try
            {
                if (e.Message == null || string.IsNullOrEmpty(e.Message))
                    return;
                if (e.Message.ToLower().Contains("login"))
                {
                    //var login = JsonConvert.DeserializeObject<RepetierLoginRequiredResult>(e.Message);
                    //var login = JsonConvert.DeserializeObject<RepetierLoginResult>(e.Message);
                }
                if (e.Message.ToLower().Contains("session"))
                {
                    Session = JsonConvert.DeserializeObject<EventSession>(e.Message);
                }
                else if (e.Message.ToLower().Contains("event"))
                {
                    RepetierEventContainer repetierEvent = JsonConvert.DeserializeObject<RepetierEventContainer>(e.Message);
                    if (repetierEvent != null)
                    {
                        foreach (RepetierEventData obj in repetierEvent.Data)
                        {
                            string jsonString = obj.Data.ToString();
                            if (obj.Event == "userCredentials")
                            {
                                //EventUserCredentialsData eventUserCredentials = JsonConvert.DeserializeObject<EventUserCredentialsData>(jsonString);
                                var login = JsonConvert.DeserializeObject<RepetierLoginResult>(jsonString);
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
                            else if (obj.Event == "temp")
                            {
                                EventTempData eventTempData = JsonConvert.DeserializeObject<EventTempData>(jsonString);
                                if (eventTempData != null)
                                    OnTempDataReceived(new RepetierTempDataEventArgs()
                                    {
                                        TemperatureData = eventTempData,
                                        CallbackId = PingCounter,
                                        SessonId = SessionId,
                                        Printer = obj.Printer,
                                    });

                            }
                            else if (obj.Event == "jobStarted")
                            {
                                EventJobStartedData eventJobStarted = JsonConvert.DeserializeObject<EventJobStartedData>(jsonString);

                            }
                            else if (obj.Event == "jobsChanged")
                            {
                                // Gets triggered when a model has been deleted
                                EventJobChangedData eventJobsChanged = JsonConvert.DeserializeObject<EventJobChangedData>(jsonString);
                                if (eventJobsChanged != null)
                                    OnJobsChanged(new RepetierJobsChangedEventArgs()
                                    {
                                        Data = eventJobsChanged,
                                        CallbackId = PingCounter,
                                        SessonId = SessionId,
                                        Printer = obj.Printer,
                                    });
                            }
                            else if (obj.Event == "jobFinished")
                            {
                                EventJobFinishedData eventJobFinished = JsonConvert.DeserializeObject<EventJobFinishedData>(jsonString);
                                if (eventJobFinished != null)
                                    OnJobFinished(new RepetierJobFinishedEventArgs()
                                    {
                                        Job = eventJobFinished,
                                        CallbackId = PingCounter,
                                        SessonId = SessionId,
                                        Printer = obj.Printer,
                                    });
                            }
                            else if (obj.Event == "messagesChanged")
                            {
                                EventMessageChangedData eventMessageChanged = JsonConvert.DeserializeObject<EventMessageChangedData>(jsonString);
                                if (eventMessageChanged != null)
                                    OnMessagesChanged(new RepetierMessagesChangedEventArgs()
                                    {
                                        RepetierMessage = eventMessageChanged,
                                        CallbackId = PingCounter,
                                        SessonId = SessionId,
                                        Printer = obj.Printer,
                                    });
                            }
                            else if (obj.Event == "prepareJob")
                            {
                                // No information provided in "Data"
                            }
                            else if (obj.Event == "timer30" || obj.Event == "timer60" || obj.Event == "timer300")
                            {

                            }
                            else if (obj.Event == "hardwareInfo")
                            {
                                EventHardwareInfoChangedData eventHardwareInfoChanged = JsonConvert.DeserializeObject<EventHardwareInfoChangedData>(jsonString);
                            }
                            else if (obj.Event == "wifiChanged")
                            {
                                EventWifiChangedData eventWifiChanged = JsonConvert.DeserializeObject<EventWifiChangedData>(jsonString);
                            }
                            else if (obj.Event == "modelGroupListChanged")
                            {

                            }
                            else if (obj.Event == "modelGroupListChanged")
                            {
                                // no data available here
                            }
                            else if (obj.Event == "gcodeInfoUpdated")
                            {
                                EventGcodeInfoUpdatedData eventGcodeInfoUpdatedChanged = JsonConvert.DeserializeObject<EventGcodeInfoUpdatedData>(jsonString);
                            }
                            else if (obj.Event == "dispatcherCount")
                            {

                            }
                            else if (obj.Event == "printerListChanged")
                            {
                                // 2
                            }
                            else if (obj.Event == "recoverChanged")
                            {

                            }
                            else if (obj.Event == "state")
                            {

                            }
                            else if (obj.Event == "printqueueChanged")
                            {
                                // {"slug": "Prusa_i3_MK3S1"}
                            }
                            else if (obj.Event == "config")
                            {

                            }
                            else if (obj.Event == "log")
                            {

                            }
                            else if (obj.Event == "workerFinished")
                            {

                            }
                            else
                            {

                            }
                        }
                    }
                }
                OnWebSocketDataReceived(new RepetierEventArgs()
                {
                    CallbackId = PingCounter,
                    Message = e.Message,
                    SessonId = this.SessionId,
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

        private void WebSocket_Closed(object sender, EventArgs e)
        {
            IsListeningToWebsocket = false;
            StopPingTimer();
            OnWebSocketDisconnected(new RepetierEventArgs()
            {
                Message = $"WebSocket connection to {WebSocket} closed. Connection state while closing was '{(IsOnline ? "online" : "offline")}'",
                Printer = GetActivePrinterSlug(),
            });
        }

        private void WebSocket_Opened(object sender, EventArgs e)
        {
            // Trigger ping to get session id
            string pingCommand = $"{{\"action\":\"ping\",\"data\":{{\"source\":\"{"App"}\"}},\"printer\":\"{GetActivePrinterSlug()}\",\"callback_id\":{PingCounter}}}";
            WebSocket.Send(pingCommand);

            PingTimer = new Timer((action) => PingServer(), null, 0, 2500);

            IsListeningToWebsocket = true;
            OnWebSocketConnected(new RepetierEventArgs()
            {
                Message = $"WebSocket connection to {WebSocket} established. Connection state while opening was '{(IsOnline ? "online" : "offline")}'",
                Printer = GetActivePrinterSlug(),
            });
        }

        private void WebSocket_DataReceived(object sender, DataReceivedEventArgs e)
        {

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
                var actionResult = JsonConvert.DeserializeObject<RepetierActionResult>(result);
                if (actionResult != null)
                    return actionResult.Ok;
                else
                    return false;
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

        async Task<RepetierApiRequestRespone> SendRestApiRequestAsync(
            string printerName,
            string command,
            string jsonDataString = "",
            int timeout = 10000,
            string requestTargetUri = ""
            )
        {
            CancellationTokenSource cts = new(new TimeSpan(0, 0, 0, 0, timeout));
            return await SendRestApiRequestAsync(printerName, command, cts, jsonDataString, requestTargetUri).ConfigureAwait(false);
        }

        async Task<RepetierApiRequestRespone> SendRestApiRequestAsync(
            string printerName,
            string command,
            CancellationTokenSource cts,
            string jsonDataString = "",
            string requestTargetUri = "")
        {
            RepetierApiRequestRespone apiRsponeResult = new() { IsOnline = IsOnline };
            if (!IsOnline) return apiRsponeResult;

            try
            {
                //CancellationTokenSource cts = new(new TimeSpan(0, 0, 0, 0, Timeout));
                if (string.IsNullOrEmpty(printerName))
                {
                    printerName = "";
                }

                // https://www.repetier-server.com/manuals/programming/API/index.html
                RestClient client = new(FullWebAddress);
                if (EnableProxy)
                {
                    WebProxy proxy = GetCurrentProxy();
                    client.Proxy = proxy;
                }
                RestRequest request = new(
                    string.IsNullOrEmpty(requestTargetUri) ?
                    !string.IsNullOrEmpty(printerName) ? "printer/api/{slug}" : "printer/api" :
                    requestTargetUri)
                {
                    RequestFormat = DataFormat.Json,
                    Method = Method.POST
                };

                if (!string.IsNullOrEmpty(printerName))
                {
                    request.AddUrlSegment("slug", printerName);
                }
                request.AddParameter("a", command, ParameterType.QueryString);
                request.AddParameter("data", jsonDataString, ParameterType.QueryString);
                if (!string.IsNullOrEmpty(API))
                {
                    request.AddParameter("apikey", API, ParameterType.QueryString);
                }
                else if (!string.IsNullOrEmpty(SessionId))
                {
                    request.AddParameter("sess", SessionId, ParameterType.QueryString);
                }

                Uri fullUri = client.BuildUri(request);
                try
                {
                    IRestResponse respone = await client.ExecuteAsync(request, cts.Token).ConfigureAwait(false);

                    if (respone.StatusCode == HttpStatusCode.OK && respone.ResponseStatus == ResponseStatus.Completed)
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
                            Uri = fullUri,
                        };
                    }
                    else if (respone.StatusCode == HttpStatusCode.NonAuthoritativeInformation || respone.StatusCode == HttpStatusCode.Forbidden)
                    {
                        apiRsponeResult.IsOnline = true;
                        apiRsponeResult.HasAuthenticationError = true;
                        apiRsponeResult.EventArgs = new RepetierRestEventArgs()
                        {
                            Status = respone.ResponseStatus.ToString(),
                            Exception = respone.ErrorException,
                            Message = respone.ErrorMessage,
                            Uri = fullUri,
                        };
                    }
                    else
                    {
                        OnRestApiError(new RepetierRestEventArgs()
                        {
                            Status = respone.ResponseStatus.ToString(),
                            Exception = respone.ErrorException,
                            Message = respone.ErrorMessage,
                            Uri = fullUri,
                        });
                        //throw respone.ErrorException;
                    }
                }
                catch (TaskCanceledException texp)
                {
                    if (!IsOnline)
                    {
                        OnError(new UnhandledExceptionEventArgs(texp, false));
                    }
                    // Throws exception on timeout, not actually an error but indicates if the server is reachable.
                }

            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
            }
            return apiRsponeResult;
        }
        
        // New web api: https://prgdoc.repetier-server.com/v1/docs/index.html#/en/web-api/direct?id=gcode
        async Task<RepetierApiRequestRespone> SendRestApiRequestAsync(
            RepetierCommandBase commandBase,
            RepetierCommandFeature commandFeature,
            CancellationTokenSource cts,
            string command = "",
            string printerName = "",
            string jsonDataString = "",
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
                RestClient client = new(FullWebAddress);
                if (EnableProxy)
                {
                    WebProxy proxy = GetCurrentProxy();
                    client.Proxy = proxy;
                }
                RestRequest request = new(
                    string.IsNullOrEmpty(requestTargetUri) ?
                    !string.IsNullOrEmpty(printerName) ? $"{commandBase}/{commandFeature}/{{slug}}" : $"{commandBase}/{commandFeature}" :
                    requestTargetUri)
                {
                    RequestFormat = DataFormat.Json,
                    Method = Method.POST
                };

                if (!string.IsNullOrEmpty(printerName))
                {
                    request.AddUrlSegment("slug", printerName);
                }
                request.AddParameter("a", command, ParameterType.QueryString);
                request.AddParameter("data", jsonDataString, ParameterType.QueryString);
                if (!string.IsNullOrEmpty(API))
                {
                    request.AddParameter("apikey", API, ParameterType.QueryString);
                }
                else if (!string.IsNullOrEmpty(SessionId))
                {
                    request.AddParameter("sess", SessionId, ParameterType.QueryString);
                }

                Uri fullUri = client.BuildUri(request);
                try
                {
                    IRestResponse respone = await client.ExecuteAsync(request, cts.Token).ConfigureAwait(false);

                    if (respone.StatusCode == HttpStatusCode.OK && respone.ResponseStatus == ResponseStatus.Completed)
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
                            Uri = fullUri,
                        };
                    }
                    else if (respone.StatusCode == HttpStatusCode.NonAuthoritativeInformation || respone.StatusCode == HttpStatusCode.Forbidden)
                    {
                        apiRsponeResult.IsOnline = true;
                        apiRsponeResult.HasAuthenticationError = true;
                        apiRsponeResult.EventArgs = new RepetierRestEventArgs()
                        {
                            Status = respone.ResponseStatus.ToString(),
                            Exception = respone.ErrorException,
                            Message = respone.ErrorMessage,
                            Uri = fullUri,
                        };
                    }
                    else
                    {
                        OnRestApiError(new RepetierRestEventArgs()
                        {
                            Status = respone.ResponseStatus.ToString(),
                            Exception = respone.ErrorException,
                            Message = respone.ErrorMessage,
                            Uri = fullUri,
                        });
                        //throw respone.ErrorException;
                    }
                }
                catch (TaskCanceledException texp)
                {
                    if (!IsOnline)
                    {
                        OnError(new UnhandledExceptionEventArgs(texp, false));
                    }
                    // Throws exception on timeout, not actually an error but indicates if the server is reachable.
                }

            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
            }
            return apiRsponeResult;
        }

        async Task<RepetierApiRequestRespone> SendRestApiRequestAsync(
            RepetierCommandBase commandBase,
            RepetierCommandFeature commandFeature,
            string command = "",
            string printerName = "",
            string jsonDataString = "",
            int timeout = 10000,
            string requestTargetUri = ""
            )
        {
            CancellationTokenSource cts = new(new TimeSpan(0, 0, 0, 0, timeout));
            return await SendRestApiRequestAsync(commandBase, commandFeature, cts, command, printerName, jsonDataString, requestTargetUri).ConfigureAwait(false);
        }

        async Task<RepetierApiRequestRespone> SendRestApiRequestAsync(
            RepetierCommandBase commandBase,
            RepetierCommandFeature commandFeature,
            object jsonData,
            string command = "",
            string printerName = "",
            int timeout = 10000,
            string requestTargetUri = ""
            )
        {
            return await SendRestApiRequestAsync(commandBase, commandFeature, command, printerName, JsonConvert.SerializeObject(jsonData ?? ""), timeout, requestTargetUri)
                .ConfigureAwait(false);
        }

        async Task<RepetierApiRequestRespone> SendOnlineCheckRestApiRequestAsync(
            RepetierCommandBase commandBase,
            RepetierCommandFeature commandFeature,
            CancellationTokenSource cts,
            string command,
            string requestTargetUri = "")
        {
            RepetierApiRequestRespone apiRsponeResult = new() { IsOnline = false };
            try
            {
                RestClient client = new(FullWebAddress);
                if (EnableProxy)
                {
                    WebProxy proxy = GetCurrentProxy();
                    client.Proxy = proxy;
                }
                RestRequest request = new(
                    string.IsNullOrEmpty(requestTargetUri) ?
                    $"{commandBase}/{commandFeature}" :
                    requestTargetUri)
                {
                    RequestFormat = DataFormat.Json,
                    Method = Method.POST
                };
                request.AddParameter("a", command, ParameterType.QueryString);
                if (!string.IsNullOrEmpty(API))
                {
                    request.AddParameter("apikey", API, ParameterType.QueryString);
                }
                else if (!string.IsNullOrEmpty(SessionId))
                {
                    request.AddParameter("sess", SessionId, ParameterType.QueryString);
                }

                Uri fullUri = client.BuildUri(request);
                try
                {
                    IRestResponse respone = await client.ExecuteAsync(request, cts.Token).ConfigureAwait(false);

                    if (respone.StatusCode == HttpStatusCode.OK && respone.ResponseStatus == ResponseStatus.Completed)
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
                            Uri = fullUri,
                        };
                    }
                    else if (respone.StatusCode == HttpStatusCode.NonAuthoritativeInformation || respone.StatusCode == HttpStatusCode.Forbidden)
                    {
                        apiRsponeResult.IsOnline = true;
                        apiRsponeResult.HasAuthenticationError = true;
                        apiRsponeResult.EventArgs = new RepetierRestEventArgs()
                        {
                            Status = respone.ResponseStatus.ToString(),
                            Exception = respone.ErrorException,
                            Message = respone.ErrorMessage,
                            Uri = fullUri,
                        };
                    }
                    else
                    {
                        OnRestApiError(new RepetierRestEventArgs()
                        {
                            Status = respone.ResponseStatus.ToString(),
                            Exception = respone.ErrorException,
                            Message = respone.ErrorMessage,
                            Uri = fullUri,
                        });
                        //throw respone.ErrorException;
                    }
                }
                catch (TaskCanceledException)
                {
                    // Throws exception on timeout, not actually an error but indicates if the server is reachable.
                }

            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
            }
            return apiRsponeResult;
        }

        async Task<RepetierApiRequestRespone> SendRestApiRequestAsync(string command, CancellationTokenSource cts, string requestTargetUri = "")
        {
            RepetierApiRequestRespone apiRsponeResult = new() { IsOnline = IsOnline };
            try
            {
                // https://www.repetier-server.com/manuals/programming/API/index.html
                RestClient client = new(FullWebAddress);
                if (EnableProxy)
                {
                    WebProxy proxy = GetCurrentProxy();
                    client.Proxy = proxy;
                }
                RestRequest request = new(
                    string.IsNullOrEmpty(requestTargetUri) ?
                    "printer/api" :
                    requestTargetUri)
                {
                    RequestFormat = DataFormat.Json,
                    Method = Method.POST
                };

                request.AddParameter("a", command, ParameterType.QueryString);
                if (!string.IsNullOrEmpty(API))
                {
                    request.AddParameter("apikey", API, ParameterType.QueryString);
                }
                else if (!string.IsNullOrEmpty(SessionId))
                {
                    request.AddParameter("sess", SessionId, ParameterType.QueryString);
                }

                Uri fullUri = client.BuildUri(request);
                try
                {
                    IRestResponse respone = await client.ExecuteAsync(request, cts.Token).ConfigureAwait(false);

                    if (respone.StatusCode == HttpStatusCode.OK && respone.ResponseStatus == ResponseStatus.Completed)
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
                            Uri = fullUri,
                        };
                    }
                    else if (respone.StatusCode == HttpStatusCode.NonAuthoritativeInformation || respone.StatusCode == HttpStatusCode.Forbidden)
                    {
                        apiRsponeResult.IsOnline = true;
                        apiRsponeResult.HasAuthenticationError = true;
                        apiRsponeResult.EventArgs = new RepetierRestEventArgs()
                        {
                            Status = respone.ResponseStatus.ToString(),
                            Exception = respone.ErrorException,
                            Message = respone.ErrorMessage,
                            Uri = fullUri,
                        };
                    }
                    else
                    {
                        OnRestApiError(new RepetierRestEventArgs()
                        {
                            Status = respone.ResponseStatus.ToString(),
                            Exception = respone.ErrorException,
                            Message = respone.ErrorMessage,
                            Uri = fullUri,
                        });
                        //throw respone.ErrorException;
                    }
                }
                catch (TaskCanceledException texp)
                {
                    // Throws exception on timeout, not actually an error but indicates if the server is reachable.
                }

            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
            }
            return apiRsponeResult;
        }

        async Task<RepetierApiRequestRespone> SendRestApiRequestAsync(string printerName, string command, object jsonData, int timeout = 10000, string requestTargetUri = "")
        {
            return await SendRestApiRequestAsync(printerName, command, JsonConvert.SerializeObject(jsonData ?? ""), timeout, requestTargetUri)
                .ConfigureAwait(false);
        }

        string SendRestAPIRequest(string printerName, string command, string jsonDataString = "", int timeout = 10000, string requestTargetUri = "")
        {
            try
            {
                if (!IsOnline) return string.Empty;

                if (string.IsNullOrEmpty(printerName))
                    printerName = "";
                // https://www.repetier-server.com/manuals/programming/API/index.html
                var client = new RestClient(FullWebAddress);
                if (EnableProxy)
                {
                    var proxy = GetCurrentProxy();
                    client.Proxy = proxy;
                }
                //var request = new RestRequest(!string.IsNullOrEmpty(PrinterName) ? "printer/api/{slug}" : "printer/api");
                RestRequest request = new(
                    string.IsNullOrEmpty(requestTargetUri) ?
                    !string.IsNullOrEmpty(printerName) ? "printer/api/{slug}" : "printer/api" :
                    requestTargetUri)
                {
                    RequestFormat = DataFormat.Json,
                    Method = Method.POST,
                    Timeout = timeout
                };

                if (!string.IsNullOrEmpty(printerName))
                {
                    request.AddUrlSegment("slug", printerName);
                }
                request.AddParameter("a", command, ParameterType.QueryString);
                request.AddParameter("data", jsonDataString, ParameterType.QueryString);
                if (!string.IsNullOrEmpty(API))
                    request.AddParameter("apikey", API, ParameterType.QueryString);
                else if (!string.IsNullOrEmpty(SessionId))
                    request.AddParameter("sess", SessionId, ParameterType.QueryString);
                Uri fullUri = client.BuildUri(request);
                IRestResponse respone = client.Post(request);

                string result = string.Empty;
                if (respone.StatusCode == HttpStatusCode.OK && respone.ResponseStatus == ResponseStatus.Completed)
                {
                    AuthenticationFailed = false;
                    result = respone.Content;
                }
                else if (respone.StatusCode == HttpStatusCode.NonAuthoritativeInformation)
                {
                    AuthenticationFailed = true;
                    OnRestApiAuthenticationError(new RepetierRestEventArgs()
                    {
                        Status = respone.ResponseStatus.ToString(),
                        Exception = respone.ErrorException,
                        Message = respone.Content,
                        Uri = fullUri,
                    });
                    throw new AuthenticationException(respone.Content);
                }
                else
                {
                    OnRestApiError(new RepetierRestEventArgs()
                    {
                        Status = respone.ResponseStatus.ToString(),
                        Exception = respone.ErrorException,
                        Message = respone.ErrorMessage,
                        Uri = fullUri,
                    });
                    throw respone.ErrorException;
                }
                return result;
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return string.Empty;
            }
        }
        string SendRestAPIRequest(string printerName, string command, object jsonData, int timeout = 10000)
        {
            try
            {
                return SendRestAPIRequest(printerName, command, JsonConvert.SerializeObject(jsonData), timeout);
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return string.Empty;
            }
        }
        #endregion

        #region Download
        public byte[] GetDynamicRenderImage(long modelId, bool thumbnail, int timeout = 5000)
        {
            try
            {
                byte[] resultObject = new byte[0];

                string currentPrinter = GetActivePrinterSlug();
                if (string.IsNullOrEmpty(currentPrinter)) return resultObject;

                // https://www.repetier-server.com/manuals/programming/API/index.html
                RestClient client = new(FullWebAddress);
                RestRequest request = new("dyn/render_image")
                {
                    RequestFormat = DataFormat.None,
                    Method = Method.GET,
                    Timeout = timeout
                };

                request.AddParameter("q", "models");
                request.AddParameter("id", modelId);
                request.AddParameter("slug", currentPrinter);
                request.AddParameter("t", thumbnail ? "s" : "l");
                request.AddParameter("apikey", API, ParameterType.QueryString);

                Uri fullUrl = client.BuildUri(request);

                byte[] respone = client.DownloadData(request, true);
                return respone;
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return new byte[0];
            }
        }
        public async Task<byte[]> GetDynamicRenderImageAsync(long modelId, bool thumbnail, int timeout = 5000)
        {
            try
            {
                byte[] respone = await Task.Run(() =>
                {
                    return GetDynamicRenderImage(modelId, thumbnail, timeout);
                }).ConfigureAwait(false);
                return respone;
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return new byte[0];
            }
        }

        public byte[] GetDynamicRenderImageByJobId(long jobId, bool thumbnail, int timeout = 10000)
        {
            try
            {
                byte[] resultObject = new byte[0];

                string currentPrinter = GetActivePrinterSlug();
                if (string.IsNullOrEmpty(currentPrinter)) return resultObject;

                // https://www.repetier-server.com/manuals/programming/API/index.html
                RestClient client = new(FullWebAddress);
                RestRequest request = new("dyn/render_image")
                {
                    RequestFormat = DataFormat.None,
                    Method = Method.GET,
                    Timeout = timeout
                };

                request.AddParameter("q", "jobs");
                request.AddParameter("id", jobId);
                request.AddParameter("slug", currentPrinter);
                request.AddParameter("t", thumbnail ? "s" : "l");
                request.AddParameter("apikey", API, ParameterType.QueryString);

                byte[] respone = client.DownloadData(request, true);
                return respone;
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return new byte[0];
            }
        }

        public async Task<byte[]> GetDynamicRenderImageByJobIdAsync(long jobId, bool thumbnail, int timeout = 10000)
        {
            try
            {
                byte[] respone = await Task.Run(() =>
                {
                    return GetDynamicRenderImage(jobId, thumbnail, timeout);
                }).ConfigureAwait(false);
                return respone;
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

                Extruders = new ObservableCollection<RepetierPrinterExtruder>(newState.Extruder);
                IsDualExtruder = Extruders != null && Extruders.Count > 1;

                HeatedBeds = new ObservableCollection<RepetierPrinterHeatbed>(newState.HeatedBeds);
                HasHeatedBed = HeatedBeds != null && HeatedBeds.Count > 0;

                HeatedChambers = new ObservableCollection<RepetierPrinterHeatchamber>(newState.HeatedChambers);
                HasHeatedBed = HeatedChambers != null && HeatedChambers.Count > 0;

                Fans = new ObservableCollection<RepetierPrinterFan>(newState.Fans);
                HasFan = Fans != null && Fans.Count > 0;

                CurX = newState.X;
                CurY = newState.Y;
                CurZ = newState.Z;

                XHomed = newState.HasXHome;
                YHomed = newState.HasYHome;
                ZHomed = newState.HasZHome;

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
                    IsConnectedPrinterOnline = _activePrintInfo.Online > 0;
                    IsPrinting = _activePrintInfo.Jobid > 0;
                    IsPaused = _activePrintInfo.Paused;
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
#if NEW_API
                result = await SendRestApiRequestAsync(
                   commandBase: RepetierCommandBase.printer,
                   commandFeature: RepetierCommandFeature.api,
                   command: "listModels",
                   printerName: printerName)
                    .ConfigureAwait(false);
#else
                result = await SendRestApiRequestAsync(printerName, "listModels", "").ConfigureAwait(false);
#endif
                RepetierModelList list = JsonConvert.DeserializeObject<RepetierModelList>(result.Result);

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
        async Task<RepetierModelGroup> GetModelGroupsAsync(string printerName)
        {
            RepetierApiRequestRespone result = new();
            try
            {
#if NEW_API
                result = await SendRestApiRequestAsync(
                   commandBase: RepetierCommandBase.printer,
                   commandFeature: RepetierCommandFeature.api,
                   command: "listModelGroups",
                   printerName: printerName)
                    .ConfigureAwait(false);
#else
                result = await SendRestApiRequestAsync(printerName, "listModelGroups", "").ConfigureAwait(false);
#endif
                RepetierModelGroup info = JsonConvert.DeserializeObject<RepetierModelGroup>(result.Result);
                return info;
            }
            catch (JsonException jecx)
            {
                OnError(new RepetierJsonConvertEventArgs()
                {
                    Exception = jecx,
                    OriginalString = result.Result,
                    TargetType = nameof(RepetierModelGroup),
                    Message = jecx.Message,
                });
                return new RepetierModelGroup();
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return new RepetierModelGroup();
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
#if NEW_API
                result = await SendRestApiRequestAsync(
                   commandBase: RepetierCommandBase.printer,
                   commandFeature: RepetierCommandFeature.api,
                   command: "listJobs",
                   printerName: printerName)
                    .ConfigureAwait(false);
#else
                result = await SendRestApiRequestAsync(printerName, "listJobs", "").ConfigureAwait(false);
#endif
                RepetierJobListRespone respone = JsonConvert.DeserializeObject<RepetierJobListRespone>(result.Result);
                return respone;
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
#if NEW_API
                result = await SendRestApiRequestAsync(
                   commandBase: RepetierCommandBase.printer,
                   commandFeature: RepetierCommandFeature.api,
                   command: "webCallsList",
                   printerName: printerName
                   )
                    .ConfigureAwait(false);
#else
                result = await SendRestApiRequestAsync(printerName, "webCallsList").ConfigureAwait(false);
#endif
                RepetierWebCallList script = JsonConvert.DeserializeObject<RepetierWebCallList>(result.Result);
                return script;
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
        Uri GetProxyUri()
        {
            return ProxyAddress.StartsWith("http://") || ProxyAddress.StartsWith("https://") ? new Uri($"{ProxyAddress}:{ProxyPort}") : new Uri($"{(SecureProxyConnection ? "https" : "http")}://{ProxyAddress}:{ProxyPort}");
        }

        WebProxy GetCurrentProxy()
        {
            WebProxy proxy = new()
            {
                Address = GetProxyUri(),
                BypassProxyOnLocal = false,
                UseDefaultCredentials = ProxyUseDefaultCredentials,
            };
            if (ProxyUseDefaultCredentials && !string.IsNullOrEmpty(ProxyUser))
            {
                proxy.Credentials = new NetworkCredential(ProxyUser, ProxyPassword);
            }
            else
            {
                proxy.UseDefaultCredentials = ProxyUseDefaultCredentials;
            }
            return proxy;
        }
        void UpdateWebClientInstance()
        {
            if (EnableProxy && !string.IsNullOrEmpty(ProxyAddress))
            {
                //var proxy = GetCurrentProxy();
                HttpMessageHandler handler = HttpHandler = new HttpClientHandler()
                //HttpMessageHandler handler = new HttpClientHandler()
                {
                    UseProxy = true,
                    Proxy = GetCurrentProxy(),
                    AllowAutoRedirect = true,
                };
                client = new HttpClient(handler: handler, disposeHandler: true);
            }
            else
            {
                // Init a default handler
                client = HttpHandler == null ? new HttpClient() : new HttpClient(handler: HttpHandler, disposeHandler: true);
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
                    PingTimer.Change(Timeout.Infinite, Timeout.Infinite);
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
                    Timer.Change(Timeout.Infinite, Timeout.Infinite);
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
            ProxyUseDefaultCredentials = true;
            ProxyAddress = address;
            ProxyPort = port;
            ProxyUser = string.Empty;
            ProxyPassword = null;
            SecureProxyConnection = secure;
            UpdateWebClientInstance();
        }
        public void SetProxy(bool secure, string address, int port, string user = "", SecureString password = null, bool enable = true)
        {
            EnableProxy = enable;
            ProxyUseDefaultCredentials = false;
            ProxyAddress = address;
            ProxyPort = port;
            ProxyUser = user;
            ProxyPassword = password;
            SecureProxyConnection = secure;
            UpdateWebClientInstance();
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
        public async Task RefreshAllAsync(RepetierImageType imageType = RepetierImageType.Thumbnail)
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
            _ = await SendRestApiRequestAsync("", "logout").ConfigureAwait(false);
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
                    ActivePrinter = Printers.FirstOrDefault(printer => printer.Online == 1);
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
                RepetierPrinter printer = Printers.FirstOrDefault(prt => prt.Slug == slug);
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
                if (client != null)
                {
                    client.CancelPendingRequests();
                    UpdateWebClientInstance();
                }
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
            }
        }
        #endregion

        #region WebCam
        public string GetWebCamUri(int camIndex = 0)
        {
            try
            {
                string currentPrinter = GetActivePrinterSlug();
                if (string.IsNullOrEmpty(currentPrinter)) return string.Empty;

                //return string.Format("{0}{1}:{2}/printer/cammjpg/{3}?cam={4}&apikey={5}", httpProtocol, ServerAddress, Port, currentPrinter, CamIndex, API);
                return $"{FullWebAddress}/printer/cammjpg/{currentPrinter}?cam={camIndex}&apikey={API}";
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return "";
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
#if NEW_API
                    RepetierApiRequestRespone respone = await SendRestApiRequestAsync(
                       commandBase: RepetierCommandBase.printer,
                       commandFeature: RepetierCommandFeature.ping,
                       command: pingCommand)
                    .ConfigureAwait(false);
#else
                    RepetierApiRequestRespone respone = await SendRestApiRequestAsync("", "ping", pingCommand, timeout).ConfigureAwait(false);
#endif
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
            CancellationTokenSource cts = new(new TimeSpan(0, 0, 0, 0, timeout));
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
#if NEW_API
                    RepetierApiRequestRespone respone = await SendOnlineCheckRestApiRequestAsync(
                       commandBase: RepetierCommandBase.printer,
                       commandFeature: RepetierCommandFeature.ping,
                       cts: cts,
                       command: "{}")
                    .ConfigureAwait(false);
#else
                    RepetierApiRequestRespone respone = await SendRestApiRequestAsync("{}", cts).ConfigureAwait(false);
#endif
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
            if (isReachable || _retries > RetriesWhenOffline)
            {
                _retries = 0;
                IsOnline = isReachable;
            }
            else
            {
                // Retry with shorter timeout to see if the connection loss is real
                _retries++;
                await CheckOnlineAsync(2000).ConfigureAwait(false);
            }
        }

        [Obsolete]
        public async Task CheckOnlineOldAsync(CancellationTokenSource cts, bool resolveDnsFirst = true)
        {
            if (IsConnecting) return; // Avoid multiple calls
            IsConnecting = true;
            bool isReachable = false;
            try
            {
                string uriString = FullWebAddress;
                try
                {
                    // This will try to resolve the hostname before sending the reqeuest.
                    if (resolveDnsFirst)
                    {
                        try
                        {
                            IPHostEntry host = Dns.GetHostEntry(ServerAddress);
                            IPAddress address = host.AddressList.FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
                            if (address != null)
                            {
                                uriString = $"{(IsSecure ? "https" : "http")}://{address}:{Port}";
                            }
                        }
                        catch (Exception exc)
                        {
                            OnError(new UnhandledExceptionEventArgs(exc, false));
                        }
                    }
                    await Task.Delay(10);
                    HttpResponseMessage response =
                        await client.GetAsync(uriString, HttpCompletionOption.ResponseHeadersRead, cts.Token).ConfigureAwait(false);
                    _ = response.EnsureSuccessStatusCode();
                    if (response != null)
                    {
                        isReachable = response.IsSuccessStatusCode;
                    }
                    await Task.Delay(50);
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
            if (isReachable || _retries > RetriesWhenOffline)
            {
                _retries = 0;
                IsOnline = isReachable;
            }
            else
            {
                // Retry with shorter timeout to see if the connection loss is real
                _retries++;
                await CheckOnlineAsync(2000).ConfigureAwait(false);
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
                    var respone = await SendRestApiRequestAsync("", "ping", pingCommand, timeout).ConfigureAwait(false);
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
                result = await SendRestApiRequestAsync("", "checkForUpdates", "").ConfigureAwait(false);
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
                result = await SendRestApiRequestAsync("", "autoupdate", "").ConfigureAwait(false);
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
                result = await SendRestApiRequestAsync("", "updateAvailable", "").ConfigureAwait(false);
                resultObject = JsonConvert.DeserializeObject<RepetierAvailableUpdateInfo>(result.Result);
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
#endregion

#region License

        public async Task<RepetierLicenseInfo> GetLicenseDataAsync()
        {
            RepetierApiRequestRespone result = new();
            try
            {
                result = await SendRestApiRequestAsync("", "getLicenceData", "").ConfigureAwait(false);
                if (result != null)
                {
                    RepetierLicenseInfo lic = JsonConvert.DeserializeObject<RepetierLicenseInfo>(result.Result);
                    return lic;
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
        public bool CheckIfConfigurationHasChanged(RepetierServerPro temp)
        {
            try
            {
                if (temp == null) return false;
                else return
                    !(ServerAddress == temp.ServerAddress &&
                        Port == temp.Port &&
                        API == temp.API &&
                        IsSecure == temp.IsSecure
                        )
                    ;
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

        public async Task<RepetierErrorCodes> SendAndMoveGcodeAsync(string printerName, string filePath, string group = "#")
        {
            // https://www.repetier-server.com/using-simplify-3d-repetier-server/
            try
            {
                FileInfo info = new(filePath);
                if (!info.Exists) return RepetierErrorCodes.FILE_NOT_FOUND;

                RestClient client = new(FullWebAddress);
                RestRequest request = new(string.Format("/printer/model/{2}", ServerAddress, Port, printerName.Replace(" ", "_")))
                {
                    Method = Method.POST,
                    Timeout = 25000,
                    AlwaysMultipartFormData = true,
                };

                request.AddHeader("x-api-key", API);
                request.AddFile(Path.GetFileNameWithoutExtension(filePath), filePath);

                IRestResponse respone = await client.ExecuteAsync(request).ConfigureAwait(false);
                if (respone.StatusCode == HttpStatusCode.OK)
                {
                    if (group != "#")
                    {
                        ObservableCollection<RepetierModel> res = await GetModelsAsync(printerName).ConfigureAwait(false);
                        List<RepetierModel> list = new(res);

                        string fileName = info.Name.Replace(Path.GetExtension(filePath), string.Empty);
                        RepetierModel model = list.FirstOrDefault(m => m.Name == fileName);

                        bool result = await MoveModelToGroupAsync(printerName, group, model.Id).ConfigureAwait(false);
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

        public async Task<ObservableCollection<RepetierModel>> GetModelsAsync(
            string PrinterName = "",
            RepetierImageType ImageType = RepetierImageType.Thumbnail,
            IProgress<int> Prog = null)
        {
            try
            {
                ObservableCollection<RepetierModel> modelDatas = new();
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
                        ObservableCollection<RepetierModel> Models = new(modelList);
                        if (ImageType != RepetierImageType.None)
                        {
                            int total = Models.Count;
                            for (int i = 0; i < total; i++)
                            {
                                RepetierModel model = Models[i];
                                model.IsLoadingImage = true;
                                model.PrinterName = currentPrinter;
                                model.ImageType = ImageType;
                                // Load image depending on settings
                                switch (ImageType)
                                {
                                    case RepetierImageType.Thumbnail:
                                        model.Thumbnail = await GetDynamicRenderImageAsync(model.Id, true).ConfigureAwait(false);
                                        break;
                                    case RepetierImageType.Image:
                                        model.Image = await GetDynamicRenderImageAsync(model.Id, false).ConfigureAwait(false);
                                        break;
                                    default:
                                        model.Thumbnail = await GetDynamicRenderImageAsync(model.Id, true).ConfigureAwait(false);
                                        model.Image = await GetDynamicRenderImageAsync(model.Id, false).ConfigureAwait(false);
                                        break;
                                }

                                if (Prog != null)
                                {
                                    float progress = ((float)i / total) * 100f;
                                    //int progressInt = Convert.ToInt32(progress);
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
                            Prog.Report(100);
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
                return new ObservableCollection<RepetierModel>();
            }
        }
        public async Task GetModelImagesAsync(ObservableCollection<RepetierModel> models, RepetierImageType imageType = RepetierImageType.Thumbnail)
        {
            string currentPrinter = GetActivePrinterSlug();
            if (string.IsNullOrEmpty(currentPrinter)) return;

            await Task.Run(async () =>
            {
                //foreach (RepetierModel model in Models)
                for (int i = 0; i < models.Count; i++)
                {
                    RepetierModel model = models[i];
                    model.IsLoadingImage = true;
                    model.ImageType = imageType;
                    // Load image depending on settings
                    switch (imageType)
                    {
                        case RepetierImageType.Thumbnail:
                            model.Thumbnail = await GetDynamicRenderImageAsync(model.Id, true).ConfigureAwait(false);
                            break;
                        case RepetierImageType.Image:
                            model.Image = await GetDynamicRenderImageAsync(model.Id, false).ConfigureAwait(false);
                            break;
                        default:
                            model.Thumbnail = await GetDynamicRenderImageAsync(model.Id, true).ConfigureAwait(false);
                            model.Image = await GetDynamicRenderImageAsync(model.Id, false).ConfigureAwait(false);
                            break;
                    }
                    model.IsLoadingImage = false;
                }
            }).ConfigureAwait(false);
        }
        public async Task<bool> DeleteModelFromServerAsync(RepetierModel model)
        {
            string currentPrinter = GetActivePrinterSlug();
            if (string.IsNullOrEmpty(currentPrinter)) return false;

            try
            {
                RepetierApiRequestRespone result =
                    await SendRestApiRequestAsync(currentPrinter, "removeModel", string.Format("{{\"id\":{0}}}", model.Id))
                    .ConfigureAwait(false);
                return GetQueryResult(result.Result);
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return false;
            }
        }

        public async Task RefreshModelsAsync(RepetierImageType imageType = RepetierImageType.Thumbnail, IProgress<int> prog = null)
        {
            try
            {
                ObservableCollection<RepetierModel> modelDatas = new();
                if (!IsReady || ActivePrinter == null)
                {
                    Models = modelDatas;
                    return;
                }
                Models = await GetModelsAsync(GetActivePrinterSlug(), imageType, prog).ConfigureAwait(false);
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                Models = new ObservableCollection<RepetierModel>();
            }
        }
        public async Task<bool> CopyModelToPrintQueueAsync(RepetierModel model, bool startPrintIfPossible = true)
        {
            string currentPrinter = GetActivePrinterSlug();
            if (string.IsNullOrEmpty(currentPrinter))
            {
                return false;
            }

            try
            {
                RepetierApiRequestRespone result =
                    await SendRestApiRequestAsync(currentPrinter, "copyModel", $"{{\"id\":{model.Id}, \"autostart\":{(startPrintIfPossible ? "true" : "false")}}}")
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
#if NEW_API
                result = await SendRestApiRequestAsync(
                   commandBase: RepetierCommandBase.printer,
                   commandFeature: RepetierCommandFeature.api,
                   command: "freeSpace")
                    .ConfigureAwait(false);
#else
                result = await SendRestApiRequestAsync("", "freeSpace", "").ConfigureAwait(false);
#endif
                RepetierFreeSpaceRespone space = JsonConvert.DeserializeObject<RepetierFreeSpaceRespone>(result.Result);
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
                    await SendRestApiRequestAsync(currentPrinter, "addModelGroup", string.Format("{{\"groupName\":\"{0}\"}}", groupName))
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
                    await SendRestApiRequestAsync(printerName, "addModelGroup", string.Format("{{\"groupName\":\"{0}\"}}", groupName))
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
                    await SendRestApiRequestAsync(currentPrinter, "delModelGroup", string.Format("{{\"groupName\":\"{0}\"}}", groupName))
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
                    await SendRestApiRequestAsync(printerName, "delModelGroup", string.Format("{{\"groupName\":\"{0}\"}}", groupName))
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
                    await SendRestApiRequestAsync(currentPrinter, "moveModelFileToGroup", string.Format("{{\"groupName\":\"{0}\", \"id\":{1}}}", groupName, id))
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
                    await SendRestApiRequestAsync(printerName, "moveModelFileToGroup", string.Format("{{\"groupName\":\"{0}\", \"id\":{1}}}", groupName, id))
                    .ConfigureAwait(false);
                return GetQueryResult(result.Result);
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return false;
            }
        }

        public async Task<ObservableCollection<string>> GetModelGroupsAsync()
        {
            RepetierApiRequestRespone result = new();
            ObservableCollection<string> resultObject = new();

            string currentPrinter = GetActivePrinterSlug();
            if (string.IsNullOrEmpty(currentPrinter)) return resultObject;

            try
            {
#if NEW_API
                result = await SendRestApiRequestAsync(
                   commandBase: RepetierCommandBase.printer,
                   commandFeature: RepetierCommandFeature.api, 
                   command: "listModelGroups", 
                   printerName: currentPrinter)
                    .ConfigureAwait(false);
#else
                result = await SendRestApiRequestAsync(currentPrinter, "listModelGroups", "").ConfigureAwait(false);
#endif
                RepetierModelGroup info = JsonConvert.DeserializeObject<RepetierModelGroup>(result.Result);
                return info != null && info.GroupNames != null ? new ObservableCollection<string>(info.GroupNames) : resultObject;
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
                return new ObservableCollection<string>();
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
                ObservableCollection<string> groups = new();
                if (!IsReady || ActivePrinter == null)
                {
                    ModelGroups = groups;
                    return;
                }

                string currentPrinter = ActivePrinter.Slug;
                if (string.IsNullOrEmpty(currentPrinter)) return;

                RepetierModelGroup result = await GetModelGroupsAsync(currentPrinter).ConfigureAwait(false);
                if (result != null)
                {
                    ModelGroups = new ObservableCollection<string>(result.GroupNames);
                }
                else ModelGroups = groups;

            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                ModelGroups = new ObservableCollection<string>();
            }
        }
#endregion

#region Jobs
        public async Task<ObservableCollection<RepetierJobListItem>> GetJobListAsync()
        {
            ObservableCollection<RepetierJobListItem> resultObject = new();

            string currentPrinter = GetActivePrinterSlug();
            if (string.IsNullOrEmpty(currentPrinter)) return resultObject;

            try
            {
                RepetierJobListRespone info = await GetJobListResponeAsync(currentPrinter).ConfigureAwait(false);
                if (info != null && info.Data != null)
                    return new ObservableCollection<RepetierJobListItem>(info.Data);
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
                ObservableCollection<RepetierJobListItem> jobList = new();
                if (!IsReady || ActivePrinter == null)
                {
                    JobList = jobList;
                    return;
                }

                string currentPrinter = ActivePrinter.Slug;
                if (string.IsNullOrEmpty(currentPrinter)) return;

                RepetierJobListRespone result = await GetJobListResponeAsync(currentPrinter).ConfigureAwait(false);
                JobList = result != null ? new(result.Data) : jobList;

            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                JobList = new();
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
                    await SendRestApiRequestAsync(currentPrinter, "startJob", string.Format("{{\"id\":{0}}}", id))
                    .ConfigureAwait(false);
                return GetQueryResult(result.Result, true);
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return false;
            }
        }

        public async Task<bool> StartJobAsync(RepetierModel model)
        {
            return await StartJobAsync(model.Id).ConfigureAwait(false);
        }

        public async Task<bool> StartJobAsync(RepetierJobListItem jobItem)
        {
            return await StartJobAsync(jobItem.Id).ConfigureAwait(false);
        }

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
                    await SendRestApiRequestAsync(currentPrinter, "removeJob", string.Format("{{\"id\":{0}}}", jobId))
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
        public async Task<bool> RemoveJobAsync(RepetierCurrentPrintInfo job)
        {
            return await RemoveJobAsync(job.Jobid).ConfigureAwait(false);
        }
        public async Task<bool> RemoveJobAsync(RepetierJobListItem job)
        {
            return await RemoveJobAsync(job.Id).ConfigureAwait(false);
        }

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
                    await SendRestApiRequestAsync(currentPrinter, "continueJob")
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
                    await SendRestApiRequestAsync(currentPrinter, "stopJob", "")
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

        #endregion

        #region PrinterState
        public async Task<RepetierPrinterStateRespone> GetStateObjectAsync(string printerName = "")
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

                result = await SendRestApiRequestAsync(currentPrinter, "stateList", "").ConfigureAwait(false);
                currentPrinter = currentPrinter.Replace(" ", "_");
                resultString = result.Result.Replace(currentPrinter, "Printer");

                RepetierPrinterStateRespone state = JsonConvert.DeserializeObject<RepetierPrinterStateRespone>(resultString);
                if (state != null && IsPrinterSlugSelected(currentPrinter))
                    State = state.Printer;
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
        public async Task RefreshPrinterStateAsync()
        {
            try
            {
                if (!IsReady || ActivePrinter == null)
                {
                    return;
                }
                var result = await GetStateObjectAsync().ConfigureAwait(false);
                if (result != null && result.Printer != null)
                {
                    State = result.Printer;
                }

            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
            }
        }

        public async Task<RepetierPrinterStateRespone> GetStateObjectForPrinterAsync(string printerName)
        {
            return await GetStateObjectAsync(printerName).ConfigureAwait(false);
        }


        #endregion

        #region Printers
        public async Task<ObservableCollection<RepetierPrinter>> GetPrintersAsync()
        {
            RepetierApiRequestRespone result = new();
            try
            {
                ObservableCollection<RepetierPrinter> repetierPrinterList = new();
                if (!IsReady)
                    return repetierPrinterList;
#if NEW_API
                result = await SendRestApiRequestAsync(RepetierCommandBase.printer, RepetierCommandFeature.list).ConfigureAwait(false);
                RepetierPrinterListRespone respone = JsonConvert.DeserializeObject<RepetierPrinterListRespone>(result.Result);
                if (respone != null)
                {
                    repetierPrinterList = new ObservableCollection<RepetierPrinter>(respone.Printers);
                    Printers = repetierPrinterList;
                }
#else
                result = await SendRestApiRequestAsync("", "listPrinter").ConfigureAwait(false);
                                RepetierPrinter[] printers = JsonConvert.DeserializeObject<RepetierPrinter[]>(result.Result);
                if (printers != null)
                {
                    repetierPrinterList = new ObservableCollection<RepetierPrinter>(printers);
                    Printers = repetierPrinterList;
                }
#endif
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
                return new ObservableCollection<RepetierPrinter>();
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return new ObservableCollection<RepetierPrinter>();
            }
        }
        public async Task RefreshPrinterListAsync()
        {
            try
            {
                ObservableCollection<RepetierPrinter> printers = new();
                if (!IsReady)
                {
                    Printers = printers;
                    return;
                }

                ObservableCollection<RepetierPrinter> result = await GetPrintersAsync().ConfigureAwait(false);
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
                Printers = new ObservableCollection<RepetierPrinter>();
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
                result = await SendRestApiRequestAsync(currentPrinter, "listPrinter").ConfigureAwait(false);
                RepetierCurrentPrintInfo[] info = JsonConvert.DeserializeObject<RepetierCurrentPrintInfo[]>(result.Result);
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
                result = await SendRestApiRequestAsync(currentPrinter, "getPrinterConfig", string.Format("{{\"printer\": \"{0}\"}}", currentPrinter)).ConfigureAwait(false);
                RepetierPrinterConfig config = JsonConvert.DeserializeObject<RepetierPrinterConfig>(result.Result);
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
                result = await SendRestApiRequestAsync(currentPrinter, "setPrinterConfig", newConfig).ConfigureAwait(false);
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
        public async Task HomeAxesAsync(bool x, bool y, bool z)
        {
            try
            {
                if (x && y && z)
                {
                    _ = await SendGcodeCommandAsync("G28").ConfigureAwait(false);
                }
                else
                {
                    string cmd = string.Format("G28{0}{1}{2}", x ? " X0 " : "", y ? " Y0 " : "", z ? " Z0 " : "");
                    _ = await SendGcodeCommandAsync(cmd).ConfigureAwait(false);
                }
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
            }
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
                    await GetStateObjectAsync().ConfigureAwait(false);

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
                /*
                object parameter = new { 
                    x = Relative ? MathHelper.Clamp(State.X + X, shape.XMin, shape.XMax) : MathHelper.Clamp(X, shape.XMin, shape.XMax),  // X axis
                    y = Relative ? MathHelper.Clamp(State.Y + Y, shape.YMin, shape.YMax) : MathHelper.Clamp(Y, shape.YMin, shape.YMax),  // Y axis
                    z = Relative ? MathHelper.Clamp(State.Z + Z, shape.ZMin, shape.ZMax) : MathHelper.Clamp(Z, shape.ZMin, shape.ZMax),  // Z axis
                    e = E,  // Extruder
                    relative = Relative,
                    speed = Speed,
                };
                */
                RepetierApiRequestRespone result = await SendRestApiRequestAsync(currentPrinter, "move", data).ConfigureAwait(false);
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

                var result = await SendRestApiRequestAsync(currentPrinter, "setExtruderTemperature",
                    string.Format("{{\"temperature\":{0}, \"extruder\":{1}}}", temperature, extruder)).ConfigureAwait(false);
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

                var result = await SendRestApiRequestAsync(currentPrinter, "setBedTemperature",
                    string.Format("{{\"temperature\":{0}, \"bedId\":{1}}}", temperature, bedId)).ConfigureAwait(false);
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

                var result = await SendRestApiRequestAsync(currentPrinter, "setChamberTemperature",
                    string.Format("{{\"temperature\":{0}, \"chamberId\":{1}}}", temperature, chamberId)).ConfigureAwait(false);
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

                RepetierApiRequestRespone result = await SendRestApiRequestAsync(currentPrinter, "setFanSpeed",
                    string.Format("{{\"speed\":{0}, \"fanid\":{1}}}", SetSpeed, fanId)).ConfigureAwait(false);
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
#if NEW_API
                result = await SendRestApiRequestAsync(
                   commandBase: RepetierCommandBase.printer,
                   commandFeature: RepetierCommandFeature.api,
                   command: "listExternalCommands")
                    .ConfigureAwait(false);
#else
                result = await SendRestApiRequestAsync("", "listExternalCommands").ConfigureAwait(false);
#endif
                ExternalCommand[] cmds = JsonConvert.DeserializeObject<ExternalCommand[]>(result.Result);
                if (cmds == null)
                {
                    // Avoid exceptions for null values
                    cmds = new ExternalCommand[] { new ExternalCommand() };
                }
                return new ObservableCollection<ExternalCommand>(cmds);
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
                RepetierApiRequestRespone result = await SendRestApiRequestAsync("", "runExternalCommand", string.Format("{{\"id\":{0}}}", command.Id)).ConfigureAwait(false);
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

                var result = await SendRestApiRequestAsync(currentPrinter,
                    "removeMessage",
                    string.Format("{{\"id\":{0}, \"a\":\"{1}\"}}", message.Id, unPause ? "unpause" : "")
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
                result = await SendRestApiRequestAsync(currentPrinter, "messages", "").ConfigureAwait(false);
                RepetierMessage[] info = JsonConvert.DeserializeObject<RepetierMessage[]>(result.Result);
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

                var result = await SendRestApiRequestAsync(currentPrinter, "setFlowMultiply", string.Format("{{\"speed\":{0}}}", multiplier)).ConfigureAwait(false);
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

                var result = await SendRestApiRequestAsync(currentPrinter, "setSpeedMultiply", string.Format("{{\"speed\":{0}}}", speed)).ConfigureAwait(false);
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

                var result = await SendRestApiRequestAsync(currentPrinter, "emergencyStop").ConfigureAwait(false);
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
                var result = await SendRestApiRequestAsync(currentPrinter, "send", string.Format("{{\"cmd\":\"{0}\"}}", command)).ConfigureAwait(false);
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
                result = await SendRestApiRequestAsync(currentPrinter, "getScript", cmd).ConfigureAwait(false);
                RepetierGcodeScript script = JsonConvert.DeserializeObject<RepetierGcodeScript>(result.Result);
                return script;
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
                var result = await SendRestApiRequestAsync(currentPrinter, "setScript", cmd).ConfigureAwait(false);
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
                var result = await SendRestApiRequestAsync(currentPrinter, "runScript", cmd).ConfigureAwait(false);
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
                /*
                object cmd = new
                {
                    name = action.Name,
                    @params = new string[] { action.Question },
                };
                */
                string cmd = string.Format("{{\"name\":\"{0}\",\"params\":{1}}}", action.Name, JsonConvert.SerializeObject(new string[] { action.Question }));
                RepetierApiRequestRespone result = await SendRestApiRequestAsync(currentPrinter, "webCallExecute", cmd, timeout).ConfigureAwait(false);
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

                var result = await SendRestApiRequestAsync(currentPrinter, "webCallRemove", cmd).ConfigureAwait(false);
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
                var result = await GetWebCallActionsAsync().ConfigureAwait(false);
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
        public byte[] GetImageFromUri(string path, int timeout = 10000)
        {
            try
            {
                // https://www.repetier-server.com/manuals/programming/API/index.html
                RestClient client = new(FullWebAddress);
                RestRequest request = new(path)
                {
                    RequestFormat = DataFormat.None,
                    Method = Method.GET,
                    Timeout = timeout
                };

                request.AddParameter("apikey", API);
                Uri fullUrl = client.BuildUri(request);

                byte[] respone = client.DownloadData(request, true);
                return respone;
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return new byte[0];
            }
        }
        public byte[] GetFileFromUri(string path, int timeout = 10000)
        {
            try
            {
                // https://www.repetier-server.com/manuals/programming/API/index.html
                RestClient client = new(FullWebAddress);
                RestRequest request = new(path)
                {
                    RequestFormat = DataFormat.None,
                    Method = Method.GET,
                    Timeout = timeout
                };

                request.AddParameter("apikey", API);
                Uri fullUrl = client.BuildUri(request);

                byte[] respone = client.DownloadData(request, true);
                return respone;
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return new byte[0];
            }
        }

        public byte[] GetProjectImage(Guid server, Guid project, string preview, string action = "mthumb", int size = 1, int timeout = 10000)
        {
            try
            {
                // https://www.repetier-server.com/manuals/programming/API/index.html
                RestClient client = new(FullWebAddress);
                RestRequest request = new($"project/{server}/{action}/{project}/{preview}/")
                {
                    RequestFormat = DataFormat.None,
                    Method = Method.GET,
                    Timeout = timeout
                };

                request.AddParameter("apikey", API);
                request.AddParameter("v", size);
                Uri fullUrl = client.BuildUri(request);

                byte[] respone = client.DownloadData(request, true);
                return respone;
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return new byte[0];
            }
        }
        public byte[] GetProjectImage(Guid Server, RepetierProjectItem projectItem, string action = "mthumb", int size = 1, int timeout = 10000)
        {
            try
            {
                return GetProjectImage(Server, projectItem.Project.Uuid, projectItem.Project.Preview, action, size, timeout);
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
                result = await SendRestApiRequestAsync(printerName, "projectsListServer").ConfigureAwait(false);
                RepetierProjectsServerListRespone info = JsonConvert.DeserializeObject<RepetierProjectsServerListRespone>(result.Result);
                return info;
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

        public RepetierProjectsServerListRespone GetProjectsListServer(string printerName = "")
        {
            string result = string.Empty;
            try
            {
                result = SendRestAPIRequest(printerName, "projectsListServer");
                var info = JsonConvert.DeserializeObject<RepetierProjectsServerListRespone>(result);
                return info;
            }
            catch (JsonException jecx)
            {
                OnError(new RepetierJsonConvertEventArgs()
                {
                    Exception = jecx,
                    OriginalString = result,
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

                result = await SendRestApiRequestAsync(printerName, "projectsGetFolder", data).ConfigureAwait(false);
                RepetierProjectsFolderRespone info = JsonConvert.DeserializeObject<RepetierProjectsFolderRespone>(result.Result);
                return info;
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
        public RepetierProjectsFolderRespone GetProjectsGetFolder(Guid serverUuid, int index = 1, string printerName = "")
        {
            string result = string.Empty;
            try
            {
                object data = new
                {
                    serveruuid = serverUuid,
                    idx = index,
                };

                result = SendRestAPIRequest(printerName, "projectsGetFolder", data);
                RepetierProjectsFolderRespone info = JsonConvert.DeserializeObject<RepetierProjectsFolderRespone>(result);
                return info;
            }
            catch (JsonException jecx)
            {
                OnError(new RepetierJsonConvertEventArgs()
                {
                    Exception = jecx,
                    OriginalString = result,
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
                    byte[] emptyProject = GetImageFromUri("img/emptyproject.png");
                    byte[] folder = GetImageFromUri("img/folder_m.png");

                    foreach (RepetierProjectItem project in items)
                    {
                        if (!project.IsFolder && project.Project != null)
                        {
                            if (!string.IsNullOrEmpty(project.Project.Preview))
                                // Load image from server
                                project.PreviewImage = GetProjectImage(serverUuid, project);
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
                    return new ObservableCollection<RepetierProjectItem>();
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return new ObservableCollection<RepetierProjectItem>();
            }
        }
        public ObservableCollection<RepetierProjectItem> GetProjectItems(Guid serverUuid, int index = 1, string printerName = "")
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

                result = GetProjectsGetFolder(serverUuid, index, printerName);
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
                    byte[] emptyProject = GetImageFromUri("img/emptyproject.png");
                    byte[] folder = GetImageFromUri("img/folder_m.png");

                    foreach (RepetierProjectItem project in items)
                    {
                        if (!project.IsFolder && project.Project != null)
                        {
                            if (!string.IsNullOrEmpty(project.Project.Preview))
                                // Load image from server
                                project.PreviewImage = GetProjectImage(serverUuid, project);
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
                    return new ObservableCollection<RepetierProjectItem>();
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

                result = await SendRestApiRequestAsync(printerName, "projectsGetProject", data).ConfigureAwait(false);
                RepetierProjectsProjectRespone info = JsonConvert.DeserializeObject<RepetierProjectsProjectRespone>(result.Result);
                return info;
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
        public RepetierProjectsProjectRespone GetProjectsGetProject(Guid serverUuid, Guid projectUuid, string printerName = "")
        {
            string result = string.Empty;
            try
            {
                object data = new
                {
                    serveruuid = serverUuid,
                    uuid = projectUuid,
                };

                result = SendRestAPIRequest(printerName, "projectsGetProject", data);
                var info = JsonConvert.DeserializeObject<RepetierProjectsProjectRespone>(result);
                return info;
            }
            catch (JsonException jecx)
            {
                OnError(new RepetierJsonConvertEventArgs()
                {
                    Exception = jecx,
                    OriginalString = result,
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

                var result = await SendRestApiRequestAsync(printerName, "projectsUpdateProject", data).ConfigureAwait(false);
                return GetQueryResult(result.Result);
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return false;
            }
        }
        public bool UpdateProjectsGetProject(Guid serverUuid, RepetierProjectsProjectRespone project, string printerName = "")
        {
            string result;
            try
            {
                object data = new
                {
                    serveruuid = serverUuid,
                    project = project,
                };

                result = SendRestAPIRequest(printerName, "projectsUpdateProject", data);
                return GetQueryResult(result);
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
                        uriString = $"{FullWebAddress}/project/{serverUuid}/{action}/{file.ProjectUuid}/{file.Name}/?apikey={API}";
                        break;

                    case RepetierProjectFileType.Image:
                        uriString = $"{FullWebAddress}/project/{serverUuid}/image/{file.ProjectUuid}/{file.Name}/?apikey={API}&v=19";
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

                RepetierApiRequestRespone result = await SendRestApiRequestAsync(printerName, "projectsDeleteFile", data).ConfigureAwait(false);
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

                RepetierApiRequestRespone result = await SendRestApiRequestAsync(printerName, "projectDelComment", data).ConfigureAwait(false);
                return GetQueryResult(result.Result);
            }

            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return false;
            }
        }
        public bool DeleteProjectFileFromServer(Guid serverUuid, Guid projectUuid, RepetierProjectsProjectComment comment, string printerName = "")
        {
            string result;
            try
            {

                object data = new
                {
                    serveruuid = serverUuid,
                    projectuuid = projectUuid,
                    username = comment.User,
                    comment = comment.Comment,
                };

                result = SendRestAPIRequest(printerName, "projectDelComment", data);
                return GetQueryResult(result);
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
#if NEW_API
                result = await SendRestApiRequestAsync(
                   commandBase: RepetierCommandBase.printer,
                   commandFeature: RepetierCommandFeature.api,
                   jsonData: data,
                   command: "historyList",
                   printerName: currentPrinter
                   )
                    .ConfigureAwait(false);
#else
                result = await SendRestApiRequestAsync(currentPrinter, "historyList", data).ConfigureAwait(false);
#endif
                RepetierHistoryListRespone info = JsonConvert.DeserializeObject<RepetierHistoryListRespone>(result.Result);
                return info;
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

                result = await SendRestApiRequestAsync(currentPrinter, "historySummary", data).ConfigureAwait(false);
                RepetierHistorySummaryRespone info = JsonConvert.DeserializeObject<RepetierHistorySummaryRespone>(result.Result);
                return info;
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
        public byte[] GetHistoryReport(long reportId, string printerName = "")
        {
            try
            {
                if (string.IsNullOrEmpty(printerName))
                    printerName = GetActivePrinterSlug();
                byte[] report = GetFileFromUri($"{FullWebAddress}/printer/export/{printerName}?a=history_report&id={reportId}&apikey={API}");
                return report;
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return new byte[0];
            }
        }
        public async Task<ObservableCollection<RepetierHistorySummaryItem>> GetHistorySummaryItenmsAsync(string printerNameForHistory, int year, bool allPrinter = false)
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

                var result = await SendRestApiRequestAsync(printerName, "historyDeleteEntry", data).ConfigureAwait(false);
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
#if NEW_API
                result = await SendRestApiRequestAsync(
                   commandBase: RepetierCommandBase.printer,
                   commandFeature: RepetierCommandFeature.api,
                   command: "GPIOGetList",
                   printerName: currentPrinter)
                    .ConfigureAwait(false);
#else
                result = await SendRestApiRequestAsync(currentPrinter, "GPIOGetList").ConfigureAwait(false);
#endif
                RepetierGpioListRespone info = JsonConvert.DeserializeObject<RepetierGpioListRespone>(result.Result);
                return info;
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

#region Static
        public static string ConvertStackToPath(Stack<string> stack, string separator)
        {
            StringBuilder sb = new();
            for (int i = stack.Count - 1; i >= 0; i--)
            {
                sb.Append(stack.ElementAt(i));
                if (i > 0)
                    sb.Append(separator);
            }
            return sb.ToString();
        }
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
            if (obj is not RepetierServerPro item)
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
    }
}

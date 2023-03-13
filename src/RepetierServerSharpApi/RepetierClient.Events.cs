using AndreasReitberger.API.Repetier.Models;
using AndreasReitberger.API.Repetier.Events;
using System;
using System.IO;
using ErrorEventArgs = SuperSocket.ClientEngine.ErrorEventArgs;

namespace AndreasReitberger.API.Repetier
{
    public partial class RepetierClient
    {
        #region EventHandlerss
        #region Debug
        public event EventHandler<RepetierIgnoredJsonResultsChangedEventArgs> RepetierIgnoredJsonResultsChanged;
        protected virtual void OnRepetierIgnoredJsonResultsChanged(RepetierIgnoredJsonResultsChangedEventArgs e)
        {
            RepetierIgnoredJsonResultsChanged?.Invoke(this, e);
        }
        #endregion

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

        public event EventHandler<RepetierWebsocketEventArgs> WebSocketMessageReceived;
        protected virtual void OnWebSocketMessageReceived(RepetierWebsocketEventArgs e)
        {
            WebSocketMessageReceived?.Invoke(this, e);
        }

        public event EventHandler<RepetierWebsocketEventArgs> WebSocketDataReceived;
        protected virtual void OnWebSocketDataReceived(RepetierWebsocketEventArgs e)
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
        protected virtual void OnListeningChangedEvent(RepetierEventListeningChangedEventArgs e)
        {
            ListeningChanged?.Invoke(this, e);
        }

        public event EventHandler<RepetierEventSessionChangedEventArgs> SessionChanged;
        protected virtual void OnSessionChangedEvent(RepetierEventSessionChangedEventArgs e)
        {
            SessionChanged?.Invoke(this, e);
        }

        public event EventHandler<RepetierMessagesChangedEventArgs> MessagesChanged;
        protected virtual void OnMessagesChangedEvent(RepetierMessagesChangedEventArgs e)
        {
            MessagesChanged?.Invoke(this, e);
        }

        public event EventHandler<RepetierActivePrintInfosChangedEventArgs> PrintInfosChanged;
        protected virtual void OnPrintInfosChangedEvent(RepetierActivePrintInfosChangedEventArgs e)
        {
            PrintInfosChanged?.Invoke(this, e);
        }

        public event EventHandler<RepetierActivePrintInfoChangedEventArgs> PrintInfoChanged;
        protected virtual void OnPrintInfoChangedEvent(RepetierActivePrintInfoChangedEventArgs e)
        {
            PrintInfoChanged?.Invoke(this, e);
        }

        public event EventHandler<RepetierJobStartedEventArgs> JobsStarted;
        protected virtual void OnJobStarted(RepetierJobStartedEventArgs e)
        {
            JobsStarted?.Invoke(this, e);
        }

        public event EventHandler<RepetierJobsChangedEventArgs> JobsChanged;
        protected virtual void OnJobsChangedEvent(RepetierJobsChangedEventArgs e)
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

        public event EventHandler<RepetierHardwareInfoChangedEventArgs> HardwareInfoChanged;
        protected virtual void OnHardwareInfoChangedEvent(RepetierHardwareInfoChangedEventArgs e)
        {
            HardwareInfoChanged?.Invoke(this, e);
        }

        public event EventHandler<RepetierWifiChangedEventArgs> WifiChangedEvent;
        protected virtual void OnWifiChangedEvent(RepetierWifiChangedEventArgs e)
        {
            WifiChangedEvent?.Invoke(this, e);
        }

        public event EventHandler<RepetierPrinterConfigChangedEventArgs> RepetierPrinterConfigChanged;
        protected virtual void OnRepetierPrinterConfigChangedEvent(RepetierPrinterConfigChangedEventArgs e)
        {
            RepetierPrinterConfigChanged?.Invoke(this, e);
        }

        public event EventHandler<RepetierPrinterStateChangedEventArgs> RepetierPrinterStateChanged;
        protected virtual void OnRepetierPrinterStateChangedEvent(RepetierPrinterStateChangedEventArgs e)
        {
            RepetierPrinterStateChanged?.Invoke(this, e);
        }

        public event EventHandler<RepetierModelsChangedEventArgs> RepetierModelsChanged;
        protected virtual void OnRepetierModelsChangedEvent(RepetierModelsChangedEventArgs e)
        {
            RepetierModelsChanged?.Invoke(this, e);
        }

        public event EventHandler<RepetierModelGroupsChangedEventArgs> RepetierModelGroupsChanged;
        protected virtual void OnRepetierModelGroupsChangedEvent(RepetierModelGroupsChangedEventArgs e)
        {
            RepetierModelGroupsChanged?.Invoke(this, e);
        }

        public event EventHandler<RepetierJobListChangedEventArgs> RepetierJobListChanged;
        protected virtual void OnRepetierJobListChangedEvent(RepetierJobListChangedEventArgs e)
        {
            RepetierJobListChanged?.Invoke(this, e);
        }

        public event EventHandler<RepetierActivePrinterChangedEventArgs> ActivePrinterChanged;
        protected virtual void OnActivePrinterChangedEvent(RepetierActivePrinterChangedEventArgs e)
        {
            ActivePrinterChanged?.Invoke(this, e);
        }

        public event EventHandler<RepetierWebCallActionsChangedEventArgs> WebCallActionsChanged;
        protected virtual void OnWebCallActionsChangedEvent(RepetierWebCallActionsChangedEventArgs e)
        {
            WebCallActionsChanged?.Invoke(this, e);
        }
        #endregion

        #region Jobs & Queue
        public event EventHandler<RepetierCurrentPrintImageChangedEventArgs> RepetierCurrentPrintImageChanged;
        protected virtual void OnRepetierCurrentPrintImageChanged(RepetierCurrentPrintImageChangedEventArgs e) {
            RepetierCurrentPrintImageChanged?.Invoke(this, e);
        }
        #endregion

        #endregion
    }
}

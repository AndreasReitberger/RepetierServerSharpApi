using AndreasReitberger.API.Repetier.Events;
using AndreasReitberger.API.Repetier.Models;
using System;

namespace AndreasReitberger.API.Repetier
{
    public partial class RepetierClient
    {
        #region EventHandlers

        #region ServerStateChanges

        [Obsolete("Use SessionChanged from Core instead")]
        public new event EventHandler<RepetierEventSessionChangedEventArgs>? SessionChanged;
        [Obsolete("Use OnSessionChanged from Core instead")]
        protected virtual void OnSessionChangedEvent(RepetierEventSessionChangedEventArgs e)
        {
            SessionChanged?.Invoke(this, e);
        }

        public event EventHandler<RepetierMessagesChangedEventArgs>? MessagesChanged;
        protected virtual void OnMessagesChangedEvent(RepetierMessagesChangedEventArgs e)
        {
            MessagesChanged?.Invoke(this, e);
        }

        public event EventHandler<RepetierActivePrintInfosChangedEventArgs>? PrintInfosChanged;
        protected virtual void OnPrintInfosChangedEvent(RepetierActivePrintInfosChangedEventArgs e)
        {
            PrintInfosChanged?.Invoke(this, e);
        }

        public new event EventHandler<RepetierJobStartedEventArgs>? JobsStarted;
        protected virtual void OnJobStarted(RepetierJobStartedEventArgs e)
        {
            JobsStarted?.Invoke(this, e);
        }

        [Obsolete("Use JobsChanged from Core instead")]
        public new event EventHandler<RepetierJobsChangedEventArgs>? JobsChanged;
        [Obsolete("Use OnJobsChanged from Core instead")]
        protected virtual void OnJobsChangedEvent(RepetierJobsChangedEventArgs e)
        {
            JobsChanged?.Invoke(this, e);
        }

        [Obsolete("Use JobFinished from Core instead")]
        public new event EventHandler<RepetierJobFinishedEventArgs>? JobFinished;
        [Obsolete("Use OnJobFinished from Core instead")]
        protected virtual void OnJobFinished(RepetierJobFinishedEventArgs e)
        {
            JobFinished?.Invoke(this, e);
        }

        public event EventHandler<RepetierHardwareInfoChangedEventArgs>? HardwareInfoChanged;
        protected virtual void OnHardwareInfoChangedEvent(RepetierHardwareInfoChangedEventArgs e)
        {
            HardwareInfoChanged?.Invoke(this, e);
        }

        public event EventHandler<RepetierWifiChangedEventArgs>? WifiChangedEvent;
        protected virtual void OnWifiChangedEvent(RepetierWifiChangedEventArgs e)
        {
            WifiChangedEvent?.Invoke(this, e);
        }

        public event EventHandler<RepetierPrinterConfigChangedEventArgs>? RepetierPrinterConfigChanged;
        protected virtual void OnRepetierPrinterConfigChangedEvent(RepetierPrinterConfigChangedEventArgs e)
        {
            RepetierPrinterConfigChanged?.Invoke(this, e);
        }

        public event EventHandler<RepetierPrinterStateChangedEventArgs>? RepetierPrinterStateChanged;
        protected virtual void OnRepetierPrinterStateChangedEvent(RepetierPrinterStateChangedEventArgs e)
        {
            RepetierPrinterStateChanged?.Invoke(this, e);
        }

        [Obsolete("Use GcodesChanged instead")]
        public event EventHandler<RepetierModelsChangedEventArgs>? RepetierModelsChanged;
        [Obsolete("Use OnGcodesChanged instead")]
        protected virtual void OnRepetierModelsChangedEvent(RepetierModelsChangedEventArgs e)
        {
            RepetierModelsChanged?.Invoke(this, e);
        }

        [Obsolete("Use GcodeGroupsChanged instead")]
        public event EventHandler<RepetierModelGroupsChangedEventArgs>? RepetierModelGroupsChanged;
        [Obsolete("Use OnGcodeGroupsChanged instead")]
        protected virtual void OnRepetierModelGroupsChangedEvent(RepetierModelGroupsChangedEventArgs e)
        {
            RepetierModelGroupsChanged?.Invoke(this, e);
        }

        [Obsolete("Use JobListChanged instead")]
        public event EventHandler<RepetierJobListChangedEventArgs>? RepetierJobListChanged;
        [Obsolete("Use OnJobListChanged instead")]
        protected virtual void OnRepetierJobListChangedEvent(RepetierJobListChangedEventArgs e)
        {
            RepetierJobListChanged?.Invoke(this, e);
        }

        public event EventHandler<RepetierWebCallActionsChangedEventArgs>? WebCallActionsChanged;
        protected virtual void OnWebCallActionsChangedEvent(RepetierWebCallActionsChangedEventArgs e)
        {
            WebCallActionsChanged?.Invoke(this, e);
        }
        #endregion

        #region Jobs & Queue

        [Obsolete("Use ActivePrintImageChanged instead")]
        public event EventHandler<RepetierCurrentPrintImageChangedEventArgs>? RepetierCurrentPrintImageChanged;
        [Obsolete("Use OnActivePrintImageChanged instead")]
        protected virtual void OnRepetierCurrentPrintImageChanged(RepetierCurrentPrintImageChangedEventArgs e)
        {
            RepetierCurrentPrintImageChanged?.Invoke(this, e);
        }
        #endregion

        #endregion
    }
}

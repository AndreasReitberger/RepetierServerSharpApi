using AndreasReitberger.Core.Utilities;
using Newtonsoft.Json;

namespace AndreasReitberger.Models
{
    public partial class RepetierPrinter : BaseModel
    {
        #region Properties
        [JsonProperty("active")]
        bool _active;
        [JsonIgnore]
        public bool Active
        {
            get { return _active; }
            set { SetProperty(ref _active, value); }
        }

        [JsonProperty("job")]
        string _job = string.Empty;
        [JsonIgnore]
        public string Job
        {
            get { return _job; }
            set { SetProperty(ref _job, value); }
        }

        [JsonProperty("name")]
        string _name = string.Empty;
        [JsonIgnore]
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        [JsonProperty("online")]
        long _online;
        [JsonIgnore]
        public long Online
        {
            get { return _online; }
            set { SetProperty(ref _online, value); }
        }

        [JsonProperty("pauseState")]
        long _pauseState;
        [JsonIgnore]
        public long PauseState
        {
            get { return _pauseState; }
            set { SetProperty(ref _pauseState, value); }
        }

        [JsonProperty("paused")]
        bool _paused;
        [JsonIgnore]
        public bool Paused
        {
            get { return _paused; }
            set { SetProperty(ref _paused, value); }
        }

        [JsonProperty("slug")]
        string _slug = string.Empty;
        [JsonIgnore]
        public string Slug
        {
            get { return _slug; }
            set { SetProperty(ref _slug, value); }
        }

        [JsonIgnore]
        double? _extruder1 = 0;
        [JsonIgnore]
        public double? Extruder1
        {
            get { return _extruder1; }
            set { SetProperty(ref _extruder1, value); }
        }

        [JsonIgnore]
        double? _extruder2 = 0;
        [JsonIgnore]
        public double? Extruder2
        {
            get { return _extruder2; }
            set { SetProperty(ref _extruder2, value); }
        }

        [JsonIgnore]
        double? _heatedBed = 0;
        [JsonIgnore]
        public double? HeatedBed
        {
            get { return _heatedBed; }
            set { SetProperty(ref _heatedBed, value); }
        }

        [JsonIgnore]
        double? _chamber = 0;
        [JsonIgnore]
        public double? Chamber
        {
            get { return _chamber; }
            set { SetProperty(ref _chamber, value); }
        }

        [JsonIgnore]
        double _progress = 0;
        [JsonIgnore]
        public double Progress
        {
            get { return _progress; }
            set { SetProperty(ref _progress, value); }
        }

        [JsonIgnore]
        double _remainingPrintTime = 0;
        [JsonIgnore]
        public double RemainingPrintTime
        {
            get { return _remainingPrintTime; }
            set { SetProperty(ref _remainingPrintTime, value); }
        }

        [JsonIgnore]
        bool _isPrinting = false;
        [JsonIgnore]
        public bool IsPrinting
        {
            get { return _isPrinting; }
            set { SetProperty(ref _isPrinting, value); }
        }
        [JsonIgnore]
        bool _isPaused = false;
        [JsonIgnore]
        public bool IsPaused
        {
            get { return _isPaused; }
            set { SetProperty(ref _isPaused, value); }
        }
        [JsonIgnore]
        bool _isSelected = false;
        [JsonIgnore]
        public bool IsSelected
        {
            get { return _isSelected; }
            set { SetProperty(ref _isSelected, value); }
        }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public override bool Equals(object obj)
        {
            if (obj is not RepetierPrinter item)
                return false;
            return Slug.Equals(item.Slug);
        }

        public override int GetHashCode()
        {
            return Slug.GetHashCode();
        }
        #endregion
    }
}

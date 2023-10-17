using AndreasReitberger.API.Repetier.Models;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier
{
    public partial class RepetierClient
    {
#if DEBUG
        #region Debug
        public static JsonSerializerSettings JsonSerializerSettings = new()
        {
            // Detect if the json respone has more or less properties than the target class
            //MissingMemberHandling = MissingMemberHandling.Error,
            MissingMemberHandling = MissingMemberHandling.Ignore,
            NullValueHandling = NullValueHandling.Include,
        };
        #endregion
#else
        #region Release
        public static JsonSerializerSettings JsonSerializerSettings = new()
        {
            // Ignore if the json respone has more or less properties than the target class
            MissingMemberHandling = MissingMemberHandling.Ignore,          
            NullValueHandling = NullValueHandling.Ignore,
        };
        #endregion
#endif
        #region Methods

#nullable enable
        public T? GetObjectFromJson<T>(string json, JsonSerializerSettings? serializerSettings = null)
        {
            try
            {
                // Workaround
                // The HttpClient on net7-android seems to missing the char for the json respone
                // Seems to be only on a specific simulator, further investigation
#if DEBUG
                if ((json?.StartsWith("{") ?? false) && (!json?.EndsWith("}") ?? false))
                {
                    //json += $"}}"; 
                }
                else if ((json?.StartsWith("[") ?? false) && (!json?.EndsWith("]") ?? false))
                {
                    //json += $"]";
                }
#endif
                return JsonConvert.DeserializeObject<T?>(json, serializerSettings ?? JsonSerializerSettings);
            }
            catch(JsonSerializationException jexc)
            {
                OnError(new RepetierJsonConvertEventArgs()
                {
                    Exception = jexc,
                    OriginalString = json,
                    Message = jexc?.Message,
                    TargetType = nameof(T)
                });
                return default;
            }
        }
#nullable disable
        #endregion
    }
}

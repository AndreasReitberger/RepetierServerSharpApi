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
            MissingMemberHandling = MissingMemberHandling.Error,
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

using System.Net.Http;

namespace AndreasReitberger.API.Repetier
{
    public partial class RepetierClient
    {
        #region Properties

        [ObservableProperty]
        HttpClient? httpClient;

        #endregion
    }
}

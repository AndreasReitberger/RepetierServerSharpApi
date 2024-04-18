using AndreasReitberger.API.Print3dServer.Core.Interfaces;
using AndreasReitberger.API.Repetier.Enum;
using AndreasReitberger.API.Repetier.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace AndreasReitberger.API.Repetier
{
    public partial class RepetierClient
    {
        #region Methods
        [Obsolete("Use GetDefaultWebCamUri instead or change to call base method")]
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
        [Obsolete("Use GetDefaultWebCamUri instead or change to call base method")]
        public async Task<string> GetWebCamUriAsync(int camIndex = 0, RepetierWebcamType type = RepetierWebcamType.Dynamic)
        {
            try
            {
                string currentPrinter = GetActivePrinterSlug();
                if (string.IsNullOrEmpty(currentPrinter)) return string.Empty;

                await RefreshPrinterConfigAsync();
                return $"{FullWebAddress}/printer/{(type == RepetierWebcamType.Dynamic ? "cammjpg" : "camjpg")}/{currentPrinter}?cam={camIndex}&apikey={ApiKey}";
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return "";
            }
        }
        public async Task<IWebCamConfig?> GetWebCamConfigAsync(int camIndex = 0, bool refreshConfigs = true)
        {
            try
            {
                string currentPrinter = GetActivePrinterSlug();
                if (string.IsNullOrEmpty(currentPrinter)) return null;

                if (refreshConfigs)
                {
                    await RefreshPrinterConfigAsync();
                }

                return WebCams?.FirstOrDefault(webCam => webCam.Position == camIndex);
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return null;
            }
        }

        public override Task<List<IWebCamConfig>?> GetWebCamConfigsAsync() => GetWebCamConfigsAsync(GetActivePrinterSlug());

        public async Task<List<IWebCamConfig>?> GetWebCamConfigsAsync(string slug)
        {
            try
            {
                RepetierPrinterConfig? config = await GetPrinterConfigAsync(slug);
                return [.. config?.Webcams];
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return null;
            }
        }
        public async Task<IWebCamConfig?> GetWebCamConfigAsync(string slug, int camIndex = 0)
        {
            try
            {
                RepetierPrinterConfig? config = await GetPrinterConfigAsync(slug);
                return config?.Webcams?.FirstOrDefault(webCam => webCam.Position == camIndex);
            }
            catch (Exception exc)
            {
                OnError(new UnhandledExceptionEventArgs(exc, false));
                return null;
            }
        }
        #endregion
    }
}

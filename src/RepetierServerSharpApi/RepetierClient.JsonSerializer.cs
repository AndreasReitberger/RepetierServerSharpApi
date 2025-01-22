using AndreasReitberger.API.Print3dServer.Core.Interfaces;
using AndreasReitberger.API.Print3dServer.Core.JSON.System;
using AndreasReitberger.API.Repetier.Models;
using AndreasReitberger.API.REST;
using AndreasReitberger.API.REST.Interfaces;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AndreasReitberger.API.Repetier
{
    public partial class RepetierClient
    {

#if DEBUG
        #region Debug

        public new static JsonSerializerOptions DefaultJsonSerializerSettings = new()
        {
            // Detect if the json respone has more or less properties than the target class
            //MissingMemberHandling = MissingMemberHandling.Error,
            ReferenceHandler = ReferenceHandler.Preserve,
            WriteIndented = true,
            Converters = { 
                // Map the converters
                new TypeMappingConverter<IAuthenticationHeader, AuthenticationHeader>(),
                new TypeMappingConverter<IGcode, RepetierModel>(),
                new TypeMappingConverter<IGcodeGroup, RepetierModelGroup>(),
                new TypeMappingConverter<IPrint3dJob, RepetierJobListItem>(),
                new TypeMappingConverter<IToolhead, RepetierPrinterToolhead>(),
                new TypeMappingConverter<IHeaterComponent, RepetierPrinterHeaterComponent>(),
                new TypeMappingConverter<IPrint3dTemperatureInfo, EventTempData>(),
                new TypeMappingConverter<IPrint3dJobStatus, RepetierCurrentPrintInfo>(),
            }
        };
        #endregion
#else
        #region Release
        public new static JsonSerializerOptions DefaultJsonSerializerSettings = new()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            WriteIndented = true,
            Converters =
            {                     
                // Map the converters
                new TypeMappingConverter<IAuthenticationHeader, AuthenticationHeader>(),
                new TypeMappingConverter<IGcode, RepetierModel>(),
                new TypeMappingConverter<IGcodeGroup, RepetierModelGroup>(),
                new TypeMappingConverter<IPrint3dJob, RepetierJobListItem>(),
                new TypeMappingConverter<IToolhead, RepetierPrinterToolhead>(),
                new TypeMappingConverter<IHeaterComponent, RepetierPrinterHeaterComponent>(),
                new TypeMappingConverter<IPrint3dTemperatureInfo, EventTempData>(),
                new TypeMappingConverter<IPrint3dJobStatus, RepetierCurrentPrintInfo>(),
            }
        };
        #endregion
#endif
    }
}

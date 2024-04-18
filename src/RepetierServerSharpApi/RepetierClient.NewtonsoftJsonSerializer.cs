﻿using AndreasReitberger.API.Repetier.Models;
using AndreasReitberger.API.Print3dServer.Core;
using AndreasReitberger.API.Print3dServer.Core.Interfaces;
using AndreasReitberger.API.Print3dServer.Core.JSON.Newtonsoft;
using Newtonsoft.Json;

namespace AndreasReitberger.API.Repetier
{
    public partial class RepetierClient
    {

#if DEBUG
        #region Debug

        [ObservableProperty]
        [property: JsonIgnore, System.Text.Json.Serialization.JsonIgnore, XmlIgnore]
        JsonSerializerSettings newtonsoftJsonSerializerSettings = DefaultNewtonsoftJsonSerializerSettings;

        public new static JsonSerializerSettings DefaultNewtonsoftJsonSerializerSettings = new()
        {
            // Detect if the json respone has more or less properties than the target class
            //MissingMemberHandling = MissingMemberHandling.Error,
            MissingMemberHandling = MissingMemberHandling.Ignore,
            NullValueHandling = NullValueHandling.Include,
            TypeNameHandling = TypeNameHandling.Auto,
            Converters =
            {
                // Map the converters
                new AbstractConverter<AuthenticationHeader, IAuthenticationHeader>(),
                new AbstractConverter<RepetierModel, IGcode>(),
                new AbstractConverter<RepetierModelGroup, IGcodeGroup>(),
                new AbstractConverter<RepetierJobListItem, IPrint3dJob>(),
                new AbstractConverter<RepetierPrinterToolhead, IToolhead>(),
                new AbstractConverter<RepetierPrinterHeaterComponent, IHeaterComponent>(),
            }
        };
        #endregion
#else
        #region Release
        public new static JsonSerializerSettings DefaultNewtonsoftJsonSerializerSettings = new()
        {
            // Ignore if the json respone has more or less properties than the target class
            MissingMemberHandling = MissingMemberHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore,
            TypeNameHandling = TypeNameHandling.Auto,
            Converters =
            {
                // Map the converters
                new AbstractConverter<KlipperGcodeMetaResult, IGcodeMeta>(),
                new AbstractConverter<KlipperGcodeThumbnail, IGcodeImage>(),
                new AbstractConverter<KlipperJobQueueItem, IPrint3dJob>(),
                new AbstractConverter<AuthenticationHeader, IAuthenticationHeader>(),
                new AbstractConverter<RepetierPrinterToolhead, IToolhead>(),
                new AbstractConverter<RepetierPrinterHeaterComponent, IHeaterComponent>(),
            }
        };
        #endregion
#endif
    }
}

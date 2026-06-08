using AndreasReitberger.API.Print3dServer.Core.Interfaces;
using AndreasReitberger.API.Print3dServer.Core.JSON.System;
using AndreasReitberger.API.Repetier.Models;

namespace AndreasReitberger.API.Repetier.TypeConverters
{
    public class RepetierPrintInfoConverter : TypeMappingConverter<IPrint3dJobStatus, RepetierCurrentPrintInfo>
    {
    }
}

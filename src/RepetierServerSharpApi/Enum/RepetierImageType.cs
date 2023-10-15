using System;

namespace AndreasReitberger.API.Repetier.Enum
{
    [Obsolete("Use GcodeImageType from Core lib")]
    public enum RepetierImageType
    {
        None,
        Thumbnail,
        Image,
        Both,
    }
}

using System;

namespace AndreasReitberger.API.Repetier.Enum
{
    [Obsolete("Use Printer3dToolHeadState instead")]
    public enum RepetierToolState
    {
        Idle = 0,
        Heating = 1,
        Ready = 2,

        Error = 99,
    }
}

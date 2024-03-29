﻿using AndreasReitberger.API.Print3dServer.Core.Interfaces;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Repetier.Models
{
    [Obsolete("Replace with RestApiRequestRespone")]
    public class RepetierApiRequestRespone// : IRestApiRequestRespone
    {
        #region Properties
        public string Result { get; set; } = string.Empty;
        public bool IsOnline { get; set; } = false;
        public bool Succeeded { get; set; } = false;
        public bool HasAuthenticationError { get; set; } = false;

        public RepetierRestEventArgs EventArgs { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}

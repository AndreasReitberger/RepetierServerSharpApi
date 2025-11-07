namespace AndreasReitberger.API.Repetier.Structs
{
    public struct RepetierCommands
    {
        public const string Base = "printer";
        public const string Api = "api";
        public const string Ping = "ping";
        public const string Info = "info";
        public const string LayerAnalysis = "layeranalysis";
        public const string List = "list";
        public const string Layer = "layer";
        public const string Gcode = "gcode";
        public const string Log = "log";
        public const string Model = "model";
        public const string PConfig = "pconfig";
        public const string Export = "export";

        #region Ctor
        public RepetierCommands() { }
        #endregion
    }
}

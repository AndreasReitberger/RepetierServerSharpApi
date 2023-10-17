namespace AndreasReitberger.API.Repetier.Structs
{
    public struct RepetierCommands
    {
        public static string Base = "printer";

        public static string Api = "api";
        public static string Ping = "ping";
        public static string Info = "info";
        public static string LayerAnalysis = "layeranalysis";
        public static string List = "list";
        public static string Layer = "layer";
        public static string Gcode = "gcode";
        public static string Log = "log";
        public static string Model = "model";
        public static string PConfig = "pconfig";
        public static string Export = "export";

        #region Ctor
        public RepetierCommands() { }
        #endregion
    }
}

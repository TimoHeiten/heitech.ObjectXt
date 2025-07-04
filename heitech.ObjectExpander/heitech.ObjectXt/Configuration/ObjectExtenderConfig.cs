namespace heitech.ObjectXt.Configuration
{
    /// <summary>
    /// Global Settings for ObjectXt
    /// </summary>
    public static class ObjectExtenderConfig
    {
        internal static bool IgnoreException { get; private set; }
        internal static bool IsTypeSpecific { get; private set; }

        /// <summary>
        /// Stops from throwing if attr cannot be invoked (key not present, incorrect type etc.) Silence!
        /// </summary>
        /// <param name="val"></param>
        public static void IgnoreExceptions(bool val) => IgnoreException = val;
        /// <summary>
        /// Configure a nested attribute map, where each map is specific to a given type. Default is one large Map for all Types
        /// </summary>
        public static void ConfigureTypeSpecific() => IsTypeSpecific = true;
    }
}

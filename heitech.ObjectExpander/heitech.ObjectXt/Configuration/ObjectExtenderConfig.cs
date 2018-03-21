using System;

namespace heitech.ObjectXt.Configuration
{
    public static class ObjectExtenderConfig
    {
        internal static bool IgnoreException { get; private set; }
        internal static bool IsTypeSpecific { get; private set; }

        public static void IgnoreExceptions(bool val) => IgnoreException = val;
        public static void ConfigureTypeSpecific() => IsTypeSpecific = true;
    }
}

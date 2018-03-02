using System;

namespace heitech.ObjectExpander.Configuration
{
    public static class ObjectExtenderConfig
    {
        internal static bool IgnoreException { get; private set; }

        public static void ConfigureTypeSpecific(this object obj)
        {
            throw new NotImplementedException();
            TypeSpecific = true;
            //AttributeFactory.SetMap(() => new TypeSpecificAttributeMap());
        }

        internal static bool TypeSpecific { get; private set; }

        public static void IgnoreExceptions(bool val)
        {
            IgnoreException = val;
        }
    }
}

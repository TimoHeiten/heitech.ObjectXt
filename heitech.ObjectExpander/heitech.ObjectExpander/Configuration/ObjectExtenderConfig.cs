using System;

namespace heitech.ObjectExpander.Configuration
{
    public static class ObjectExtenderConfig
    {
        internal static bool IgnoreException { get; private set; }

        public static void ConfigureTypeSpecific(this object obj)
        {
            throw new NotImplementedException();
            //AttributeFactory.SetMap(() => new TypeSpecificAttributeMap());
        }

        public static void IgnoreExceptions(bool val)
        {
            IgnoreException = val;
        }
    }
}

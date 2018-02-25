using heitech.ObjectExpander.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace heitech.ObjectExpander.ExtensionMap
{
    internal class GlobalAttributeMap : IAttributeMap
    {
        protected Dictionary<object, IExtensionAttribute> MappedAttributes { get; } = new Dictionary<object, IExtensionAttribute>();

        public void Add<TKey>(object extended, TKey key, IExtensionAttribute func)
        {
            // in global extended object is not of interest
            if (!MappedAttributes.ContainsKey(key))
                MappedAttributes.Add(key, func);
            else throw new AlreadyExtendedException(key);
        }

        public bool CanInvoke<TKey>(TKey key, Type returnType = null, params object[] parameters)
            => MappedAttributes.Any(x => x.Value.CanInvoke(key, returnType, parameters));

        public bool HasKey<TKey>(TKey key)
            => MappedAttributes.ContainsKey(key);

        public object Invoke<TKey>(TKey key, params object[] parameters)
        {
            if (MappedAttributes.TryGetValue(key, out IExtensionAttribute attr))
                return attr.Invoke(parameters);
            else return null;
        }

        internal class AlreadyExtendedException : Exception
        {
            public object ExtendedWithKey { get; }
            public AlreadyExtendedException(object extendedWithKey)
                : this("", extendedWithKey)
            { }

            public AlreadyExtendedException(string message, object extendedWithKey) : base(message)
            {
                ExtendedWithKey = extendedWithKey;
            }
        }
    }
}

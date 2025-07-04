using heitech.ObjectXt.Interfaces;
using System;
using System.Collections.Generic;

namespace heitech.ObjectXt.ExtensionMap
{
    internal class TypeSpecificAttributeMap : IAttributeMap
    {
        readonly Func<IAttributeMap> factory;
        protected Dictionary<Type, IAttributeMap> dictionary = new Dictionary<Type, IAttributeMap>();

        internal TypeSpecificAttributeMap(Func<IAttributeMap> factory)
            => this.factory = factory;

        public void Add<TKey>(object extended, TKey key, IExtensionAttribute func)
        {
            var type = extended.GetType();
            if (dictionary.TryGetValue(type, out IAttributeMap map))
            {
                map.Add(extended, key, func);
            }
            else
            {
                IAttributeMap nested = factory();
                dictionary.Add(type, nested);
                nested.Add(extended, key, func);
            }
        }

        public bool CanInvoke<TKey>(object extended, TKey key, Type expectedReturnType, params object[] parameters)
        {
            Type type = extended.GetType();
            if (dictionary.TryGetValue(type, out IAttributeMap map)) 
                return map.CanInvoke(extended, key, expectedReturnType, parameters);
            
            return false;
        }

        public bool HasKey<TKey>(object extended, TKey key)
        {
            Type type = extended.GetType();
            if (dictionary.TryGetValue(type, out IAttributeMap map)) 
                return map.HasKey(extended, key);
            
            return false;
        }

        public object Invoke<TKey>(object extended, TKey key, params object[] parameters)
        {
            var type = extended.GetType();
            if (dictionary.TryGetValue(type, out IAttributeMap map)) 
                return map.Invoke(extended, key, parameters);
            
            throw new AttributeNotFoundException($"key on {extended.GetType().Name} not found");
        }
    }
}

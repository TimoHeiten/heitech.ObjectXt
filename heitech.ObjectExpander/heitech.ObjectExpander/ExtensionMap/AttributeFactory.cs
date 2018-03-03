using heitech.ObjectExpander.ExtensionMap.Attributes;
using heitech.ObjectExpander.Interfaces;
using System;

namespace heitech.ObjectExpander.ExtensionMap
{
    internal class AttributeFactory : IAttributeFactory
    {
        private readonly IAttributeMap map;

        private AttributeFactory(IAttributeMap map)
        {
            this.map = map;
        }

        public IAttributeMap GetMap() => map;

        internal static IAttributeFactory Create(IAttributeMap map) => new AttributeFactory(map);

        internal static IAttributeFactory CreateGlobal() => new AttributeFactory(new GlobalAttributeMap());

        // Actions
// ##################################################################################################
        public IExtensionAttribute CreateActionAttribute<TKey>(TKey key, Action a)
            => new ActionAttribute(key, a);

        public IExtensionAttribute CreateActionAttribute<TKey, T>(TKey key, Action<T> a)
            => new ActionParamAttribute<T>(key, a);

        public IExtensionAttribute CreateActionAttribute<TKey, T, T2>(TKey key, Action<T, T2> a)
            => new ActionDualParameter<T, T2>(key, a);


        // Funcs
// ################################################################################################## 
        public IExtensionAttribute CreateFuncAttribute<TKey, TResult>(TKey key,
            Func<TResult> func)
            => new FuncAttribute<TResult>(key, func);

        public IExtensionAttribute CreateFuncAttribute<TKey, TResult, TParam>(TKey key,
            Func<TParam, TResult> func)
            => new FuncAttributeParam<TResult, TParam>(key, func);

        public IExtensionAttribute CreateFuncAttribute<TKey, TResult, TParam, TParam2>(
            TKey key, Func<TParam, TParam2, TResult> func)
            => new FuncAttributeDualParam<TResult, TParam, TParam2>(key, func);

        
    }
}

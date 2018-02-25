using heitech.ObjectExpander.ExtensionMap.Attributes;
using heitech.ObjectExpander.Interfaces;
using System;

namespace heitech.ObjectExpander.ExtensionMap
{
    internal static class AttributeFactory
    {
        private static Func<IAttributeMap> injectedMap;
        internal static void SetMap(Func<IAttributeMap> func) => injectedMap = func;

        internal static IAttributeMap CreateMap()
        {
            if (injectedMap != null) return injectedMap();
            else return new GlobalAttributeMap();
        }


        // Actions
// ##################################################################################################
        internal static IExtensionAttribute CreateActionAttribute<TKey>(TKey key, Action a)
            => new ActionAttribute(key, a);

        internal static IExtensionAttribute CreateActionAttribute<TKey, T>(TKey key, Action<T> a)
            => new ActionParamAttribute<T>(key, a);

        internal static IExtensionAttribute CreateActionAttribute<TKey, T, T2>(TKey key, Action<T, T2> a)
            => new ActionDualParameter<T, T2>(key, a);


        // Funcs
// ################################################################################################## 
        internal static IExtensionAttribute CreateFuncAttribute<TKey, TResult>(TKey key,
            Func<TResult> func)
            => new FuncAttribute<TResult>(key, func);

        internal static IExtensionAttribute CreateFuncAttribute<TKey, TResult, TParam>(TKey key,
            Func<TParam, TResult> func)
            => new FuncAttributeParam<TResult, TParam>(key, func);

        internal static IExtensionAttribute CreateFuncAttribute<TKey, TResult, TParam, TParam2>(
            TKey key, Func<TParam, TParam2, TResult> func)
            => new FuncAttributeDualParam<TResult, TParam, TParam2>(key, func);

        
    }
}

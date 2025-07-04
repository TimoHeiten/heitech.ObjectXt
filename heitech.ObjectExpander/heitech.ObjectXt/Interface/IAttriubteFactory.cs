using System;

namespace heitech.ObjectXt.Interfaces
{
    internal interface IAttributeFactory
    {
        IAttributeMap GetMap();
        IExtensionAttribute CreateActionAttribute<TKey>(TKey key, Action a);
        IExtensionAttribute CreateActionAttribute<TKey, T>(TKey key, Action<T> a);
        IExtensionAttribute CreateActionAttribute<TKey, T, T2>(TKey key, Action<T, T2> a);

        IExtensionAttribute CreateFuncAttribute<TKey, TResult>(TKey key, Func<TResult> func);
        IExtensionAttribute CreateFuncAttribute<TKey, TResult, TParam>(TKey key, Func<TParam, TResult> func);
        IExtensionAttribute CreateFuncAttribute<TKey, TResult, TParam, TParam2>(TKey key, Func<TParam, TParam2, TResult> func);
    }
}

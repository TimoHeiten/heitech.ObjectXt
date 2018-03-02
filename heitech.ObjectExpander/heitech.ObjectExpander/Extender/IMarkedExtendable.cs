using System;
using heitech.ObjectExpander.Interfaces;
using static heitech.ObjectExpander.ExtensionMap.AttributeFactory;

namespace heitech.ObjectExpander.Extender
{
    /// <summary>
    /// only a Marker for extensibility
    /// . objectExtension convoluted intellisense too much
    /// </summary>
    public interface IMarkedExtendable
    { }

    internal class MarkedExtendable : IMarkedExtendable
    {
        private readonly object origin;
        private readonly Type originType;

        private readonly IAttributeMap map;

        internal MarkedExtendable(IAttributeMap map, object origin)
        {
            this.origin = origin;
            originType = origin.GetType();
            this.map = map;
        }

        internal void Register<TKey>(TKey key, Action action)
            => map.Add(origin, key, CreateActionAttribute(key, action));

        internal void Register<TKey, TParam>(TKey key, Action<TParam> action)
            => map.Add(origin, key, CreateActionAttribute(key, action));


        internal void Register<TKey, TParam, TParam2>(TKey key, Action<TParam, TParam2> action)
            => map.Add(origin, key, CreateActionAttribute(key, action));

        internal void RegisterFunc<TKey, TResult>(TKey key, Func<TResult> func)
            => map.Add(origin, key, CreateFuncAttribute(key, func));

        internal void RegisterFunc<TKey, TResult, TParam>(TKey key, Func<TParam, TResult> func)
            => map.Add(origin, key, CreateFuncAttribute(key, func));

        internal void RegisterFunc<TKey, TResult, TParam, TParam2>(TKey key, Func<TResult, TParam, TParam2> func)
            => map.Add(origin, key, CreateFuncAttribute(key, func));

        // todo async.

        internal void CallAction<Tkey>(Tkey key)
            => map.Invoke(key);

        internal void CallAction<Tkey, TParam>(Tkey key, TParam param)
            => map.Invoke(key, new object[] { param });
        internal void CallAction<Tkey, TParam, TParam1>(Tkey key, TParam param, TParam1 param1)
            => map.Invoke(key, new object[] { param, param1 });

        internal TResult CallFunc<Tkey, TResult>(Tkey tkey)
            => (TResult)map.Invoke(tkey);
        internal TResult CallFunc<Tkey, TParam, TResult>(Tkey tkey, TParam param)
            => (TResult)map.Invoke(tkey, new object[] { param });
        internal TResult CallFunc<Tkey, TParam, TParam1, TResult>(Tkey tkey, TParam param, TParam1 param1)
            => (TResult)map.Invoke(tkey, new object[] { param, param1 });

    }
}

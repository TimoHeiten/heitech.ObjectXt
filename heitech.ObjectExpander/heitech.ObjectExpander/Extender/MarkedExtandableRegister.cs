using System;

namespace heitech.ObjectExpander.Extender
{
    internal static class MarkedExtandableRegister
    {
        static object locker = new object();

        internal static void Register<TKey>(this IMarkedExtendable marked, TKey key, Action action)
        {
            MarkedExtendable ext = TryGet(marked);
            lock (locker)
            {
                ext.RegisterAction(key, action);
            }
        }

        internal static void Register<TKey, TParam>(this IMarkedExtendable marked, TKey key, Action<TParam> action)
        {
            MarkedExtendable ext = TryGet(marked);
            lock (locker)
            {
                ext.Register(key, action);
            }
        }

        internal static void Register<TKey, TParam, TParam1>(this IMarkedExtendable marked, TKey key, Action<TParam, TParam1> action)
        {
            MarkedExtendable ext = TryGet(marked);
            lock(locker)
            {
                ext.Register(key, action);
            }
        }

        internal static void Func<Tkey, TResult>(this IMarkedExtendable marked, Tkey key, Func<TResult> func)
        {
            MarkedExtendable ext = TryGet(marked);
            lock (locker)
            {
                ext.RegisterFunc(key, func);
            }
        }

        internal static void Func<Tkey, TParam, TResult>(this IMarkedExtendable marked, Tkey key, Func<TParam, TResult> func)
        {
            MarkedExtendable ext = TryGet(marked);
            lock (locker)
            {
                ext.RegisterFunc(key, func);
            }
        }

        internal static void Func<Tkey, TParam, TParam2, TResult>(this IMarkedExtendable marked, Tkey key, Func<TParam, TParam2, TResult> func)
        {
            MarkedExtendable ext = TryGet(marked);
            lock (locker)
            {
                ext.RegisterFunc(key, func);
            }
        }

        internal static MarkedExtendable TryGet(this IMarkedExtendable marked)
        {
            if (marked is MarkedExtendable extendable)
            {
                return extendable;
            }
            else throw new ArgumentException($"must use CreateExtendable to generate an Extendable object");
        }

    }
}

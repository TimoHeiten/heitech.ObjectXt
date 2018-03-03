using heitech.ObjectExpander.Interfaces;
using System;
using System.Threading.Tasks;
using static heitech.ObjectExpander.ExtensionMap.AttributeFactory;

namespace heitech.ObjectExpander.Extender
{
    public static class ObjectExtender
    {
        internal static void TestCleanup() => map = null;

        static object locker = new object();
        static IAttributeMap map;
        internal static IAttributeMap AttributeMap()
        {
            lock (locker)
            {
                if (map == null)
                {
                    lock (locker)
                    {
                        map = CreateMap();
                    }
                }
                return map;
            }
        }

        public static void RegisterAction<TKey>(this IMarkedExtendable obj, TKey key, Action action)
        {
            lock (locker)
            {
                AttributeMap().Add(obj, key, CreateActionAttribute(key, action));
            }
        }

        public static void RegisterAction<TKey, TParam>(this IMarkedExtendable obj, TKey key, Action<TParam> action)
        {
            lock (locker)
            {
                AttributeMap().Add(obj, key, CreateActionAttribute(key, action));
            }
        }

        public static void RegisterAction<TKey, TParam, TParam2>(this IMarkedExtendable obj, TKey key, Action<TParam, TParam2> action)
        {
            lock (locker)
            {
                AttributeMap().Add(obj, key, CreateActionAttribute(key, action));
            }
        }

        public static void RegisterAsyncAction<TKey>(this IMarkedExtendable obj, TKey key, Func<Task> func)
            => throw new NotImplementedException();
        public static void RegisterAsyncAction<TKey, TParam>(this IMarkedExtendable obj, TKey key, Func<TParam, Task> func)
            => throw new NotImplementedException();
        public static void RegisterAsyncAction<TKey, TParam, TParam2>(this IMarkedExtendable obj, TKey key, Func<TParam, TParam2, Task> func)
            => throw new NotImplementedException();

        public static void RegisterFunc<TKey, TResult>(this IMarkedExtendable obj, TKey key, Func<TResult> func)
        {
            lock(locker)
            {
                AttributeMap().Add(obj, key, CreateFuncAttribute<TKey, TResult>(key, func));
            }
        }
        public static void RegisterFunc<TKey, TResult, TParam>(this IMarkedExtendable obj, TKey key, Func<TParam, TResult> func)
        {
            lock (locker)
            {
                AttributeMap().Add(obj, key, CreateFuncAttribute(key, func));
            }
        }
        public static void RegisterFunc<TKey, TResult, TParam, TParam2>(this IMarkedExtendable obj, TKey key, Func<TResult, TParam, TParam2> func)
        {
            lock (locker)
            {
                AttributeMap().Add(obj, key, CreateFuncAttribute(key, func));
            }
        }

        public static void RegisterFuncAsync<TKey, TResult>(this IMarkedExtendable obj, TKey key, Func<Task<TResult>> func) 
            => throw new NotImplementedException();
        public static void RegisterFuncAsync<TKey, TResult, TParam>(this IMarkedExtendable obj, TKey key, Func<TParam, Task<TResult>> func)
             => throw new NotImplementedException();
        public static void RegisterFuncAsync<TKey, TResult, TParam, TParam2>(this IMarkedExtendable obj, TKey key, Func<TParam, TParam2, Task<TResult>> func)
             => throw new NotImplementedException();
    }
}

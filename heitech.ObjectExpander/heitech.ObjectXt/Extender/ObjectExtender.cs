using heitech.ObjectXt.ExtensionMap;
using heitech.ObjectXt.Interfaces;
using System;
using System.Threading.Tasks;

namespace heitech.ObjectXt.Extender
{
    public static class ObjectExtender
    {
        public static void StartExtension()
        {
            lock (locker)
            {
                if (factory == null)
                {
                    lock (locker)
                    {
                        if (factory == null)
                        {
                            if (Configuration.ObjectExtenderConfig.IsTypeSpecific)
                            {
                                factory = AttributeFactory.Create(new TypeSpecificAttributeMap(() => new GlobalAttributeMap()));
                            }
                            else factory = AttributeFactory.CreateGlobal();
                        }
                    }
                }
            }
        }

        static object locker = new object();
        internal static void SetFactory(IAttributeFactory _factory)
        {
            lock (locker)
            {
                factory = _factory;
            }
        }
        private static IAttributeFactory factory;
        internal static IAttributeMap AttributeMap() => factory.GetMap();

        public static void RegisterAction<TKey>(this IMarkedExtendable obj, TKey key, Action action)
        {
            lock (locker)
            {
                AttributeMap().Add(obj, key, factory.CreateActionAttribute(key, action));
            }
        }

        public static void RegisterAction<TKey, TParam>(this IMarkedExtendable obj, TKey key, Action<TParam> action)
        {
            lock (locker)
            {
                AttributeMap().Add(obj, key, factory.CreateActionAttribute(key, action));
            }
        }

        public static void RegisterAction<TKey, TParam, TParam2>(this IMarkedExtendable obj, TKey key, Action<TParam, TParam2> action)
        {
            lock (locker)
            {
                AttributeMap().Add(obj, key, factory.CreateActionAttribute(key, action));
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
                AttributeMap().Add(obj, key, factory.CreateFuncAttribute<TKey, TResult>(key, func));
            }
        }
        public static void RegisterFunc<TKey, TResult, TParam>(this IMarkedExtendable obj, TKey key, Func<TParam, TResult> func)
        {
            lock (locker)
            {
                AttributeMap().Add(obj, key, factory.CreateFuncAttribute(key, func));
            }
        }
        public static void RegisterFunc<TKey, TResult, TParam, TParam2>(this IMarkedExtendable obj, TKey key, Func<TResult, TParam, TParam2> func)
        {
            lock (locker)
            {
                AttributeMap().Add(obj, key, factory.CreateFuncAttribute(key, func));
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

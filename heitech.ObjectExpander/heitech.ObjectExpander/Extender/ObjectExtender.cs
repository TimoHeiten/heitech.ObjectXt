using heitech.ObjectExpander.ExtensionMap;
using heitech.ObjectExpander.Interfaces;
using System;
using System.Threading.Tasks;
using static heitech.ObjectExpander.ExtensionMap.AttributeFactory;

namespace heitech.ObjectExpander.Extender
{
    public static class ObjectExtender
    {
        public static IMarkedExtendable CreateExtendable(this object obj)
        {
            if (obj == null) throw new ArgumentException($"object param must not be null");

            IAttributeMap map;
            if (Configuration.ObjectExtenderConfig.TypeSpecific)
                map = new TypeSpecificAttributeMap();
            else map = new GlobalAttributeMap();

            return new MarkedExtendable(map, obj);
        }

        public static void RegisterAction<TKey>(this IMarkedExtendable obj, TKey key, Action action)
            => obj.Register(key, action);

        public static void RegisterAction<TKey, TParam>(this IMarkedExtendable obj, TKey key, Action<TParam> action)
            => obj.Register(key, action);

        public static void RegisterAction<TKey, TParam, TParam2>(this IMarkedExtendable obj, TKey key, Action<TParam, TParam2> action)
            => obj.Register(key, action);

        public static void RegisterAsyncAction<TKey>(this IMarkedExtendable obj, TKey key, Func<Task> func)
            => throw new NotImplementedException();
        public static void RegisterAsyncAction<TKey, TParam>(this IMarkedExtendable obj, TKey key, Func<TParam, Task> func)
            => throw new NotImplementedException();
        public static void RegisterAsyncAction<TKey, TParam, TParam2>(this IMarkedExtendable obj, TKey key, Func<TParam, TParam2, Task> func)
            => throw new NotImplementedException();

        public static void RegisterFunc<TKey, TResult>(this IMarkedExtendable obj, TKey key, Func<TResult> func)
            => obj.Func(key, func);
        public static void RegisterFunc<TKey, TResult, TParam>(this IMarkedExtendable obj, TKey key, Func<TParam, TResult> func)
            => obj.Func(key, func);

        public static void RegisterFunc<TKey, TResult, TParam, TParam2>(this IMarkedExtendable obj, TKey key, Func<TResult, TParam, TParam2> func)
            => obj.Func(key, func);

        public static void RegisterFuncAsync<TKey, TResult>(this IMarkedExtendable obj, TKey key, Func<Task<TResult>> func) 
            => throw new NotImplementedException();
        public static void RegisterFuncAsync<TKey, TResult, TParam>(this IMarkedExtendable obj, TKey key, Func<TParam, Task<TResult>> func)
             => throw new NotImplementedException();
        public static void RegisterFuncAsync<TKey, TResult, TParam, TParam2>(this IMarkedExtendable obj, TKey key, Func<TParam, TParam2, Task<TResult>> func)
             => throw new NotImplementedException();
    }
}

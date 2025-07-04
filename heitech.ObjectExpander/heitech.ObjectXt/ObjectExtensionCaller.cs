using System;
using System.Threading.Tasks;
using heitech.ObjectXt.Configuration;
using heitech.ObjectXt.ExtensionMap;
using static heitech.ObjectXt.ObjectExtender;

namespace heitech.ObjectXt
{
    public static class ObjectExtensionCaller
    {
        public static void Call<TKey>(this IMarkedExtendable obj, TKey key)
        {
            var args = new IMarkedExtendable[] { };
            InvokeOnMap(() => AttributeMap().Invoke(obj, key, args), obj, key, null, args);
        }

        public static void Call<TKey, TParam>(this IMarkedExtendable obj, TKey key, TParam param)
        {
            var args = new object[] { param};
            InvokeOnMap(() => AttributeMap().Invoke(obj, key, args), obj, key, null, args);
        }

        public static void Call<TKey, TParam, TParam2>(this IMarkedExtendable obj, TKey key, TParam param, TParam2 param2)
        {
            var args = new object[] { param, param2 };
            InvokeOnMap(() => AttributeMap().Invoke(obj, key, args), obj, key, null, args);
        }

        static void Throw(IMarkedExtendable obj, object key) => throw new AttributeNotFoundException(obj.GetType(), key);

        public static Task CallAsync<TKey>(this IMarkedExtendable obj, TKey key) 
            => throw new NotImplementedException();
        public static Task CallAsync<TKey, TParam>(this IMarkedExtendable obj, TKey key, TParam param)
            => throw new NotImplementedException();
        public static Task CallAsync<TKey, TParam, TParam2>(this IMarkedExtendable obj, TKey key, TParam param, TParam2 param2)
            => throw new NotImplementedException();


        public static TResult Invoke<TKey, TResult>(this IMarkedExtendable obj, TKey key)
        {
            TResult result = default(TResult);
            InvokeOnMap(() => result = (TResult)AttributeMap().Invoke(obj, key),
                obj, key, typeof(TResult));

            return result;
        }

        public static TResult Invoke<TKey, TResult, TParam>(this IMarkedExtendable obj, TKey key, TParam param)
        {
            TResult result = default(TResult);
                InvokeOnMap(() => result = (TResult)AttributeMap().Invoke(obj, key, param),
                obj, key, typeof(TResult), param);
            return result;
        }

        static void InvokeOnMap(Action _do, IMarkedExtendable obj, object key, Type returnType, params object[] parameters)
        {
            if (AttributeMap().CanInvoke(obj, key, returnType, parameters))
            {
                _do();
                return;
            }
            
            if (!ObjectExtenderConfig.IgnoreException)
                Throw(obj, key);
        }

        public static TResult Invoke<TKey, TResult, TParam, TParam2>(this IMarkedExtendable obj, TKey key, TParam param, TParam2 param2)
        {
            var result = default(TResult);
            InvokeOnMap(() => result = (TResult)AttributeMap().Invoke(obj, key, param, param2),
                 obj, key, typeof(TResult), param, param2);
            
            return result;
        }

        public static Task<TResult> InvokeAsync<TKey, TResult>(this IMarkedExtendable obj, TKey key) 
            => throw new NotImplementedException();
        public static Task<TResult> InvokeAsync<TKey, TResult, TParam>(this IMarkedExtendable obj, TKey key, TParam param)
            => throw new NotImplementedException();
        public static Task<TResult> InvokeAsync<TKey, TResult, TParam, TParam2>(this IMarkedExtendable obj, TKey key, TParam param, TParam2 param2)
            => throw new NotImplementedException();
    }
}

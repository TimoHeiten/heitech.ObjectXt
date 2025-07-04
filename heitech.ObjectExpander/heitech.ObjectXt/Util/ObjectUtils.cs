using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace heitech.ObjectXt.Util
{
    /// <summary>
    /// Utils to work with obj extender internals
    /// </summary>
    public static class ObjectUtils
    {
        public static IMappedPropertyManager GeneratePropertyManager(this object obj)
            => new MappedPropertyManager(obj);

        internal static Dictionary<string, object> AllProperties(this object obj)
        {
            var dict = new Dictionary<string, object>();
            PropertyInfo[] propertyInfos = obj.GetType().GetProperties(GetFlags());
            foreach (PropertyInfo item in propertyInfos)
            {
                if (TryGetPropertyValue(obj, item.Name, propertyInfos, out object value))
                {
                    dict.Add(item.Name, value);
                }
            }
            return dict;
        }

        internal static BindingFlags GetFlags()
            => BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static;

        static bool TryGetPropertyValue(object obj, string name, PropertyInfo[] infos, out object value)
        {
            bool isSuccess = false;
            value = null;
            PropertyInfo first = infos.FirstOrDefault(x => x.Name == name);
            if (first != null)
            {
                try
                {
                    value = first.GetValue(obj);
                    isSuccess = true;
                }
                catch (TargetParameterCountException)
                {
                    isSuccess = false;
                }
            }
            return isSuccess;
        }

        internal static bool TryGetPropertyValue<TProperty>(this object obj, string name, out TProperty value)
        {
            value = default(TProperty);
            bool isSuccess = false;
            if (TryGetPropertyValue(obj, name, obj.GetType().GetProperties(GetFlags()), out object val))
            {
                if (val != null && val.GetType().IsDownCastable(typeof(TProperty)) && !val.Equals(default(TProperty)))
                {
                    value = (TProperty)val;
                    isSuccess = true;
                }
            }
            return isSuccess;
        }

        public static bool IsDownCastable(this Type t, Type other)
            => other.IsAssignableFrom(t) || t.IsSubclassOf(other);

        public static bool IsUpCastable(this Type t, Type other)
            => t.IsAssignableFrom(other) || other.IsSubclassOf(t);

        public static bool HasRelation(this Type t, Type other)
            => IsDownCastable(t, other) || IsUpCastable(t, other);

        public static bool HasRelation(this object obj, Type t)
            => obj.GetType().HasRelation(t);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace heitech.ObjectExpander.Util
{
    public static class ObjectUtils
    {
        public static Dictionary<string, object> AllProperties(this object obj)
        {
            var dict = new Dictionary<string, object>();
            PropertyInfo[] propertyInfos = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo item in propertyInfos)
            {
                if (TryGetPropertyValue(obj, item.Name, out object value))
                {
                    dict.Add(item.Name, value);
                }
            }
            return dict;
        }

        static bool TryGetPropertyValue(object obj, string name, out object value)
        {
            bool isSuccess = false;
            value = null;
            PropertyInfo first = obj.GetType().GetProperties().FirstOrDefault(x => x.Name == name);
            if (first != null)
            {
                value = first.GetValue(obj);
                isSuccess = true;
            }
            return isSuccess;
        }

        public static bool TryGetPropertyValue<TProperty>(this object obj, string name, out TProperty value)
        {
            value = default(TProperty);
            bool isSuccess = false;
            if (TryGetPropertyValue(obj, name, out object val))
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

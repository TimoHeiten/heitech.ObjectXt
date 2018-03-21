using System;
using System.Collections.Generic;
using System.Reflection;

namespace heitech.ObjectXt.Util
{
    internal class MappedPropertyManager : IMappedPropertyManager
    {
        protected object origin;
        private Type originType;
        protected Dictionary<string, object> dictionary;

        internal MappedPropertyManager(object obj)
        {
            this.origin = obj;
            this.originType = obj.GetType();
            dictionary = obj.AllProperties();
        }

        BindingFlags flags() => ObjectUtils.GetFlags();
        public object this[string name]
        {
            get => dictionary[name];
            set
            {
                PropertyInfo info = originType.GetProperty(name, flags());
                if (info == null) throw new KeyNotFoundException($"key {name} was not found in mappedProperties");

                Type propertyType = info.PropertyType;
                Type expected = value.GetType();

                if (ExpectedOrAssignable(propertyType, expected))
                {
                    dictionary[name] = value;
                    SetValueOnOriginObject(name, value);
                }
                else throw new ArgumentException($"name with type {expected.Name} could not be set to the propertyType {propertyType}");
            }
        } 

        public bool TryGetProperty<T>(string propName, out T value)
        {
            bool isSuccess = false;
            value = default(T);
            if (TryGetExpectedType(propName, out T expected))
            {
                value = expected;
                isSuccess = true;
            }
            return isSuccess;
        }

        private bool TryGetExpectedType<T>(string key, out T val)
        {
            val = default(T);
            bool isSuccess = false;
            if (dictionary.TryGetValue(key, out object obj))
            {
                Type t = typeof(T);
                if (WithNullValueAndCorrectObjectType(obj, key, t) || ExpectedOrAssignable(obj.GetType(), t))
                {
                    val = (T)obj;
                    isSuccess = true;
                }
            }
            return isSuccess;
        }

        private bool WithNullValueAndCorrectObjectType(object obj, string key, Type t)
            => obj == null && originType.GetProperty(key, flags()).PropertyType.IsDownCastable(t);
        private static bool ExpectedOrAssignable(Type propType, Type expected) 
            => propType == expected || propType.IsDownCastable(expected);

        public bool TrySetProperty<T>(string propName, T value)
        {
            bool isSuccess = false;
            if (TryGetExpectedType(propName, out T expected))
            {
                dictionary[propName] = value;
                SetValueOnOriginObject(propName, value);
                isSuccess = true;
            }
            return isSuccess;
        }

        private void SetValueOnOriginObject(string propName, object val)
            => GetBackingField(GetBackingFieldName(propName)).SetValue(origin, val);

        private string GetBackingFieldName(string propertyName) => ($"<{propertyName}>k__BackingField");

        private FieldInfo GetBackingField(string backingFieldName)
            => originType.GetField(backingFieldName, flags());

        public IDictionary<string, object> AllProperties() => dictionary;

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == "o")
            {
                return $"{this.GetType().Name}: Origin '{origin.ToString()}', OriginType '{originType}'";
            }
            else if (format =="a")
            {
                string s = "MappedProperties: ";
                foreach (var item in dictionary)
                {
                    s += $"'key:{item.Key}, value:{item.Value.GetType().Name}'\n";
                }
                return s;
            }
            else return this.GetType().Name;
        }
    }
}

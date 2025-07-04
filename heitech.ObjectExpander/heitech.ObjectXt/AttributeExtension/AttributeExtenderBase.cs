using heitech.ObjectXt.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using heitech.ObjectXt.Interface;
using System.Collections;
using System.Diagnostics;

namespace heitech.ObjectXt.AttributeExtension
{
    public abstract class AttributeExtenderBase<T> : IAttributeExtender<T>
    {
        protected Dictionary<T, object> Attributes { get; } = new Dictionary<T, object>();
        protected Func<T, object, IAttributeExtenderItem<T>> Factory { get; }

        protected AttributeExtenderBase(Func<T, object, IAttributeExtenderItem<T>> factory)
        {
            this.Factory = factory;
        }

        public virtual object this[T key]
        {
            get => Attributes[key];
            set
            {
                if (Attributes.ContainsKey(key))
                    Attributes[key] = value;
                else
                    Add(key, value);
            }
        }

        public virtual void Add(T key, object obj)
        {
            if (key == null || obj == null)
                throw new ArgumentException();

            Attributes.Add(key, obj);
        }

        public virtual void Remove(T key)
        {
            if (Attributes.ContainsKey(key))
                Attributes.Remove(key);
            else
                throw new KeyNotFoundException();
        }

        public virtual bool TryGetAttribute<A>(T key, out A attribute)
        {
            bool isSuccess = false;
            attribute = default(A);

            if (Attributes.TryGetValue(key, out object v)
                && v.GetType() == typeof(A))
            {
                attribute = (A)v;
                isSuccess = true;
            }
            return isSuccess;
        }

        public virtual bool HasAttribute(T key)
            => Attributes.ContainsKey(key);

        public virtual  (bool hasValue, T key, A attribute) GetKeyAttributePair<A>(T key)
        {
            if (Attributes.TryGetValue(key, out object attribute) && attribute.GetType() == typeof(A))
                return (true, key, (A)attribute);
            else
                return (false, key, default(A));
        }

        public virtual bool HasAttributeOfType<A>(out T key)
        {
            bool isSuccess = false;
            Type type_of_v = typeof(A);
            key = default(T);
            var hasAny = Attributes.FirstOrDefault(x => x.Value.GetType() == type_of_v);

            if (!hasAny.Equals(default(KeyValuePair<T, object>)))
            {
                key = hasAny.Key;
                isSuccess = true;
            }
            return isSuccess;
        }

        public virtual IEnumerator<IAttributeExtenderItem<T>> GetEnumerator()
        {
            foreach (var item in Attributes)
                yield return Factory(item.Key, item.Value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Equals(IAttributeExtender<T> other)
        {
            if (!SameCount(other))
                return false;

            foreach (IAttributeExtenderItem<T> item in other)
                if (!EqualsAny(item))
                    return false;

            return true;
        }

        private bool EqualsAny(IAttributeExtenderItem<T> item)
            => this.Any(x => x.Equals(item));

        private bool SameCount(IAttributeExtender<T> other)
            => other.Count() == this.Attributes.Count;
    }
}

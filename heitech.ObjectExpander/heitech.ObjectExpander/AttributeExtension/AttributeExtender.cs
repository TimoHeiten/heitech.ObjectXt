using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using heitech.ObjectExpander.Interfaces;

namespace heitech.ObjectExpander.AttributeExtension
{
    internal class AttributeExtender<T> : IAttributeExtender<T>
    {
        protected Dictionary<T, object> Attributes { get; } = new Dictionary<T, object>();

        public object this[string attributename]
        {
            get { return null; }
            set { }
        }

        public void Add(T key, object obj)
        {
            if (key == null || obj == null)
                throw new ArgumentException();

            Attributes.Add(key, obj);
        }

        public void Remove(T key)
        {
            if (Attributes.ContainsKey(key))
                Attributes.Remove(key);
            else
                throw new KeyNotFoundException();
        }

        public bool TryGetAttribute<V>(T key, out V val)
        {
            bool isSuccess = false;
            val = default(V);



            return isSuccess;
        }

        public bool HasKey(T key)
        {
            return false;
        }

        public (bool hasValue, T key, V value) GetKeyValue<V>(T key)
        {
            return (false, key, default(V));
        }

        public bool HasAttributeOfType<V>()
        {
            return false;
        }


    }
}

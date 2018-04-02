using heitech.ObjectXt.Interface;
using heitech.ObjectXt.Util;

namespace heitech.ObjectXt.AttributeExtension
{
    internal class AttributeExtenderItem<T> : IAttributeExtenderItem<T>
    {
        public T Key { get; }
        public object Value { get; }

        internal AttributeExtenderItem(T key, object value)
        {
            Key = key;
            Value = value;
        }

        public bool Equals(IAttributeExtenderItem<T> other)
            => Key.Equals(other.Key) && Value.Equals(other.Value);

        public bool IsValueOfType<V>()
            => Value.GetType().HasRelation(typeof(V));

        public bool TryGetValue<V>(out V value)
        {
            bool isSucces = false;
            value = default(V);
            if (IsValueOfType<V>())
                value = (V)Value;

            return isSucces;
        }
    }
}

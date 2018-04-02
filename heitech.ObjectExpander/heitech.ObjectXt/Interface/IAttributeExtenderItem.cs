using System;

namespace heitech.ObjectXt.Interface
{
    public interface IAttributeExtenderItem<T> : IEquatable<IAttributeExtenderItem<T>>
    {
        T Key { get; }
        object Value { get; }

        bool IsValueOfType<V>();
        bool TryGetValue<V>(out V value);
    }
}

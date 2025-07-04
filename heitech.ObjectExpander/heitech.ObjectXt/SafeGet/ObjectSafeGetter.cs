using System;
using System.Collections.Generic;
using System.Linq;

namespace heitech.ObjectXt.SafeGet
{
    public sealed class ObjectSafeGetter
    {
        private readonly IEnumerable<object> _items;
        public ObjectSafeGetter(params object[] items)
            => _items = items;

        public bool TryGet<T>(out T value)
        {
            value = default;
            var expected = typeof(T);

            var result = _items.Where(x => HasRelation(x.GetType(), expected)).ToArray();

            if (result.Length != 1) 
                return false;
            
            value = (T)result[0];
            return true;
        }

        public bool TryGetAll<T>(out IEnumerable<T> enumerable)
        {
            var expected = typeof(T);
            enumerable = _items.Where(x => HasRelation(x.GetType(), expected)).Cast<T>();

            return enumerable.Any();
        }

        private static bool HasRelation(Type type, Type expected)
            => expected == type || expected.IsAssignableFrom(type) || type.IsSubclassOf(expected);
    }
}

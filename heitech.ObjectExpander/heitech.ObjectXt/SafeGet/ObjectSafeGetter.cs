using System;
using System.Collections.Generic;
using System.Linq;

namespace heitech.ObjectXt.SafeGet
{
    public class ObjectSafeGetter
    {
        private readonly IEnumerable<object> _items;
        public ObjectSafeGetter(params object[] items)
        {
            _items = items;
        }

        public bool TryGet<T>(out T value)
        {
            value = default(T);
            bool isSuccess = false;
            Type expected = typeof(T);

            var result = _items.Where(x => HasRelation(x.GetType(), expected));

            if (result.Count() == 1)
            {
                value = (T)result.ElementAt(0);
                isSuccess = true;
            }

            return isSuccess;
        }

        public bool TryGetAll<T>(out IEnumerable<T> enumerable)
        {
            Type expected = typeof(T);
            enumerable = _items.Where(x => HasRelation(x.GetType(), expected)).Cast<T>();

            return enumerable.Any();
        }

        private bool HasRelation(Type type, Type expected)
            => expected == type || expected.IsAssignableFrom(type) || type.IsSubclassOf(expected);
    }
}

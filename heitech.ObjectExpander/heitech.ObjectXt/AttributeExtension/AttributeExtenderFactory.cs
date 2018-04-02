using heitech.ObjectXt.Interface;
using heitech.ObjectXt.Interfaces;
using System;

namespace heitech.ObjectXt.AttributeExtension
{
    public static class AttributeExtenderFactory
    {
        public static IAttributeExtender<K> Create<K>()
            => new AttributeExtender<K>(BasicFactory);

        public static IAttributeExtender<K> Create<K>(Func<K, object, IAttributeExtenderItem<K>> factory)
            => new AttributeExtender<K>(factory);

        public static IAttributeExtender<K> CreateConcurrent<K>()
            => new ConcurrentAttributeExtender<K>(BasicFactory);

        public static IAttributeExtender<K> CreateConcurrent<K>(Func<K, object, IAttributeExtenderItem<K>> factory)
            => new ConcurrentAttributeExtender<K>(factory);

        public static IAttributeExtenderItem<K> BasicFactory<K>(K key, object val)
            => new AttributeExtenderItem<K>(key, val);
    }
}

using heitech.ObjectXt.Interfaces;

namespace heitech.ObjectXt.AttributeExtension
{
    public static class AttributeExtenderFactory
    {
        public static IAttributeExtender<K> Create<K>()
            => new AttributeExtender<K>();

        public static IAttributeExtender<K> CreateConcurrent<K>()
            => new ConcurrentAttributeExtender<K>();
    }
}

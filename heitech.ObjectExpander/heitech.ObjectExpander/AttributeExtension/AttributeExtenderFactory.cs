using heitech.ObjectExpander.Interfaces;

namespace heitech.ObjectExpander.AttributeExtension
{
    public static class AttributeExtenderFactory
    {
        public static IAttributeExtender<K> Create<K>()
            => new AttributeExtender<K>();

    }
}

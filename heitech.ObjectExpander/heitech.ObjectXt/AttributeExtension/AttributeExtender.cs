using System;
using System.Runtime.CompilerServices;
using heitech.ObjectXt.Interface;

[assembly:InternalsVisibleTo("heitech.ObjectXt.Tests")]
namespace heitech.ObjectXt.AttributeExtension
{
    internal class AttributeExtender<T> : AttributeExtenderBase<T>
    {
        internal AttributeExtender(Func<T, object, IAttributeExtenderItem<T>> factory) : base(factory)
        { }
    }
}

using System;
using System.Runtime.Serialization;

namespace heitech.ObjectExpander.ExtensionMap
{
    public class AttributeNotFoundException : Exception
    {
        internal AttributeNotFoundException(Type t, object key)
            : this($"attribute with key {key.ToString()} on Type {t.Name} not found")
        { }

        internal AttributeNotFoundException(string message) : base(message)
        {
        }

        internal AttributeNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AttributeNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

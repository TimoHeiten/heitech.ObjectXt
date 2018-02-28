using System.Collections.Generic;

namespace heitech.ObjectExpander.Util
{
    public interface IMappedPropertyManager
    {
        object this[string name] { get; set; }

        bool TryGetProperty<T>(string propName, out T value);
        bool TrySetProperty<T>(string propName, T value);

        IDictionary<string, object> AllProperties();
    }
}

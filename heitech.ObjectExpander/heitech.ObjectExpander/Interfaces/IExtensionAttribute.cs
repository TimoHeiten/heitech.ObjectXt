using System;

namespace heitech.ObjectExpander.Interfaces
{
    internal interface IExtensionAttribute
    {
        bool CanInvoke<TKey>(TKey key, Type expectedReturnType = null, params object[] parameters);
        object Invoke(params object[] parameters);
    }
}

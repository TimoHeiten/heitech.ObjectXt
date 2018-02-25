using System;

namespace heitech.ObjectExpander.Interfaces
{
    internal interface IAttributeMap
    {
        void Add<TKey>(object extended, TKey key, IExtensionAttribute func);

        bool HasKey<TKey>(TKey key);

        bool CanInvoke<TKey>(TKey key, Type expectedReturnType, params object[] parameters);
        object Invoke<TKey>(TKey key, params object[] parameters);
    }
}

using System;

namespace heitech.ObjectXt.Interfaces
{
    internal interface IAttributeMap
    {
        void Add<TKey>(object extended, TKey key, IExtensionAttribute func);

        bool HasKey<TKey>(object extended, TKey key);

        bool CanInvoke<TKey>(object extended, TKey key, Type expectedReturnType, params object[] parameters);
        object Invoke<TKey>(object extended, TKey key, params object[] parameters);
    }
}

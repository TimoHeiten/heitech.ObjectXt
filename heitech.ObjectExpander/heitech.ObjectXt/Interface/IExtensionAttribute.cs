using System;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace heitech.ObjectXt.Interfaces
{
    internal interface IExtensionAttribute
    {
        bool CanInvoke<TKey>(TKey key, Type expectedReturnType = null, params object[] parameters);
        object Invoke(params object[] parameters);
    }
}

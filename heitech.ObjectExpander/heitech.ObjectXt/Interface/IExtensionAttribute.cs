using System;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace heitech.ObjectXt.Interface
{
    /// <summary>
    /// Extension attribute that can be invoked
    /// </summary>
    internal interface IExtensionAttribute
    {
        bool CanInvoke<TKey>(TKey key, Type expectedReturnType = null, params object[] parameters);
        object Invoke(params object[] parameters);
    }
}

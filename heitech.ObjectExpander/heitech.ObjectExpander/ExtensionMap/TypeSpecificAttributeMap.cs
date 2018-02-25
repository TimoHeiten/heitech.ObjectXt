using heitech.ObjectExpander.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace heitech.ObjectExpander.ExtensionMap
{
    internal class TypeSpecificAttributeMap : IAttributeMap
    {
        public void Add<TKey>(object extended, TKey key, IExtensionAttribute func)
        {
            throw new NotImplementedException();
        }

        public bool CanInvoke<TKey>(TKey key, Type expectedReturnType, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public bool HasKey<TKey>(TKey key)
        {
            throw new NotImplementedException();
        }

        public object Invoke<TKey>(TKey key, params object[] parameters)
        {
            throw new NotImplementedException();
        }
    }
}

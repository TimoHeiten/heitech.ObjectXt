using heitech.ObjectXt.Interfaces;
using heitech.ObjectXt.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using heitech.ObjectXt.Interface;

namespace heitech.ObjectXt.ExtensionMap.Attributes
{
    internal abstract class ExtensionAttributeBase : IExtensionAttribute
    {
        protected Type[] ExpectedParameters { get; private set; }
        protected Type ExpectedReturnType { get; private set; }
        protected Type ExpectedKeyType { get; }
        protected object _Key { get; }

        protected object Invokable { get; }

        internal ExtensionAttributeBase(object key, Type expectedReturnType = null, params Type[] expectedParams)
        {
            _Key = key;
            ExpectedKeyType = key.GetType();
            ExpectedParameters = expectedParams;
            ExpectedReturnType = expectedReturnType;
        }

        public bool CanInvoke<TKey>(TKey key, Type expectedReturnType = null, params object[] parameters)
            => ExpectedKeyType == key.GetType()
            && _Key.Equals(key)
            && expectedReturnType == ExpectedReturnType
            && ExpectedParameters.Count() == parameters.Count()
            && AreItemsEqual(parameters);

        private bool AreItemsEqual(object[] items)
        {
            List<Type> list = ExpectedParameters.ToList();
            foreach (var item in items)
            {
                Type itemType = item.GetType();
                (bool rmFlag, Type rmType) = FindType(itemType, list);

                if (rmFlag) list.Remove(rmType);
                else return false;
            }
            return true;
        }

        private (bool rmFlag, Type rmType) FindType(Type itemType, List<Type> types)
        {
            Type first = types.FirstOrDefault(x => x.HasRelation(itemType));
            if (first != null) return (true, first);
            else return (false, null);
        }

        public abstract object Invoke(params object[] parameters);
    }
}

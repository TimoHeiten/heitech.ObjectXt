using System;

namespace heitech.ObjectXt.ExtensionMap.Attributes
{
    internal sealed class ActionAttribute : ExtensionAttributeBase
    {
        Action invokeable;
        internal ActionAttribute(object key, Action invokable) 
            : base(key, null, new Type[] { })
        {
            invokeable = invokable;
        }

        public override object Invoke(params object[] parameters)
        {
            invokeable.DynamicInvoke(parameters);
            return null;
        }
    }

    internal class ActionParamAttribute<T> : ExtensionAttributeBase
    {
        Action<T> invokable;
        internal ActionParamAttribute(object key, Action<T> action) 
            : base(key, null, new Type[] { typeof(T) })
        {
            invokable = action;
        }

        public override object Invoke(params object[] parameters)
        {
            invokable.DynamicInvoke(parameters);
            return null;
        }
    }

    internal class ActionDualParameter<T, T1> : ExtensionAttributeBase
    {
        Action<T, T1> invokable;
        internal ActionDualParameter(object key, Action<T, T1> action) 
            : base(key, null, new Type[] { typeof(T), typeof(T1) })
        {
            invokable = action;
        }

        public override object Invoke(params object[] parameters)
        {
            invokable.DynamicInvoke(parameters);
            return null;
        }
    }
}

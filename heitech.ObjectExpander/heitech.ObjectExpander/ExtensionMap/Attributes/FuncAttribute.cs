using System;

namespace heitech.ObjectExpander.ExtensionMap.Attributes
{
    internal class FuncAttribute<TResult> : ExtensionAttributeBase
    {
        Func<TResult> func;
        internal FuncAttribute(object key, Func<TResult> func) 
            : base(key, typeof(TResult), new Type[] { })
        {
            this.func = func;
        }

        public override object Invoke(params object[] parameters)
            => func.DynamicInvoke(parameters);
    }

    internal class FuncAttributeParam<TResult, TParam> : ExtensionAttributeBase
    {
        Func<TParam, TResult> func;
        internal FuncAttributeParam(object key, Func<TParam, TResult> func)
            : base(key, typeof(TResult), new Type[] { typeof(TParam) })
        {
            this.func = func;
        }

        public override object Invoke(params object[] parameters)
            => func.DynamicInvoke(parameters);
    }

    internal class FuncAttributeDualParam<TResult, TParam, TParam2> : ExtensionAttributeBase
    {
        Func<TParam, TParam2, TResult> func;
        internal FuncAttributeDualParam(object key, Func<TParam, TParam2, TResult> func)
            : base(key, typeof(TResult), new Type[] { typeof(TParam), typeof(TParam2) })
        {
            this.func = func;
        }

        public override object Invoke(params object[] parameters)
            => func.DynamicInvoke(parameters);
    }
}

using heitech.ObjectXt.ExtensionMap.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Reflection;

namespace heitech.ObjectXt.Tests.ExtensionMap
{
    [TestClass]
    public class ExtensionAttributeBaseTests
    {
        ExtensionAttributeBase attribute;

        [TestMethod]
        public void ExtensionAttribute_CanInvokeReturnsFalseOnFalseKeyType()
        {
            attribute = new AttributeExt("key");
            Assert.IsFalse(attribute.CanInvoke(12));
        }

        [TestMethod]
        public void ExtensionAttribute_CanInvokeReturnsFalseOnFalseReturnType()
        {
            attribute = new AttributeExt("key", null);
            Assert.IsFalse(attribute.CanInvoke("key", typeof(int)));
        }

        [TestMethod]
        public void ExtensionAttribute_CanInvokeReturnsFalseIfParameterListDoesNotHaveSameCount()
        {
            attribute = new AttributeExt("key");
            Assert.IsFalse(attribute.CanInvoke(12, parameters: new object[] { 1, 2, 3 }));
        }

        [TestMethod]
        public void ExtensionAttribute_CanInvokeReturnsFalseIfParameterListCountMatchesYetTypesDoNotMatch()
        {
            attribute = new AttributeExt("key", expectedParams: new Type[] { typeof(string), typeof(int), typeof(string)});
            Assert.IsFalse(attribute.CanInvoke("key", null, new object[] { 1, 2, 3 }));
        }

        [TestMethod]
        public void ExtensionAttribute_CanInvokeIfAllMatches()
        {
            attribute = new AttributeExt("key", expectedReturnType: typeof(int),
                expectedParams: new Type[] { typeof(string), typeof(float), typeof(Type) });
            Assert.IsTrue(attribute.CanInvoke("key", typeof(int), new object[]{ new Typester(),
                42.0f, "abcAffeschnee" }));

            attribute = new AttributeExt("key", expectedReturnType: null);
            Assert.IsTrue(attribute.CanInvoke("key", null));
        }

        private class AttributeExt : ExtensionAttributeBase
        {
            internal AttributeExt(object key, Type expectedReturnType = null, params Type[] expectedParams) 
                : base(key, expectedReturnType, expectedParams)
            { }

            public override object Invoke(object[] parameters)
            {
                throw new NotImplementedException();
            }
        }

        private class Typester : Type
        {
            public override Guid GUID => throw new NotImplementedException();

            public override Module Module => throw new NotImplementedException();

            public override Assembly Assembly => throw new NotImplementedException();

            public override string FullName => throw new NotImplementedException();

            public override string Namespace => throw new NotImplementedException();

            public override string AssemblyQualifiedName => throw new NotImplementedException();

            public override Type BaseType => throw new NotImplementedException();

            public override Type UnderlyingSystemType => throw new NotImplementedException();

            public override string Name => throw new NotImplementedException();

            public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
            {
                throw new NotImplementedException();
            }

            public override object[] GetCustomAttributes(bool inherit)
            {
                throw new NotImplementedException();
            }

            public override object[] GetCustomAttributes(Type attributeType, bool inherit)
            {
                throw new NotImplementedException();
            }

            public override Type GetElementType()
            {
                throw new NotImplementedException();
            }

            public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
            {
                throw new NotImplementedException();
            }

            public override EventInfo[] GetEvents(BindingFlags bindingAttr)
            {
                throw new NotImplementedException();
            }

            public override FieldInfo GetField(string name, BindingFlags bindingAttr)
            {
                throw new NotImplementedException();
            }

            public override FieldInfo[] GetFields(BindingFlags bindingAttr)
            {
                throw new NotImplementedException();
            }

            public override Type GetInterface(string name, bool ignoreCase)
            {
                throw new NotImplementedException();
            }

            public override Type[] GetInterfaces()
            {
                throw new NotImplementedException();
            }

            public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
            {
                throw new NotImplementedException();
            }

            public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
            {
                throw new NotImplementedException();
            }

            public override Type GetNestedType(string name, BindingFlags bindingAttr)
            {
                throw new NotImplementedException();
            }

            public override Type[] GetNestedTypes(BindingFlags bindingAttr)
            {
                throw new NotImplementedException();
            }

            public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
            {
                throw new NotImplementedException();
            }

            public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
            {
                throw new NotImplementedException();
            }

            public override bool IsDefined(Type attributeType, bool inherit)
            {
                throw new NotImplementedException();
            }

            protected override TypeAttributes GetAttributeFlagsImpl()
            {
                throw new NotImplementedException();
            }

            protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
            {
                throw new NotImplementedException();
            }

            protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
            {
                throw new NotImplementedException();
            }

            protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
            {
                throw new NotImplementedException();
            }

            protected override bool HasElementTypeImpl()
            {
                throw new NotImplementedException();
            }

            protected override bool IsArrayImpl()
            {
                throw new NotImplementedException();
            }

            protected override bool IsByRefImpl()
            {
                throw new NotImplementedException();
            }

            protected override bool IsCOMObjectImpl()
            {
                throw new NotImplementedException();
            }

            protected override bool IsPointerImpl()
            {
                throw new NotImplementedException();
            }

            protected override bool IsPrimitiveImpl()
            {
                throw new NotImplementedException();
            }
        }
    }
}

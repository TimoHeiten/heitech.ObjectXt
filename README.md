# ObjectExtender
Extends objects that implement the markerinterface IMarkedExtendable with Actions/Funcs to register additional functionality.
Also helps to get/set Properties on any object with IMappedPropertyManager. (sorta reflectionhelper tooling)

Use AttributeExtender<T> to add methods on objects in .Net/C# 7
similiar to python _getattr_ / _setattr_ (or ExpandoObject, yet without compiler support and without DLR performance penalty)

Use MappedPropertyManager to Reflect properties, show all, or set values.

## Why would you want such a library?
To extend C# with some dynamic flavors without resenting to the DLR. Scenarios are prototyping or a highly dynamic environment where the use of good OO design is more hassle than it is worth.

## example ObjectExtender:
```csharp
class MarkedExtendable: IMarkedExtendable 
{

}
...
var obj = new MarkedExtendable();
obj.RegisterAction("key", () => Console.WriteLine("extended object called key"));
obj.RegisterFunc("funcy", () => 42);

obj.Call("key"); // writes to console: extended object called key
Console.WriteLine(obj.Invoke<string, int>("funcy")); // prints 42 to console
...

```

Features:
 - Add Action [with Parameters  ] to custom type at runtime
 - Add Func [with up to two Parameters] to custom type at runtime
 - Call added methods at runtime.
 - notice that async extensions for MarkedExtensions are not functional yet.

## example AttributeExtender
```csharp
IAttributeExtender<string> attr = AttributeExtension.AttributeExtenderFactory.Create<string>();
attr["key"] = "valueOfAnyType";
attr["key2"] = 21; //sets/adds
attr["key2"] = 42; // overwrites key without notice

attr.Add("key3") = new object(); // same as set yet little more verbose
attr.HasKey("key3"); // returns true
attr.Remove("key3");
attr.HasKey("key3"); // returns false

attr.TryGetAttribute("key2", out string _42); // returns False! (type does not match attribute)
attr.TryGetAttribute("key2", out int __42); // returns true, __42 holds int(42)
(bool b, string key, int value) tuple = attr.GetKeyAttributePair<int>("key2");

// or derive from abstract base which has all methods virtual
class MyTypeForRuntimeExtension : AttributeExtenderBase<string>
{
   internal string AnyProperty {get;}
   internal string AnotherProperty {get;}
}

var myType = new MyTypeForRuntimeExtension();
myType["thridProperty"] = new Action(() => ConsoleWriteLine("from abstract base class"));
bool hasAttr = myType.HasKey("thridProperty");
...

```
 
Features/Suggested Use of AttributeExtension:
  - Derive from abstract base class and add attributes to class at runtime,
  - Add a Property on your custom type that initiliazies with AttributeExtenderFactory.Create()
  for extension of attributes
  
  
Features ObjectUtils:  
  - Utils: Get Dictionarylike IMappedPropertyManager with all Propertys as Values
  - Utils: IMappedPropertyManager can get/set values on object and sync with dictionary
  
 
 

# ObjectExtender
Extends objects that implement the markerinterface IMarkedExtendable with ExtensionMethod to Register additional functionality.
Also helps to get/set Properties on any object with IMappedPropertyManager.

Use Extender to add methods on objects in .Net/C# 7
similiar to python _getattr_ / _setattr_ (or ExpandoObject, yet without compiler support and without DLR performance penalty)

Use MappedPropertyManager to Relfect properties, show all, or set values.

Why? --> to extend C# with some dynamic flavors without resenting to the DLR.

example:
class MarkedExtendable: IMarkedExtendable { }
  var obj = new MarkedExtendable();
  obj.RegisterAction("key", () => Console.WriteLine("extended object called key"));
  
  obj.Call("key");
 
Features:
  - Configure Attributes for typesepcific keys,
  - Add Action with Parameters
  - Add Func with up to two Parameters
  - Utils: Get Dictionarylike IMappedPropertyManager with all Propertys as Values
  - Utils: IMappedPropertyManager can get/set values on object and sync with dictionary
 

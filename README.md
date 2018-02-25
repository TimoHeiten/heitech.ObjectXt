# ObjectExtender
Extends Object with ExtensionMethod to Register additional functionality

Add Methods on object in .Net/C# 7
similiar to python _getattr_ / _setattr_

example:
  var obj = new object();
  obj.RegisterAction("key", () => Console.WriteLine("extended object called key"));
  
  obj.Call("key");
 
Features:
  - Configure Attributes for typesepcific keys,
  - Add Action with Parameters
  - Add Func with up to two Parameters
  - Utils: Get Dictionary with all Propertys as Values
 

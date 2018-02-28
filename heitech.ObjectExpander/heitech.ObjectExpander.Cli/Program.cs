using heitech.ObjectExpander.Extender;
using heitech.ObjectExpander.ExtensionMap;
using System;

namespace heitech.ObjectExpander.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            var obj = new MarkedObject();

            obj.RegisterAction("write", () => Console.WriteLine("von key aufgerufen"));
            obj.RegisterAction<string, int>("writeNumber", i => Console.WriteLine("mit nummer: " + i));
            obj.Call("write");

            Action _do = () =>
            {
                int index = 0;
                for (int i = 0; i < 10000; i++)
                {
                    obj.Call<string, int>("writeNumber", index);
                    index++;
                }
            };

            _do();

            try
            {
                obj.RegisterFunc<string, int>("funcy", () => 42);
                int i = obj.Invoke<string, int>("FUNKY");
            }
            catch (AttributeNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine(obj.Invoke<string, int>("funcy"));
            Console.ReadLine();
        }

        private class MarkedObject : IMarkedExtendable
        { }
    }
}

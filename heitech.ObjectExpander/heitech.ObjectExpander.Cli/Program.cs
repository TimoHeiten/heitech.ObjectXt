using heitech.ObjectExpander.Extender;
using heitech.ObjectExpander.ExtensionMap;
using heitech.ObjectExpander.Util;
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
                for (int i = 0; i < 1000; i++)
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

            Console.WriteLine("######################################");
            Console.WriteLine("TestPropertyManager:");
            TestPropertyMapper(obj);

            Console.ReadLine();
        }

        /// <summary>
        /// (sloppy coded) integration test
        /// </summary>
        private static void TestPropertyMapper(MarkedObject obj)
        {
            var mapper = obj.GeneratePropertyManager();
            if (mapper.TryGetProperty(nameof(MarkedObject.No), out int val))
            {
                Console.WriteLine(val + " number initially (should be zero)");
                Set(mapper, nameof(MarkedObject.No), 42);
                Console.WriteLine("after it was set: " + obj.No);
            }

            string name = nameof(MarkedObject.IntNo);
            if (mapper.TryGetProperty(name, out val))
            {
                Console.WriteLine(val + " 'Internal' number initially (should be zero)");
                Set(mapper, name, 112);
                Console.WriteLine("after it was set: " + obj.IntNo);
            }

            name = nameof(MarkedObject.IntText);
            if (mapper.TryGetProperty(name, out string s))
            {
                Console.WriteLine(s + " number initially (should be null/empty)");
                Set(mapper, name, "wassettothistext");
                Console.WriteLine("after it was set: " + obj.IntText);
            }

            name = nameof(MarkedObject.StaticText);
            if (mapper.TryGetProperty(name, out s))
            {
                Console.WriteLine(s + " static initially (should be null/empty)");
                Set(mapper, name, "Static was set to this text");
                Console.WriteLine("after it was set: " + MarkedObject.StaticText);
            }
        }

        private static void Set(IMappedPropertyManager mapper, string name, object val)
            => mapper.TrySetProperty(name, val);


        private class MarkedObject : IMarkedExtendable
        {
            public int No { get; }
            public string Text { get; }

            internal string IntText { get; }
            internal int IntNo { get; }

            public static string StaticText { get; }
        }
    }
}

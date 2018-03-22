using System;
namespace heitech.ObjectXt.AttributeExtension
{
    public class ConcurrentAttributeExtender<T> : AttributeExtenderBase<T>
    {
        private readonly object locker = new object();

        public override void Add(T key, object obj)
        {
            lock (locker)
            {
                base.Add(key, obj);
            }
        }

        public override (bool hasValue, T key, A attribute) GetKeyAttributePair<A>(T key)
        {
            lock (locker)
            {
                return base.GetKeyAttributePair<A>(key);
            }
        }

        public override bool HasAttribute(T key)
        {
            lock (locker)
            {
                return base.HasAttribute(key);
            }
        }

        public override bool HasAttributeOfType<A>(out T key)
        {
            lock (locker)
            {
                return base.HasAttributeOfType<A>(out key);
            }
        }

        public override void Remove(T key)
        {
            lock (locker)
            {
                base.Remove(key);
            }
        }

        public override bool TryGetAttribute<A>(T key, out A attribute)
        {
            lock (locker)
            {
                return base.TryGetAttribute(key, out attribute);
            }
        }

        public override object this[T key]
        {
            get { lock (locker) { return base[key]; } }
            set { lock (locker) { base[key] = value; } }
        }
    }
}

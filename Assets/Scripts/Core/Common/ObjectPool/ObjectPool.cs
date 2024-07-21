using System;
using System.Collections.Generic;

namespace DoodleJump.Core
{
    public sealed class ObjectPool<T> : IObjectPool<T> where T : class
    {
        private readonly List<T> _objects;
        private readonly Func<T> _createFunc;

        public ObjectPool(Func<T> createFunc, int capacity = 10)
        {
            _createFunc = createFunc ?? throw new ArgumentNullException(nameof(createFunc));

            _objects = new(capacity);
        }

        public T Get()
        {
            T value;

            if (_objects.Count != 0)
            {
                var index = _objects.Count - 1;
                value = _objects[index];

                _objects.RemoveAt(index);
            }
            else
            {
                value = _createFunc();
            }

            return value;
        }

        public void Release(T pooledObject)
        {
            _objects.Add(pooledObject);
        }

        public void Destroy()
        {
            _objects.Clear();
        }
    }
}

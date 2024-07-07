using System;
using System.Collections.Generic;

namespace DoodleJump.Core
{
    internal sealed class ObjectPool<T> : IDisposable, IObjectPool<T> where T : class
    {
        private readonly List<T> _objects;
        private readonly Func<T> _createFunc;

        internal ObjectPool(Func<T> createFunc, int capacity = 10)
        {
            _createFunc = createFunc ?? throw new ArgumentNullException(nameof(createFunc));

            _objects = new List<T>(capacity);
        }

        public void Clear()
        {
            _objects.Clear();
        }

        public void Dispose()
        {
            Clear();
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
    }
}

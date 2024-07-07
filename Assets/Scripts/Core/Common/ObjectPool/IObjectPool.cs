namespace DoodleJump.Core
{
    internal interface IObjectPool<T> where T : class
    {
        public T Get();

        public void Release(T pooledObject);

        public void Dispose();
    }
}

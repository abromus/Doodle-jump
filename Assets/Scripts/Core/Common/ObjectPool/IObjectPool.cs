namespace DoodleJump.Core
{
    public interface IObjectPool<T> : IDestroyable where T : class
    {
        public T Get();

        public void Release(T pooledObject);
    }
}

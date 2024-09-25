namespace DoodleJump.Game.Data
{
    internal interface IPersistentDataStorage
    {
        public void Init();

        public void Save();

        public void Dispose();

        public TData GetData<TData>() where TData : class, IPersistentData;
    }
}

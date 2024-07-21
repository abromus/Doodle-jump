namespace DoodleJump.Game.Data
{
    internal interface IPersistentDataStorage
    {
        public TData GetData<TData>() where TData : class, IPersistentData;
    }
}

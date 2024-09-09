namespace DoodleJump.Game.Services
{
    internal interface ISaveLoadService : Core.Services.IService
    {
        public Data.IPersistentDataStorage PersistentDataStorage { get; }
    }
}

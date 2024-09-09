namespace DoodleJump.Game.Data
{
    internal interface IGameData : Core.IDestroyable
    {
        public Core.Data.ICoreData CoreData { get; }

        public Core.Settings.IConfigStorage ConfigStorage { get; }

        public Core.Factories.IFactoryStorage FactoryStorage { get; }

        public Core.Services.IServiceStorage ServiceStorage { get; }
    }
}

namespace DoodleJump.Core.Data
{
    public interface ICoreData : IDestroyable
    {
        public Settings.IConfigStorage ConfigStorage { get; }

        public Factories.IFactoryStorage FactoryStorage { get; }

        public Services.IServiceStorage ServiceStorage { get; }
    }
}

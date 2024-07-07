namespace DoodleJump.Core.Factories
{
    public interface IFactoryStorage : IDestroyable
    {
        public void AddFactory<TFactory>(TFactory factory) where TFactory : class, IFactory;

        public TFactory GetFactory<TFactory>() where TFactory : class, IFactory;
    }
}

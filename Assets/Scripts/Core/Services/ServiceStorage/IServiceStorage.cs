namespace DoodleJump.Core.Services
{
    public interface IServiceStorage : IDestroyable
    {
        public UnityEngine.Transform UiServiceContainer { get; }

        public void AddService<TService>(TService service) where TService : class, IService;

        public TService GetService<TService>() where TService : class, IService;
    }
}

using DoodleJump.Core.Factories;
using DoodleJump.Core.Services;
using DoodleJump.Core.Settings;

namespace DoodleJump.Core.Data
{
    public interface ICoreData : IDestroyable
    {
        public IConfigStorage ConfigStorage { get; }

        public IFactoryStorage FactoryStorage { get; }

        public IServiceStorage ServiceStorage { get; }
    }
}

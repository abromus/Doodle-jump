using DoodleJump.Core;
using DoodleJump.Core.Data;
using DoodleJump.Core.Factories;
using DoodleJump.Core.Services;
using DoodleJump.Core.Settings;

namespace DoodleJump.Game.Data
{
    internal interface IGameData : IDestroyable
    {
        public ICoreData CoreData { get; }

        public IConfigStorage ConfigStorage { get; }

        public IFactoryStorage FactoryStorage { get; }

        public IServiceStorage ServiceStorage { get; }
    }
}

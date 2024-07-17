using DoodleJump.Core.Factories;
using DoodleJump.Game.Worlds.Entities;

namespace DoodleJump.Game.Factories
{
    internal interface IDoodlerFactory : IFactory
    {
        public void Init(DoodlerArgs args);

        public IDoodler Create();
    }
}

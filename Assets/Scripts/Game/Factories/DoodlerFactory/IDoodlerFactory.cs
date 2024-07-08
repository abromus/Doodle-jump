using DoodleJump.Core.Factories;
using DoodleJump.Game.Entities;

namespace DoodleJump.Game.Factories
{
    internal interface IDoodlerFactory : IFactory
    {
        public IDoodler Create();

        public void Init(DoodlerArgs args);
    }
}

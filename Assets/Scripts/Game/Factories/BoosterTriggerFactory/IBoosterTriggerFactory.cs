using DoodleJump.Core.Factories;
using DoodleJump.Game.Settings;
using DoodleJump.Game.Worlds.Boosters;

namespace DoodleJump.Game.Factories
{
    internal interface IBoosterTriggerFactory : IFactory
    {
        public void Init(Worlds.Entities.IDoodler doodler);

        public IBoosterTrigger Create(IBoosterCollisionInfo info, IBooster booster, IBoosterConfig boosterConfig);
    }
}

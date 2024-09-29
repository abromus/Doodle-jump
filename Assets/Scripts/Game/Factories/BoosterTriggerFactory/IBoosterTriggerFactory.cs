using DoodleJump.Core.Factories;
using DoodleJump.Game.Settings;
using DoodleJump.Game.Worlds.Boosters;

namespace DoodleJump.Game.Factories
{
    internal interface IBoosterTriggerFactory : IFactory
    {
        public void Init(Data.IPersistentDataStorage persistentDataStorage, Worlds.Entities.IDoodler doodler);

        public IBoosterTrigger Create(IBoosterCollisionInfo info, BoosterTriggerType triggerType);

        public IBoosterTrigger Create(IBoosterCollisionInfo info, IWorldBoosterConfig worldBoosterConfig);
    }
}

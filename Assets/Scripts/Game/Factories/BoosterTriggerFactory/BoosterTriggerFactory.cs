using DoodleJump.Game.Settings;
using DoodleJump.Game.Worlds.Boosters;

namespace DoodleJump.Game.Factories
{
    internal sealed class BoosterTriggerFactory : IBoosterTriggerFactory
    {
        private Worlds.Entities.IDoodler _doodler;

        public void Init(Worlds.Entities.IDoodler doodler)
        {
            _doodler = doodler;
        }

        public IBoosterTrigger Create(IBoosterCollisionInfo info, IWorldBoosterConfig worldBoosterConfig)
        {
            var triggerType = worldBoosterConfig.TriggerType;

            switch (triggerType)
            {
                case BoosterTriggerType.Collect:
                    return CreateCollectTrigger(info);
                default:
                    break;
            }

            return null;
        }

        public void Destroy() { }

        private IBoosterTrigger CreateCollectTrigger(IBoosterCollisionInfo info)
        {
            var trigger = new CollectTrigger(_doodler, info);

            return trigger;
        }
    }
}

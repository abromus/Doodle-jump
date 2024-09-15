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

        public IBoosterTrigger Create(IBoosterCollisionInfo info, IBooster booster, IBoosterConfig boosterConfig)
        {
            var triggerType = boosterConfig.TriggerType;

            switch (triggerType)
            {
                case BoosterTriggerType.Shield:
                    return CreateShieldTrigger(booster);
                default:
                    break;
            }

            return null;
        }

        public void Destroy() { }

        private IBoosterTrigger CreateShieldTrigger(IBooster booster)
        {
            var trigger = new ShieldTrigger(_doodler, booster);

            return trigger;
        }
    }
}

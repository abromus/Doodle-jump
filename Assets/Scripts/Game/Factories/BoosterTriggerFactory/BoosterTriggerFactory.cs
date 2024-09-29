using DoodleJump.Game.Data;
using DoodleJump.Game.Settings;
using DoodleJump.Game.Worlds.Boosters;

namespace DoodleJump.Game.Factories
{
    internal sealed class BoosterTriggerFactory : IBoosterTriggerFactory
    {
        private Worlds.Entities.IDoodler _doodler;
        private IPlayerData _playerData;

        public void Init(IPersistentDataStorage persistentDataStorage, Worlds.Entities.IDoodler doodler)
        {
            _playerData = persistentDataStorage.GetPlayerData();
            _doodler = doodler;
        }

        public IBoosterTrigger Create(IBoosterCollisionInfo info, IWorldBoosterConfig worldBoosterConfig)
        {
            var triggerType = worldBoosterConfig.TriggerType;

            switch (triggerType)
            {
                case BoosterTriggerType.Collectable:
                    return CreateCollectableTrigger(info);
                case BoosterTriggerType.Money:
                    return CreateMoneyTrigger(info);
                default:
                    break;
            }

            return null;
        }

        public void Destroy() { }

        private IBoosterTrigger CreateCollectableTrigger(IBoosterCollisionInfo info)
        {
            var trigger = new CollectableTrigger(_doodler, info);

            return trigger;
        }

        private IBoosterTrigger CreateMoneyTrigger(IBoosterCollisionInfo info)
        {
            var trigger = new MoneyTrigger(_playerData, info);

            return trigger;
        }
    }
}

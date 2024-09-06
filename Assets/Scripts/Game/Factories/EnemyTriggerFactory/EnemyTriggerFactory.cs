using DoodleJump.Game.Settings;
using DoodleJump.Game.Worlds;
using DoodleJump.Game.Worlds.Entities;

namespace DoodleJump.Game.Factories
{
    internal sealed class EnemyTriggerFactory : IEnemyTriggerFactory
    {
        private IDoodler _doodler;
        private IWorldData _worldData;

        public void Init(IDoodler doodler, IWorldData worldData)
        {
            _doodler = doodler;
            _worldData = worldData;
        }

        public IEnemyTrigger Create(IEnemyCollisionInfo info, IEnemy enemy, IEnemyConfig enemyConfig)
        {
            var triggerType = enemyConfig.TriggerType;

            switch (triggerType)
            {
                case EnemyTriggerType.Destroy:
                    return CreateDestroyTrigger(enemy);
                default:
                    break;
            }

            return null;
        }

        public void Destroy() { }

        private IEnemyTrigger CreateDestroyTrigger(IEnemy enemy)
        {
            var trigger = new DestroyTrigger(_worldData, enemy);

            return trigger;
        }
    }
}

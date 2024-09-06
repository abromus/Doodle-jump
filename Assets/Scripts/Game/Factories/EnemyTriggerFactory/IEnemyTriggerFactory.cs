using DoodleJump.Core.Factories;
using DoodleJump.Game.Settings;
using DoodleJump.Game.Worlds;
using DoodleJump.Game.Worlds.Entities;

namespace DoodleJump.Game.Factories
{
    internal interface IEnemyTriggerFactory : IFactory
    {
        public void Init(IDoodler doodler, IWorldData worldData);

        public IEnemyTrigger Create(IEnemyCollisionInfo info, IEnemy enemy, IEnemyConfig enemyConfig);
    }
}

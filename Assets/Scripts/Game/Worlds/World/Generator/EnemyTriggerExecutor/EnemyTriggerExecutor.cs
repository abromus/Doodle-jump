using System.Collections.Generic;
using DoodleJump.Game.Factories;
using DoodleJump.Game.Settings;
using DoodleJump.Game.Worlds.Entities;

namespace DoodleJump.Game.Worlds
{
    internal sealed class EnemyTriggerExecutor : IEnemyTriggerExecutor
    {
        private readonly IEnemyTriggerFactory _factory;
        private readonly Dictionary<IEnemy, IEnemyTrigger> _triggers = new(16);

        internal EnemyTriggerExecutor(IEnemyTriggerFactory factory)
        {
            _factory = factory;
        }

        public void Execute(IProgressInfo currentProgress, IEnemyCollisionInfo info)
        {
            var trigger = GetTrigger(currentProgress, info);

            trigger.Execute();
        }

        private IEnemyTrigger GetTrigger(IProgressInfo currentProgress, IEnemyCollisionInfo info)
        {
            var enemy = info.Enemy;

            if (_triggers.ContainsKey(enemy) == false)
            {
                var newTrigger = CreateTrigger(currentProgress, info, enemy);

                return newTrigger;
            }

            var trigger = _triggers[enemy];
            trigger.UpdateInfo(info);

            return trigger;
        }

        private IEnemyTrigger CreateTrigger(IProgressInfo currentProgress, IEnemyCollisionInfo info, IEnemy enemy)
        {
            var enemyId = enemy.Id;
            var configs = currentProgress.EnemyConfigs;

            foreach (var config in configs)
            {
                if (config.EnemyPrefab.Id.Equals(enemyId) == false)
                    continue;

                var trigger = _factory.Create(info, enemy, config);

                _triggers.Add(enemy, trigger);

                return trigger;
            }

            return null;
        }
    }
}

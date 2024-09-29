using System.Collections.Generic;
using DoodleJump.Game.Factories;
using DoodleJump.Game.Settings;
using DoodleJump.Game.Worlds.Boosters;

namespace DoodleJump.Game.Worlds
{
    internal sealed class BoosterTriggerExecutor : IBoosterTriggerExecutor
    {
        private readonly IBoosterTriggerFactory _factory;
        private readonly Dictionary<IWorldBooster, IBoosterTrigger> _triggers = new(16);

        internal BoosterTriggerExecutor(IBoosterTriggerFactory factory)
        {
            _factory = factory;
        }

        public void Execute(IProgressInfo currentProgress, IBoosterCollisionInfo info)
        {
            var trigger = GetTrigger(currentProgress, info);

            trigger.Execute();
        }

        public void Execute(IWorldBooster worldBooster, BoosterTriggerType boosterTriggerType)
        {
            var trigger = GetTrigger(worldBooster, boosterTriggerType);

            trigger.Execute();
        }

        private IBoosterTrigger GetTrigger(IProgressInfo currentProgress, IBoosterCollisionInfo info)
        {
            var worldBooster = info.WorldBooster;

            if (_triggers.ContainsKey(worldBooster) == false)
            {
                var newTrigger = CreateTrigger(currentProgress, info);

                return newTrigger;
            }

            var trigger = _triggers[worldBooster];
            trigger.UpdateInfo(info);

            return trigger;
        }

        private IBoosterTrigger GetTrigger(IWorldBooster worldBooster, BoosterTriggerType boosterTriggerType)
        {
            if (_triggers.ContainsKey(worldBooster) == false)
            {
                var newTrigger = CreateTrigger(worldBooster, boosterTriggerType);

                return newTrigger;
            }

            var trigger = _triggers[worldBooster];
            trigger.UpdateInfo(worldBooster.Info);

            return trigger;
        }

        private IBoosterTrigger CreateTrigger(IProgressInfo currentProgress, IBoosterCollisionInfo info)
        {
            var booster = info.WorldBooster;
            var boosterId = booster.Id;
            var configs = currentProgress.WorldBoosterConfigs;

            foreach (var config in configs)
            {
                if (config.WorldBoosterPrefab.Id.Equals(boosterId) == false)
                    continue;

                var trigger = _factory.Create(info, config);

                _triggers.Add(booster, trigger);

                return trigger;
            }

            return null;
        }

        private IBoosterTrigger CreateTrigger(IWorldBooster worldBooster, BoosterTriggerType boosterTriggerType)
        {
            var trigger = _factory.Create(worldBooster.Info, boosterTriggerType);

            _triggers.Add(worldBooster, trigger);

            return trigger;
        }
    }
}

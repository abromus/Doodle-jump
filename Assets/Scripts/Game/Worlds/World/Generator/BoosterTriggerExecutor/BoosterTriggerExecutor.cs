using System.Collections.Generic;
using DoodleJump.Game.Factories;
using DoodleJump.Game.Settings;
using DoodleJump.Game.Worlds.Boosters;

namespace DoodleJump.Game.Worlds
{
    internal sealed class BoosterTriggerExecutor : IBoosterTriggerExecutor
    {
        private readonly IBoosterTriggerFactory _factory;
        private readonly Dictionary<IBooster, IBoosterTrigger> _triggers = new(16);

        internal BoosterTriggerExecutor(IBoosterTriggerFactory factory)
        {
            _factory = factory;
        }

        public void Execute(IProgressInfo currentProgress, IBoosterCollisionInfo info)
        {
            var trigger = GetTrigger(currentProgress, info);

            trigger.Execute();
        }

        private IBoosterTrigger GetTrigger(IProgressInfo currentProgress, IBoosterCollisionInfo info)
        {
            var booster = info.Booster;

            if (_triggers.ContainsKey(booster) == false)
            {
                var newTrigger = CreateTrigger(currentProgress, info, booster);

                return newTrigger;
            }

            var trigger = _triggers[booster];
            trigger.UpdateInfo(info);

            return trigger;
        }

        private IBoosterTrigger CreateTrigger(IProgressInfo currentProgress, IBoosterCollisionInfo info, IBooster booster)
        {
            var boosterId = booster.Id;
            var configs = currentProgress.BoosterConfigs;

            foreach (var config in configs)
            {
                if (config.BoosterPrefab.Id.Equals(boosterId) == false)
                    continue;

                var trigger = _factory.Create(info, booster, config);

                _triggers.Add(booster, trigger);

                return trigger;
            }

            return null;
        }
    }
}

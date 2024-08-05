using System.Collections.Generic;
using DoodleJump.Game.Factories;
using DoodleJump.Game.Settings;

namespace DoodleJump.Game.Worlds
{
    internal sealed class TriggerExecutor : ITriggerExecutor
    {
        private readonly ITriggerFactory _factory;
        private readonly Dictionary<IPlatform, ITrigger> _triggers = new(16);

        internal TriggerExecutor(ITriggerFactory factory)
        {
            _factory = factory;
        }

        public void Execute(IProgressInfo currentProgress, IPlatformCollisionInfo info)
        {
            var trigger = GetTrigger(currentProgress, info);

            trigger.Execute();
        }

        private ITrigger GetTrigger(IProgressInfo currentProgress, IPlatformCollisionInfo info)
        {
            var platform = info.Platform;

            if (_triggers.ContainsKey(platform) == false)
            {
                var newTrigger = CreateTrigger(currentProgress, info, platform);

                return newTrigger;
            }

            var trigger = _triggers[platform];
            trigger.UpdateInfo(info);

            return trigger;
        }

        private ITrigger CreateTrigger(IProgressInfo currentProgress, IPlatformCollisionInfo info, IPlatform platform)
        {
            var platformId = platform.Id;
            var configs = currentProgress.PlatformConfigs;

            foreach (var config in configs)
            {
                if (config.PlatformPrefab.Id.Equals(platformId) == false)
                    continue;

                var trigger = _factory.Create(info, platform, config);

                _triggers.Add(platform, trigger);

                return trigger;
            }

            return null;
        }
    }
}

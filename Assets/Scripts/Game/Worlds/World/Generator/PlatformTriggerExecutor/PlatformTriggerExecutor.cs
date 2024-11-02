using System.Collections.Generic;
using DoodleJump.Game.Factories;
using DoodleJump.Game.Settings;
using DoodleJump.Game.Worlds.Platforms;

namespace DoodleJump.Game.Worlds
{
    internal sealed class PlatformTriggerExecutor : IPlatformTriggerExecutor
    {
        private readonly IPlatformTriggerFactory _factory;
        private readonly Dictionary<IPlatform, IPlatformTrigger> _triggers = new(16);

        internal PlatformTriggerExecutor(IPlatformTriggerFactory factory)
        {
            _factory = factory;
        }

        public void Execute(IProgressInfo currentProgress, IPlatformCollisionInfo info)
        {
            var trigger = GetTrigger(currentProgress, info);

            trigger.Execute();
        }

        private IPlatformTrigger GetTrigger(IProgressInfo currentProgress, IPlatformCollisionInfo info)
        {
            var platform = info.Platform;
            var platformId = platform.Id;
            var configs = currentProgress.PlatformConfigs;

            if (_triggers.ContainsKey(platform) == false)
            {
                var newTrigger = CreateTrigger(currentProgress, info, platform);

                return newTrigger;
            }

            var trigger = _triggers[platform];

            foreach (var config in configs)
            {
                if (config.PlatformPrefab.Id.Equals(platformId) == false)
                    continue;

                trigger.UpdateInfo(info, config);

                break;
            }

            return trigger;
        }

        private IPlatformTrigger CreateTrigger(IProgressInfo currentProgress, IPlatformCollisionInfo info, IPlatform platform)
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

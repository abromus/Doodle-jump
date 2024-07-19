using System.Collections.Generic;
using DoodleJump.Game.Factories;
using DoodleJump.Game.Settings;

namespace DoodleJump.Game.Worlds
{
    internal sealed class TriggerExecutor : ITriggerExecutor
    {
        private readonly ITriggerFactory _factory;
        private readonly IPlatformsConfig _platformsConfig;

        private readonly Dictionary<IPlatform, ITrigger> _triggers = new(16);

        internal TriggerExecutor(ITriggerFactory factory, IPlatformsConfig platformsConfig)
        {
            _factory = factory;
            _platformsConfig = platformsConfig;
        }

        public void Execute(IPlatform platform)
        {
            var trigger = GetTrigger(platform);

            trigger.Execute();
        }

        private ITrigger GetTrigger(IPlatform platform)
        {
            if (_triggers.ContainsKey(platform))
                return _triggers[platform];

            var trigger = CreateTrigger(platform);

            return trigger;
        }

        private ITrigger CreateTrigger(IPlatform platform)
        {
            var platformId = platform.Id;
            var configs = _platformsConfig.Configs;

            foreach (var config in configs)
            {
                if (config.PlatformPrefab.Id.Equals(platformId) == false)
                    continue;

                var trigger = _factory.Create(platform, config);

                _triggers.Add(platform, trigger);

                return trigger;
            }

            return null;
        }
    }
}

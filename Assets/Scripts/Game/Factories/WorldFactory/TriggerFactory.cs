﻿using DoodleJump.Game.Settings;
using DoodleJump.Game.Worlds;
using DoodleJump.Game.Worlds.Entities;

namespace DoodleJump.Game.Factories
{
    internal sealed class TriggerFactory : ITriggerFactory
    {
        private IDoodler _doodler;

        public void Init(IDoodler doodler)
        {
            _doodler = doodler;
        }

        public ITrigger Create(IPlatform platform, IPlatformConfig platformConfig)
        {
            var triggerType = platformConfig.TriggerType;

            switch (triggerType)
            {
                case TriggerType.Jump:
                    return CreateJumpTrigger(platformConfig);
                case TriggerType.Destroy:
                    return CreateDestroyTrigger(platform);
                default:
                    break;
            }

            return null;
        }

        public void Destroy() { }

        private ITrigger CreateJumpTrigger(IPlatformConfig platformConfig)
        {
            if (platformConfig is not IJumpConfig jumpConfig)
                return null;

            var trigger = new JumpTrigger(_doodler, jumpConfig.JumpForce);

            return trigger;
        }

        private ITrigger CreateDestroyTrigger(IPlatform platform)
        {
            var trigger = new DestroyTrigger(platform);

            return trigger;
        }
    }
}
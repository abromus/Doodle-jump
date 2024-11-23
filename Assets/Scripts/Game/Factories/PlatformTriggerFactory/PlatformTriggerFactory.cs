using DoodleJump.Game.Settings;
using DoodleJump.Game.Worlds.Platforms;

namespace DoodleJump.Game.Factories
{
    internal sealed class PlatformTriggerFactory : IPlatformTriggerFactory
    {
        private Worlds.Entities.IDoodler _doodler;

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Init(Worlds.Entities.IDoodler doodler)
        {
            _doodler = doodler;
        }

        public IPlatformTrigger Create(IPlatformCollisionInfo info, IPlatform platform, IPlatformConfig platformConfig)
        {
            var triggerType = platformConfig.TriggerType;

            switch (triggerType)
            {
                case PlatformTriggerType.Jump:
                    return CreateJumpTrigger(platformConfig);
                case PlatformTriggerType.SpringJump:
                    return CreateSpringJumpTrigger(info, platformConfig);
                case PlatformTriggerType.Destroy:
                    return CreateDestroyTrigger(platform);
                case PlatformTriggerType.Temporary:
                    return CreateTemporaryTrigger(platformConfig);
                case PlatformTriggerType.Quantity:
                    return CreateQuantityTrigger(platformConfig);
                default:
                    break;
            }

            return null;
        }

        private IPlatformTrigger CreateJumpTrigger(IPlatformConfig platformConfig)
        {
            if (platformConfig is not IJumpConfig jumpConfig)
                return null;

            var trigger = new JumpTrigger(_doodler, jumpConfig.JumpForce);

            return trigger;
        }

        private IPlatformTrigger CreateSpringJumpTrigger(IPlatformCollisionInfo info, IPlatformConfig platformConfig)
        {
            if (platformConfig is not ISpringJumpConfig springJumpConfig)
                return null;

            var trigger = new SpringJumpTrigger(info, _doodler, springJumpConfig.JumpForce, springJumpConfig.SpringJumpForce);

            return trigger;
        }

        private IPlatformTrigger CreateDestroyTrigger(IPlatform platform)
        {
            var trigger = new DestroyTrigger(platform);

            return trigger;
        }

        private IPlatformTrigger CreateTemporaryTrigger(IPlatformConfig platformConfig)
        {
            if (platformConfig is not ITemporaryConfig temporaryConfig)
                return null;

            var trigger = new TemporaryTrigger(_doodler, temporaryConfig.JumpForce);

            return trigger;
        }

        private IPlatformTrigger CreateQuantityTrigger(IPlatformConfig platformConfig)
        {
            if (platformConfig is not IQuantityConfig quantityConfig)
                return null;

            var trigger = new QuantityTrigger(_doodler, quantityConfig.JumpForce);

            return trigger;
        }
    }
}

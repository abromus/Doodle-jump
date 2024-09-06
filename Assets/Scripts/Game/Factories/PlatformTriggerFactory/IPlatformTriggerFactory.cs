using DoodleJump.Core.Factories;
using DoodleJump.Game.Settings;
using DoodleJump.Game.Worlds.Entities;
using DoodleJump.Game.Worlds.Platforms;

namespace DoodleJump.Game.Factories
{
    internal interface IPlatformTriggerFactory : IFactory
    {
        public void Init(IDoodler doodler);

        public IPlatformTrigger Create(IPlatformCollisionInfo info, IPlatform platform, IPlatformConfig platformConfig);
    }
}

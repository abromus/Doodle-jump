using DoodleJump.Core.Factories;
using DoodleJump.Game.Settings;
using DoodleJump.Game.Worlds;
using DoodleJump.Game.Worlds.Entities;

namespace DoodleJump.Game.Factories
{
    internal interface ITriggerFactory : IFactory
    {
        public void Init(IDoodler doodler);

        public ITrigger Create(IPlatform platform, IPlatformConfig platformConfig);
    }
}

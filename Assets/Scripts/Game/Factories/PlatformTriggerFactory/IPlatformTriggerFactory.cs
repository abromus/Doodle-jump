namespace DoodleJump.Game.Factories
{
    internal interface IPlatformTriggerFactory : Core.Factories.IFactory
    {
        public void Init(Worlds.Entities.IDoodler doodler);

        public Worlds.Platforms.IPlatformTrigger Create(Worlds.Platforms.IPlatformCollisionInfo info, Worlds.Platforms.IPlatform platform, Settings.IPlatformConfig platformConfig);
    }
}

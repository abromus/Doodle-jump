namespace DoodleJump.Game.Worlds
{
    internal interface IWorldInitializer
    {
        public IWorldData WorldData { get; }

        public IDoodlerChecker DoodlerChecker { get; }

        public IGenerator Generator { get; }

        public IBackgroundChecker BackgroundChecker { get; }

        public ICameraFollower CameraFollower { get; }
    }
}

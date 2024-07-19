namespace DoodleJump.Game.Settings
{
    internal interface IConfigData
    {
        public ICameraConfig CameraConfig { get; }

        public IDoodlerConfig DoodlerConfig { get; }

        public IGeneratorConfig GeneratorConfig { get; }

        public IPlatformsConfig PlatformsConfig { get; }
    }
}

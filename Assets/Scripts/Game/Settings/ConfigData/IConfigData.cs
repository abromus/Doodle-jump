namespace DoodleJump.Game.Settings
{
    internal interface IConfigData
    {
        public ICameraConfig CameraConfig { get; }

        public IDoodlerConfig DoodlerConfig { get; }
    }
}

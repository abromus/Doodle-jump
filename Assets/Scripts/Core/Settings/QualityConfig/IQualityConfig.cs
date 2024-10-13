namespace DoodleJump.Core.Settings
{
    public interface IQualityConfig : IConfig
    {
        public bool IsVerticalSyncEnabled { get; }

        public bool IsFpsUnlimited { get; }

        public int MinFps { get; }

        public int CurrentFps { get; }

        public int MaxFps { get; }
    }
}

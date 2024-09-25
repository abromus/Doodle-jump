namespace DoodleJump.Game.Settings
{
    public interface IScreenSystemConfig : Core.Settings.IConfig
    {
        public IMainScreenConfig MainScreenConfig { get; }
    }
}

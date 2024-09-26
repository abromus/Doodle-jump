namespace DoodleJump.Game.Settings
{
    internal interface IBoostersConfig : Core.Settings.IConfig
    {
        public System.Collections.Generic.IReadOnlyList<IBoosterConfig> BoosterConfigs { get; }

        public T GetBoosterConfig<T>() where T : IBoosterConfig;
    }
}

namespace DoodleJump.Core.Settings
{
    public interface IConfigStorage : IConfig, IDestroyable
    {
        public void AddConfig<TConfig>(TConfig config) where TConfig : class, IConfig;

        public TConfig GetConfig<TConfig>() where TConfig : class, IConfig;
    }
}

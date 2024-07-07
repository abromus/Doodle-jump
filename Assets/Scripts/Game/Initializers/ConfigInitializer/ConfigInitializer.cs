using DoodleJump.Game.Settings;

namespace DoodleJump.Game.Initializers
{
    internal sealed class ConfigInitializer : IConfigInitializer
    {
        private readonly IGame _game;
        private readonly IConfigData _configData;

        internal ConfigInitializer(IGame game, IConfigData configData)
        {
            _game = game;
            _configData = configData;
        }

        public void Initialize()
        {
            InitConfigs();
        }

        private void InitConfigs()
        {
            var storage = _game.GameData.ConfigStorage;
        }
    }
}

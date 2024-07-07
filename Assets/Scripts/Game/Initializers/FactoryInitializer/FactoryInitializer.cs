using DoodleJump.Core.Factories;
using DoodleJump.Core.Settings;

namespace DoodleJump.Game.Initializers
{
    internal sealed class FactoryInitializer : IFactoryInitializer
    {
        private readonly IGame _game;
        private readonly IFactoryStorage _factoryStorage;

        internal FactoryInitializer(IGame game)
        {
            _game = game;

            _factoryStorage = _game.GameData.FactoryStorage;
        }

        public void Initialize()
        {
            InitFactories();
        }

        private void InitFactories()
        {
            var gameData = _game.GameData;
            var configStorage = gameData.ConfigStorage;
            var uiFactories = configStorage.GetUiFactoryConfig().UiFactories;
        }
    }
}

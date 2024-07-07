using DoodleJump.Core.Settings;

namespace DoodleJump.Game.Initializers
{
    internal sealed class ServiceInitializer : IServiceInitializer
    {
        private readonly IGame _game;

        internal ServiceInitializer(IGame game)
        {
            _game = game;
        }

        public void Initialize()
        {
            InitServices();
        }

        private void InitServices()
        {
            var serviceStorage = _game.GameData.ServiceStorage;
            var uiServices = _game.GameData.ConfigStorage.GetUiServiceConfig().UiServices;
        }
    }
}

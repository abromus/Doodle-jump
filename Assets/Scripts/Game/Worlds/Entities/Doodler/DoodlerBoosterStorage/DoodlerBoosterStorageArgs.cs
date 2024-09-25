namespace DoodleJump.Game.Worlds.Entities
{
    internal readonly struct DoodlerBoosterStorageArgs
    {
        private readonly UnityEngine.Transform _boosterContainer;
        private readonly Core.Services.IUpdater _updater;
        private readonly Factories.IBoosterFactory _boosterFactory;
        private readonly Data.IPlayerData _playerData;
        private readonly Settings.IBoostersConfig _boostersConfig;

        internal UnityEngine.Transform BoosterContainer => _boosterContainer;

        internal Core.Services.IUpdater Updater => _updater;

        internal Factories.IBoosterFactory BoosterFactory => _boosterFactory;

        internal Data.IPlayerData PlayerData => _playerData;

        internal Settings.IBoostersConfig BoostersConfig => _boostersConfig;

        internal DoodlerBoosterStorageArgs(
            UnityEngine.Transform boosterContainer,
            Core.Services.IUpdater updater,
            Factories.IBoosterFactory boosterFactory,
            Data.IPlayerData playerData,
            Settings.IBoostersConfig boostersConfig)
        {
            _boosterContainer = boosterContainer;
            _updater = updater;
            _boosterFactory = boosterFactory;
            _playerData = playerData;
            _boostersConfig = boostersConfig;
        }
    }
}

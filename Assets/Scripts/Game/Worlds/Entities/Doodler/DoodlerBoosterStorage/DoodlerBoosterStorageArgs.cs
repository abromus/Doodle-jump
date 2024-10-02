namespace DoodleJump.Game.Worlds.Entities
{
    internal readonly struct DoodlerBoosterStorageArgs
    {
        private readonly Core.Services.IUpdater _updater;
        private readonly Factories.IBoosterFactory _boosterFactory;
        private readonly Data.IPlayerData _playerData;
        private readonly Settings.IBoostersConfig _boostersConfig;
        private readonly UnityEngine.Transform _boosterContainer;
        private readonly IDoodler _doodler;
        private readonly UnityEngine.Rigidbody2D _rigidbody;

        internal readonly Core.Services.IUpdater Updater => _updater;

        internal readonly Factories.IBoosterFactory BoosterFactory => _boosterFactory;

        internal readonly Data.IPlayerData PlayerData => _playerData;

        internal readonly Settings.IBoostersConfig BoostersConfig => _boostersConfig;

        internal readonly UnityEngine.Transform BoosterContainer => _boosterContainer;

        internal readonly IDoodler Doodler => _doodler;

        internal readonly UnityEngine.Rigidbody2D Rigidbody => _rigidbody;

        internal DoodlerBoosterStorageArgs(
            Core.Services.IUpdater updater,
            Factories.IBoosterFactory boosterFactory,
            Data.IPlayerData playerData,
            Settings.IBoostersConfig boostersConfig,
            UnityEngine.Transform boosterContainer,
            IDoodler doodler,
            UnityEngine.Rigidbody2D rigidbody)
        {
            _updater = updater;
            _boosterFactory = boosterFactory;
            _playerData = playerData;
            _boostersConfig = boostersConfig;
            _boosterContainer = boosterContainer;
            _doodler = doodler;
            _rigidbody = rigidbody;
        }
    }
}

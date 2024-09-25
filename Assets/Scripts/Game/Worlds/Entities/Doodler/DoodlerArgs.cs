using DoodleJump.Core.Services;
using DoodleJump.Game.Data;
using DoodleJump.Game.Factories;
using DoodleJump.Game.Services;
using DoodleJump.Game.Settings;

namespace DoodleJump.Game.Worlds.Entities
{
    internal readonly struct DoodlerArgs
    {
        private readonly IUpdater _updater;
        private readonly IAudioService _audioService;
        private readonly IInputService _inputService;
        private readonly ICameraService _cameraService;
        private readonly IBoosterFactory _boosterFactory;
        private readonly IDoodlerConfig _doodlerConfig;
        private readonly IBoostersConfig _boostersConfig;
        private readonly IPlayerData _playerData;

        internal IUpdater Updater => _updater;

        internal IAudioService AudioService => _audioService;

        internal IInputService InputService => _inputService;

        internal ICameraService CameraService => _cameraService;

        internal IBoosterFactory BoosterFactory => _boosterFactory;

        internal IDoodlerConfig DoodlerConfig => _doodlerConfig;

        internal IBoostersConfig BoostersConfig => _boostersConfig;

        internal IPlayerData PlayerData => _playerData;

        internal DoodlerArgs(
            IUpdater updater,
            IAudioService audioService,
            IInputService inputService,
            ICameraService cameraService,
            IBoosterFactory boosterFactory,
            IDoodlerConfig doodlerConfig,
            IBoostersConfig boostersConfig,
            IPlayerData playerData)
        {
            _updater = updater;
            _audioService = audioService;
            _inputService = inputService;
            _cameraService = cameraService;
            _boosterFactory = boosterFactory;
            _doodlerConfig = doodlerConfig;
            _boostersConfig = boostersConfig;
            _playerData = playerData;
        }
    }
}

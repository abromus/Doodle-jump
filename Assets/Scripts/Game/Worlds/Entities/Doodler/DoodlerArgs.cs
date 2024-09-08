using DoodleJump.Core.Services;
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
        private readonly IDoodlerConfig _doodlerConfig;

        internal IUpdater Updater => _updater;

        internal IAudioService AudioService => _audioService;

        internal IInputService InputService => _inputService;

        internal ICameraService CameraService => _cameraService;

        internal IDoodlerConfig DoodlerConfig => _doodlerConfig;

        internal DoodlerArgs(
            IUpdater updater,
            IAudioService audioService,
            IInputService inputService,
            ICameraService cameraService,
            IDoodlerConfig doodlerConfig)
        {
            _updater = updater;
            _audioService = audioService;
            _inputService = inputService;
            _cameraService = cameraService;
            _doodlerConfig = doodlerConfig;
        }
    }
}

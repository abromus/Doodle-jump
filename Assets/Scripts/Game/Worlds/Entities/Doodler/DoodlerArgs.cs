using DoodleJump.Core.Services;
using DoodleJump.Game.Settings;

namespace DoodleJump.Game.Worlds.Entities
{
    internal readonly struct DoodlerArgs
    {
        private readonly IUpdater _updater;
        private readonly IInputService _inputService;
        private readonly ICameraService _cameraService;
        private readonly ICameraConfig _cameraConfig;
        private readonly IDoodlerConfig _doodlerConfig;

        internal IUpdater Updater => _updater;

        internal IInputService InputService => _inputService;

        internal ICameraService CameraService => _cameraService;

        internal ICameraConfig CameraConfig => _cameraConfig;

        internal IDoodlerConfig DoodlerConfig => _doodlerConfig;

        internal DoodlerArgs(
            IUpdater updater,
            IInputService inputService,
            ICameraService cameraService,
            ICameraConfig cameraConfig,
            IDoodlerConfig doodlerConfig)
        {
            _updater = updater;
            _inputService = inputService;
            _cameraService = cameraService;
            _cameraConfig = cameraConfig;
            _doodlerConfig = doodlerConfig;
        }
    }
}

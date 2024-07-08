using DoodleJump.Core.Services;
using DoodleJump.Game.Settings;

namespace DoodleJump.Game.Entities
{
    public readonly struct DoodlerArgs
    {
        private readonly IUpdater _updater;
        private readonly IInputService _inputService;
        private readonly ICameraService _cameraService;
        private readonly ICameraConfig _cameraConfig;
        private readonly IDoodlerConfig _doodlerConfig;

        public IUpdater Updater => _updater;

        public IInputService InputService => _inputService;

        public ICameraService CameraService => _cameraService;

        public ICameraConfig CameraConfig => _cameraConfig;

        public IDoodlerConfig DoodlerConfig => _doodlerConfig;

        public DoodlerArgs(
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

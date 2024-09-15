using DoodleJump.Core.Services;
using DoodleJump.Game.Services;
using UnityEngine;

namespace DoodleJump.Game.Worlds.Entities
{
    internal readonly struct DoodlerShootingArgs
    {
        private readonly Transform _doodlerTransform;
        private readonly IDoodlerInput _doodlerInput;
        private readonly IAudioService _audioService;
        private readonly ICameraService _cameraService;
        private readonly IUpdater _updater;
        private readonly bool _canShootAround;
        private readonly Projectile _projectilePrefab;

        internal Transform DoodlerTransform => _doodlerTransform;

        internal IDoodlerInput DoodlerInput => _doodlerInput;

        internal IAudioService AudioService => _audioService;

        internal ICameraService CameraService => _cameraService;

        internal IUpdater Updater => _updater;

        internal bool CanShootAround => _canShootAround;

        internal Projectile ProjectilePrefab => _projectilePrefab;

        internal DoodlerShootingArgs(
            Transform doodlerTransform,
            IDoodlerInput doodlerInput,
            IAudioService audioService,
            ICameraService cameraService,
            IUpdater updater,
            bool canShootAround,
            Projectile projectilePrefab)
        {
            _doodlerTransform = doodlerTransform;
            _doodlerInput = doodlerInput;
            _audioService = audioService;
            _cameraService = cameraService;
            _updater = updater;
            _canShootAround = canShootAround;
            _projectilePrefab = projectilePrefab;
        }
    }
}

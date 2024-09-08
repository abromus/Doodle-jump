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
        private readonly IUpdater _updater;
        private readonly Camera _camera;
        private readonly Projectile _projectilePrefab;

        internal Transform DoodlerTransform => _doodlerTransform;

        internal IDoodlerInput DoodlerInput => _doodlerInput;

        internal IAudioService AudioService => _audioService;

        internal IUpdater Updater => _updater;

        internal Camera Camera => _camera;

        internal Projectile ProjectilePrefab => _projectilePrefab;

        internal DoodlerShootingArgs(
            Transform doodlerTransform,
            IDoodlerInput doodlerInput,
            IAudioService audioService,
            IUpdater updater,
            Camera camera,
            Projectile projectilePrefab)
        {
            _doodlerTransform = doodlerTransform;
            _doodlerInput = doodlerInput;
            _audioService = audioService;
            _updater = updater;
            _camera = camera;
            _projectilePrefab = projectilePrefab;
        }
    }
}

﻿using DoodleJump.Core.Services;
using DoodleJump.Game.Data;
using DoodleJump.Game.Services;
using DoodleJump.Game.Settings;
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
        private readonly IPlayerData _playerData;
        private readonly IDoodlerConfig _doodlerConfig;
        private readonly Projectile _projectilePrefab;
        private readonly DoodlerNose _nose;

        internal readonly Transform DoodlerTransform => _doodlerTransform;

        internal readonly IDoodlerInput DoodlerInput => _doodlerInput;

        internal readonly IAudioService AudioService => _audioService;

        internal readonly ICameraService CameraService => _cameraService;

        internal readonly IUpdater Updater => _updater;

        internal readonly IPlayerData PlayerData => _playerData;

        internal readonly IDoodlerConfig DoodlerConfig => _doodlerConfig;

        internal readonly Projectile ProjectilePrefab => _projectilePrefab;

        internal readonly DoodlerNose Nose => _nose;

        internal DoodlerShootingArgs(
            Transform doodlerTransform,
            IDoodlerInput doodlerInput,
            IAudioService audioService,
            ICameraService cameraService,
            IUpdater updater,
            IPlayerData playerData,
            IDoodlerConfig doodlerConfig,
            Projectile projectilePrefab,
            DoodlerNose nose)
        {
            _doodlerTransform = doodlerTransform;
            _doodlerInput = doodlerInput;
            _audioService = audioService;
            _cameraService = cameraService;
            _updater = updater;
            _playerData = playerData;
            _doodlerConfig = doodlerConfig;
            _projectilePrefab = projectilePrefab;
            _nose = nose;
        }
    }
}

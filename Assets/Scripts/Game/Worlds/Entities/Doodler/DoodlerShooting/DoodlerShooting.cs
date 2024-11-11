using System.Collections.Generic;
using DoodleJump.Core;
using DoodleJump.Core.Services;
using DoodleJump.Game.Data;
using DoodleJump.Game.Services;
using UnityEngine;

namespace DoodleJump.Game.Worlds.Entities
{
    internal sealed class DoodlerShooting : IDoodlerShooting
    {
        private Transform _projectilesContainer;
        private bool _isPaused;
        private NoseInfo _noseInfo;

        private readonly Transform _doodlerTransform;
        private readonly IDoodlerInput _doodlerInput;
        private readonly IAudioService _audioService;
        private readonly ICameraService _cameraService;
        private readonly IUpdater _updater;
        private readonly IPlayerData _playerData;
        private readonly Projectile _projectilePrefab;
        private readonly DoodlerNose _nose;

        private readonly IObjectPool<IProjectile> _projectilePool;
        private readonly List<IProjectile> _projectiles = new(32);
        private readonly IShootingStrategyResolver _shootingStrategyResolver;

        internal DoodlerShooting(in DoodlerShootingArgs args)
        {
            _doodlerTransform = args.DoodlerTransform;
            _doodlerInput = args.DoodlerInput;
            _audioService = args.AudioService;
            _cameraService = args.CameraService;
            _updater = args.Updater;
            _playerData = args.PlayerData;
            _projectilePrefab = args.ProjectilePrefab;
            _nose = args.Nose;

            _projectilePool = new ObjectPool<IProjectile>(CreateProjectile);
            _shootingStrategyResolver = new ShootingStrategyResolver(_cameraService, args.DoodlerConfig);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void SetProjectileContainer(Transform projectilesContainer)
        {
            _projectilesContainer = projectilesContainer;
        }

        public void Restart()
        {
            _nose.transform.SetPositionAndRotation(_nose.RotationOffset, Quaternion.identity);

            _playerData.SetCurrentShots(_playerData.MaxShots);

            foreach (var projectile in _projectiles)
            {
                projectile.Destroyed -= OnProjectileDestroyed;
                projectile.Destroy();

                _projectilePool.Release(projectile);
            }

            _projectiles.Clear();
        }

        public void Tick(float deltaTime)
        {
            if (CanShoot() == false)
                return;

            CheckNosePosition();
            InitProjectilePosition();
            UpdateShots(_playerData.CurrentShots - 1);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void SetPause(bool isPaused)
        {
            _isPaused = isPaused;
        }

        public void Destroy()
        {
            foreach (var projectile in _projectiles)
            {
                projectile.Destroyed -= OnProjectileDestroyed;

                _projectilePool.Release(projectile);

                Object.Destroy(projectile.GameObject);
            }

            _projectiles.Clear();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private bool CanShoot()
        {
            return _doodlerInput.IsShooting && _isPaused == false && 0 < _playerData.CurrentShots;
        }

        private IProjectile CreateProjectile()
        {
            var projectile = Object.Instantiate(_projectilePrefab, _projectilesContainer);
            projectile.Init(_audioService, _updater, _shootingStrategyResolver.ShootingStrategy);
            projectile.GameObject.RemoveCloneSuffix();
            projectile.Destroyed += OnProjectileDestroyed;

            return projectile;
        }

        private void CheckNosePosition()
        {
            var shootPosition = _doodlerInput.ShootPosition;
            var shootWorldPosition = _cameraService.Camera.ScreenToWorldPoint(shootPosition);
            shootWorldPosition.z = 0f;

            var shootDirection = (shootWorldPosition - _doodlerTransform.position).normalized;
            _noseInfo = _shootingStrategyResolver.ShootingStrategy.GetNoseInfo(shootDirection, _nose);

            _nose.transform.SetPositionAndRotation(_noseInfo.Position, _noseInfo.Rotation);
        }

        private void InitProjectilePosition()
        {
            var nosePosition = _noseInfo.ShootPosition;
            var doodlerDirection = _doodlerTransform.localScale.x == Constants.Left ? Constants.Left : Constants.Right;
            var shootPosition = _doodlerInput.ShootPosition;
            var projectile = _projectilePool.Get();
            projectile.InitPosition(nosePosition, doodlerDirection, shootPosition);

            _projectiles.Add(projectile);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void UpdateShots(int currentShots)
        {
            _playerData.SetCurrentShots(currentShots);
        }

        private void OnProjectileDestroyed(IProjectile projectile)
        {
            projectile.Clear();

            _projectiles.Remove(projectile);
            _projectilePool.Release(projectile);
        }
    }
}

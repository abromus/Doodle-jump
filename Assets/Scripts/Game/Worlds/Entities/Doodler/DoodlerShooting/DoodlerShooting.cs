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

        private readonly Transform _doodlerTransform;
        private readonly IDoodlerInput _doodlerInput;
        private readonly IAudioService _audioService;
        private readonly ICameraService _cameraService;
        private readonly IUpdater _updater;
        private readonly IPlayerData _playerData;
        private readonly Projectile _projectilePrefab;

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
            _playerData.SetCurrentShots(_playerData.MaxShots);

            foreach (var projectile in _projectiles)
            {
                projectile.Destroyed -= OnProjectileDestroyed;
                projectile.Destroy();

                _projectilePool.Release(projectile);
            }

            _projectiles.Clear();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Tick(float deltaTime)
        {
            TryShoot();
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

        private void TryShoot()
        {
            var currentShots = _playerData.CurrentShots;

            if (_doodlerInput.IsShooting == false || _isPaused || currentShots == 0)
                return;

            InitProjectilePosition();
            UpdateShots(currentShots - 1);
        }

        private IProjectile CreateProjectile()
        {
            var projectile = Object.Instantiate(_projectilePrefab, _projectilesContainer);
            projectile.Init(_audioService, _updater, _cameraService, _shootingStrategyResolver.ShootingStrategy);
            projectile.GameObject.RemoveCloneSuffix();
            projectile.Destroyed += OnProjectileDestroyed;

            return projectile;
        }

        private void InitProjectilePosition()
        {
            var doodlerDirection = _doodlerTransform.localScale.x == Constants.Left ? Constants.Left : Constants.Right;
            var doodlerPosition = _doodlerTransform.position;
            var shootPosition = _doodlerInput.ShootPosition;
            var projectile = _projectilePool.Get();
            projectile.InitPosition(doodlerPosition, doodlerDirection, shootPosition);

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

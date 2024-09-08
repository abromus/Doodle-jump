using System.Collections.Generic;
using DoodleJump.Core;
using DoodleJump.Core.Services;
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
        private readonly IUpdater _updater;
        private readonly Camera _camera;
        private readonly Projectile _projectilePrefab;

        private readonly IObjectPool<IProjectile> _projectilePool;
        private readonly List<IProjectile> _projectiles = new(32);

        internal DoodlerShooting(DoodlerShootingArgs args)
        {
            _doodlerTransform = args.DoodlerTransform;
            _doodlerInput = args.DoodlerInput;
            _audioService = args.AudioService;
            _updater = args.Updater;
            _camera = args.Camera;
            _projectilePrefab = args.ProjectilePrefab;

            _projectilePool = new ObjectPool<IProjectile>(CreateProjectile);
        }

        public void SetProjectileContainer(Transform projectilesContainer)
        {
            _projectilesContainer = projectilesContainer;
        }

        public void Tick(float deltaTime)
        {
            TryShoot();
        }

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

                UnityEngine.Object.Destroy(projectile.GameObject);
            }

            _projectiles.Clear();
        }

        private void TryShoot()
        {
            if (_doodlerInput.IsShooting == false || _isPaused)
                return;

            var doodlerDirection = _doodlerTransform.localScale.x == Constants.Left ? Constants.Left : Constants.Right;
            var doodlerPosition = _doodlerTransform.position;
            var shootPosition = _doodlerInput.ShootPosition;
            var shootWorldPosition = _camera.ScreenToWorldPoint(shootPosition);
            shootWorldPosition.z = 0f;

            Debug.DrawLine(doodlerPosition, shootWorldPosition, Color.blue);
            var shootDirection = (shootWorldPosition - doodlerPosition).normalized;
            Debug.DrawRay(doodlerPosition, shootDirection, Color.green);

            var projectile = _projectilePool.Get();
            projectile.InitPosition(doodlerPosition, doodlerDirection, shootDirection);

            _projectiles.Add(projectile);
        }

        private IProjectile CreateProjectile()
        {
            var projectile = UnityEngine.Object.Instantiate(_projectilePrefab, _projectilesContainer);
            projectile.Init(_audioService, _updater);
            projectile.GameObject.RemoveCloneSuffix();
            projectile.Destroyed += OnProjectileDestroyed;

            return projectile;
        }

        private void OnProjectileDestroyed(IProjectile projectile)
        {
            projectile.Clear();

            _projectiles.Remove(projectile);
            _projectilePool.Release(projectile);
        }
    }
}

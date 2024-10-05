using DoodleJump.Game.Settings;
using DoodleJump.Game.Worlds.Entities;
using DoodleJump.Game.Worlds.Platforms;
using UnityEngine;

namespace DoodleJump.Game.Worlds
{
    internal sealed class Generator : IGenerator
    {
        private readonly Transform _cameraTransform;
        private readonly Transform _doodlerTransform;
        private readonly Rect _screenRect;
        private readonly IPlatformStorage _platformStorage;
        private readonly IEnemyStorage _enemyStorage;
        private readonly IBoosterStorage _boosterStorage;
        private readonly IPlatformTriggerExecutor _platformTriggerExecutor;
        private readonly IEnemyTriggerExecutor _enemyTriggerExecutor;
        private readonly IBoosterTriggerExecutor _boosterTriggerExecutor;

        internal Generator(
            Data.IGameData gameData,
            in WorldArgs args,
            Rect screenRect,
            Transform platformsContainer,
            Transform enemiesContainer,
            Transform boostersContainer)
        {
            _screenRect = screenRect;

            _cameraTransform = args.CameraService.Camera.transform;
            _doodlerTransform = args.Doodler.GameObject.transform;

            var boosterTriggerFactory = args.BoosterTriggerFactory;

            _platformStorage = new PlatformStorage(gameData, in args, platformsContainer, _screenRect);
            _platformStorage.Collided += OnPlatformCollided;

            _enemyStorage = new EnemyStorage(gameData, in args, enemiesContainer, _screenRect, boosterTriggerFactory);
            _enemyStorage.Collided += OnEnemyCollided;
            _enemyStorage.BoosterDropped += OnEnemyBoosterDropped;

            _boosterStorage = new BoosterStorage(gameData, in args, boostersContainer, _screenRect, _platformStorage);
            _boosterStorage.Collided += OnBoosterCollided;

            _platformTriggerExecutor = new PlatformTriggerExecutor(args.PlatformTriggerFactory);
            _enemyTriggerExecutor = new EnemyTriggerExecutor(args.EnemyTriggerFactory);
            _boosterTriggerExecutor = new BoosterTriggerExecutor(boosterTriggerFactory);
        }

        public void Restart()
        {
            _platformStorage.Clear();
            _platformStorage.GenerateStartPlatform();
            _platformStorage.GeneratePlatforms();

            _enemyStorage.Clear();
            _enemyStorage.GenerateEnemies();

            _boosterStorage.Clear();
            _boosterStorage.GenerateBoosters();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Tick()
        {
            CheckDoodlerPosition();
        }

        public void Destroy()
        {
            _platformStorage.Collided -= OnPlatformCollided;
            _platformStorage.Destroy();

            _enemyStorage.Collided -= OnEnemyCollided;
            _enemyStorage.BoosterDropped -= OnEnemyBoosterDropped;
            _enemyStorage.Destroy();

            _boosterStorage.Collided -= OnBoosterCollided;
            _boosterStorage.Destroy();
        }

        private void CheckDoodlerPosition()
        {
            var doodlerPosition = _doodlerTransform.position.y;
            var halfScreenHeight = _screenRect.height * Constants.Half;
            var cameraPosition = _cameraTransform.position.y;

            if (doodlerPosition < cameraPosition)
                return;

            var minPosition = cameraPosition - halfScreenHeight;

            ClearPlatforms(minPosition);
            ClearEnemies(minPosition);
            ClearBoosters(minPosition);

            if (doodlerPosition + _screenRect.height < _platformStorage.HighestPlatformY)
                return;

            _platformStorage.GeneratePlatforms();
            _enemyStorage.GenerateEnemies();
            _boosterStorage.GenerateBoosters();
        }

        private void ClearPlatforms(float minPosition)
        {
            var platforms = _platformStorage.Platforms;
            var count = platforms.Count;

            for (int i = count - 1; 0 < i + 1; i--)
            {
                var platform = platforms[i];

                if (platform.Position.y < minPosition)
                    _platformStorage.DestroyPlatform(platform);
            }
        }

        private void ClearEnemies(float minPosition)
        {
            var enemies = _enemyStorage.Enemies;
            var count = enemies.Count;

            for (int i = count - 1; 0 < i + 1; i--)
            {
                var enemy = enemies[i];

                if (enemy.Position.y < minPosition)
                    _enemyStorage.DestroyEnemy(enemy);
            }
        }

        private void ClearBoosters(float minPosition)
        {
            var boosters = _boosterStorage.WorldBoosters;
            var count = boosters.Count;

            for (int i = count - 1; 0 < i + 1; i--)
            {
                var booster = boosters[i];

                if (booster.Position.y < minPosition)
                    _boosterStorage.DestroyBooster(booster);
            }
        }

        private void OnPlatformCollided(IProgressInfo currentProgress, IPlatformCollisionInfo info)
        {
            _platformTriggerExecutor.Execute(currentProgress, info);
        }

        private void OnEnemyCollided(IProgressInfo currentProgress, IEnemyCollisionInfo info)
        {
            _enemyTriggerExecutor.Execute(currentProgress, info);
        }

        private void OnEnemyBoosterDropped(Boosters.IWorldBooster worldBooster, Boosters.BoosterTriggerType boosterTriggerType)
        {
            _boosterTriggerExecutor.Execute(worldBooster, boosterTriggerType);
        }

        private void OnBoosterCollided(IProgressInfo currentProgress, Boosters.IBoosterCollisionInfo info)
        {
            _boosterTriggerExecutor.Execute(currentProgress, info);
        }
    }
}

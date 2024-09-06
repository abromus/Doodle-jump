using DoodleJump.Game.Settings;
using DoodleJump.Game.Worlds.Entities;
using DoodleJump.Game.Worlds.Platforms;
using UnityEngine;

namespace DoodleJump.Game.Worlds
{
    internal sealed class Generator : IGenerator
    {
        private readonly Transform _doodlerTransform;
        private readonly Rect _screenRect;
        private readonly IPlatformStorage _platformStorage;
        private readonly IEnemyStorage _enemyStorage;
        private readonly IPlatformTriggerExecutor _platformTriggerExecutor;
        private readonly IEnemyTriggerExecutor _enemyTriggerExecutor;

        internal Generator(Data.IGameData gameData, WorldArgs args, Rect screenRect, Transform platformsContainer, Transform enemiesContainer)
        {
            _screenRect = screenRect;

            _doodlerTransform = args.Doodler.GameObject.transform;

            _platformStorage = new PlatformStorage(gameData, args, platformsContainer, _screenRect);
            _platformStorage.Collided += OnPlatformCollided;

            _enemyStorage = new EnemyStorage(gameData, args, enemiesContainer, _screenRect);
            _enemyStorage.Collided += OnEnemyCollided;

            _platformTriggerExecutor = new PlatformTriggerExecutor(args.PlatformTriggerFactory);
            _enemyTriggerExecutor = new EnemyTriggerExecutor(args.EnemyTriggerFactory);

            Restart();
        }

        public void Restart()
        {
            _platformStorage.Clear();
            _platformStorage.GenerateStartPlatform();
            _platformStorage.GeneratePlatforms();

            _enemyStorage.Clear();
            _enemyStorage.GenerateEnemies();
        }

        public void Tick()
        {
            CheckDoodlerPosition();
        }

        public void Destroy()
        {
            _platformStorage.Collided -= OnPlatformCollided;
            _platformStorage.Destroy();

            _enemyStorage.Collided -= OnEnemyCollided;
            _enemyStorage.Destroy();
        }

        private void CheckDoodlerPosition()
        {
            var doodlerPosition = _doodlerTransform.position.y;
            var screenHeight = _screenRect.height;

            if (doodlerPosition + screenHeight < _platformStorage.HighestPlatformY)
                return;

            ClearPlatforms(doodlerPosition, screenHeight);
            ClearEnemies(doodlerPosition, screenHeight);

            _platformStorage.GeneratePlatforms();
            _enemyStorage.GenerateEnemies();
        }

        private void ClearPlatforms(float doodlerPosition, float screenHeight)
        {
            var platforms = _platformStorage.Platforms;
            var count = platforms.Count;

            for (int i = count - 1; 0 < i + 1; i--)
            {
                var platform = platforms[i];

                if (platform.Position.y < doodlerPosition - screenHeight)
                    _platformStorage.DestroyPlatform(platform);
            }
        }

        private void ClearEnemies(float doodlerPosition, float screenHeight)
        {
            var enemies = _enemyStorage.Enemies;
            var count = enemies.Count;

            for (int i = count - 1; 0 < i + 1; i--)
            {
                var enemy = enemies[i];

                if (enemy.Position.y < doodlerPosition - screenHeight)
                    _enemyStorage.DestroyEnemy(enemy);
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
    }
}

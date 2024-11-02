using System.Collections.Generic;
using DoodleJump.Core;
using DoodleJump.Game.Data;
using DoodleJump.Game.Factories;
using DoodleJump.Game.Settings;
using DoodleJump.Game.Worlds.Entities;
using UnityEngine;

namespace DoodleJump.Game.Worlds
{
    internal sealed class EnemyStorage : IEnemyStorage
    {
        private Vector3 _currentEnemyPosition;
        private float _highestEnemyY;
        private float _spawnChanceFactor = 0f;

        private bool _isMaxProgress;
        private IProgressInfo _currentProgress;
        private int _generatedEnemiesCount;
        private float _enemySpawnProbability;

        private readonly IGameData _gameData;
        private readonly IWorldFactory _worldFactory;
        private readonly Transform _enemiesContainer;
        private readonly Rect _screenRect;
        private readonly IBoosterTriggerFactory _boosterTriggerFactory;
        private readonly Transform _doodlerTransform;

        private readonly Vector3 _startPosition;
        private readonly IProgressInfo[] _progressInfos;

        private readonly List<IProbable> _probables = new(16);
        private readonly List<IEnemyConfig> _enemyConfigs = new(16);
        private readonly List<IEnemy> _enemies = new(256);
        private readonly Dictionary<int, IObjectPool<IEnemy>> _pools = new(16);

        public IReadOnlyList<IEnemy> Enemies => _enemies;

        public event System.Action<IProgressInfo, IEnemyCollisionInfo> Collided;

        public event System.Action<Boosters.IWorldBooster, Boosters.BoosterTriggerType> BoosterDropped;

        internal EnemyStorage(IGameData gameData, in WorldArgs args, Transform enemiesContainer, in Rect screenRect, IBoosterTriggerFactory boosterTriggerFactory)
        {
            _gameData = gameData;
            _worldFactory = args.WorldFactory;
            _enemiesContainer = enemiesContainer;
            _screenRect = screenRect;
            _boosterTriggerFactory = boosterTriggerFactory;
            _doodlerTransform = args.Doodler.GameObject.transform;

            var generatorConfig = args.GeneratorConfig;
            _startPosition = generatorConfig.EnemiesStartPosition;

            _currentEnemyPosition = _startPosition;
            _highestEnemyY = _startPosition.y;
            _progressInfos = generatorConfig.ProgressInfos;

            CheckCurrentProgress();
            InitEnemyConfigs();
            InitPools();
        }

        public void Clear()
        {
            Destroy();

            _currentEnemyPosition = _startPosition;
            _highestEnemyY = _startPosition.y;
            _isMaxProgress = false;
            _generatedEnemiesCount = 0;

            CheckCurrentProgress();
        }

        public void GenerateEnemies()
        {
            CheckCurrentProgress();

            var enemyMaxCount = _currentProgress.EnemyMaxCount;

            for (int i = 0; i < _currentProgress.EnemySimultaneouslyCount; i++)
                if (enemyMaxCount == -1 || _generatedEnemiesCount < enemyMaxCount)
                    TryGenerateEnemy();
        }

        public void DestroyEnemy(IEnemy enemy)
        {
            enemy.Collided -= OnCollided;
            enemy.BoosterDropped -= OnBoosterDropped;
            enemy.Destroyed -= OnDestroyed;
            enemy.Clear();

            _pools[enemy.Id].Release(enemy);
            _enemies.Remove(enemy);
        }

        public void Destroy()
        {
            var count = _enemies.Count;

            for (int i = count - 1; 0 < i + 1; i--)
                DestroyEnemy(_enemies[i]);
        }

        private void InitEnemyConfigs()
        {
            _enemyConfigs.Clear();
            _probables.Clear();

            var configs = _currentProgress.EnemyConfigs;

            if (configs == null)
                return;

            var spawnChanceSum = 0f;

            foreach (var config in configs)
            {
                spawnChanceSum += config.SpawnChance;

                _enemyConfigs.Add(config);
            }

            _spawnChanceFactor = 1f / spawnChanceSum;
            _enemyConfigs.Sort(SortByChance);
            _probables.AddRange(_enemyConfigs);

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            static int SortByChance(IEnemyConfig x, IEnemyConfig y)
            {
                return x.SpawnChance.CompareTo(y.SpawnChance);
            }
        }

        private void InitPools()
        {
            var configs = _currentProgress.EnemyConfigs;

            foreach (var config in configs)
            {
                var prefab = config.EnemyPrefab;

                if (_pools.ContainsKey(prefab.Id))
                    continue;

                var enemyMaxCount = _currentProgress.EnemyMaxCount;
                var capacity = 128;

                _pools.Add(prefab.Id, new ObjectPool<IEnemy>(() => CreateEnemy(prefab), enemyMaxCount < 0 ? capacity : enemyMaxCount));
            }
        }

        private IEnemy CreateEnemy<T>(T enemyPrefab) where T : MonoBehaviour, IEnemy
        {
            var enemy = _worldFactory.CreateEnemy(enemyPrefab, _enemiesContainer);
            enemy.Init(_gameData, _boosterTriggerFactory);

            return enemy;
        }

        private BaseEnemy GetEnemyPrefab()
        {
            var index = ProbableUtils.GetConfigIndex(_probables, _spawnChanceFactor);

            return _enemyConfigs[index].EnemyPrefab;
        }

        private void CheckCurrentProgress()
        {
            if (_isMaxProgress)
                return;

            foreach (var progressInfo in _progressInfos)
            {
                var minProgress = progressInfo.MinProgress;
                var maxProgress = progressInfo.MaxProgress;
                var doodlerPosition = _doodlerTransform.position.y;

                if (doodlerPosition < minProgress || minProgress <= doodlerPosition && doodlerPosition <= maxProgress)
                {
                    if (_currentProgress == progressInfo)
                        return;

                    _currentProgress = progressInfo;
                    _generatedEnemiesCount = 0;
                    _enemySpawnProbability = 0f;

                    InitEnemyConfigs();
                    InitPools();

                    return;
                }
            }

            _isMaxProgress = true;
            _currentProgress = _progressInfos[_progressInfos.Length - 1];
        }

        private void GenerateNextPosition()
        {
            var targetPosition = _doodlerTransform.position.y + _screenRect.height;

            if (_highestEnemyY < targetPosition)
                _highestEnemyY = targetPosition;

            _currentEnemyPosition.y = _highestEnemyY + Random.Range(_currentProgress.MinOffsetY, _currentProgress.MaxOffsetY);
            _currentEnemyPosition.x = Random.Range(_screenRect.xMin, _screenRect.xMax);
        }

        private bool IsIntersectedEnemies(in Vector3 currentEnemyPosition, in Vector2 size)
        {
            foreach (var enemy in _enemies)
                if (enemy.IsIntersectedArea(currentEnemyPosition, size))
                    return true;

            return false;
        }

        private void GenerateEnemy(BaseEnemy enemyPrefab)
        {
            var enemy = _pools[enemyPrefab.Id].Get();
            enemy.InitPosition(_currentEnemyPosition);
            enemy.Collided += OnCollided;
            enemy.BoosterDropped += OnBoosterDropped;
            enemy.Destroyed += OnDestroyed;

            ++_generatedEnemiesCount;

            _enemies.Add(enemy);
        }

        private void TryGenerateEnemy()
        {
            CheckCurrentProgress();
            GenerateNextPosition();

            if (CanGenerateEnemy())
            {
                var enemyPrefab = GetEnemyPrefab();
                var size = enemyPrefab.Size;

                if (enemyPrefab == null || IsIntersectedEnemies(_currentEnemyPosition, in size))
                    return;

                GenerateEnemy(enemyPrefab);
                CheckHighestPosition(_currentEnemyPosition.y);
            }
            else
            {
                UpdateEnemySpawnProbability();
            }
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private bool CanGenerateEnemy()
        {
            return _currentProgress.EnemySpawnProbability < _enemySpawnProbability + Random.value;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void UpdateEnemySpawnProbability()
        {
            _enemySpawnProbability += _currentProgress.EnemySpawnProbabilityFactor;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void CheckHighestPosition(float y)
        {
            if (_highestEnemyY < y)
                _highestEnemyY = y;
        }

        private void OnCollided(IEnemyCollisionInfo info)
        {
            Collided.SafeInvoke(_currentProgress, info);
        }

        private void OnBoosterDropped(Boosters.IWorldBooster worldBooster, Boosters.BoosterTriggerType boosterTriggerType)
        {
            BoosterDropped.SafeInvoke(worldBooster, boosterTriggerType);
        }

        private void OnDestroyed(IEnemy enemy)
        {
            DestroyEnemy(enemy);
        }
    }
}

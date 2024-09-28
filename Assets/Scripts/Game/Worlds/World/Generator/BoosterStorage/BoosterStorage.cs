using System.Collections.Generic;
using DoodleJump.Core;
using DoodleJump.Game.Data;
using DoodleJump.Game.Factories;
using DoodleJump.Game.Settings;
using DoodleJump.Game.Worlds.Boosters;
using DoodleJump.Game.Worlds.Platforms;
using UnityEngine;

namespace DoodleJump.Game.Worlds
{
    internal sealed class BoosterStorage : IBoosterStorage
    {
        private Vector3 _currentBoosterPosition;
        private float _highestBoosterY;
        private float _spawnChanceFactor = 0f;

        private bool _isMaxProgress;
        private IProgressInfo _currentProgress;
        private int _generatedBoostersCount;
        private float _boosterSpawnProbability;

        private readonly IGameData _gameData;
        private readonly IWorldFactory _worldFactory;
        private readonly Transform _boostersContainer;
        private readonly Rect _screenRect;
        private readonly IPlatformStorage _platformStorage;
        private readonly Transform _doodlerTransform;

        private readonly Vector3 _startPosition;
        private readonly IProgressInfo[] _progressInfos;

        private readonly List<IProbable> _probables = new(16);
        private readonly List<IWorldBoosterConfig> _worldBoosterConfigs = new(16);
        private readonly List<IWorldBooster> _worldBoosters = new(256);
        private readonly Dictionary<IPlatform, IWorldBooster> _platforms = new(256);
        private readonly List<IPlatform> _removedPlatforms = new(256);
        private readonly Dictionary<int, IObjectPool<IWorldBooster>> _pools = new(16);

        public float HighestBoosterY => _highestBoosterY;

        public IReadOnlyList<IWorldBooster> WorldBoosters => _worldBoosters;

        public event System.Action<IProgressInfo, IBoosterCollisionInfo> Collided;

        public BoosterStorage(IGameData gameData, WorldArgs args, Transform boostersContainer, Rect screenRect, IPlatformStorage platformStorage)
        {
            _gameData = gameData;
            _worldFactory = args.WorldFactory;
            _boostersContainer = boostersContainer;
            _screenRect = screenRect;
            _platformStorage = platformStorage;
            _doodlerTransform = args.Doodler.GameObject.transform;

            var generatorConfig = args.GeneratorConfig;
            _startPosition = generatorConfig.BoostersStartPosition;

            _currentBoosterPosition = _startPosition;
            _highestBoosterY = _startPosition.y;
            _progressInfos = generatorConfig.ProgressInfos;

            CheckCurrentProgress();
            InitBoosterConfigs();
            InitPools();
        }

        public void Clear()
        {
            Destroy();

            _currentBoosterPosition = _startPosition;
            _highestBoosterY = _startPosition.y;
            _isMaxProgress = false;
            _generatedBoostersCount = 0;

            CheckCurrentProgress();
        }

        public void GenerateBoosters()
        {
            CheckCurrentProgress();

            var boosterMaxCount = _currentProgress.BoosterMaxCount;

            for (int i = 0; i < _currentProgress.BoosterSimultaneouslyCount; i++)
                if (boosterMaxCount == -1 || _generatedBoostersCount < boosterMaxCount)
                    TryGenerateBooster();
        }

        public void DestroyBooster(IWorldBooster worldBooster)
        {
            foreach (var platformInfo in _platforms)
                if (platformInfo.Value == worldBooster)
                    _removedPlatforms.Add(platformInfo.Key);

            foreach (var platform in _removedPlatforms)
                _platforms.Remove(platform);

            _removedPlatforms.Clear();

            worldBooster.Collided -= OnCollided;
            worldBooster.Destroyed -= OnDestroyed;
            worldBooster.Clear();

            _pools[worldBooster.Id].Release(worldBooster);
            _worldBoosters.Remove(worldBooster);
        }

        public void Destroy()
        {
            var count = _worldBoosters.Count;

            for (int i = count - 1; 0 < i + 1; i--)
                DestroyBooster(_worldBoosters[i]);
        }

        private void InitBoosterConfigs()
        {
            _worldBoosterConfigs.Clear();
            _probables.Clear();

            var configs = _currentProgress.WorldBoosterConfigs;

            if (configs == null)
                return;

            var spawnChanceSum = 0f;

            foreach (var config in configs)
            {
                spawnChanceSum += config.SpawnChance;

                _worldBoosterConfigs.Add(config);
            }

            _spawnChanceFactor = 1f / spawnChanceSum;
            _worldBoosterConfigs.Sort(SortByChance);
            _probables.AddRange(_worldBoosterConfigs);

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            static int SortByChance(IWorldBoosterConfig x, IWorldBoosterConfig y)
            {
                return x.SpawnChance.CompareTo(y.SpawnChance);
            }
        }

        private void InitPools()
        {
            var configs = _currentProgress.WorldBoosterConfigs;

            foreach (var config in configs)
            {
                var prefab = config.WorldBoosterPrefab;
                var prefabId = prefab.Id;

                if (_pools.ContainsKey(prefabId))
                    continue;

                var boosterMaxCount = _currentProgress.BoosterMaxCount;
                var capacity = 64;

                _pools.Add(prefabId, new ObjectPool<IWorldBooster>(() => CreateBooster(prefab), boosterMaxCount < 0 ? capacity : boosterMaxCount));
            }
        }

        private IWorldBooster CreateBooster<T>(T worldBoosterPrefab) where T : MonoBehaviour, IWorldBooster
        {
            var booster = _worldFactory.CreateBooster(worldBoosterPrefab, _boostersContainer);
            booster.Init(_gameData);

            return booster;
        }

        private WorldBooster GetWorldBoosterPrefab()
        {
            var index = ProbableUtils.GetConfigIndex(_probables, _spawnChanceFactor);

            return _worldBoosterConfigs[index].WorldBoosterPrefab;
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
                    _generatedBoostersCount = 0;
                    _boosterSpawnProbability = 0f;

                    InitBoosterConfigs();
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

            if (_highestBoosterY < targetPosition)
                _highestBoosterY = targetPosition;

            _currentBoosterPosition.y = _highestBoosterY + Random.Range(_currentProgress.MinOffsetY, _currentProgress.MaxOffsetY);
            _currentBoosterPosition.x = Random.Range(_screenRect.xMin, _screenRect.xMax);
        }

        private IWorldBooster GenerateWorldBooster(WorldBooster worldBoosterPrefab)
        {
            var worldBooster = _pools[worldBoosterPrefab.Id].Get();
            worldBooster.InitPosition(_currentBoosterPosition);
            worldBooster.Collided += OnCollided;
            worldBooster.Destroyed += OnDestroyed;

            ++_generatedBoostersCount;

            _worldBoosters.Add(worldBooster);

            return worldBooster;
        }

        private void TryGenerateBooster()
        {
            CheckCurrentProgress();
            GenerateNextPosition();

            if (CanGenerateBooster())
            {
                var worldBoosterPrefab = GetWorldBoosterPrefab();

                if (worldBoosterPrefab == null || TryGetIntersectedPlatform(_currentBoosterPosition, worldBoosterPrefab.Size, out var platforms) == false)
                    return;

                var worldBooster = GenerateWorldBooster(worldBoosterPrefab);

                var platformIndex = Random.Range(0, platforms.Count);
                var platform = platforms[platformIndex];
                platform.InitBooster(worldBooster);
                platform.Destroyed += OnPlatformDestroyed;

                _platforms.Add(platform, worldBooster);

                CheckHighestPosition(_currentBoosterPosition.y);
            }
            else
            {
                UpdateBoosterSpawnProbability();
            }
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private bool CanGenerateBooster()
        {
            return _currentProgress.BoosterSpawnProbability < _boosterSpawnProbability + Random.value;
        }

        private bool TryGetIntersectedPlatform(Vector3 currentBoosterPosition, Vector2 size, out List<IPlatform> intersectedPlatforms)
        {
            var platforms = _platformStorage.Platforms;
            size.x = _screenRect.width;

            intersectedPlatforms = new(16);

            foreach (var platform in platforms)
            {
                if (platform.IsIntersectedArea(currentBoosterPosition, size) == false || _platforms.ContainsKey(platform))
                    continue;

                intersectedPlatforms.Add(platform);

                return true;
            }

            return false;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void UpdateBoosterSpawnProbability()
        {
            _boosterSpawnProbability += _currentProgress.BoosterSpawnProbabilityFactor;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void CheckHighestPosition(float y)
        {
            if (_highestBoosterY < y)
                _highestBoosterY = y;
        }

        private void OnCollided(IBoosterCollisionInfo info)
        {
            Collided.SafeInvoke(_currentProgress, info);
        }

        private void OnDestroyed(IWorldBooster worldBooster)
        {
            DestroyBooster(worldBooster);
        }

        private void OnPlatformDestroyed(IPlatform platform)
        {
            var booster = _platforms[platform];
            platform.Destroyed -= OnPlatformDestroyed;

            DestroyBooster(booster);

            _platforms.Remove(platform);
        }
    }
}

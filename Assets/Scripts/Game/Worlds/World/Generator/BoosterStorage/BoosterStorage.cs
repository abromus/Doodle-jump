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
        private readonly Rect _screenRect;
        private readonly IPlatformStorage _platformStorage;
        private readonly Transform _doodlerTransform;

        private readonly Vector3 _startPosition;
        private readonly IProgressInfo[] _progressInfos;

        private readonly List<IBoosterConfig> _boosterConfigs = new(16);
        private readonly List<IBooster> _boosters = new(256);
        private readonly Dictionary<IPlatform, IBooster> _platforms = new(256);
        private readonly List<IPlatform> _removedPlatforms = new(256);
        private readonly Dictionary<int, IObjectPool<IBooster>> _pools = new(16);

        public float HighestBoosterY => _highestBoosterY;

        public IReadOnlyList<IBooster> Boosters => _boosters;

        public event System.Action<IProgressInfo, IBoosterCollisionInfo> Collided;

        public BoosterStorage(IGameData gameData, WorldArgs args, Rect screenRect, IPlatformStorage platformStorage)
        {
            _gameData = gameData;
            _worldFactory = args.WorldFactory;
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

        public void DestroyBooster(IBooster booster)
        {
            foreach (var platformInfo in _platforms)
                if (platformInfo.Value == booster)
                    _removedPlatforms.Add(platformInfo.Key);

            foreach (var platform in _removedPlatforms)
                _platforms.Remove(platform);

            _removedPlatforms.Clear();

            booster.Collided -= OnCollided;
            booster.Destroyed -= OnDestroyed;
            booster.Clear();

            _pools[booster.Id].Release(booster);
            _boosters.Remove(booster);
        }

        public void Destroy()
        {
            var count = _boosters.Count;

            for (int i = count - 1; 0 < i + 1; i--)
                DestroyBooster(_boosters[i]);
        }

        private void InitBoosterConfigs()
        {
            _boosterConfigs.Clear();

            var configs = _currentProgress.BoosterConfigs;

            if (configs == null)
                return;

            var spawnChanceSum = 0f;

            foreach (var config in configs)
            {
                spawnChanceSum += config.SpawnChance;

                _boosterConfigs.Add(config);
            }

            _spawnChanceFactor = 1f / spawnChanceSum;
            _boosterConfigs.Sort(SortByChance);

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            static int SortByChance(IBoosterConfig x, IBoosterConfig y)
            {
                return x.SpawnChance.CompareTo(y.SpawnChance);
            }
        }

        private void InitPools()
        {
            var configs = _currentProgress.BoosterConfigs;

            foreach (var config in configs)
            {
                var prefab = config.BoosterPrefab;

                if (_pools.ContainsKey(prefab.Id))
                    continue;

                var boosterMaxCount = _currentProgress.BoosterMaxCount;
                var capacity = 64;

                _pools.Add(prefab.Id, new ObjectPool<IBooster>(() => CreateBooster(prefab), boosterMaxCount < 0 ? capacity : boosterMaxCount));
            }
        }

        private IBooster CreateBooster(Booster boosterPrefab)
        {
            var booster = _worldFactory.CreateBooster(boosterPrefab);
            booster.Init(_gameData);

            return booster;
        }

        private Booster GetBoosterPrefab()
        {
            var spawnChance = Random.value;

            foreach (var config in _boosterConfigs)
            {
                if (config.SpawnChance * _spawnChanceFactor < spawnChance)
                    continue;

                return config.BoosterPrefab;
            }

            return _boosterConfigs[_boosterConfigs.Count - 1].BoosterPrefab;
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

        private bool TryGetIntersectedPlatform(Vector3 currentBoosterPosition, Vector2 size, out List<IPlatform> intersectedPlatforms)
        {
            var platforms = _platformStorage.Platforms;
            size.x = _screenRect.width;

            intersectedPlatforms = new(16);

            foreach (var platform in platforms)
            {
                if (platform.IsIntersectedArea(currentBoosterPosition, size) == false)
                    continue;

                intersectedPlatforms.Add(platform);

                return true;
            }

            return false;
        }

        private IBooster GenerateBooster(Booster boosterPrefab)
        {
            var booster = _pools[boosterPrefab.Id].Get();
            booster.InitPosition(_currentBoosterPosition);
            booster.Collided += OnCollided;
            booster.Destroyed += OnDestroyed;

            ++_generatedBoostersCount;

            _boosters.Add(booster);

            return booster;
        }

        private void TryGenerateBooster()
        {
            CheckCurrentProgress();
            GenerateNextPosition();

            if (CanGenerateBooster())
            {
                var boosterPrefab = GetBoosterPrefab();

                if (boosterPrefab == null || TryGetIntersectedPlatform(_currentBoosterPosition, boosterPrefab.Size, out var platforms) == false)
                    return;

                var booster = GenerateBooster(boosterPrefab);

                var platformIndex = Random.Range(0, platforms.Count);
                var platform = platforms[platformIndex];
                platform.InitBooster(booster);
                platform.Destroyed += OnPlatformDestroyed;

                _platforms.Add(platform, booster);

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

        private void OnDestroyed(IBooster booster)
        {
            DestroyBooster(booster);
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

using System.Collections.Generic;
using DoodleJump.Core;
using DoodleJump.Game.Data;
using DoodleJump.Game.Factories;
using DoodleJump.Game.Services;
using DoodleJump.Game.Settings;
using UnityEngine;

namespace DoodleJump.Game.Worlds
{
    internal sealed class PlatformStorage : IPlatformStorage
    {
        private Vector3 _currentPlatformPosition;
        private float _highestPlatformY;
        private float _spawnChanceFactor = 0f;

        private readonly IGameData _gameData;
        private readonly IAudioService _audioService;
        private readonly IWorldFactory _worldFactory;
        private readonly IPlatformsConfig _platformsConfig;
        private readonly Transform _platformsContainer;
        private readonly Rect _screenRect;

        private readonly Vector3 _startPosition;
        private readonly int _platformStartCount;
        private readonly int _platformMaxCount;
        private readonly float _minY;
        private readonly float _maxY;

        private readonly List<IPlatformConfig> _platformConfigs = new(16);
        private readonly List<IPlatform> _platforms = new(256);
        private readonly Dictionary<int, IObjectPool<IPlatform>> _pools = new(16);

        public float HighestPlatformY => _highestPlatformY;

        public IReadOnlyList<IPlatform> Platforms => _platforms;

        public event System.Action<IPlatformCollisionInfo> Collided;

        public PlatformStorage(IGameData gameData, WorldArgs args, IPlatformsConfig platformsConfig, Transform platformsContainer, Rect screenRect)
        {
            _gameData = gameData;
            _audioService = args.AudioService;
            _worldFactory = args.WorldFactory;
            _platformsConfig = platformsConfig;
            _platformsContainer = platformsContainer;
            _screenRect = screenRect;

            var generatorConfig = args.GeneratorConfig;
            _startPosition = generatorConfig.StartPosition;
            _platformStartCount = generatorConfig.PlatformStartCount;
            _platformMaxCount = generatorConfig.PlatformMaxCount;

            _currentPlatformPosition = _startPosition;
            _highestPlatformY = _startPosition.y;
            _minY = generatorConfig.MinY;
            _maxY = generatorConfig.MaxY;

            InitPlatformConfigs();
            InitPools();
        }

        public void Clear()
        {
            Destroy();

            _currentPlatformPosition = _startPosition;
            _highestPlatformY = _startPosition.y;
        }

        public void GenerateStartPlatform()
        {
            for (int i = 0; i < _platformStartCount; i++)
            {
                var platformPrefab = GetPlatformPrefab();

                if (platformPrefab == null || IsIntersectedPlatforms(_currentPlatformPosition, platformPrefab.Size))
                    return;

                GeneratePlatform(platformPrefab);
                GenerateNextPosition();
                CheckHighestPosition(_currentPlatformPosition.y);
            }
        }

        public void TryGeneratePlatform()
        {
            GenerateNextPosition();

            var platformPrefab = GetPlatformPrefab();

            if (platformPrefab == null || IsIntersectedPlatforms(_currentPlatformPosition, platformPrefab.Size))
                return;

            GeneratePlatform(platformPrefab);
            CheckHighestPosition(_currentPlatformPosition.y);
        }

        public void GeneratePlatforms()
        {
            for (int i = 0; i < _platformMaxCount; i++)
                TryGeneratePlatform();
        }

        public void Destroy()
        {
            var count = _platforms.Count;

            for (int i = count - 1; 0 < i + 1; i--)
                DestroyPlatform(_platforms[i]);
        }

        public void DestroyPlatform(IPlatform platform)
        {
            platform.Collided -= OnCollided;
            platform.Destroyed -= OnDestroyed;
            platform.Clear();

            _pools[platform.Id].Release(platform);
            _platforms.Remove(platform);
        }

        private void InitPlatformConfigs()
        {
            var configs = _platformsConfig.Configs;
            var spawnChanceSum = 0f;

            foreach (var config in configs)
            {
                spawnChanceSum += config.SpawnChance;

                _platformConfigs.Add(config);
            }

            _spawnChanceFactor = 1f / spawnChanceSum;
            _platformConfigs.Sort(SortByChance);

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            static int SortByChance(IPlatformConfig x, IPlatformConfig y)
            {
                return x.SpawnChance.CompareTo(y.SpawnChance);
            }
        }

        private void InitPools()
        {
            var configs = _platformsConfig.Configs;

            foreach (var config in configs)
            {
                var prefab = config.PlatformPrefab;

                _pools.Add(prefab.Id, new ObjectPool<IPlatform>(() => CreatePlatform(prefab), _platformMaxCount));
            }
        }

        private IPlatform CreatePlatform(Platform platformPrefab)
        {
            var platform = _worldFactory.CreatePlatform(platformPrefab, _platformsContainer);
            platform.Init(_gameData);

            return platform;
        }

        private Platform GetPlatformPrefab()
        {
            var spawnChance = Random.value;

            foreach (var config in _platformConfigs)
            {
                if (config.SpawnChance * _spawnChanceFactor < spawnChance)
                    continue;

                return config.PlatformPrefab;
            }

            return _platformConfigs[_platformConfigs.Count - 1].PlatformPrefab;
        }

        private void GenerateNextPosition()
        {
            _currentPlatformPosition.y = _highestPlatformY + Random.Range(_minY, _maxY);
            _currentPlatformPosition.x = Random.Range(_screenRect.xMin, _screenRect.xMax);
        }

        private bool IsIntersectedPlatforms(Vector3 currentPlatformPosition, Vector2 size)
        {
            foreach (var platform in _platforms)
                if (platform.IsIntersectedArea(currentPlatformPosition, size))
                    return true;

            return false;
        }

        private void GeneratePlatform(Platform platformPrefab)
        {
            var platform = _pools[platformPrefab.Id].Get();
            platform.InitPosition(_currentPlatformPosition);
            platform.Collided += OnCollided;
            platform.Destroyed += OnDestroyed;

            _platforms.Add(platform);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void CheckHighestPosition(float y)
        {
            if (_highestPlatformY < y)
                _highestPlatformY = y;
        }

        private void OnCollided(IPlatformCollisionInfo info)
        {
            Collided.SafeInvoke(info);
        }

        private void OnDestroyed(IPlatform platform)
        {
            DestroyPlatform(platform);
        }
    }
}

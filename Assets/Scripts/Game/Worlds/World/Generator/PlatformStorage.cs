using System.Collections.Generic;
using DoodleJump.Core;
using DoodleJump.Game.Factories;
using DoodleJump.Game.Settings;
using UnityEngine;

namespace DoodleJump.Game.Worlds
{
    internal sealed class PlatformStorage : IPlatformStorage
    {
        private Vector3 _currentPlatformPosition;
        private float _highestPlatformY;

        private readonly IWorldFactory _worldFactory;
        private readonly IPlatformsConfig _platformsConfig;
        private readonly Transform _platformsContainer;
        private readonly Rect _screenRect;

        private readonly int _platformCount;
        private readonly Transform _doodlerTransform;
        private readonly float _minY;
        private readonly float _maxY;

        private readonly List<IPlatformConfig> _platformConfigs = new(16);
        private readonly List<IPlatform> _platforms = new(256);
        private readonly Dictionary<int, IObjectPool<IPlatform>> _pools = new(16);

        public float HighestPlatformY => _highestPlatformY;

        public IReadOnlyList<IPlatform> Platforms => _platforms;

        public event System.Action<IPlatform> Collided;

        public PlatformStorage(IWorldFactory worldFactory, IGeneratorConfig generatorConfig, IPlatformsConfig platformsConfig, Transform platformsContainer, Transform doodlerTransform, Rect screenRect)
        {
            _worldFactory = worldFactory;
            _platformsConfig = platformsConfig;
            _platformsContainer = platformsContainer;
            _doodlerTransform = doodlerTransform;
            _screenRect = screenRect;

            _platformCount = generatorConfig.PlatformCount;
            _highestPlatformY = _doodlerTransform.position.y - _screenRect.height / 2f;
            _minY = generatorConfig.MinY;
            _maxY = generatorConfig.MaxY;

            InitPlatformConfigs();
            InitPools();
        }

        public void TryGeneratePlatform()
        {
            GenerateNextPosition();

            var positionY = _currentPlatformPosition.y;
            var platformPrefab = GetPlatformPrefab();

            if (platformPrefab == null || IsIntersectedPlatforms(_currentPlatformPosition, platformPrefab.Size))
                return;

            GeneratePlatform(platformPrefab);
            CheckHighestPosition(positionY);
        }

        public void GeneratePlatforms()
        {
            for (int i = 0; i < _platformCount; i++)
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

            foreach (var config in configs)
                _platformConfigs.Add(config);

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
            var capacity = _platformCount;

            foreach (var config in configs)
            {
                var prefab = config.PlatformPrefab;

                _pools.Add(prefab.Id, new ObjectPool<IPlatform>(() => CreatePlatform(prefab), capacity));
            }
        }

        private IPlatform CreatePlatform(Platform platformPrefab)
        {
            var platform = _worldFactory.CreatePlatform(platformPrefab, _platformsContainer);

            return platform;
        }

        private Platform GetPlatformPrefab()
        {
            var spawnChance = Random.value;

            foreach (var config in _platformConfigs)
            {
                if (config.SpawnChance < spawnChance)
                    continue;

                return config.PlatformPrefab;
            }

            return null;
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
            platform.Init(_currentPlatformPosition);
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

        private void OnCollided(IPlatform platform)
        {
            Collided.SafeInvoke(platform);
        }

        private void OnDestroyed(IPlatform platform)
        {
            DestroyPlatform(platform);
        }
    }
}

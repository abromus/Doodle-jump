using System.Collections.Generic;
using DoodleJump.Core;
using DoodleJump.Game.Data;
using DoodleJump.Game.Factories;
using DoodleJump.Game.Settings;
using DoodleJump.Game.Worlds.Platforms;
using UnityEngine;

namespace DoodleJump.Game.Worlds
{
    internal sealed class PlatformStorage : IPlatformStorage
    {
        private Vector3 _currentPlatformPosition;
        private float _highestPlatformY;
        private float _spawnChanceFactor = 0f;

        private bool _isMaxProgress;
        private IProgressInfo _currentProgress;

        private readonly IGameData _gameData;
        private readonly IWorldFactory _worldFactory;
        private readonly Transform _platformsContainer;
        private readonly Rect _screenRect;

        private readonly Vector3 _startPosition;
        private readonly int _platformStartCount;
        private readonly IProgressInfo[] _progressInfos;

        private readonly List<IProbable> _probables = new(16);
        private readonly List<IPlatformConfig> _platformConfigs = new(16);
        private readonly List<IPlatform> _platforms = new(256);
        private readonly Dictionary<int, IObjectPool<IPlatform>> _pools = new(16);

        public float HighestPlatformY => _highestPlatformY;

        public IReadOnlyList<IPlatform> Platforms => _platforms;

        public event System.Action<IProgressInfo, IPlatformCollisionInfo> Collided;

        internal PlatformStorage(IGameData gameData, in WorldArgs args, Transform platformsContainer, in Rect screenRect)
        {
            _gameData = gameData;
            _worldFactory = args.WorldFactory;
            _platformsContainer = platformsContainer;
            _screenRect = screenRect;

            var generatorConfig = args.GeneratorConfig;
            _startPosition = generatorConfig.PlatformsStartPosition;
            _platformStartCount = generatorConfig.PlatformStartCount;

            _currentPlatformPosition = _startPosition;
            _highestPlatformY = _startPosition.y;
            _progressInfos = generatorConfig.ProgressInfos;

            CheckCurrentProgress();
            InitPlatformConfigs();
            InitPools();
        }

        public void Clear()
        {
            Destroy();

            _currentPlatformPosition = _startPosition;
            _highestPlatformY = _startPosition.y;
            _isMaxProgress = false;

            CheckCurrentProgress();
        }

        public void GenerateStartPlatform()
        {
            for (int i = 0; i < _platformStartCount; i++)
            {
                GetPlatformPrefab(out var platformConfig, out var platformPrefab);

                var size = platformPrefab.Size;

                if (platformPrefab == null || IsIntersectedPlatforms(in _currentPlatformPosition, in size))
                    return;

                CheckCurrentProgress();
                GeneratePlatform(platformConfig, platformPrefab);
                GenerateNextPosition();
                CheckHighestPosition(_currentPlatformPosition.y);
            }
        }

        public void GeneratePlatforms()
        {
            for (int i = 0; i < _currentProgress.PlatformMaxCount; i++)
                TryGeneratePlatform();
        }

        public void DestroyPlatform(IPlatform platform)
        {
            platform.Collided -= OnCollided;
            platform.Destroyed -= OnDestroyed;
            platform.Clear();

            _pools[platform.Id].Release(platform);
            _platforms.Remove(platform);
        }

        public void Destroy()
        {
            var count = _platforms.Count;

            for (int i = count - 1; 0 < i + 1; i--)
                DestroyPlatform(_platforms[i]);
        }

        private void InitPlatformConfigs()
        {
            _platformConfigs.Clear();
            _probables.Clear();

            var configs = _currentProgress.PlatformConfigs;
            var spawnChanceSum = 0f;

            foreach (var config in configs)
            {
                spawnChanceSum += config.SpawnChance;

                _platformConfigs.Add(config);
            }

            _spawnChanceFactor = 1f / spawnChanceSum;
            _platformConfigs.Sort(SortByChance);
            _probables.AddRange(_platformConfigs);

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            static int SortByChance(IPlatformConfig x, IPlatformConfig y)
            {
                return x.SpawnChance.CompareTo(y.SpawnChance);
            }
        }

        private void InitPools()
        {
            var configs = _currentProgress.PlatformConfigs;

            foreach (var config in configs)
            {
                var prefab = config.PlatformPrefab;

                if (_pools.ContainsKey(prefab.Id))
                    continue;

                _pools.Add(prefab.Id, new ObjectPool<IPlatform>(() => CreatePlatform(prefab), _currentProgress.PlatformMaxCount));
            }
        }

        private IPlatform CreatePlatform<T>(T platformPrefab) where T : MonoBehaviour, IPlatform
        {
            var platform = _worldFactory.CreatePlatform(platformPrefab, _platformsContainer);
            platform.Init(_gameData);

            return platform;
        }

        private void GetPlatformPrefab(out IPlatformConfig platformConfig, out BasePlatform platform)
        {
            var index = ProbableUtils.GetConfigIndex(_probables, _spawnChanceFactor);

            platformConfig = _platformConfigs[index];
            platform = platformConfig.PlatformPrefab;
        }

        private void CheckCurrentProgress()
        {
            if (_isMaxProgress)
                return;

            foreach (var progressInfo in _progressInfos)
            {
                var minProgress = progressInfo.MinProgress;
                var maxProgress = progressInfo.MaxProgress;

                if (_highestPlatformY < minProgress || minProgress <= _highestPlatformY && _highestPlatformY <= maxProgress)
                {
                    if (_currentProgress == progressInfo)
                        return;

                    _currentProgress = progressInfo;

                    InitPlatformConfigs();
                    InitPools();

                    return;
                }
            }

            _isMaxProgress = true;
            _currentProgress = _progressInfos[_progressInfos.Length - 1];
        }

        private void GenerateNextPosition()
        {
            _currentPlatformPosition.y = _highestPlatformY + Random.Range(_currentProgress.MinOffsetY, _currentProgress.MaxOffsetY);
            _currentPlatformPosition.x = Random.Range(_screenRect.xMin, _screenRect.xMax);
        }

        private bool IsIntersectedPlatforms(in Vector3 currentPlatformPosition, in Vector2 size)
        {
            foreach (var platform in _platforms)
                if (platform.IsIntersectedArea(currentPlatformPosition, size))
                    return true;

            return false;
        }

        private void GeneratePlatform(IPlatformConfig platformConfig, BasePlatform platformPrefab)
        {
            var platform = _pools[platformPrefab.Id].Get();
            platform.InitConfig(platformConfig);
            platform.InitPosition(_currentPlatformPosition);
            platform.Collided += OnCollided;
            platform.Destroyed += OnDestroyed;

            _platforms.Add(platform);
        }

        private void TryGeneratePlatform()
        {
            CheckCurrentProgress();
            GenerateNextPosition();

            GetPlatformPrefab(out var platformConfig, out var platformPrefab);

            var size = platformPrefab.Size;

            if (platformPrefab == null || IsIntersectedPlatforms(in _currentPlatformPosition, in size))
                return;

            GeneratePlatform(platformConfig, platformPrefab);
            CheckHighestPosition(_currentPlatformPosition.y);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void CheckHighestPosition(float y)
        {
            if (_highestPlatformY < y)
                _highestPlatformY = y;
        }

        private void OnCollided(IPlatformCollisionInfo info)
        {
            Collided.SafeInvoke(_currentProgress, info);
        }

        private void OnDestroyed(IPlatform platform)
        {
            DestroyPlatform(platform);
        }
    }
}

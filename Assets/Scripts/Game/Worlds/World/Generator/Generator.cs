using System.Collections.Generic;
using DoodleJump.Core;
using DoodleJump.Game.Factories;
using DoodleJump.Game.Settings;
using DoodleJump.Game.Worlds.Entities;
using UnityEngine;

namespace DoodleJump.Game.Worlds
{
    internal sealed class Generator : IGenerator
    {
        private Vector3 _currentPlatformPosition;
        private float _highestPlatformY;

        private readonly IWorldFactory _worldFactory;
        private readonly IDoodler _doodler;
        private readonly Camera _camera;
        private readonly IGeneratorConfig _generatorConfig;
        private readonly float _minY;
        private readonly float _maxY;
        private readonly Transform _doodlerTransform;
        private readonly Rect _screenRect;
        private readonly IObjectPool<IPlatform> _pool;

        private readonly List<IPlatform> _platforms = new(256);

        internal Generator(IWorldFactory worldFactory, IDoodler doodler, Camera camera, IGeneratorConfig generatorConfig)
        {
            _worldFactory = worldFactory;
            _doodler = doodler;
            _camera = camera;
            _generatorConfig = generatorConfig;

            _minY = _generatorConfig.MinY;
            _maxY = _generatorConfig.MaxY;

            _doodlerTransform = _doodler.GameObject.transform;

            _screenRect = GetScreenRect();
            _highestPlatformY = _doodlerTransform.position.y - _screenRect.height / 2f;
            _pool = new ObjectPool<IPlatform>(CreatePlatform, _generatorConfig.PlatformCount);

            GeneratePlatform();

            GeneratePlatforms();
        }

        public void Tick()
        {
            CheckDoodlerPosition();
        }

        public void Destroy()
        {
            for (int i = _platforms.Count - 1; i >= 0; i--)
                DestroyPlatform(_platforms[i]);
        }

        private IPlatform CreatePlatform()
        {
            var platform = _worldFactory.CreatePlatform(_generatorConfig.PlatformPrefab);

            return platform;
        }

        private void GeneratePlatform()
        {
            var positionY = _highestPlatformY + Random.Range(_minY, _maxY);

            _currentPlatformPosition.y = positionY;
            _currentPlatformPosition.x = Random.Range(_screenRect.xMin, _screenRect.xMax);

            var platform = _pool.Get();
            platform.Init(_currentPlatformPosition);

            if (_highestPlatformY < positionY)
                _highestPlatformY = positionY;

            _platforms.Add(platform);
        }

        private void GeneratePlatforms()
        {
            for (int i = 0; i < _generatorConfig.PlatformCount; i++)
                GeneratePlatform();
        }

        private void CheckDoodlerPosition()
        {
            if (_doodlerTransform.position.y + _screenRect.height / 2f < _highestPlatformY)
                return;

            for (int i = _platforms.Count - 1; i >= 0; i--)
            {
                var platform = _platforms[i];

                if (platform.Position.y < _doodlerTransform.position.y - _screenRect.height / 2f)
                    DestroyPlatform(platform);
            }

            GeneratePlatforms();
        }

        private void DestroyPlatform(IPlatform platform)
        {
            platform.Clear();

            _pool.Release(platform);
            _platforms.Remove(platform);
        }

        private Rect GetScreenRect()
        {
            var bottomLeft = _camera.ScreenToWorldPoint(Vector2.zero);
            var topRight = _camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
            var xMin = bottomLeft.x;
            var xMax = topRight.x;
            var yMin = bottomLeft.y;
            var yMax = topRight.y;

            return Rect.MinMaxRect(
                xMin < 0f ? xMin : -xMin,
                yMin < 0f ? yMin : -yMin,
                0f < xMax ? xMax : -xMax,
                0f < yMax ? yMax : -yMax);
        }
    }
}

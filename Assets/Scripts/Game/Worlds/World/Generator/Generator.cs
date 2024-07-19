using UnityEngine;

namespace DoodleJump.Game.Worlds
{
    internal sealed class Generator : IGenerator
    {
        private readonly Transform _doodlerTransform;
        private readonly Rect _screenRect;
        private readonly IPlatformStorage _platformStorage;
        private readonly ITriggerExecutor _triggerExecutor;

        internal Generator(WorldArgs args, Transform platformsContainer)
        {
            _screenRect = args.CameraService.GetScreenRect();

            _doodlerTransform = args.Doodler.GameObject.transform;

            var platformsConfig = args.PlatformsConfig;

            _platformStorage = new PlatformStorage(args.WorldFactory, args.GeneratorConfig, platformsConfig, platformsContainer, _doodlerTransform, _screenRect);
            _platformStorage.Collided += OnCollided;

            _triggerExecutor = new TriggerExecutor(args.TriggerFactory, platformsConfig);

            _platformStorage.TryGeneratePlatform();
            _platformStorage.GeneratePlatforms();
        }

        public void Tick()
        {
            CheckDoodlerPosition();
        }

        public void Destroy()
        {
            _platformStorage.Collided -= OnCollided;
            _platformStorage.Destroy();
        }

        private void CheckDoodlerPosition()
        {
            if (_doodlerTransform.position.y + _screenRect.height / 2f < _platformStorage.HighestPlatformY)
                return;

            var platforms = _platformStorage.Platforms;
            var count = platforms.Count;

            for (int i = count - 1; 0 < i + 1; i--)
            {
                var platform = platforms[i];

                if (platform.Position.y < _doodlerTransform.position.y - _screenRect.height / 2f)
                    _platformStorage.DestroyPlatform(platform);
            }

            _platformStorage.GeneratePlatforms();
        }

        private void OnCollided(IPlatform platform)
        {
            _triggerExecutor.Execute(platform);
        }
    }
}

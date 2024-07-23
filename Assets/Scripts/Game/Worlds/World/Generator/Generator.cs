using UnityEngine;

namespace DoodleJump.Game.Worlds
{
    internal sealed class Generator : IGenerator
    {
        private readonly Transform _doodlerTransform;
        private readonly Rect _screenRect;
        private readonly IPlatformStorage _platformStorage;
        private readonly ITriggerExecutor _triggerExecutor;

        internal Generator(WorldArgs args, Rect screenRect, Transform platformsContainer)
        {
            _screenRect = screenRect;

            _doodlerTransform = args.Doodler.GameObject.transform;

            var platformsConfig = args.PlatformsConfig;

            _platformStorage = new PlatformStorage(args.AudioService, args.WorldFactory, args.GeneratorConfig, platformsConfig, platformsContainer, _screenRect);
            _platformStorage.Collided += OnCollided;

            _triggerExecutor = new TriggerExecutor(args.TriggerFactory, platformsConfig);

            Restart();
        }

        public void Restart()
        {
            _platformStorage.Clear();
            _platformStorage.GenerateStartPlatform();
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
            var doodlerPosition = _doodlerTransform.position.y;
            var screenHeight = _screenRect.height;

            if (doodlerPosition + screenHeight < _platformStorage.HighestPlatformY)
                return;

            var platforms = _platformStorage.Platforms;
            var count = platforms.Count;

            for (int i = count - 1; 0 < i + 1; i--)
            {
                var platform = platforms[i];

                if (platform.Position.y < doodlerPosition - screenHeight)
                    _platformStorage.DestroyPlatform(platform);
            }

            _platformStorage.GeneratePlatforms();
        }

        private void OnCollided(IPlatformCollisionInfo info)
        {
            _triggerExecutor.Execute(info);
        }
    }
}

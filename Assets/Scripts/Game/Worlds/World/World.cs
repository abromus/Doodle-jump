using DoodleJump.Core.Services;
using DoodleJump.Game.Data;
using DoodleJump.Game.Worlds.Entities;
using UnityEngine;

namespace DoodleJump.Game.Worlds
{
    internal class World : MonoBehaviour, IWorld
    {
        [SerializeField] private Transform _platformsContainer;
        [SerializeField] private Transform _enemiesContainer;
        [SerializeField] private Transform _boostersContainer;
        [SerializeField] private Transform _projectilesContainer;
        [SerializeField] private SpriteRenderer[] _backgrounds;
        [Core.Separator(Core.CustomColor.Lime)]
        [SerializeField] private float _animationDelay;
        [SerializeField] private float _animationDuration;

        private IUpdater _updater;
        private ICameraService _cameraService;
        private IDoodler _doodler;
        private IWorldInitializer _worldInitializer;
        private IWorldData _worldData;
        private IDoodlerChecker _doodlerChecker;
        private IGenerator _generator;
        private IBackgroundChecker _backgroundChecker;
        private ICameraFollower _cameraFollower;

        private bool _canUpdate;

        public IWorldData WorldData => _worldData;

        public void Init(IGameData gameData, in WorldArgs args)
        {
            _updater = args.Updater;
            _cameraService = args.CameraService;
            _doodler = args.Doodler;

            var worldInitializerArgs = new WorldInitializerArgs(gameData, in args, transform, _platformsContainer, _enemiesContainer, _boostersContainer, _projectilesContainer, _backgrounds, _animationDelay, _animationDuration, _doodler);

            _worldInitializer = new WorldInitializer(in worldInitializerArgs);
            _worldData = _worldInitializer.WorldData;
            _doodlerChecker = _worldInitializer.DoodlerChecker;
            _generator = _worldInitializer.Generator;
            _backgroundChecker = _worldInitializer.BackgroundChecker;
            _cameraFollower = _worldInitializer.CameraFollower;

            Subscribe();
        }

        public void GameOver(GameOverType type)
        {
            _canUpdate = false;

            _cameraFollower.GameOver(type);
            _doodler.GameOver(type);
        }

        public void Restart()
        {
            _canUpdate = true;
            _cameraFollower.Restart();
            _doodler.Restart();
            _generator.Restart();
            _doodlerChecker.Restart();
            _backgroundChecker.Restart();
        }

        public void Tick(float deltaTime)
        {
            if (_canUpdate == false)
                return;

            _doodlerChecker.Tick();
            _generator.Tick();
            _backgroundChecker.Tick();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void LateTick(float deltaTime)
        {
            _cameraFollower.LateTick(deltaTime);
        }

        public void Destroy()
        {
            Unsubscribe();

            _cameraService?.Detach();
        }

        private void Subscribe()
        {
            _updater.AddUpdatable(this);
            _updater.AddLateUpdatable(this);
        }

        private void Unsubscribe()
        {
            _updater.RemoveUpdatable(this);
            _updater.RemoveLateUpdatable(this);
        }
    }
}

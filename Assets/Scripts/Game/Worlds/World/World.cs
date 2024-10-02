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

        private IUpdater _updater;
        private ICameraService _cameraService;
        private IDoodler _doodler;
        private IWorldInitializer _worldInitializer;
        private IWorldData _worldData;
        private IDoodlerChecker _doodlerChecker;
        private IGenerator _generator;
        private IBackgroundChecker _backgroundChecker;
        private ICameraFollower _cameraFollower;

        public void Init(IGameData gameData, WorldArgs args)
        {
            _updater = args.Updater;
            _cameraService = args.CameraService;
            _doodler = args.Doodler;

            var worldInitializerArgs = new WorldInitializerArgs(gameData, args, transform, _platformsContainer, _enemiesContainer, _boostersContainer, _projectilesContainer, _backgrounds, _doodler);

            _worldInitializer = new WorldInitializer(in worldInitializerArgs);
            _worldData = _worldInitializer.WorldData;
            _doodlerChecker = _worldInitializer.DoodlerChecker;
            _generator = _worldInitializer.Generator;
            _backgroundChecker = _worldInitializer.BackgroundChecker;
            _cameraFollower = _worldInitializer.CameraFollower;

            Subscribe();
        }

        public void Restart()
        {
            _cameraFollower.Restart();
            _doodler.Restart();
            _generator.Restart();
            _doodlerChecker.Restart();
            _backgroundChecker.Restart();
        }

        public void Tick(float deltaTime)
        {
            _doodlerChecker.Tick();
            _generator.Tick();
            _backgroundChecker.Tick();
        }

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

            _worldData.GameOver += OnGameOver;
        }

        private void Unsubscribe()
        {
            _updater.RemoveUpdatable(this);
            _updater.RemoveLateUpdatable(this);

            _worldData.GameOver -= OnGameOver;
        }

        private void OnGameOver()
        {
            Restart();
        }
    }
}

using DoodleJump.Core.Services;
using DoodleJump.Game.Worlds.Entities;
using UnityEngine;

namespace DoodleJump.Game.Worlds
{
    internal class World : MonoBehaviour, IWorld
    {
        [SerializeField] private Transform _platformsContainer;

        private WorldArgs _args;
        private IDoodler _doodler;
        private ICameraService _cameraService;
        private IDoodlerChecker _doodlerChecker;
        private IGenerator _generator;

        public void Init(WorldArgs args)
        {
            _args = args;
            _doodler = _args.Doodler;
            _cameraService = _args.CameraService;

            InitTriggerFactory();
            InitDoodlerChecker();
            InitGenerator();
            Subscribe();
        }

        public void Tick(float deltaTime)
        {
            _doodlerChecker.Tick();
            _generator.Tick();
        }

        public void Destroy()
        {
            Unsubscribe();
        }

        private void InitTriggerFactory()
        {
            _args.TriggerFactory.Init(_doodler);
        }

        private void InitDoodlerChecker()
        {
            var doodlerTransform = _doodler.GameObject.transform;
            var cameraTransform = _cameraService.Camera.transform;
            var screenRect = _cameraService.GetScreenRect();

            _doodlerChecker = new DoodlerChecker(doodlerTransform, cameraTransform, screenRect);
        }

        private void InitGenerator()
        {
            _generator = new Generator(_args, _platformsContainer);
        }

        private void RestartGame()
        {
            _doodler.Restart();
            _generator.Restart();
        }

        private void Subscribe()
        {
            _args.Updater.AddUpdatable(this);
            _doodlerChecker.GameOver += OnGameOver;
        }

        private void Unsubscribe()
        {
            _args.Updater.RemoveUpdatable(this);
            _doodlerChecker.GameOver -= OnGameOver;
        }

        private void OnGameOver()
        {
            RestartGame();
        }
    }
}

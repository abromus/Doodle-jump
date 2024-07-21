using DoodleJump.Core.Services;
using DoodleJump.Game.Data;
using DoodleJump.Game.Services;
using DoodleJump.Game.Settings;
using DoodleJump.Game.Worlds.Entities;
using UnityEngine;

namespace DoodleJump.Game.Worlds
{
    internal class World : MonoBehaviour, IWorld
    {
        [SerializeField] private Transform _platformsContainer;

        private WorldArgs _args;
        private IEventSystemService _eventSystemService;
        private IScreenSystemService _screenSystemService;
        private IDoodler _doodler;
        private ICameraService _cameraService;
        private ICameraConfig _cameraConfig;
        private IPersistentDataStorage _persistentDataStorage;
        private IDoodlerChecker _doodlerChecker;
        private IGenerator _generator;

        public void Init(WorldArgs args)
        {
            _args = args;
            _eventSystemService = args.EventSystemService;
            _screenSystemService = args.ScreenSystemService;
            _doodler = _args.Doodler;
            _cameraService = _args.CameraService;
            _cameraConfig = args.CameraConfig;
            _persistentDataStorage = _args.PersistentDataStorage;

            var doodlerTransform = _doodler.GameObject.transform;

            InitUi();
            InitTriggerFactory();
            InitDoodler(doodlerTransform);
            InitDoodlerChecker(doodlerTransform);
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

            _cameraService?.Detach();
        }

        private void InitTriggerFactory()
        {
            _args.TriggerFactory.Init(_doodler);
        }

        private void InitDoodler(Transform doodlerTransform)
        {
            doodlerTransform.SetParent(transform);
            doodlerTransform.position = Vector3.zero;
        }

        private void InitDoodlerChecker(Transform doodlerTransform)
        {
            var cameraTransform = _cameraService.Camera.transform;
            var screenRect = _cameraService.GetScreenRect();

            _doodlerChecker = new DoodlerChecker(_persistentDataStorage, doodlerTransform, cameraTransform, screenRect);
        }

        private void InitGenerator()
        {
            _generator = new Generator(_args, _platformsContainer);
        }

        private void InitUi()
        {
            InitCamera();

            _eventSystemService.AddTo(gameObject.scene);
            _screenSystemService.AttachTo(transform);
        }

        private void InitCamera()
        {
            _cameraService.AttachTo(transform);

            ResetCamera();
        }

        private void RestartGame()
        {
            ResetCamera();

            _doodler.Restart();
            _generator.Restart();
            _doodlerChecker.Restart();
        }

        private void ResetCamera()
        {
            var cameraTransform = _cameraService.Camera.transform;
            cameraTransform.localScale = Vector3.one;
            cameraTransform.position = _cameraConfig.Offset;
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

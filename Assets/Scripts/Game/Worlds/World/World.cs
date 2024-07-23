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
        [SerializeField] private SpriteRenderer[] _backgrounds;

        private WorldArgs _args;
        private IEventSystemService _eventSystemService;
        private IScreenSystemService _screenSystemService;
        private IAudioService _audioService;
        private IDoodler _doodler;
        private ICameraService _cameraService;
        private ICameraConfig _cameraConfig;
        private IPersistentDataStorage _persistentDataStorage;
        private IDoodlerChecker _doodlerChecker;
        private IGenerator _generator;
        private IBackgroundChecker _backgroundChecker;
        private Rect _screenRect;

        public void Init(WorldArgs args)
        {
            _args = args;
            _eventSystemService = args.EventSystemService;
            _screenSystemService = args.ScreenSystemService;
            _audioService = args.AudioService;
            _doodler = _args.Doodler;
            _cameraService = _args.CameraService;
            _cameraConfig = args.CameraConfig;
            _persistentDataStorage = _args.PersistentDataStorage;

            var doodlerTransform = _doodler.GameObject.transform;
            var cameraTransform = _cameraService.Camera.transform;

            InitDoodler(doodlerTransform);
            InitUi();
            InitTriggerFactory();
            InitDoodlerChecker(doodlerTransform, cameraTransform);
            InitGenerator();
            InitBackgroundChecker(cameraTransform);
            Subscribe();
        }

        public void Tick(float deltaTime)
        {
            _doodlerChecker.Tick();
            _generator.Tick();
            _backgroundChecker.Tick();
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
            doodlerTransform.localPosition = Vector3.zero;
        }

        private void InitDoodlerChecker(Transform doodlerTransform, Transform cameraTransform)
        {
            _doodlerChecker = new DoodlerChecker(_persistentDataStorage, doodlerTransform, _doodler.Size.x, cameraTransform, _screenRect);
        }

        private void InitGenerator()
        {
            _generator = new Generator(_args, _screenRect, _platformsContainer);
        }

        private void InitUi()
        {
            InitCamera();

            _eventSystemService.AddTo(gameObject.scene);
            _screenSystemService.AttachTo(transform);

            _screenRect = _cameraService.GetScreenRect();
        }

        private void InitCamera()
        {
            _cameraService.AttachTo(transform);

            ResetCamera();
        }

        private void InitBackgroundChecker(Transform cameraTransform)
        {
            _backgroundChecker = new BackgroundChecker(cameraTransform, _screenRect, _backgrounds);
        }

        private void RestartGame()
        {
            ResetCamera();

            _doodler.Restart();
            _generator.Restart();
            _doodlerChecker.Restart();
            _backgroundChecker.Restart();
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

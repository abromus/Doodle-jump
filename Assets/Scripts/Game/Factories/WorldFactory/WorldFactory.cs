using DoodleJump.Core;
using DoodleJump.Core.Factories;
using DoodleJump.Core.Services;
using DoodleJump.Game.Data;
using DoodleJump.Game.Services;
using DoodleJump.Game.Settings;
using DoodleJump.Game.Worlds;
using DoodleJump.Game.Worlds.Entities;
using UnityEngine;

namespace DoodleJump.Game.Factories
{
    internal sealed class WorldFactory : UiFactory, IWorldFactory
    {
        [SerializeField] private World _world;

        private IGameData _gameData;
        private IUpdater _updater;
        private ICameraService _cameraService;
        private IEventSystemService _eventSystemService;
        private IScreenSystemService _screenSystemService;
        private IAudioService _audioService;
        private ITriggerFactory _triggerFactory;
        private ICameraConfig _cameraConfig;
        private IGeneratorConfig _generatorConfig;
        private IPlatformsConfig _platformsConfig;
        private IPersistentDataStorage _persistentDataStorage;

        public override UiFactoryType UiFactoryType => UiFactoryType.WorldFactory;

        public void Init(WorldFactoryArgs args)
        {
            _gameData = args.GameData;
            _updater = args.Updater;
            _cameraService = args.CameraService;
            _eventSystemService = args.EventSystemService;
            _screenSystemService = args.ScreenSystemService;
            _audioService = args.AudioService;
            _triggerFactory = args.TriggerFactory;
            _cameraConfig = args.CameraConfig;
            _generatorConfig = args.GeneratorConfig;
            _platformsConfig = args.PlatformsConfig;
            _persistentDataStorage = args.PersistentDataStorage;
        }

        public IWorld CreateWorld(IDoodler doodler)
        {
            var args = new WorldArgs(
                _updater,
                _cameraService,
                _eventSystemService,
                _screenSystemService,
                _audioService,
                this,
                _triggerFactory,
                doodler,
                _cameraConfig,
                _generatorConfig,
                _platformsConfig,
                _persistentDataStorage);
            var world = Instantiate(_world);
            world.Init(_gameData, args);
            world.gameObject.RemoveCloneSuffix();

            return world;
        }

        public IPlatform CreatePlatform(Platform prefab, Transform container)
        {
            var platform = Instantiate(prefab, container);
            platform.gameObject.RemoveCloneSuffix();

            return platform;
        }
    }
}

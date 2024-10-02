using DoodleJump.Core.Services;
using DoodleJump.Game.Data;
using DoodleJump.Game.Factories;
using DoodleJump.Game.Services;
using DoodleJump.Game.Worlds.Entities;
using UnityEngine;

namespace DoodleJump.Game.Worlds
{
    internal sealed class WorldInitializer : IWorldInitializer
    {
        private readonly IWorldData _worldData;
        private readonly IDoodlerChecker _doodlerChecker;
        private readonly IGenerator _generator;
        private readonly IBackgroundChecker _backgroundChecker;
        private readonly ICameraFollower _cameraFollower;

        public IWorldData WorldData => _worldData;

        public IDoodlerChecker DoodlerChecker => _doodlerChecker;

        public IGenerator Generator => _generator;

        public IBackgroundChecker BackgroundChecker => _backgroundChecker;

        public ICameraFollower CameraFollower => _cameraFollower;

        internal WorldInitializer(in WorldInitializerArgs args)
        {
            var gameData = args.GameData;
            var worldArgs = args.WorldArgs;
            var worldTransform = args.WorldTransform;

            var cameraService = worldArgs.CameraService;
            var doodler = worldArgs.Doodler;
            var persistentDataStorage = worldArgs.PersistentDataStorage;

            var doodlerTransform = args.Doodler.GameObject.transform;
            var cameraTransform = cameraService.Camera.transform;

            _worldData = GetWorldData();
            _cameraFollower = GetCameraFollower(cameraService, worldTransform, doodlerTransform, cameraTransform, worldArgs.CameraConfig.Offset);

            var screenRect = cameraService.GetScreenRect();

            InitDoodler(doodler, worldTransform, doodlerTransform, args.ProjectilesContainer);
            InitUi(worldTransform, worldArgs.EventSystemService, worldArgs.ScreenSystemService, worldArgs.AudioService, gameData, _worldData);
            InitTriggerFactories(_worldData, persistentDataStorage, worldArgs.PlatformTriggerFactory, worldArgs.EnemyTriggerFactory, worldArgs.BoosterTriggerFactory, doodler);

            _doodlerChecker = GetDoodlerChecker(_worldData, persistentDataStorage, doodlerTransform, doodler.Size.x, cameraTransform, screenRect);
            _generator = GetGenerator(gameData, worldArgs, screenRect, args.PlatformsContainer, args.EnemiesContainer, args.BoostersContainer);
            _backgroundChecker = GetBackgroundChecker(cameraTransform, screenRect, args.Backgrounds);
        }

        private void InitDoodler(
            IDoodler doodler,
            Transform worldTransform,
            Transform doodlerTransform,
            Transform projectilesContainer)
        {
            doodlerTransform.SetParent(worldTransform);
            doodlerTransform.localPosition = Vector3.zero;

            doodler.SetProjectileContainer(projectilesContainer);
        }

        private void InitUi(
            Transform worldTransform,
            IEventSystemService eventSystemService,
            IScreenSystemService screenSystemService,
            IAudioService audioService,
            IGameData gameData,
            IWorldData worldData)
        {
            eventSystemService.AddTo(worldTransform.gameObject.scene);

            screenSystemService.AttachTo(worldTransform);
            screenSystemService.Init(gameData, worldData);

            audioService.PlayBackground(BackgroundType.World);
        }

        private void InitTriggerFactories(
            IWorldData worldData,
            IPersistentDataStorage persistentDataStorage,
            IPlatformTriggerFactory platformTriggerFactory,
            IEnemyTriggerFactory enemyTriggerFactory,
            IBoosterTriggerFactory boosterTriggerFactory,
            IDoodler doodler)
        {
            platformTriggerFactory.Init(doodler);
            enemyTriggerFactory.Init(doodler, worldData);
            boosterTriggerFactory.Init(persistentDataStorage, doodler);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private IWorldData GetWorldData()
        {
            return new WorldData();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private ICameraFollower GetCameraFollower(ICameraService cameraService, Transform worldTransform, Transform doodlerTransform, Transform cameraTransform, Vector3 cameraOffset)
        {
            return new CameraFollower(cameraService, worldTransform, doodlerTransform, cameraTransform, cameraOffset);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private IDoodlerChecker GetDoodlerChecker(IWorldData worldData, IPersistentDataStorage persistentDataStorage, Transform doodlerTransform, float doodlerWidth, Transform cameraTransform, Rect screenRect)
        {
            return new DoodlerChecker(worldData, persistentDataStorage, doodlerTransform, doodlerWidth, cameraTransform, screenRect);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private IGenerator GetGenerator(IGameData gameData, WorldArgs args, Rect screenRect, Transform platformsContainer, Transform enemiesContainer, Transform boostersContainer)
        {
            return new Generator(gameData, args, screenRect, platformsContainer, enemiesContainer, boostersContainer);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private IBackgroundChecker GetBackgroundChecker(Transform cameraTransform, Rect screenRect, SpriteRenderer[] backgrounds)
        {
            return new BackgroundChecker(cameraTransform, screenRect, backgrounds);
        }
    }
}

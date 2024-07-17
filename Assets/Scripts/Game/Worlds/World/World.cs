using DoodleJump.Core.Services;
using DoodleJump.Game.Factories;
using DoodleJump.Game.Settings;
using DoodleJump.Game.Worlds.Entities;
using UnityEngine;

namespace DoodleJump.Game.Worlds
{
    internal class World : MonoBehaviour, IWorld
    {
        private IUpdater _updater;
        private ICameraService _cameraService;
        private IWorldFactory _worldFactory;
        private IDoodler _doodler;
        private IGeneratorConfig _generatorConfig;
        private IGenerator _generator;

        public void Init(IUpdater updater, ICameraService cameraService, IWorldFactory worldFactory, IDoodler doodler, IGeneratorConfig generatorConfig)
        {
            _updater = updater;
            _cameraService = cameraService;
            _worldFactory = worldFactory;
            _doodler = doodler;
            _generatorConfig = generatorConfig;

            InitGenerator();
            Subscribe();
        }

        public void Tick(float deltaTime)
        {
            _generator.Tick();
        }

        public void Destroy()
        {
            Unsubscribe();
        }

        private void InitGenerator()
        {
            _generator = new Generator(_worldFactory, _doodler, _cameraService.Camera, _generatorConfig);
        }

        private void Subscribe()
        {
            _updater.AddUpdatable(this);
        }

        private void Unsubscribe()
        {
            _updater.RemoveUpdatable(this);
        }
    }
}

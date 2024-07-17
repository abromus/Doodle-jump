using DoodleJump.Core;
using DoodleJump.Core.Services;
using DoodleJump.Game.Factories;
using DoodleJump.Game.Settings;
using DoodleJump.Game.Worlds.Entities;

namespace DoodleJump.Game.Worlds
{
    internal interface IWorld : IDestroyable, IUpdatable
    {
        public void Init(IUpdater updater, ICameraService cameraService, IWorldFactory worldFactory, IDoodler doodler, IGeneratorConfig generatorConfig);
    }
}

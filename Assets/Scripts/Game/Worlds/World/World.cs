using UnityEngine;

namespace DoodleJump.Game.Worlds
{
    internal class World : MonoBehaviour, IWorld
    {
        private WorldArgs _args;
        private IGenerator _generator;

        public void Init(WorldArgs args)
        {
            _args = args;

            InitTriggerFactory();
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

        private void InitTriggerFactory()
        {
            _args.TriggerFactory.Init(_args.Doodler);
        }

        private void InitGenerator()
        {
            _generator = new Generator(_args);
        }

        private void Subscribe()
        {
            _args.Updater.AddUpdatable(this);
        }

        private void Unsubscribe()
        {
            _args.Updater.RemoveUpdatable(this);
        }
    }
}

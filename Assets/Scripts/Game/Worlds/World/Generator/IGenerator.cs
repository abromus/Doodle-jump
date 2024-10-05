using DoodleJump.Core;

namespace DoodleJump.Game
{
    internal interface IGenerator : IDestroyable
    {
        public void Prepare();

        public void Restart();

        public void Tick();
    }
}

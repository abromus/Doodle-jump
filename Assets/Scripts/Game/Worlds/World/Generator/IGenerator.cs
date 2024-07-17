using DoodleJump.Core;

namespace DoodleJump.Game
{
    internal interface IGenerator : IDestroyable
    {
        public void Tick();
    }
}
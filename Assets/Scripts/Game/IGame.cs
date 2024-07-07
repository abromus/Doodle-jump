using DoodleJump.Core;

namespace DoodleJump.Game
{
    internal interface IGame : IDestroyable
    {
        public IGameData GameData { get; }

        public void Run();
    }
}

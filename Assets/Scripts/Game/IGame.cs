using DoodleJump.Core;
using DoodleJump.Game.Data;

namespace DoodleJump.Game
{
    internal interface IGame : IDestroyable
    {
        public IGameData GameData { get; }

        public void Run();
    }
}

using DoodleJump.Core;
using DoodleJump.Core.Services;

namespace DoodleJump.Game.Worlds
{
    internal interface IWorld : IDestroyable, IUpdatable, ILateUpdatable
    {
        public IWorldData WorldData { get; }

        public void Init(Data.IGameData gameData, in WorldArgs args);

        public void GameOver(GameOverType type);

        public void Restart();
    }
}

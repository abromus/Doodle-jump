using DoodleJump.Core;
using DoodleJump.Core.Services;

namespace DoodleJump.Game.Worlds
{
    internal interface IWorld : IDestroyable, IUpdatable, ILateUpdatable
    {
        public void Init(Data.IGameData gameData, WorldArgs args);

        public void Restart();
    }
}

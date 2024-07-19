using DoodleJump.Core;
using DoodleJump.Core.Services;

namespace DoodleJump.Game.Worlds
{
    internal interface IWorld : IDestroyable, IUpdatable
    {
        public void Init(WorldArgs args);
    }
}

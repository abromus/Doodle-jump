using DoodleJump.Core;
using DoodleJump.Core.Services;

namespace DoodleJump.Game.Worlds.Entities
{
    internal interface IEntity : IDestroyable, IUpdatable, IPausable { }
}

using DoodleJump.Core;
using DoodleJump.Core.Services;
using UnityEngine;

namespace DoodleJump.Game.Worlds.Entities
{
    internal interface IEntity : IDestroyable, IUpdatable, IFixedUpdatable, ILateUpdatable, IPausable
    {
        public GameObject GameObject { get; }
    }
}

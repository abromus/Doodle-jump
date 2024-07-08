using DoodleJump.Core;
using DoodleJump.Core.Services;
using UnityEngine;

namespace DoodleJump.Game.Entities
{
    internal interface IEntity : IDestroyable, IUpdatable, IFixedUpdatable
    {
        public GameObject GameObject { get; }
    }
}

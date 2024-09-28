using UnityEngine;

namespace DoodleJump.Game.Worlds.Entities
{
    internal sealed class UpwardsShootingStrategy : IShootingStrategy
    {
        public Vector3 GetDirection(Vector3 doodlerPosition, Vector3 shootPosition)
        {
            return Vector3.up;
        }
    }
}

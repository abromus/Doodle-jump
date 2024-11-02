namespace DoodleJump.Game.Worlds.Entities
{
    internal sealed class UpwardsShootingStrategy : IShootingStrategy
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public UnityEngine.Vector3 GetDirection(UnityEngine.Vector3 doodlerPosition, UnityEngine.Vector3 shootPosition)
        {
            return UnityEngine.Vector3.up;
        }
    }
}

namespace DoodleJump.Game.Worlds.Entities
{
    internal sealed class UpwardsShootingStrategy : IShootingStrategy
    {
        private NoseInfo _noseInfo = new();

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public UnityEngine.Vector3 GetDirection(UnityEngine.Vector3 doodlerPosition, UnityEngine.Vector3 shootPosition)
        {
            return UnityEngine.Vector3.up;
        }

        public NoseInfo GetNoseInfo(UnityEngine.Vector3 shootDirection, IDoodlerNose nose)
        {
            var position = nose.RotationPointPosition + nose.RotationOffset;
            var rotation = UnityEngine.Quaternion.identity;

            _noseInfo.Position = position;
            _noseInfo.Rotation = rotation;

            position.y += nose.Height;
            _noseInfo.ShootPosition = position;

            return _noseInfo;
        }
    }
}

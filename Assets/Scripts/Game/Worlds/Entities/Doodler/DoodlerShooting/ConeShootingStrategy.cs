namespace DoodleJump.Game.Worlds.Entities
{
    internal sealed class ConeShootingStrategy : IShootingStrategy
    {
        private NoseInfo _noseInfo = new();

        private readonly Core.Services.ICameraService _cameraService;
        private readonly float _maxAngle;

        internal ConeShootingStrategy(Core.Services.ICameraService cameraService, float maxAngle)
        {
            _cameraService = cameraService;
            _maxAngle = maxAngle;
        }

        public UnityEngine.Vector3 GetDirection(UnityEngine.Vector3 doodlerPosition, UnityEngine.Vector3 shootPosition)
        {
            var shootWorldPosition = _cameraService.Camera.ScreenToWorldPoint(shootPosition);
            shootWorldPosition.y = UnityEngine.Mathf.Abs(shootWorldPosition.y);
            shootWorldPosition.z = 0f;

            var shotDirection = shootWorldPosition - doodlerPosition;
            var angle = UnityEngine.Vector3.Angle(UnityEngine.Vector3.up, shotDirection);

            if (_maxAngle < angle)
                shotDirection.y = UnityEngine.Mathf.Cos(_maxAngle * UnityEngine.Mathf.Deg2Rad) * shotDirection.magnitude;

            return shotDirection.normalized;
        }

        public NoseInfo GetNoseInfo(UnityEngine.Vector3 shootDirection, IDoodlerNose nose)
        {
            var angle = UnityEngine.Vector3.SignedAngle(UnityEngine.Vector3.up, shootDirection, UnityEngine.Vector3.forward);
            angle = UnityEngine.Mathf.Clamp(angle, -_maxAngle, _maxAngle);

            var rotationPointPosition = nose.RotationPointPosition;
            var position = RotateAroundPivot(rotationPointPosition + nose.RotationOffset, rotationPointPosition, angle);
            var rotation = UnityEngine.Quaternion.Euler(0f, 0f, angle);

            var noseSize = new UnityEngine.Vector3(0f, nose.Height, 0f);
            var shootPosition = position + RotateAroundPivot(noseSize, UnityEngine.Vector3.zero, angle);

            _noseInfo.Position = position;
            _noseInfo.Rotation = rotation;
            _noseInfo.ShootPosition = shootPosition;

            return _noseInfo;
        }

        private UnityEngine.Vector3 RotateAroundPivot(UnityEngine.Vector3 point, UnityEngine.Vector3 pivot, float angle)
        {
            var angleInRadians = angle * UnityEngine.Mathf.Deg2Rad;
            var direction = point - pivot;

            var cosAngle = UnityEngine.Mathf.Cos(angleInRadians);
            var sinAngle = UnityEngine.Mathf.Sin(angleInRadians);

            var rotatedDirection = new UnityEngine.Vector3(
                cosAngle * direction.x - sinAngle * direction.y,
                sinAngle * direction.x + cosAngle * direction.y,
                point.z
            );

            return pivot + rotatedDirection;
        }
    }
}

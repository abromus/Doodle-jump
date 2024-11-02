namespace DoodleJump.Game.Worlds.Entities
{
    internal sealed class ConeShootingStrategy : IShootingStrategy
    {
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
    }
}

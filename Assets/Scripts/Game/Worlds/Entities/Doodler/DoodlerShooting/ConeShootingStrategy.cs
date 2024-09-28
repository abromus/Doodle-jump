using DoodleJump.Core.Services;
using UnityEngine;

namespace DoodleJump.Game.Worlds.Entities
{
    internal sealed class ConeShootingStrategy : IShootingStrategy
    {
        private readonly ICameraService _cameraService;
        private readonly float _maxAngle;

        internal ConeShootingStrategy(ICameraService cameraService, float maxAngle)
        {
            _cameraService = cameraService;
            _maxAngle = maxAngle;
        }

        public Vector3 GetDirection(Vector3 doodlerPosition, Vector3 shootPosition)
        {
            var shootWorldPosition = _cameraService.Camera.ScreenToWorldPoint(shootPosition);
            shootWorldPosition.y = Mathf.Abs(shootWorldPosition.y);
            shootWorldPosition.z = 0f;

            var shotDirection = shootWorldPosition - doodlerPosition;
            var angle = Vector3.Angle(Vector3.up, shotDirection);

            if (_maxAngle < angle)
                shotDirection.y = Mathf.Cos(_maxAngle * Mathf.Deg2Rad) * shotDirection.magnitude;

            return shotDirection.normalized;
        }
    }
}

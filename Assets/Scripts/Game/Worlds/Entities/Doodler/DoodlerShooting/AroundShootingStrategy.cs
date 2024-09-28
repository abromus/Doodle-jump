using DoodleJump.Core.Services;
using UnityEngine;

namespace DoodleJump.Game.Worlds.Entities
{
    internal sealed class AroundShootingStrategy : IShootingStrategy
    {
        private readonly ICameraService _cameraService;

        internal AroundShootingStrategy(ICameraService cameraService)
        {
            _cameraService = cameraService;
        }

        public Vector3 GetDirection(Vector3 doodlerPosition, Vector3 shootPosition)
        {
            var shootWorldPosition = _cameraService.Camera.ScreenToWorldPoint(shootPosition);
            shootWorldPosition.z = 0f;

            return (shootWorldPosition - doodlerPosition).normalized;
        }
    }
}

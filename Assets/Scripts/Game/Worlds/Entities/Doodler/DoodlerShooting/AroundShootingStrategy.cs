namespace DoodleJump.Game.Worlds.Entities
{
    internal sealed class AroundShootingStrategy : IShootingStrategy
    {
        private readonly Core.Services.ICameraService _cameraService;

        internal AroundShootingStrategy(Core.Services.ICameraService cameraService)
        {
            _cameraService = cameraService;
        }

        public UnityEngine.Vector3 GetDirection(UnityEngine.Vector3 doodlerPosition, UnityEngine.Vector3 shootPosition)
        {
            var shootWorldPosition = _cameraService.Camera.ScreenToWorldPoint(shootPosition);
            shootWorldPosition.z = 0f;

            return (shootWorldPosition - doodlerPosition).normalized;
        }
    }
}

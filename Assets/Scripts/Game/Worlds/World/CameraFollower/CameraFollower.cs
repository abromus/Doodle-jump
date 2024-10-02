namespace DoodleJump.Game.Worlds.Entities
{
    internal sealed class CameraFollower : ICameraFollower
    {
        private readonly UnityEngine.Transform _doodlerTransform;
        private readonly UnityEngine.Transform _cameraTransform;
        private readonly float _halfScreenHeight;
        private readonly UnityEngine.Vector3 _cameraOffset;

        internal CameraFollower(Core.Services.ICameraService cameraService, UnityEngine.Transform worldTransform, UnityEngine.Transform doodlerTransform, UnityEngine.Transform cameraTransform, UnityEngine.Vector3 cameraOffset)
        {
            _doodlerTransform = doodlerTransform;
            _cameraTransform = cameraTransform;
            _cameraOffset = cameraOffset;

            cameraService.AttachTo(worldTransform);

            _halfScreenHeight = cameraService.GetScreenRect().height * Constants.Half;

            ResetCamera();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Restart()
        {
            ResetCamera();
        }

        public void LateTick(float deltaTime)
        {
            var cameraPosition = _cameraTransform.position;
            var cameraPositionY = cameraPosition.y;
            var doodlerPositionY = _doodlerTransform.position.y;

            if (doodlerPositionY <= cameraPositionY && cameraPositionY - _halfScreenHeight <= doodlerPositionY)
                return;

            cameraPosition.y = doodlerPositionY;

            _cameraTransform.position = cameraPosition;
        }

        private void ResetCamera()
        {
            _cameraTransform.localScale = UnityEngine.Vector3.one;
            _cameraTransform.position = _cameraOffset;
        }
    }
}

using UnityEngine;

namespace DoodleJump.Game.Entities
{
    internal sealed class DoodlerCameraFollower : IDoodlerCameraFollower
    {
        private readonly Transform _doodler;
        private readonly Transform _camera;

        public DoodlerCameraFollower(Transform doodler, Transform camera)
        {
            _doodler = doodler;
            _camera = camera;
        }

        public void LateTick(float deltaTime)
        {
            if (_doodler.position.y <= _camera.position.y)
                return;

            var cameraPosition = _camera.position;
            cameraPosition.y = _doodler.position.y;

            _camera.position = cameraPosition;
        }
    }
}

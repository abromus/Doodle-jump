using UnityEngine;

namespace DoodleJump.Core.Services
{
    internal sealed class CameraService : BaseUiService, ICameraService
    {
        [SerializeField] private Camera _cameraPrefab;
        [SerializeField] private Camera _additionalCameraPrefab;

        private Transform _container;
        private Camera _camera;
        private Camera _cameraLeft;
        private Camera _cameraRight;

        public Camera Camera => _camera;

        public Camera CameraLeft => _cameraLeft;

        public Camera CameraRight => _cameraRight;

        public event System.Action<Transform> Attached;

        public void Init(Transform container)
        {
            _container = container;

            _camera = InstantiateCamera(_cameraPrefab, container);
            _cameraLeft = InstantiateCamera(_additionalCameraPrefab, _camera.transform);
            _cameraRight = InstantiateCamera(_additionalCameraPrefab, _camera.transform);
        }

        public void AttachTo(Transform parent)
        {
            _camera.transform.SetParent(parent);

            Attached.SafeInvoke(parent);
        }

        public void Detach()
        {
            if (_camera == null)
                return;

            _camera.transform.SetParent(_container);

            Attached.SafeInvoke(_container);
        }

        public Rect GetScreenRect()
        {
            var bottomLeft = _camera.ScreenToWorldPoint(Vector2.zero);
            var topRight = _camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

            return Rect.MinMaxRect(bottomLeft.x, bottomLeft.y, topRight.x, topRight.y);
        }

        public void Destroy()
        {
            if (_camera != null)
                Destroy(_camera.gameObject);
        }

        private Camera InstantiateCamera(Camera cameraPrefab, Transform container)
        {
            var camera = Instantiate(cameraPrefab, container);
            camera.transform.SetAsFirstSibling();
            camera.gameObject.RemoveCloneSuffix();

            return camera;
        }

        private sealed class Keys
        {
            internal const string EntityLayer = "Entity";
        }
    }
}

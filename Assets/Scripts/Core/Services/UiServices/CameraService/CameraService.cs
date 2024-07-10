using System;
using UnityEngine;

namespace DoodleJump.Core.Services
{
    internal sealed class CameraService : UiService, ICameraService
    {
        [SerializeField] private Camera _cameraPrefab;

        private Transform _container;
        private Camera _camera;

        public override UiServiceType UiServiceType => UiServiceType.CameraService;

        public Camera Camera => _camera;

        public event Action<Transform> Attached;

        public void Init(Transform container)
        {
            _container = container;

            _camera = InstantiateCamera(_cameraPrefab, container);
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
    }
}

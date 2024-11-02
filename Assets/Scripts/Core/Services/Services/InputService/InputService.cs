namespace DoodleJump.Core.Services
{
    internal sealed class InputService : IInputService
    {
        private float _xSensitivity = 1f;

        private readonly IEventSystemService _eventSystemService;

        internal InputService(IEventSystemService eventSystemService)
        {
            _eventSystemService = eventSystemService;
        }

        public float XSensitivity => _xSensitivity;

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public float GetHorizontalAxis()
        {
            return UnityEngine.Input.GetAxis(AxisKeys.Horizontal);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public float GetHorizontalAxisRaw()
        {
            return UnityEngine.Input.GetAxisRaw(AxisKeys.Horizontal);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public int GetTouchCount()
        {
            return UnityEngine.Input.touchCount;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public UnityEngine.Touch GetTouch(int index)
        {
            return UnityEngine.Input.GetTouch(index);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public bool GetMouseButtonDown(int button)
        {
            return UnityEngine.Input.GetMouseButtonDown(button);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public UnityEngine.Vector3 GetMousePosition()
        {
            return UnityEngine.Input.mousePosition;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public bool IsPointerOverGameObject(in UnityEngine.SceneManagement.Scene scene)
        {
            return _eventSystemService.IsPointerOverGameObject(scene);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public bool IsPointerOverGameObject(in UnityEngine.SceneManagement.Scene scene, int fingerId)
        {
            return _eventSystemService.IsPointerOverGameObject(scene, fingerId);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void SetXSensitivity(float xSensitivity)
        {
            _xSensitivity = xSensitivity;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Destroy() { }

        private sealed class AxisKeys
        {
            internal const string Horizontal = "Horizontal";
            internal const string Vertical = "Vertical";
        }
    }
}

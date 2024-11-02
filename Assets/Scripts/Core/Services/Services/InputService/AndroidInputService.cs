#if UNITY_EDITOR == false && UNITY_ANDROID
using UnityEngine;

namespace DoodleJump.Core.Services
{
    internal sealed class AndroidInputService : IInputService
    {
        private bool _isAccelerometerEnabled;

        private readonly float _minAcceleration = 0.2f;
        private readonly float _zero = 0f;
        private readonly float _left = -1f;
        private readonly float _right = 1f;

        private float _xSensitivity = 0.5f;

        private readonly IEventSystemService _eventSystemService;

        public float XSensitivity => _xSensitivity;

        internal AndroidInputService(IEventSystemService eventSystemService)
        {
            _eventSystemService = eventSystemService;

            InitAccelerometer();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public float GetHorizontalAxis()
        {
            return _isAccelerometerEnabled ? Input.acceleration.x : _zero;
        }

        public float GetHorizontalAxisRaw()
        {
            if (_isAccelerometerEnabled == false)
                return _zero;

            var xAcceleration = Input.acceleration.x;
            var direction = xAcceleration < -_minAcceleration ? _left : _minAcceleration < xAcceleration ? _right : _zero;

            return direction;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public int GetTouchCount()
        {
            return Input.touchCount;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Touch GetTouch(int index)
        {
            return Input.GetTouch(index);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public bool GetMouseButtonDown(int button)
        {
            return Input.GetMouseButtonDown(button);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Vector3 GetMousePosition()
        {
            return Input.mousePosition;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public bool IsPointerOverGameObject(in UnityEngine.SceneManagement.Scene scene)
        {
            return IsPointerOverGameObject(scene, 0);
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

        private void InitAccelerometer()
        {
            if (_isAccelerometerEnabled)
                return;

            if (SystemInfo.supportsAccelerometer == false)
            {
                Debug.LogError("Accelerometer is not supported on this device");
                return;
            }

            Input.gyro.enabled = true;

            _isAccelerometerEnabled = true;
        }
    }
}
#endif

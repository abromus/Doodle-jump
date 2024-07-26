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

        public float XSensitivity => _xSensitivity;

        internal AndroidInputService()
        {
            InitAccelerometer();
        }

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

        public void SetXSensitivity(float xSensitivity)
        {
            _xSensitivity = xSensitivity;
        }

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

using UnityEngine;

namespace DoodleJump.Core.Services
{
    internal sealed class InputService : IInputService
    {
        private float _xSensitivity = 1f;

        public float XSensitivity => _xSensitivity;

        public float GetHorizontalAxis()
        {
            return Input.GetAxis(AxisKeys.Horizontal);
        }

        public float GetHorizontalAxisRaw()
        {
            return Input.GetAxisRaw(AxisKeys.Horizontal);
        }

        public int GetTouchCount()
        {
            return Input.touchCount;
        }

        public Touch GetTouch(int index)
        {
            return Input.GetTouch(index);
        }

        public bool GetMouseButtonDown(int button)
        {
            return Input.GetMouseButtonDown(button);
        }

        public Vector3 GetMousePosition()
        {
            return Input.mousePosition;
        }

        public void SetXSensitivity(float xSensitivity)
        {
            _xSensitivity = xSensitivity;
        }

        public void Destroy() { }

        private sealed class AxisKeys
        {
            internal const string Horizontal = "Horizontal";
            internal const string Vertical = "Vertical";
        }
    }
}

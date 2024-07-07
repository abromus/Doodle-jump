using UnityEngine;

namespace DoodleJump.Core.Services
{
    internal sealed class InputService : IInputService
    {
        public bool GetButton(ButtonInfo buttonInfo)
        {
            var name = buttonInfo.Name.ToString();
            var type = buttonInfo.PressType;

            if (string.IsNullOrEmpty(name))
                return false;

            if (type == ButtonPressType.Key)
                return GetButton(name);
            else if (type == ButtonPressType.KeyDown)
                return GetButtonDown(name);
            else if (type == ButtonPressType.KeyUp)
                return GetButtonUp(name);

            return false;
        }

        public float GetHorizontalAxis()
        {
            return Input.GetAxis(AxisKeys.Horizontal);
        }

        public float GetHorizontalAxisRaw()
        {
            return Input.GetAxisRaw(AxisKeys.Horizontal);
        }

        public bool GetMouseButtonDown(MouseKey key)
        {
            return Input.GetMouseButtonDown((int)key);
        }

        public float GetMouseXAxis()
        {
            return Input.GetAxis(AxisKeys.MouseX);
        }

        public float GetMouseXAxisRaw()
        {
            return Input.GetAxisRaw(AxisKeys.MouseX);
        }

        public float GetMouseYAxis()
        {
            return Input.GetAxis(AxisKeys.MouseY);
        }

        public float GetMouseYAxisRaw()
        {
            return Input.GetAxisRaw(AxisKeys.MouseY);
        }

        public float GetVerticalAxis()
        {
            return Input.GetAxisRaw(AxisKeys.Vertical);
        }

        public float GetVerticalAxisRaw()
        {
            return Input.GetAxisRaw(AxisKeys.Vertical);
        }

        public void Destroy() { }

        private bool GetButton(string buttonName)
        {
            return Input.GetButton(buttonName);
        }

        private bool GetButtonDown(string buttonName)
        {
            return Input.GetButtonDown(buttonName);
        }

        private bool GetButtonUp(string buttonName)
        {
            return Input.GetButtonUp(buttonName);
        }

        private sealed class AxisKeys
        {
            internal const string Horizontal = "Horizontal";
            internal const string Vertical = "Vertical";
            internal const string MouseX = "Mouse X";
            internal const string MouseY = "Mouse Y";
        }
    }
}

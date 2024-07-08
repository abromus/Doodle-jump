using UnityEngine;

namespace DoodleJump.Core.Services
{
    internal sealed class InputService : IInputService
    {
        public float GetHorizontalAxis()
        {
            return Input.GetAxis(AxisKeys.Horizontal);
        }

        public float GetHorizontalAxisRaw()
        {
            return Input.GetAxisRaw(AxisKeys.Horizontal);
        }

        public void Destroy() { }

        private sealed class AxisKeys
        {
            internal const string Horizontal = "Horizontal";
            internal const string Vertical = "Vertical";
        }
    }
}

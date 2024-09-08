namespace DoodleJump.Core.Services
{
    public interface IInputService : IService
    {
        public float XSensitivity { get; }

        public float GetHorizontalAxis();

        public float GetHorizontalAxisRaw();

        public int GetTouchCount();

        public UnityEngine.Touch GetTouch(int index);

        public bool GetMouseButtonDown(int button);

        public UnityEngine.Vector3 GetMousePosition();

        public void SetXSensitivity(float xSensitivity);
    }
}

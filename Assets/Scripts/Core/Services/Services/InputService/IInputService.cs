namespace DoodleJump.Core.Services
{
    public interface IInputService : IService
    {
        public float XSensitivity { get; }

        public float GetHorizontalAxis();

        public float GetHorizontalAxisRaw();

        public void SetXSensitivity(float xSensitivity);
    }
}

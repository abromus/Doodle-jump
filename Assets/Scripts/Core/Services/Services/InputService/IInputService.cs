namespace DoodleJump.Core.Services
{
    internal interface IInputService : IService
    {
        public bool GetButton(ButtonInfo buttonInfo);

        public float GetHorizontalAxis();

        public float GetHorizontalAxisRaw();

        public bool GetMouseButtonDown(MouseKey key);

        public float GetMouseXAxis();

        public float GetMouseXAxisRaw();

        public float GetMouseYAxis();

        public float GetMouseYAxisRaw();

        public float GetVerticalAxis();

        public float GetVerticalAxisRaw();
    }
}

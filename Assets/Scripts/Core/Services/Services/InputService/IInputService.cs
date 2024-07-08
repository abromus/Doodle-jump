namespace DoodleJump.Core.Services
{
    public interface IInputService : IService
    {
        public float GetHorizontalAxis();

        public float GetHorizontalAxisRaw();
    }
}

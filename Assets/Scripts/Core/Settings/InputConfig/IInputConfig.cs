namespace DoodleJump.Core.Settings
{
    public interface IInputConfig : IConfig
    {
        public float MinXSensitivity { get; }

        public float MaxXSensitivity { get; }

        public float CurrentXSensitivity { get; }
    }
}

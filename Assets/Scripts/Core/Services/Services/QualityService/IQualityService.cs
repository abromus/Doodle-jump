namespace DoodleJump.Core.Services
{
    public interface IQualityService : IService
    {
        public void SetVerticalSyncActive(bool isActive);

        public void SetUnlimitedFps(bool isUnlimited);

        public void SetFps(int fps);
    }
}

using UnityEngine;

namespace DoodleJump.Core.Services
{
    internal sealed class QualityService : IQualityService
    {
        private int _currentFps;

        public QualityService(Settings.IQualityConfig qualityConfig)
        {
            SetVerticalSyncActive(qualityConfig.IsVerticalSyncEnabled);
            SetFps(Screen.currentResolution.refreshRate);
        }

        public void SetVerticalSyncActive(bool isActive)
        {
            QualitySettings.vSyncCount = isActive ? Constants.MaxVSyncCount : Constants.MinVSyncCount;
        }

        public void SetUnlimitedFps(bool isUnlimited)
        {
            Application.targetFrameRate = isUnlimited ? Constants.UnlimitedFps : _currentFps;
        }

        public void SetFps(int fps)
        {
            _currentFps = fps;

            Application.targetFrameRate = _currentFps;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Destroy() { }

        private static class Constants
        {
            internal const int UnlimitedFps = -1;
            internal const int MinVSyncCount = 0;
            internal const int MaxVSyncCount = 1;
        }
    }
}

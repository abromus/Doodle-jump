using DoodleJump.Core.Services;

namespace DoodleJump.Game.Services
{
    internal interface IAudioService : IService
    {
        public void PlayBackground(BackgroundType backgroundType);

        public void PlaySound(ClipType clipType);

        public void SetActiveBackgroundMusic(bool isActive);

        public void SetActiveSounds(bool isActive);

        public void SetBackgroundMusicVolume(float volume);

        public void SetSoundsVolume(float volume);
    }
}

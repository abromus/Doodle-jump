using DoodleJump.Core.Services;

namespace DoodleJump.Game.Services
{
    internal interface IAudioService : IService
    {
        public void PlayBackground(BackgroundType backgroundType);

        public void Play(ClipType clipType);
    }
}

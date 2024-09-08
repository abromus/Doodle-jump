using DoodleJump.Core.Services;

namespace DoodleJump.Game.Services
{
    internal interface IAudioService : IService, IPausable
    {
        public void Init(IUpdater updater);

        public void PlayBackground(BackgroundType backgroundType);

        public void PlaySound(PlatformClipType clipType);

        public void PlaySound(EnemyTriggerClipType clipType);

        public void PlaySound(ProjectileClipType clipType);

        public UnityEngine.AudioSource PlayLoopSound(EnemyClipType clipType);

        public void StopLoopSound(UnityEngine.AudioSource loopSound);

        public void SetActiveBackgroundMusic(bool isActive);

        public void SetActiveSounds(bool isActive);

        public void SetBackgroundMusicVolume(float volume);

        public void SetSoundsVolume(float volume);
    }
}

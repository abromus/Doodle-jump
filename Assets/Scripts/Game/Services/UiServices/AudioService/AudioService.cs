using DoodleJump.Core;
using UnityEngine;

namespace DoodleJump.Game.Services
{
    internal sealed class AudioService : UiService, IAudioService
    {
        [SerializeField] private AudioSource _backgroundMusic;
        [SerializeField] private AudioSource _sounds;
        [SerializeField] private BackgroundInfo[] _backgroundInfos;
        [Separator(CustomColor.Lime)]
        [SerializeField] private ClipInfo[] _clipInfos;

        public override UiServiceType UiServiceType => UiServiceType.AudioService;

        public void PlayBackground(BackgroundType backgroundType)
        {
            if (backgroundType == BackgroundType.None)
                return;

            var clip = GetBackgroundClip(backgroundType);

            if (clip == null)
                return;

            _backgroundMusic.clip = clip;
            _backgroundMusic.loop = true;
            _backgroundMusic.Play();
        }

        public void PlaySound(ClipType clipType)
        {
            if (clipType == ClipType.None)
                return;

            var clip = GetClip(clipType);

            if (clip == null)
                return;

            _sounds.PlayOneShot(clip);
        }

        public void SetActiveBackgroundMusic(bool isActive)
        {
            _backgroundMusic.enabled = isActive;
        }

        public void SetActiveSounds(bool isActive)
        {
            _sounds.enabled = isActive;
        }

        public void SetBackgroundMusicVolume(float volume)
        {
            _backgroundMusic.volume = volume;
        }

        public void SetSoundsVolume(float volume)
        {
            _sounds.volume = volume;
        }

        public void Destroy() { }

        private AudioClip GetBackgroundClip(BackgroundType backgroundType)
        {
            foreach (var backgroundInfo in _backgroundInfos)
                if (backgroundInfo.BackgroundType == backgroundType)
                    return backgroundInfo.AudioClip;

            return null;
        }

        private AudioClip GetClip(ClipType clipType)
        {
            foreach (var clipInfo in _clipInfos)
                if (clipInfo.ClipType == clipType)
                    return clipInfo.AudioClip;

            return null;
        }
    }
}

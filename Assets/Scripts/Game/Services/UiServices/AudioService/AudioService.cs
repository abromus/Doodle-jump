using DoodleJump.Core;
using UnityEngine;

namespace DoodleJump.Game.Services
{
    internal sealed class AudioService : UiService, IAudioService
    {
        [SerializeField] private AudioSource _audioSource;
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

            _audioSource.clip = clip;
            _audioSource.loop = true;
            _audioSource.Play();
        }

        public void Play(ClipType clipType)
        {
            if (clipType == ClipType.None)
                return;

            var clip = GetClip(clipType);

            if (clip == null)
                return;

            _audioSource.PlayOneShot(clip);
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

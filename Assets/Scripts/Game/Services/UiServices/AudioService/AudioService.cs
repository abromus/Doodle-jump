using UnityEngine;

namespace DoodleJump.Game.Services
{
    internal sealed class AudioService : UiService, IAudioService
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private ClipInfo[] _clipInfos;

        public override UiServiceType UiServiceType => UiServiceType.AudioService;

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

        private AudioClip GetClip(ClipType clipType)
        {
            foreach (var clipInfo in _clipInfos)
                if (clipInfo.ClipType == clipType)
                    return clipInfo.AudioClip;

            return null;
        }
    }
}

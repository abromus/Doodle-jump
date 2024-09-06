using UnityEngine;

namespace DoodleJump.Game.Services
{
    [System.Serializable]
    internal struct PlatformClipInfo
    {
        [SerializeField] private PlatformClipType _clipType;
        [SerializeField] private AudioClip _audioClip;

        internal readonly PlatformClipType ClipType => _clipType;

        internal readonly AudioClip AudioClip => _audioClip;
    }
}

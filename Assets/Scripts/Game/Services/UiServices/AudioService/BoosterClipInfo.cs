using UnityEngine;

namespace DoodleJump.Game.Services
{
    [System.Serializable]
    internal struct BoosterClipInfo
    {
        [SerializeField] private BoosterClipType _clipType;
        [SerializeField] private AudioClip _audioClip;

        internal readonly BoosterClipType ClipType => _clipType;

        internal readonly AudioClip AudioClip => _audioClip;
    }
}

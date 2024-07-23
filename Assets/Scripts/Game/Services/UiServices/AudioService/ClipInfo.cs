using UnityEngine;

namespace DoodleJump.Game.Services
{
    [System.Serializable]
    internal struct ClipInfo
    {
        [SerializeField] private ClipType _clipType;
        [SerializeField] private AudioClip _audioClip;

        internal readonly ClipType ClipType => _clipType;

        internal readonly AudioClip AudioClip => _audioClip;
    }
}

using UnityEngine;

namespace DoodleJump.Game.Services
{
    [System.Serializable]
    internal struct BackgroundInfo
    {
        [SerializeField] private BackgroundType _backgroundType;
        [SerializeField] private AudioClip _audioClip;

        internal readonly BackgroundType BackgroundType => _backgroundType;

        internal readonly AudioClip AudioClip => _audioClip;
    }
}

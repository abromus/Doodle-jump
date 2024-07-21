using UnityEngine;

namespace DoodleJump.Game.UI
{
    [System.Serializable]
    internal struct ScreenInfo
    {
        [SerializeField] private ScreenType _screenType;
        [SerializeField] private ScreenBase _screenPrefab;

        internal readonly ScreenType ScreenType => _screenType;

        internal readonly ScreenBase ScreenPrefab => _screenPrefab;
    }
}

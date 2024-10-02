using UnityEngine;

namespace DoodleJump.Game.UI
{
    [System.Serializable]
    internal struct ScreenInfo
    {
        [SerializeField] private ScreenType _screenType;
        [SerializeField] private BaseScreen _screenPrefab;

        internal readonly ScreenType ScreenType => _screenType;

        internal readonly BaseScreen ScreenPrefab => _screenPrefab;
    }
}

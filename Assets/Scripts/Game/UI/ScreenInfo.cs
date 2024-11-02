namespace DoodleJump.Game.UI
{
    [System.Serializable]
    internal struct ScreenInfo
    {
        [UnityEngine.SerializeField] private ScreenType _screenType;
        [UnityEngine.SerializeField] private BaseScreen _screenPrefab;

        internal readonly ScreenType ScreenType => _screenType;

        internal readonly BaseScreen ScreenPrefab => _screenPrefab;
    }
}

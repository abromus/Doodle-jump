namespace DoodleJump.Game.UI
{
    [System.Serializable]
    public struct BoosterViewInfo
    {
        [UnityEngine.SerializeField] private Worlds.Boosters.BoosterType _boosterType;
        [UnityEngine.SerializeField] private UiBooster _uiBoosterPrefab;

        internal readonly Worlds.Boosters.BoosterType BoosterType => _boosterType;

        internal readonly UiBooster UiBoosterPrefab => _uiBoosterPrefab;
    }
}

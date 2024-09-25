namespace DoodleJump.Game.Settings
{
    [System.Serializable]
    internal struct MainScreenConfig : IMainScreenConfig
    {
        [UnityEngine.SerializeField] private BoosterViewInfo[] _boosterViewInfos;

        public readonly BoosterViewInfo[] BoosterViewInfos => _boosterViewInfos;
    }
}

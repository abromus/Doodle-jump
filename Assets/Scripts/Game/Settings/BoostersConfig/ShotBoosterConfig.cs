namespace DoodleJump.Game.Settings
{
    [System.Serializable]
    internal struct ShotBoosterConfig : IShotBoosterConfig
    {
        [UnityEngine.SerializeField] private Worlds.Boosters.BoosterType _boosterType;
        [UnityEngine.SerializeField] private Worlds.Entities.Boosters.Booster _boosterPrefab;
        [UnityEngine.SerializeField] private int _shotCount;

        public readonly string Title => "Конфиг снарядов";

        public readonly Worlds.Boosters.BoosterType BoosterType => _boosterType;

        public readonly Worlds.Entities.Boosters.Booster BoosterPrefab => _boosterPrefab;

        public readonly int ShotsCount => _shotCount;
    }
}

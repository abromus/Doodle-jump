namespace DoodleJump.Game.Settings
{
    [System.Serializable]
    internal struct MoneyBoosterConfig : IMoneyBoosterConfig
    {
        [UnityEngine.SerializeField] private Worlds.Boosters.BoosterType _boosterType;
        [UnityEngine.SerializeField] private Worlds.Entities.Boosters.Booster _boosterPrefab;
        [UnityEngine.SerializeField] private int _count;

        public readonly string Title => "Конфиг монет";

        public readonly Worlds.Boosters.BoosterType BoosterType => _boosterType;

        public readonly Worlds.Entities.Boosters.Booster BoosterPrefab => _boosterPrefab;

        public readonly int Count => _count;
    }
}

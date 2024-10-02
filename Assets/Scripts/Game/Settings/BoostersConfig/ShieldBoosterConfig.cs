namespace DoodleJump.Game.Settings
{
    [System.Serializable]
    internal struct ShieldBoosterConfig : IShieldBoosterConfig
    {
        [UnityEngine.SerializeField] private Worlds.Boosters.BoosterType _boosterType;
        [UnityEngine.SerializeField] private Worlds.Entities.Boosters.BaseBooster _boosterPrefab;
        [UnityEngine.SerializeField] private float _existenseTime;

        public readonly string Title => "Конфиг щита";

        public readonly Worlds.Boosters.BoosterType BoosterType => _boosterType;

        public readonly Worlds.Entities.Boosters.BaseBooster BoosterPrefab => _boosterPrefab;

        public readonly float ExistenseTime => _existenseTime;
    }
}

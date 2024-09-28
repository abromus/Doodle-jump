namespace DoodleJump.Game.Settings
{
    [System.Serializable]
    internal struct JetpackBoosterConfig : IJetpackBoosterConfig
    {
        [UnityEngine.SerializeField] private Worlds.Boosters.BoosterType _boosterType;
        [UnityEngine.SerializeField] private Worlds.Entities.Boosters.Booster _boosterPrefab;
        [UnityEngine.SerializeField] private float _existenseTime;
        [UnityEngine.SerializeField] private float _jumpForce;

        public readonly string Title => "Конфиг ракетного ранца";

        public readonly Worlds.Boosters.BoosterType BoosterType => _boosterType;

        public readonly Worlds.Entities.Boosters.Booster BoosterPrefab => _boosterPrefab;

        public readonly float ExistenseTime => _existenseTime;

        public readonly float JumpForce => _jumpForce;
    }
}

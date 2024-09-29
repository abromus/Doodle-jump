namespace DoodleJump.Game.Worlds.Boosters
{
    internal readonly struct MoneyTrigger : IBoosterTrigger
    {
        private readonly Data.IPlayerData _playerData;
        private readonly IMoneyCollisionInfo _info;

        public readonly BoosterTriggerType TriggerType => BoosterTriggerType.Money;

        public MoneyTrigger(Data.IPlayerData playerData, IBoosterCollisionInfo info)
        {
            _playerData = playerData;
            _info = info as IMoneyCollisionInfo;
        }

        public readonly void Execute()
        {
            _playerData.SetCurrentScore(_playerData.CurrentScore + _info.Money);
            _info.WorldBooster.Destroy();
        }

        public void UpdateInfo(IBoosterCollisionInfo info) { }
    }
}

﻿namespace DoodleJump.Game.Worlds.Boosters
{
    internal readonly struct MoneyTrigger : IBoosterTrigger
    {
        private readonly Data.IPlayerData _playerData;
        private readonly IBoosterCollisionInfo _info;
        private readonly Settings.IMoneyBoosterConfig _config;

        public readonly BoosterTriggerType TriggerType => BoosterTriggerType.Money;

        internal MoneyTrigger(Data.IPlayerData playerData, IBoosterCollisionInfo info)
        {
            _playerData = playerData;
            _info = info;
            _config = _info.Config as Settings.IMoneyBoosterConfig;
        }

        public readonly void Execute()
        {
            _playerData.SetCurrentScore(_playerData.CurrentScore + _config.Count);
            _info.WorldBooster.Destroy();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void UpdateInfo(IBoosterCollisionInfo info) { }
    }
}

namespace DoodleJump.Game.Worlds.Boosters
{
    internal readonly struct ShotsTrigger : IBoosterTrigger
    {
        private readonly Data.IPlayerData _playerData;
        private readonly IBoosterCollisionInfo _info;
        private readonly Settings.IShotBoosterConfig _config;

        public readonly BoosterTriggerType TriggerType => BoosterTriggerType.Shots;

        public ShotsTrigger(Data.IPlayerData playerData, IBoosterCollisionInfo info)
        {
            _playerData = playerData;
            _info = info;
            _config = _info.Config as Settings.IShotBoosterConfig;
        }

        public readonly void Execute()
        {
            _playerData.SetCurrentShots(_playerData.CurrentShots + _config.ShotsCount);
            _info.WorldBooster.Destroy();
        }

        public void UpdateInfo(IBoosterCollisionInfo info) { }
    }
}

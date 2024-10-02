using DoodleJump.Core;

namespace DoodleJump.Game.Worlds.Entities
{
    internal sealed class ShotsBooster : Boosters.BaseBooster
    {
        private Settings.IShotBoosterConfig _config;
        private Data.IPlayerData _playerData;

        public override event System.Action<Boosters.IBooster> Executed;

        public override void Init(Settings.IBoosterConfig boosterConfig, in DoodlerBoosterStorageArgs args)
        {
            _config = (Settings.IShotBoosterConfig)boosterConfig;
            _playerData = args.PlayerData;
        }

        public override void Execute()
        {
            _playerData.SetCurrentShots(_playerData.CurrentShots + _config.ShotsCount);

            transform.localPosition = UnityEngine.Vector3.zero;

            Show();

            Executed.SafeInvoke(this);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override void Destroy()
        {
            Hide();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void Show()
        {
            SpriteRenderer.enabled = true;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void Hide()
        {
            SpriteRenderer.enabled = false;
        }
    }
}

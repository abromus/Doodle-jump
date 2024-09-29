using System;
using DoodleJump.Core;
using DoodleJump.Game.Settings;

namespace DoodleJump.Game.Worlds.Boosters
{
    internal sealed class JetpackWorldBooster : WorldBooster
    {
        private IBoosterCollisionInfo _info;
        private IJetpackBoosterConfig _config;

        public override IBoosterCollisionInfo Info => _info;

        public override event Action<IBoosterCollisionInfo> Collided;

        public override event Action<IWorldBooster> Destroyed;

        public override void Init(Data.IGameData gameData)
        {
            base.Init(gameData);

            _config = gameData.ConfigStorage.GetBoostersConfig().GetBoosterConfig<IJetpackBoosterConfig>();

            _info = new JetpackCollisionInfo(this, _config);
        }

        public override void Destroy()
        {
            base.Destroy();

            Destroyed.SafeInvoke(this);
        }

        private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
        {
            Collided.SafeInvoke(_info);

            PlaySound(ClipType);
        }
    }
}

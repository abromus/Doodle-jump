using DoodleJump.Core.Services;
using DoodleJump.Game.Data;
using UnityEngine;

namespace DoodleJump.Game.Worlds.Entities
{
    internal sealed class InkBlot : Enemy
    {
        private Rect _screenRect;
        private IEnemyCollisionInfo _info;

        public override void Init(IGameData gameData, Factories.IBoosterTriggerFactory boosterTriggerFactory)
        {
            base.Init(gameData, boosterTriggerFactory);

            _screenRect = gameData.CoreData.ServiceStorage.GetCameraService().GetScreenRect();
        }

        public override void InitPosition(Vector3 position)
        {
            var value = Random.value;
            var isRight = value < Constants.Half;
            var localScale = Vector3.one;
            var halfSize = localScale.x * Constants.Half;

            position.x = isRight ? _screenRect.xMin + halfSize : _screenRect.xMax - halfSize;
            localScale.x = isRight ? Constants.Left : Constants.Right;
            transform.localScale = localScale;

            base.InitPosition(position);
        }

        protected override IEnemyCollisionInfo GetCollisionInfo()
        {
            return _info;
        }

        private void Awake()
        {
            _info = new InkBlotCollisionInfo(this);
        }
    }
}

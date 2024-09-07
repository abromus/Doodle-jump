using System;
using DoodleJump.Core;
using DoodleJump.Core.Services;
using DoodleJump.Game.Data;
using UnityEngine;

namespace DoodleJump.Game.Worlds.Entities
{
    internal sealed class InkBlot : Enemy
    {
        private Rect _screenRect;
        private IEnemyCollisionInfo _info;

        public override event Action<IEnemyCollisionInfo> Collided;

        public override event Action<IEnemy> Destroyed;

        public override void Init(IGameData gameData)
        {
            base.Init(gameData);

            _screenRect = gameData.CoreData.ServiceStorage.GetCameraService().GetScreenRect();
        }

        public override void InitPosition(Vector3 position)
        {
            var value = UnityEngine.Random.value;
            var half = 0.5f;
            var isRight = value < half;
            var localScale = Vector3.one;
            var halfSize = localScale.x * half;

            position.x = isRight ? _screenRect.xMin + halfSize : _screenRect.xMax - halfSize;
            localScale.x = isRight ? Constants.Left : Constants.Right;
            transform.localScale = localScale;

            base.InitPosition(position);
        }

        public override void Destroy()
        {
            base.Destroy();

            Destroyed.SafeInvoke(this);
        }

        private void Awake()
        {
            _info = new InkBlotCollisionInfo(this);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.TryGetComponent<IDoodler>(out var doodler) == false)
                return;

            Collided.SafeInvoke(_info);

            PlayTriggerSound();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.TryGetComponent<IDoodler>(out var doodler) == false)
                return;

            Collided.SafeInvoke(_info);

            PlayTriggerSound();
        }
    }
}

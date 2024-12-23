﻿using DoodleJump.Core;
using DoodleJump.Game.Settings;

namespace DoodleJump.Game.Worlds.Platforms
{
    internal sealed class QuantityPlatform : BasePlatform
    {
        [UnityEngine.SerializeField] private UnityEngine.SpriteRenderer _spriteRenderer;

        private QuantityPlatformConfig _config;
        private QuantityPlatformCollisionInfo _info;
        private int _quantity;
        private int _maxQuantity;

        private readonly float _maxAlpha = 1f;

        public override event System.Action<IPlatformCollisionInfo> Collided;

        public override event System.Action<IPlatform> Destroyed;

        public override void InitConfig(IPlatformConfig platformConfig)
        {
            _config = (QuantityPlatformConfig)platformConfig;
            _info = new QuantityPlatformCollisionInfo(this, _config.Quantity);
            _maxQuantity = _config.Quantity;
        }

        public override void InitPosition(UnityEngine.Vector3 position)
        {
            base.InitPosition(position);

            ResetQuantity();
            UpdateColor(_maxAlpha);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override void Destroy()
        {
            Destroyed.SafeInvoke(this);
        }

        private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
        {
            if (0f < collision.relativeVelocity.y || collision.transform.TryGetComponent<Entities.IDoodler>(out var doodler) == false)
                return;

            --_quantity;

            _info.UpdateQuantity(_quantity);

            UpdateColor(1f * _quantity / _maxQuantity);
            PlaySound(ClipType);

            Collided.SafeInvoke(_info);

            if (_quantity == 0)
                Destroy();
        }

        private void ResetQuantity()
        {
            _quantity = _config.Quantity;

            _info.UpdateQuantity(_quantity);
        }

        private void UpdateColor(float alpha)
        {
            var color = _spriteRenderer.color;
            color.a = alpha;
            _spriteRenderer.color = color;
        }
    }
}

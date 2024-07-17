using DoodleJump.Game.Worlds.Entities;
using UnityEngine;

namespace DoodleJump.Game.Worlds
{
    internal sealed class EarthPlatform : Platform
    {
        [SerializeField] private Vector2 _size;
        [SerializeField] private float _jumpForce = 10f;

        public override Vector2 Size => _size;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.relativeVelocity.y <= 0f && collision.transform.TryGetComponent<Doodler>(out var doodler))
                doodler.Jump(_jumpForce);
        }
    }
}

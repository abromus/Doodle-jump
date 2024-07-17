using DoodleJump.Game.Worlds.Entities;
using UnityEngine;

namespace DoodleJump.Game.Worlds
{
    internal sealed class EarthPlatform : Platform
    {
        [SerializeField] private float _jumpForce = 10f;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.relativeVelocity.y <= 0f && collision.transform.TryGetComponent<Doodler>(out var doodler))
                doodler.Jump(_jumpForce);
        }
    }
}

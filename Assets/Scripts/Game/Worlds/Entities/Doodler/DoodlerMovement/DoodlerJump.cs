using UnityEngine;

namespace DoodleJump.Game.Worlds.Entities
{
    internal sealed class DoodlerJump : IDoodlerJump
    {
        private readonly Transform _transform;
        private readonly Rigidbody2D _rigidbody;

        internal DoodlerJump(in DoodlerMovementArgs args)
        {
            _transform = args.Transform;
            _rigidbody = args.Rigidbody;
        }

        public void Do(float height)
        {
            /*var velocity = _rigidbody.velocity;
            velocity.y = jumpHeight;
            _rigidbody.velocity = velocity;*/

            _rigidbody.AddForce(_transform.up * height, ForceMode2D.Impulse);
        }
    }
}

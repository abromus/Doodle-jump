using DoodleJump.Core;

namespace DoodleJump.Game.Worlds.Entities
{
    internal sealed class DoodlerJump : IDoodlerJump
    {
        private readonly UnityEngine.Transform _transform;
        private readonly UnityEngine.Rigidbody2D _rigidbody;

        public event System.Action Jumped;

        internal DoodlerJump(in DoodlerMovementArgs args)
        {
            _transform = args.Transform;
            _rigidbody = args.Rigidbody;
        }

        public void Do(float height)
        {
            _rigidbody.AddForce(_transform.up * height, UnityEngine.ForceMode2D.Impulse);

            Jumped.SafeInvoke();
        }
    }
}

using DoodleJump.Core;

namespace DoodleJump.Game.Worlds.Platforms
{
    internal sealed class EarthPlatform : BasePlatform
    {
        private IPlatformCollisionInfo _info;

        public override event System.Action<IPlatformCollisionInfo> Collided;

        public override event System.Action<IPlatform> Destroyed;

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override void Destroy()
        {
            Destroyed.SafeInvoke(this);
        }

        private void Awake()
        {
            _info = new JumpPlatformCollisionInfo(this);
        }

        private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
        {
            if (0f < collision.relativeVelocity.y || collision.transform.TryGetComponent<Entities.IDoodler>(out var doodler) == false)
                return;

            Collided.SafeInvoke(_info);

            PlaySound(ClipType);
        }
    }
}

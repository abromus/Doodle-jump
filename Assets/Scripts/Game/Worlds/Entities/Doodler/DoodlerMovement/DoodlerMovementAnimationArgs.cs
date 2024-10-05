namespace DoodleJump.Game.Worlds.Entities
{
    internal readonly struct DoodlerMovementAnimationArgs
    {
        private readonly float _parabolaMoveDuration;
        private readonly float _parabolaHeight;
        private readonly float _downMoveDuration;

        internal readonly float ParabolaMoveDuration => _parabolaMoveDuration;

        internal readonly float ParabolaHeight => _parabolaHeight;

        internal readonly float DownMoveDuration => _downMoveDuration;

        internal DoodlerMovementAnimationArgs(
            float parabolaMoveDuration,
            float parabolaHeight,
            float downMoveDuration)
        {
            _parabolaMoveDuration = parabolaMoveDuration;
            _parabolaHeight = parabolaHeight;
            _downMoveDuration = downMoveDuration;
        }
    }
}

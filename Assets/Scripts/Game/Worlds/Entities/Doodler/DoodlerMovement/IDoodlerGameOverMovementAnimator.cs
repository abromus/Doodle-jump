namespace DoodleJump.Game.Worlds.Entities
{
    internal interface IDoodlerGameOverMovementAnimator
    {
        public void Start(GameOverType type);

        public void End();
    }
}

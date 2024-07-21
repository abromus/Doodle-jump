namespace DoodleJump.Game.Worlds
{
    internal interface ITriggerExecutor
    {
        public void Execute(IPlatformCollisionInfo info);
    }
}

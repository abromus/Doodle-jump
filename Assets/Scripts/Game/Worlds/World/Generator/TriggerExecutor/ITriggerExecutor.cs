namespace DoodleJump.Game.Worlds
{
    internal interface ITriggerExecutor
    {
        public void Execute(Settings.IProgressInfo currentProgress, IPlatformCollisionInfo info);
    }
}

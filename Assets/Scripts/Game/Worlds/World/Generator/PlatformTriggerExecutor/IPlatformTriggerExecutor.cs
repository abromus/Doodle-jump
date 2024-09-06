namespace DoodleJump.Game.Worlds
{
    internal interface IPlatformTriggerExecutor
    {
        public void Execute(Settings.IProgressInfo currentProgress, Platforms.IPlatformCollisionInfo info);
    }
}

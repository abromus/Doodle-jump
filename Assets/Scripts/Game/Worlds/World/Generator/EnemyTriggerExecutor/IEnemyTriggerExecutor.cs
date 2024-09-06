namespace DoodleJump.Game.Worlds
{
    internal interface IEnemyTriggerExecutor
    {
        public void Execute(Settings.IProgressInfo currentProgress, Entities.IEnemyCollisionInfo info);
    }
}

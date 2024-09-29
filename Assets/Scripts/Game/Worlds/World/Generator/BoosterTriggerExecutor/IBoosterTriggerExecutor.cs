namespace DoodleJump.Game.Worlds
{
    internal interface IBoosterTriggerExecutor
    {
        public void Execute(Settings.IProgressInfo currentProgress, Boosters.IBoosterCollisionInfo info);

        public void Execute(Boosters.IWorldBooster worldBooster, Boosters.BoosterTriggerType boosterTriggerType);
    }
}

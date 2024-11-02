namespace DoodleJump.Game.Worlds
{
    internal interface IEnemyStorage : Core.IDestroyable
    {
        public System.Collections.Generic.IReadOnlyList<Entities.IEnemy> Enemies { get; }

        public event System.Action<Settings.IProgressInfo, Entities.IEnemyCollisionInfo> Collided;

        public event System.Action<Boosters.IWorldBooster, Boosters.BoosterTriggerType> BoosterDropped;

        public void Clear();

        public void GenerateEnemies();

        public void DestroyEnemy(Entities.IEnemy enemy);
    }
}

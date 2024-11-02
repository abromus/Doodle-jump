namespace DoodleJump.Game.Factories
{
    internal interface IEnemyTriggerFactory : Core.Factories.IFactory
    {
        public void Init(Worlds.Entities.IDoodler doodler, Worlds.IWorldData worldData);

        public Worlds.Entities.IEnemyTrigger Create(Worlds.Entities.IEnemyCollisionInfo info, Worlds.Entities.IEnemy enemy, Settings.IEnemyConfig enemyConfig);
    }
}

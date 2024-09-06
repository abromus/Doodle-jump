namespace DoodleJump.Game.Worlds.Entities
{
    internal sealed class BirdCollisionInfo : IEnemyCollisionInfo
    {
        private readonly IEnemy _enemy;

        public IEnemy Enemy => _enemy;

        public BirdCollisionInfo(IEnemy enemy)
        {
            _enemy = enemy;
        }
    }
}

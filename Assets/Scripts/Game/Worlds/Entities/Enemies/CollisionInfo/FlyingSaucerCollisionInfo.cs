namespace DoodleJump.Game.Worlds.Entities
{
    internal sealed class FlyingSaucerCollisionInfo : IEnemyCollisionInfo
    {
        private readonly IEnemy _enemy;

        public IEnemy Enemy => _enemy;

        internal FlyingSaucerCollisionInfo(IEnemy enemy)
        {
            _enemy = enemy;
        }
    }
}

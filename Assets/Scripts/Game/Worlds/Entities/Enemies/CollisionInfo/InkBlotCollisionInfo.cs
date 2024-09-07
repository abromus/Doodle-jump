namespace DoodleJump.Game.Worlds.Entities
{
    internal sealed class InkBlotCollisionInfo : IEnemyCollisionInfo
    {
        private readonly IEnemy _enemy;

        public IEnemy Enemy => _enemy;

        public InkBlotCollisionInfo(IEnemy enemy)
        {
            _enemy = enemy;
        }
    }
}

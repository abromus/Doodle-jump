namespace DoodleJump.Game.Worlds
{
    internal interface IWorldData
    {
        public event System.Action<GameOverType> GameOvered;

        public event System.Action GameRestarted;

        public void GameOver(GameOverType type);

        public void Restart();
    }
}

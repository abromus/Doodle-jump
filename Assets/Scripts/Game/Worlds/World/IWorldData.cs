namespace DoodleJump.Game.Worlds
{
    internal interface IWorldData
    {
        public event System.Action GameStarted;

        public event System.Action<GameOverType> GameOvered;

        public event System.Action GameRestarted;

        public void SetGameStarted();

        public void SetGameOvered(GameOverType type);

        public void SetGameRestarted();
    }
}

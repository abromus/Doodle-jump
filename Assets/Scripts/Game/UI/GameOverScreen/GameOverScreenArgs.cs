namespace DoodleJump.Game.UI
{
    internal readonly struct GameOverScreenArgs : Services.IScreenArgs
    {
        private readonly Worlds.GameOverType _gameOverType;

        internal readonly Worlds.GameOverType GameOverType => _gameOverType;

        internal GameOverScreenArgs(Worlds.GameOverType gameOverType)
        {
            _gameOverType = gameOverType;
        }
    }
}

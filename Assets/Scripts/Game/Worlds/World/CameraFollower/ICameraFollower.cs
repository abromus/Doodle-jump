namespace DoodleJump.Game.Worlds
{
    internal interface ICameraFollower
    {
        public void GameOver(GameOverType type);

        public void Restart();

        public void LateTick(float deltaTime);
    }
}

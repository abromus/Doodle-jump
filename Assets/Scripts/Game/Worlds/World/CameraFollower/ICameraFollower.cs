namespace DoodleJump.Game.Worlds
{
    internal interface ICameraFollower
    {
        public void Restart();

        public void LateTick(float deltaTime);
    }
}

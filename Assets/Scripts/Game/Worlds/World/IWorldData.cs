namespace DoodleJump.Game.Worlds
{
    internal interface IWorldData
    {
        public event System.Action GameOver;

        public void Restart();
    }
}

namespace DoodleJump.Game
{
    internal interface IGenerator : Core.IDestroyable
    {
        public void Prepare();

        public void Restart();

        public void Tick();
    }
}

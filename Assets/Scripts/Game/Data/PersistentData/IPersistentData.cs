namespace DoodleJump.Game.Data
{
    internal interface IPersistentData
    {
        public void Init();

        public void Save();

        public void Dispose();
    }
}

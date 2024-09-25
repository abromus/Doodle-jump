namespace DoodleJump.Game.Data
{
    internal interface IDataStorage
    {
        public void Init();

        public void Save();

        public void Dispose();
    }
}

namespace DoodleJump.Game.Data
{
    internal interface IPersistentData
    {
        public void Init(Mono.Data.Sqlite.SqliteConnection connection);

        public void Save();

        public void Dispose();
    }
}

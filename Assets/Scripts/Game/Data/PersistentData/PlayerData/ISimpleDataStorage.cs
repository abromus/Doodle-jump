namespace DoodleJump.Game.Data
{
    internal interface ISimpleDataStorage : IDataStorage
    {
        public SimpleInfo Info { get; }

        public event System.Action ScoreChanged;

        public void SetCurrentScore(int score);
    }
}

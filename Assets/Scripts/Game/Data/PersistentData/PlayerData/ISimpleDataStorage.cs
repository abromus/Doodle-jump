namespace DoodleJump.Game.Data
{
    internal interface ISimpleDataStorage : IDataStorage
    {
        public bool IsFirstSession { get; }

        public SimpleInfo Info { get; }

        public event System.Action ScoreChanged;

        public event System.Action<int, int> ShotsChanged;

        public event System.Action<int, int> MaxShotsChanged;

        public void SetCurrentScore(int score);

        public void SetCurrentShots(int shots);

        public void SetMaxShots(int maxShots);
    }
}

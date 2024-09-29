namespace DoodleJump.Game.Data
{
    internal interface IPlayerData : IPersistentData
    {
        public bool IsFirstSession { get; }

        public int CurrentScore { get; }

        public int MaxScore { get; }

        public int CurrentShots { get; }

        public int MaxShots { get; }

        public System.Collections.Generic.Dictionary<Worlds.Boosters.BoosterType, int> Boosters { get; }

        public event System.Action ScoreChanged;

        public event System.Action<int, int> ShotsChanged;

        public event System.Action<int, int> MaxShotsChanged;

        public event System.Action<Worlds.Boosters.BoosterType, int> BoosterChanged;

        public event System.Action<Worlds.Boosters.BoosterType> BoosterUsed;

        public void SetCurrentScore(int score);

        public void SetCurrentShots(int shots);

        public void SetMaxShots(int maxShots);

        public void AddBooster(Worlds.Boosters.IBoosterCollisionInfo info, int count);

        public void RemoveBooster(Worlds.Boosters.BoosterType boosterType, int count = 1);

        public void UseBooster(Worlds.Boosters.BoosterType boosterType, int count = 1);
    }
}

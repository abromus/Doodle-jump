using DoodleJump.Core;

namespace DoodleJump.Game.Data
{
    internal sealed class PlayerData : IPlayerData
    {
        private readonly Settings.IDoodlerConfig _doodlerConfig;
        private readonly Mono.Data.Sqlite.SqliteConnection _connection;
        private readonly ISimpleDataStorage _simpleDataStorage;
        private readonly IComplexDataStorage _complexDataStorage;

        public bool IsFirstSession => _simpleDataStorage.IsFirstSession;

        public int CurrentScore => _simpleDataStorage.Info.CurrentScore;

        public int MaxScore => _simpleDataStorage.Info.MaxScore;

        public int CurrentShots => _simpleDataStorage.Info.CurrentShots;

        public int MaxShots => _simpleDataStorage.Info.MaxShots;

        public System.Collections.Generic.Dictionary<Worlds.Boosters.BoosterType, int> Boosters => _complexDataStorage.Boosters;

        public event System.Action ScoreChanged;

        public event System.Action<int, int> ShotsChanged;

        public event System.Action<int, int> MaxShotsChanged;

        public event System.Action<Worlds.Boosters.BoosterType, int> BoosterChanged;

        public event System.Action<Worlds.Boosters.BoosterType> BoosterUsed;

        public PlayerData(Settings.IDoodlerConfig doodlerConfig)
        {
            _doodlerConfig = doodlerConfig;
            _connection = new Mono.Data.Sqlite.SqliteConnection($"URI=file:{UnityEngine.Application.persistentDataPath}/PlayerData.db");
            _connection.Open();

            _simpleDataStorage = new SimpleDataStorage(_connection);
            _complexDataStorage = new ComplexDataStorage(_connection);

            Subscribe();
        }

        public void Init()
        {
            _simpleDataStorage.Init();
            _complexDataStorage.Init();

            if (_simpleDataStorage.IsFirstSession)
                SetMaxShots(_doodlerConfig.MaxShots);
        }

        public void Save()
        {
            _simpleDataStorage.Save();
            _complexDataStorage.Save();
        }

        public void Dispose()
        {
            Unsubscribe();

            _simpleDataStorage.Dispose();
            _complexDataStorage.Dispose();

            _connection.Close();
        }

        public void SetCurrentScore(int score)
        {
            _simpleDataStorage.SetCurrentScore(score);
        }

        public void SetCurrentShots(int shots)
        {
            _simpleDataStorage.SetCurrentShots(shots);
        }

        public void SetMaxShots(int shots)
        {
            _simpleDataStorage.SetMaxShots(shots);
        }

        public void AddBooster(Worlds.Boosters.IBoosterCollisionInfo info, int count)
        {
            _complexDataStorage.AddBooster(info, count);
        }

        public void RemoveBooster(Worlds.Boosters.BoosterType boosterType, int count = 1)
        {
            _complexDataStorage.UseBooster(boosterType, count);
        }

        public void UseBooster(Worlds.Boosters.BoosterType boosterType, int count = 1)
        {
            BoosterUsed.SafeInvoke(boosterType);
        }

        private void Subscribe()
        {
            _simpleDataStorage.ScoreChanged += OnScoreChanged;
            _simpleDataStorage.ShotsChanged += OnShotsChanged;
            _complexDataStorage.BoosterChanged += OnBoosterChanged;
        }

        private void Unsubscribe()
        {
            _simpleDataStorage.ScoreChanged -= OnScoreChanged;
            _simpleDataStorage.ShotsChanged -= OnShotsChanged;
            _complexDataStorage.BoosterChanged -= OnBoosterChanged;
        }

        private void OnScoreChanged()
        {
            ScoreChanged.SafeInvoke();
        }

        private void OnShotsChanged(int previousShots, int currentShots)
        {
            ShotsChanged.SafeInvoke(previousShots, currentShots);
        }

        private void OnMaxShotsChanged(int previousMaxShots, int currentMaxShots)
        {
            MaxShotsChanged.SafeInvoke(previousMaxShots, currentMaxShots);
        }

        private void OnBoosterChanged(Worlds.Boosters.BoosterType boosterType, int score)
        {
            BoosterChanged.SafeInvoke(boosterType, score);
        }
    }
}

using DoodleJump.Core;

namespace DoodleJump.Game.Data
{
    internal sealed class PlayerData : IPlayerData
    {
        private readonly Mono.Data.Sqlite.SqliteConnection _connection;
        private readonly ISimpleDataStorage _simpleDataStorage;
        private readonly IComplexDataStorage _complexDataStorage;

        public int CurrentScore => _simpleDataStorage.Info.CurrentScore;

        public int MaxScore => _simpleDataStorage.Info.MaxScore;

        public System.Collections.Generic.Dictionary<Worlds.Boosters.BoosterType, int> Boosters => _complexDataStorage.Boosters;

        public event System.Action ScoreChanged;

        public event System.Action<Worlds.Boosters.BoosterType, int> BoosterChanged;

        public event System.Action<Worlds.Boosters.BoosterType> BoosterUsed;

        public PlayerData()
        {
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
            _complexDataStorage.BoosterChanged += OnBoosterChanged;
        }

        private void Unsubscribe()
        {
            _simpleDataStorage.ScoreChanged -= OnScoreChanged;
            _complexDataStorage.BoosterChanged -= OnBoosterChanged;
        }

        private void OnScoreChanged()
        {
            ScoreChanged.SafeInvoke();
        }

        private void OnBoosterChanged(Worlds.Boosters.BoosterType boosterType, int score)
        {
            BoosterChanged.SafeInvoke(boosterType, score);
        }
    }
}

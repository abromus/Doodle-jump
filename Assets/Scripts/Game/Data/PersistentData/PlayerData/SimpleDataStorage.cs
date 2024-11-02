using DoodleJump.Core;

namespace DoodleJump.Game.Data
{
    internal sealed class SimpleDataStorage : ISimpleDataStorage
    {
        private bool _isFirstSession = true;
        private SimpleInfo _info = new();
        private int _saveableMarker;

        private readonly Mono.Data.Sqlite.SqliteConnection _connection;
        private readonly Mono.Data.Sqlite.SqliteCommand _commandInsertInfo;
        private readonly Mono.Data.Sqlite.SqliteCommand _commandUpdateInfo;
        private readonly Mono.Data.Sqlite.SqliteCommand _commandSelectInfo;
        private readonly System.Collections.Generic.Dictionary<string, Mono.Data.Sqlite.SqliteParameter> _dbParameters = new(4);
        private readonly SaveableInfo[] _saveableInfos = new SaveableInfo[32];
        private readonly int _defaultId = 1;

        public bool IsFirstSession => _isFirstSession;

        public SimpleInfo Info => _info;

        public event System.Action ScoreChanged;

        public event System.Action<int, int> ShotsChanged;

        public event System.Action<int, int> MaxShotsChanged;

        internal SimpleDataStorage(Mono.Data.Sqlite.SqliteConnection connection)
        {
            _connection = connection;

            var info = _info;
            info.Id = _defaultId;
            _info = info;

            _dbParameters.Add(ParameterKeys.Id, new Mono.Data.Sqlite.SqliteParameter(ParameterKeys.Id, System.Data.DbType.Int32));
            _dbParameters.Add(ParameterKeys.CurrentScore, new Mono.Data.Sqlite.SqliteParameter(ParameterKeys.CurrentScore, System.Data.DbType.Int32));
            _dbParameters.Add(ParameterKeys.MaxScore, new Mono.Data.Sqlite.SqliteParameter(ParameterKeys.MaxScore, System.Data.DbType.Boolean));
            _dbParameters.Add(ParameterKeys.CurrentShots, new Mono.Data.Sqlite.SqliteParameter(ParameterKeys.CurrentShots, System.Data.DbType.Int32));
            _dbParameters.Add(ParameterKeys.MaxShots, new Mono.Data.Sqlite.SqliteParameter(ParameterKeys.MaxShots, System.Data.DbType.Boolean));

            _commandInsertInfo = GetCommandInsertInfo();
            _commandUpdateInfo = GetCommandUpdateInfo();
            _commandSelectInfo = GetCommandSelectInfo();
        }

        public void Init()
        {
            CreateSchema();

            LoadData();
        }

        public void Save()
        {
            for (int i = 0; i < _saveableMarker; i++)
            {
                ref readonly var saveableInfo = ref _saveableInfos[i];
                var info = saveableInfo.Info;
                var command = saveableInfo.Command;
                var parameters = command.Parameters;
                parameters[ParameterKeys.Id].Value = info.Id;
                parameters[ParameterKeys.CurrentScore].Value = info.CurrentScore;
                parameters[ParameterKeys.MaxScore].Value = info.MaxScore;
                parameters[ParameterKeys.CurrentShots].Value = info.CurrentShots;
                parameters[ParameterKeys.MaxShots].Value = info.MaxShots;

                command.ExecuteNonQuery();
            }

            _saveableMarker = 0;
        }

        public void Dispose()
        {
            _commandInsertInfo.Dispose();
            _commandUpdateInfo.Dispose();
            _commandSelectInfo.Dispose();
        }

        public void SetCurrentScore(int score)
        {
            if (_info.CurrentScore == score)
                return;

            _info.CurrentScore = score;

            if (_info.MaxScore < score)
                _info.MaxScore = score;

            MarkSaveInfo();

            ScoreChanged.SafeInvoke();
        }

        public void SetCurrentShots(int shots)
        {
            if (_info.CurrentShots == shots)
                return;

            var previousShots = _info.CurrentShots;
            var maxShots = _info.MaxShots;

            if (maxShots < shots)
                shots = maxShots;

            _info.CurrentShots = shots;

            MarkSaveInfo();

            ShotsChanged.SafeInvoke(previousShots, shots);
        }

        public void SetMaxShots(int maxShots)
        {
            if (_info.MaxShots == maxShots)
                return;

            var previousMaxShots = _info.MaxShots;

            _info.MaxShots = maxShots;

            MarkSaveInfo();

            MaxShotsChanged.SafeInvoke(previousMaxShots, maxShots);
        }

        private void MarkSaveInfo()
        {
            _saveableInfos[_saveableMarker] = new SaveableInfo(in _info, _commandUpdateInfo);

#if UNITY_EDITOR
            UnityEngine.Assertions.Assert.IsTrue(++_saveableMarker < _saveableInfos.Length);
#else
            ++_saveableMarker;
#endif
        }

        private Mono.Data.Sqlite.SqliteCommand GetCommandInsertInfo()
        {
            var command = _connection.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.Parameters.Clear();
            command.CommandText = SqlInfo.CommandInsertInfo;
            command.Parameters.Add(_dbParameters[ParameterKeys.Id]);
            command.Parameters.Add(_dbParameters[ParameterKeys.CurrentScore]);
            command.Parameters.Add(_dbParameters[ParameterKeys.MaxScore]);
            command.Parameters.Add(_dbParameters[ParameterKeys.CurrentShots]);
            command.Parameters.Add(_dbParameters[ParameterKeys.MaxShots]);

            return command;
        }

        private Mono.Data.Sqlite.SqliteCommand GetCommandUpdateInfo()
        {
            var command = _connection.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.Parameters.Clear();
            command.CommandText = SqlInfo.CommandUpdateInfo;
            command.Parameters.Add(_dbParameters[ParameterKeys.Id]);
            command.Parameters.Add(_dbParameters[ParameterKeys.CurrentScore]);
            command.Parameters.Add(_dbParameters[ParameterKeys.MaxScore]);
            command.Parameters.Add(_dbParameters[ParameterKeys.CurrentShots]);
            command.Parameters.Add(_dbParameters[ParameterKeys.MaxShots]);

            return command;
        }

        private Mono.Data.Sqlite.SqliteCommand GetCommandSelectInfo()
        {
            var command = _connection.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.Parameters.Clear();
            command.CommandText = SqlInfo.CommandSelectInfo;

            return command;
        }

        private void CreateSchema()
        {
            var command = _connection.CreateCommand();
            command.CommandText = SqlInfo.CommandCreateTable;
            command.ExecuteNonQuery();
        }

        private void LoadData()
        {
            var reader = _commandSelectInfo.ExecuteReader();
            var hasData = false;

            using (reader)
            {
                if (reader.Read())
                {
                    _isFirstSession = false;

                    hasData = true;

                    _info.Id = reader.GetInt32(0);
                    _info.MaxScore = reader.GetInt32(2);
                    _info.CurrentShots = reader.GetInt32(3);
                    _info.MaxShots = reader.GetInt32(4);
                }
            }

            reader.Close();

            if (hasData == false)
            {
                var parameters = _commandInsertInfo.Parameters;
                parameters[ParameterKeys.Id].Value = _info.Id;

                _commandInsertInfo.ExecuteNonQuery();
            }
        }

        private sealed class SqlInfo
        {
            internal static readonly string TableName = "Main";

            internal static readonly string CommandCreateTable =
                $"CREATE TABLE IF NOT EXISTS {TableName} " +
                $"({ParameterKeys.Id} INTEGER NOT NULL, " +
                $"{ParameterKeys.CurrentScore} INTEGER NOT NULL, " +
                $"{ParameterKeys.MaxScore} INTEGER NOT NULL, " +
                $"{ParameterKeys.CurrentShots} INTEGER NOT NULL, " +
                $"{ParameterKeys.MaxShots} INTEGER NOT NULL);";

            internal static readonly string CommandInsertInfo =
                $"INSERT OR IGNORE INTO {TableName} " +
                $"({ParameterKeys.Id}, " +
                $"{ParameterKeys.CurrentScore}, " +
                $"{ParameterKeys.MaxScore}, " +
                $"{ParameterKeys.CurrentShots}, " +
                $"{ParameterKeys.MaxShots}) " +
                $"VALUES " +
                $"(@{ParameterKeys.Id}, " +
                $"0, " +
                $"0, " +
                $"0, " +
                $"0);";

            internal static readonly string CommandUpdateInfo =
                $"UPDATE {TableName} SET " +
                $"{ParameterKeys.CurrentScore} = @{ParameterKeys.CurrentScore}, " +
                $"{ParameterKeys.MaxScore} = @{ParameterKeys.MaxScore}, " +
                $"{ParameterKeys.CurrentShots} = @{ParameterKeys.CurrentShots}, " +
                $"{ParameterKeys.MaxShots} = @{ParameterKeys.MaxShots} " +
                $"WHERE {ParameterKeys.Id} = @{ParameterKeys.Id}";

            internal static readonly string CommandSelectInfo =
                $"SELECT " +
                $"{ParameterKeys.Id}, " +
                $"{ParameterKeys.CurrentScore}, " +
                $"{ParameterKeys.MaxScore}, " +
                $"{ParameterKeys.CurrentShots}, " +
                $"{ParameterKeys.MaxShots} " +
                $"FROM {TableName};";
        }

        private sealed class ParameterKeys
        {
            internal static readonly string Id = "Id";
            internal static readonly string CurrentScore = "CurrentScore";
            internal static readonly string MaxScore = "MaxScore";
            internal static readonly string CurrentShots = "CurrentShots";
            internal static readonly string MaxShots = "MaxShots";
        }

        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
        private readonly struct SaveableInfo
        {
            internal readonly SimpleInfo Info;
            internal readonly Mono.Data.Sqlite.SqliteCommand Command;

            internal SaveableInfo(in SimpleInfo info, Mono.Data.Sqlite.SqliteCommand command)
            {
                Info = info;
                Command = command;
            }
        }
    }
}

using DoodleJump.Core;
using Mono.Data.Sqlite;

namespace DoodleJump.Game.Data
{
    internal sealed class SimpleDataStorage : ISimpleDataStorage
    {
        private SimpleInfo _info = new();
        private int _saveableMarker;

        private readonly SqliteConnection _connection;
        private readonly SqliteCommand _commandInsertInfo;
        private readonly SqliteCommand _commandUpdateInfo;
        private readonly SqliteCommand _commandSelectInfo;
        private readonly System.Collections.Generic.Dictionary<string, SqliteParameter> _dbParameters = new(4);
        private readonly SaveableInfo[] _saveableInfos = new SaveableInfo[32];
        private readonly int _defaultId = 1;

        public SimpleInfo Info => _info;

        public event System.Action ScoreChanged;

        public SimpleDataStorage(SqliteConnection connection)
        {
            _connection = connection;

            var info = _info;
            info.Id = _defaultId;
            _info = info;

            _dbParameters.Add(ParameterKeys.Id, new SqliteParameter(ParameterKeys.Id, System.Data.DbType.Int32));
            _dbParameters.Add(ParameterKeys.CurrentScore, new SqliteParameter(ParameterKeys.CurrentScore, System.Data.DbType.Int32));
            _dbParameters.Add(ParameterKeys.MaxScore, new SqliteParameter(ParameterKeys.MaxScore, System.Data.DbType.Boolean));

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

            var info = _info;
            info.CurrentScore = score;

            if (_info.MaxScore < score)
                info.MaxScore = score;

            _info = info;

            _saveableInfos[_saveableMarker] = new SaveableInfo(_info, _commandUpdateInfo);

#if UNITY_EDITOR
            UnityEngine.Assertions.Assert.IsTrue(++_saveableMarker < _saveableInfos.Length);
#else
            ++_saveableMarker;
#endif

            ScoreChanged.SafeInvoke();
        }

        private SqliteCommand GetCommandInsertInfo()
        {
            var command = _connection.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.Parameters.Clear();
            command.CommandText = SqlInfo.CommandInsertInfo;
            command.Parameters.Add(_dbParameters[ParameterKeys.Id]);
            command.Parameters.Add(_dbParameters[ParameterKeys.CurrentScore]);
            command.Parameters.Add(_dbParameters[ParameterKeys.MaxScore]);

            return command;
        }

        private SqliteCommand GetCommandUpdateInfo()
        {
            var command = _connection.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.Parameters.Clear();
            command.CommandText = SqlInfo.CommandUpdateInfo;
            command.Parameters.Add(_dbParameters[ParameterKeys.Id]);
            command.Parameters.Add(_dbParameters[ParameterKeys.CurrentScore]);
            command.Parameters.Add(_dbParameters[ParameterKeys.MaxScore]);

            return command;
        }

        private SqliteCommand GetCommandSelectInfo()
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
                    hasData = true;

                    var info = _info;
                    info.Id = reader.GetInt32(0);
                    info.MaxScore = reader.GetInt32(2);

                    _info = info;
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
                $"{ParameterKeys.MaxScore} INTEGER NOT NULL);";

            internal static readonly string CommandInsertInfo =
                $"INSERT OR IGNORE INTO {TableName} " +
                $"({ParameterKeys.Id}, {ParameterKeys.CurrentScore}, {ParameterKeys.MaxScore}) " +
                $"VALUES (@{ParameterKeys.Id}, 0, 0);";

            internal static readonly string CommandUpdateInfo =
                $"UPDATE {TableName} SET " +
                $"{ParameterKeys.CurrentScore} = @{ParameterKeys.CurrentScore}, " +
                $"{ParameterKeys.MaxScore} = @{ParameterKeys.MaxScore} " +
                $"WHERE {ParameterKeys.Id} = @{ParameterKeys.Id}";

            internal static readonly string CommandSelectInfo =
                $"SELECT " +
                $"{ParameterKeys.Id}, {ParameterKeys.CurrentScore}, {ParameterKeys.MaxScore} " +
                $"FROM {TableName};";
        }

        private sealed class ParameterKeys
        {
            internal static readonly string Id = "Id";
            internal static readonly string CurrentScore = "CurrentScore";
            internal static readonly string MaxScore = "MaxScore";
        }

        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
        private readonly struct SaveableInfo
        {
            public readonly SimpleInfo Info;
            public readonly SqliteCommand Command;

            public SaveableInfo(SimpleInfo info, SqliteCommand command)
            {
                Info = info;
                Command = command;
            }
        }
    }
}

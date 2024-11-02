using DoodleJump.Core;

namespace DoodleJump.Game.Data
{
    internal sealed class ComplexDataStorage : IComplexDataStorage
    {
        private int _saveableMarker;

        private readonly System.Collections.Generic.Dictionary<Worlds.Boosters.BoosterType, int> _boosters = new(32);
        private readonly int _defaultBoosterCount = 1;

        private readonly Mono.Data.Sqlite.SqliteConnection _connection;
        private readonly Mono.Data.Sqlite.SqliteCommand _commandInsertInfo;
        private readonly Mono.Data.Sqlite.SqliteCommand _commandUpdateInfo;
        private readonly Mono.Data.Sqlite.SqliteCommand _commandSelectInfo;
        private readonly System.Collections.Generic.Dictionary<string, Mono.Data.Sqlite.SqliteParameter> _dbParameters = new(4);
        private readonly SaveableInfo[] _saveableInfos = new SaveableInfo[32];

        public System.Collections.Generic.Dictionary<Worlds.Boosters.BoosterType, int> Boosters => _boosters;

        public event System.Action<Worlds.Boosters.BoosterType, int> BoosterChanged;

        internal ComplexDataStorage(Mono.Data.Sqlite.SqliteConnection connection)
        {
            _connection = connection;

            _dbParameters.Add(ParameterKeys.Id, new Mono.Data.Sqlite.SqliteParameter(ParameterKeys.Id, System.Data.DbType.Int32));
            _dbParameters.Add(ParameterKeys.BoosterType, new Mono.Data.Sqlite.SqliteParameter(ParameterKeys.BoosterType, System.Data.DbType.Int32));
            _dbParameters.Add(ParameterKeys.Count, new Mono.Data.Sqlite.SqliteParameter(ParameterKeys.Count, System.Data.DbType.Boolean));

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
                parameters[ParameterKeys.BoosterType].Value = info.BoosterType;
                parameters[ParameterKeys.Count].Value = info.Count;

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

        public void AddBooster(Worlds.Boosters.IBoosterCollisionInfo info, int count)
        {
            var boosterType = info.WorldBooster.BoosterType;
            var hasBooster = _boosters.ContainsKey(boosterType);
            var command = hasBooster ? _commandUpdateInfo : _commandInsertInfo;

            if (hasBooster)
                _boosters[boosterType]++;
            else
                _boosters.Add(boosterType, _defaultBoosterCount);

            UpdateBoosterCount(boosterType, _boosters[boosterType], command);
        }

        public void UseBooster(Worlds.Boosters.BoosterType boosterType, int count)
        {
#if UNITY_EDITOR
            UnityEngine.Assertions.Assert.IsTrue(_boosters.ContainsKey(boosterType));
#endif
            _boosters[boosterType]--;

            UpdateBoosterCount(boosterType, _boosters[boosterType], _commandUpdateInfo);
        }

        private void UpdateBoosterCount(Worlds.Boosters.BoosterType boosterType, int count, Mono.Data.Sqlite.SqliteCommand command)
        {
            var info = new ComplexInfo(boosterType, _boosters[boosterType]);

            _saveableInfos[_saveableMarker] = new SaveableInfo(in info, command);

#if UNITY_EDITOR
            UnityEngine.Assertions.Assert.IsTrue(++_saveableMarker < _saveableInfos.Length);
#else
            ++_saveableMarker;
#endif

            BoosterChanged.SafeInvoke(boosterType, _boosters[boosterType]);
        }

        private Mono.Data.Sqlite.SqliteCommand GetCommandInsertInfo()
        {
            var command = _connection.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.Parameters.Clear();
            command.CommandText = SqlInfo.CommandInsertInfo;
            command.Parameters.Add(_dbParameters[ParameterKeys.BoosterType]);
            command.Parameters.Add(_dbParameters[ParameterKeys.Count]);

            return command;
        }

        private Mono.Data.Sqlite.SqliteCommand GetCommandUpdateInfo()
        {
            var command = _connection.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.Parameters.Clear();
            command.CommandText = SqlInfo.CommandUpdateInfo;
            command.Parameters.Add(_dbParameters[ParameterKeys.BoosterType]);
            command.Parameters.Add(_dbParameters[ParameterKeys.Count]);

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

            using (reader)
            {
                while (reader.Read())
                {
                    var boosterId = (Worlds.Boosters.BoosterType)reader.GetInt32(0);
                    var count = reader.GetInt32(1);

                    _boosters.Add(boosterId, count);
                }
            }

            reader.Close();
        }

        private sealed class SqlInfo
        {
            internal static readonly string TableName = "Boosters";

            internal static readonly string CommandCreateTable =
                $"CREATE TABLE IF NOT EXISTS {TableName} " +
                $"({ParameterKeys.Id} INTEGER PRIMARY KEY AUTOINCREMENT, " +
                $"{ParameterKeys.BoosterType} INTEGER NOT NULL, " +
                $"{ParameterKeys.Count} INTEGER NOT NULL);";

            internal static readonly string CommandInsertInfo =
                $"INSERT OR IGNORE INTO {TableName} " +
                $"({ParameterKeys.BoosterType}, {ParameterKeys.Count}) " +
                $"VALUES (@{ParameterKeys.BoosterType}, @{ParameterKeys.Count});";

            internal static readonly string CommandUpdateInfo =
                $"UPDATE {TableName} SET " +
                $"{ParameterKeys.Count} = @{ParameterKeys.Count} " +
                $"WHERE {ParameterKeys.BoosterType} = @{ParameterKeys.BoosterType}";

            internal static readonly string CommandSelectInfo =
                $"SELECT " +
                $"{ParameterKeys.BoosterType}, {ParameterKeys.Count} " +
                $"FROM {TableName};";
        }

        private sealed class ParameterKeys
        {
            internal static readonly string Id = "Id";
            internal static readonly string BoosterType = "BoosterType";
            internal static readonly string Count = "Count";
        }

        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
        private readonly struct SaveableInfo
        {
            public readonly ComplexInfo Info;
            public readonly Mono.Data.Sqlite.SqliteCommand Command;

            internal SaveableInfo(in ComplexInfo info, Mono.Data.Sqlite.SqliteCommand command)
            {
                Info = info;
                Command = command;
            }
        }
    }
}

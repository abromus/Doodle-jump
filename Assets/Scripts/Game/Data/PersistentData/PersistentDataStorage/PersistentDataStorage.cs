using System;
using System.Collections.Generic;

namespace DoodleJump.Game.Data
{
    internal sealed class PersistentDataStorage : IPersistentDataStorage
    {
        private readonly Dictionary<Type, IPersistentData> _data;

        public PersistentDataStorage()
        {
            var playerData = new PlayerData() as IPlayerData;

            _data = new(8)
            {
                [typeof(IPlayerData)] = playerData,
            };
        }

        public void Init()
        {
            foreach (var data in _data.Values)
                data.Init();
        }

        public void Save()
        {
            foreach (var data in _data.Values)
                data.Save();
        }

        public void Dispose()
        {
            foreach (var data in _data.Values)
                data.Dispose();
        }

        public TData GetData<TData>() where TData : class, IPersistentData
        {
            return _data[typeof(TData)] as TData;
        }
    }
}

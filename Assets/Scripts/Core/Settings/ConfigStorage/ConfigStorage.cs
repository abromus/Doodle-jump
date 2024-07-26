using System;
using System.Collections.Generic;
using UnityEngine;

namespace DoodleJump.Core.Settings
{
    [CreateAssetMenu(fileName = nameof(ConfigStorage), menuName = ConfigKeys.CorePathKey + nameof(ConfigStorage))]
    internal sealed class ConfigStorage : ScriptableObject, IConfigStorage
    {
        [SerializeField] private UiFactoryConfig _uiFactoryConfig;
        [SerializeField] private UiServiceConfig _uiServiceConfig;
        [SerializeField] private InputConfig _inputConfig;

        private Dictionary<Type, IConfig> _configs;

        public void Init()
        {
            _configs = new(4)
            {
                [typeof(IUiFactoryConfig)] = _uiFactoryConfig,
                [typeof(IUiServiceConfig)] = _uiServiceConfig,
                [typeof(IInputConfig)] = _inputConfig,
            };
        }

        public void AddConfig<TConfig>(TConfig config) where TConfig : class, IConfig
        {
            var type = typeof(TConfig);

            if (_configs.ContainsKey(type))
                _configs[type] = config;
            else
                _configs.Add(type, config);
        }

        public TConfig GetConfig<TConfig>() where TConfig : class, IConfig
        {
            return _configs[typeof(TConfig)] as TConfig;
        }

        public void Destroy()
        {
            _configs.Clear();
            _configs = null;
        }
    }
}

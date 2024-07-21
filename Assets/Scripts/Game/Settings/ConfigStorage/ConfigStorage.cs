using System;
using System.Collections.Generic;
using DoodleJump.Core.Settings;
using UnityEngine;

namespace DoodleJump.Game.Settings
{
    [CreateAssetMenu(fileName = nameof(ConfigStorage), menuName = ConfigKeys.GamePathKey + nameof(ConfigStorage))]
    internal sealed class ConfigStorage : ScriptableObject, IConfigStorage
    {
        [SerializeField] private UiFactoryConfig _uiFactoryConfig;
        [SerializeField] private UiServiceConfig _uiServiceConfig;
        [SerializeField] private CameraConfig _cameraConfig;
        [SerializeField] private DoodlerConfig _doodlerConfig;
        [SerializeField] private GeneratorConfig _generatorConfig;
        [SerializeField] private PlatformsConfig _platformsConfig;

        private Dictionary<Type, IConfig> _configs;

        public void Init()
        {
            _configs = new(8)
            {
                [typeof(IUiFactoryConfig)] = _uiFactoryConfig,
                [typeof(IUiServiceConfig)] = _uiServiceConfig,
                [typeof(ICameraConfig)] = _cameraConfig,
                [typeof(IDoodlerConfig)] = _doodlerConfig,
                [typeof(IGeneratorConfig)] = _generatorConfig,
                [typeof(IPlatformsConfig)] = _platformsConfig,
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

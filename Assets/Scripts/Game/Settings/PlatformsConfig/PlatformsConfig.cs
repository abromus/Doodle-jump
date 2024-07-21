using System.Collections.Generic;
using UnityEngine;

namespace DoodleJump.Game.Settings
{
    [CreateAssetMenu(fileName = nameof(PlatformsConfig), menuName = ConfigKeys.GamePathKey + nameof(PlatformsConfig))]
    internal sealed class PlatformsConfig : ScriptableObject, IPlatformsConfig
    {
#if UNITY_EDITOR
        [Core.Label(nameof(GetTitles))]
#endif
        [SerializeReference]
        private List<IPlatformConfig> _configs = new(16);

        public IReadOnlyList<IPlatformConfig> Configs => _configs;

#if UNITY_EDITOR
        [Core.Button]
        private void AddPlatformConfig()
        {
            var menu = new UnityEditor.GenericMenu();
            var configs = GetAllConfigs();

            foreach (var config in configs)
                menu.AddItem(new GUIContent((System.Activator.CreateInstance(config) as IPlatformConfig)?.Title), false, AddConfig, config);

            menu.ShowAsContext();
        }

        private IReadOnlyList<System.Type> GetAllConfigs()
        {
            var types = new List<System.Type>(16);
            var sample = typeof(IPlatformConfig);

            foreach (var type in sample.Assembly.GetTypes())
            {
                var interfaces = type.GetInterfaces();

                foreach (var interfaceType in interfaces)
                    if (interfaceType.Equals(sample))
                        types.Add(type);
            }

            return types;
        }

        private void AddConfig(object abstractConfig)
        {
            if (abstractConfig is not System.Type config)
            {
                Debug.LogError($"[PlatformConfig]: Wrong config. Current: {abstractConfig.GetType().Name}!");

                return;
            }

            var configItem = System.Activator.CreateInstance(config);

            if (configItem is not IPlatformConfig)
            {
                Debug.LogError($"[PlatformConfig]: Wrong config instance. Current: {abstractConfig.GetType().Name}!");

                return;
            }

            _configs.Add((IPlatformConfig)configItem);

            UnityEditor.AssetDatabase.SaveAssets();
            UnityEditor.AssetDatabase.Refresh();
        }

        private IReadOnlyList<string> GetTitles()
        {
            if (_configs == null || _configs.Count == 0)
                return null;

            var titles = new List<string>(_configs.Count);

            foreach (var config in _configs)
                titles.Add(config.Title);

            return titles;
        }

        [Core.Button]
        private void NormalizeSpawnChances()
        {
            var spawnChanceSum = 0f;

            foreach (var config in _configs)
                spawnChanceSum += config.SpawnChance;

            var spawnChanceFactor = 1f / spawnChanceSum;

            foreach (var config in _configs)
                config.ChangeSpawnChance(spawnChanceFactor);
        }
#endif
    }
}

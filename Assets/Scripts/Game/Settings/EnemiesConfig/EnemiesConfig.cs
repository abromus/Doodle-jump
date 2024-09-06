using System.Collections.Generic;
using UnityEngine;

namespace DoodleJump.Game.Settings
{
    [CreateAssetMenu(fileName = nameof(EnemiesConfig), menuName = ConfigKeys.GamePathKey + nameof(EnemiesConfig))]
    internal sealed class EnemiesConfig : ScriptableObject, IEnemiesConfig
    {
#if UNITY_EDITOR
        [Core.Label(nameof(GetTitles))]
#endif
        [SerializeReference]
        private List<IEnemyConfig> _configs = new(16);

        public IReadOnlyList<IEnemyConfig> Configs => _configs;

#if UNITY_EDITOR
        [Core.Button]
        private void AddEnemyConfig()
        {
            var menu = new UnityEditor.GenericMenu();
            var configs = GetAllConfigs();

            foreach (var config in configs)
                menu.AddItem(new GUIContent((System.Activator.CreateInstance(config) as IEnemyConfig)?.Title), false, AddConfig, config);

            menu.ShowAsContext();
        }

        private IReadOnlyList<System.Type> GetAllConfigs()
        {
            var types = new List<System.Type>(16);
            var sample = typeof(IEnemyConfig);

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
                Debug.LogError($"[EnemiesConfig]: Wrong config. Current: {abstractConfig.GetType().Name}!");

                return;
            }

            var configItem = System.Activator.CreateInstance(config);

            if (configItem is not IEnemyConfig)
            {
                Debug.LogError($"[EnemiesConfig]: Wrong config instance. Current: {abstractConfig.GetType().Name}!");

                return;
            }

            _configs.Add((IEnemyConfig)configItem);

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

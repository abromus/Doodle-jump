using System.Collections.Generic;
using UnityEngine;

namespace DoodleJump.Game.Settings
{
    [CreateAssetMenu(fileName = nameof(ProgressInfo), menuName = ConfigKeys.GamePathKey + nameof(ProgressInfo))]
    internal sealed class ProgressInfo : ScriptableObject, IProgressInfo
    {
        [SerializeField] private float _minProgress;
        [SerializeField] private float _maxProgress;
        [SerializeField] private int _platformMaxCount;
        [SerializeField] private int _enemySimultaneouslyCount;
        [SerializeField] private int _enemyMaxCount;
        [SerializeField] private float _enemySpawnProbability;
        [SerializeField] private float _enemySpawnProbabilityFactor;
        [SerializeField] private float _minOffsetY;
        [SerializeField] private float _maxOffsetY;

#if UNITY_EDITOR
        [Core.Label(nameof(GetPlatformTitles))]
#endif
        [SerializeReference] private List<IPlatformConfig> _platformConfigs = new(16);

#if UNITY_EDITOR
        [Core.Label(nameof(GetEnemyTitles))]
#endif
        [SerializeReference] private List<IEnemyConfig> _enemyConfigs = new(16);

        public float MinProgress => _minProgress;

        public float MaxProgress => _maxProgress;

        public int PlatformMaxCount => _platformMaxCount;

        public int EnemySimultaneouslyCount => _enemySimultaneouslyCount;

        public int EnemyMaxCount => _enemyMaxCount;

        public float EnemySpawnProbability => _enemySpawnProbability;

        public float EnemySpawnProbabilityFactor => _enemySpawnProbabilityFactor;

        public float MinOffsetY => _minOffsetY;

        public float MaxOffsetY => _maxOffsetY;

        public IReadOnlyList<IPlatformConfig> PlatformConfigs => _platformConfigs;

        public IReadOnlyList<IEnemyConfig> EnemyConfigs => _enemyConfigs;

#if UNITY_EDITOR
        [Core.Button]
        private void AddPlatformConfig()
        {
            var menu = new UnityEditor.GenericMenu();
            var configs = GetAllConfigs(typeof(IPlatformConfig));

            foreach (var config in configs)
                menu.AddItem(new GUIContent((System.Activator.CreateInstance(config) as IPlatformConfig)?.Title), false, AddPlatformConfig, config);

            menu.ShowAsContext();
        }

        [Core.Button]
        private void AddEnemyConfig()
        {
            var menu = new UnityEditor.GenericMenu();
            var configs = GetAllConfigs(typeof(IEnemyConfig));

            foreach (var config in configs)
                menu.AddItem(new GUIContent((System.Activator.CreateInstance(config) as IEnemyConfig)?.Title), false, AddEnemyConfig, config);

            menu.ShowAsContext();
        }

        private IReadOnlyList<System.Type> GetAllConfigs(System.Type sampleType)
        {
            var types = new List<System.Type>(16);

            foreach (var type in sampleType.Assembly.GetTypes())
            {
                var interfaces = type.GetInterfaces();

                foreach (var interfaceType in interfaces)
                    if (interfaceType.Equals(sampleType))
                        types.Add(type);
            }

            return types;
        }

        private void AddPlatformConfig(object abstractConfig)
        {
            if (TryGetConfigItem<IPlatformConfig>(abstractConfig, out var configItem) == false)
                return;

            _platformConfigs.Add(configItem);

            UnityEditor.AssetDatabase.SaveAssets();
            UnityEditor.AssetDatabase.Refresh();
        }

        private void AddEnemyConfig(object abstractConfig)
        {
            if (TryGetConfigItem<IEnemyConfig>(abstractConfig, out var configItem) == false)
                return;

            _enemyConfigs.Add(configItem);

            UnityEditor.AssetDatabase.SaveAssets();
            UnityEditor.AssetDatabase.Refresh();
        }

        private bool TryGetConfigItem<T>(object abstractConfig, out T configItem)
        {
            configItem = default;

            if (abstractConfig is not System.Type config)
            {
                Debug.LogError($"[ProgressInfo]: Wrong config. Current: {abstractConfig.GetType().Name}!");

                return false;
            }

            configItem = (T)System.Activator.CreateInstance(config);

            if (configItem is not null)
                return true;

            Debug.LogError($"[ProgressInfo]: Wrong config instance. Current: {abstractConfig.GetType().Name}!");

            return false;
        }

        private IReadOnlyList<string> GetPlatformTitles()
        {
            if (_platformConfigs == null || _platformConfigs.Count == 0)
                return null;

            var titles = new List<string>(_platformConfigs.Count);

            foreach (var config in _platformConfigs)
                titles.Add(config.Title);

            return titles;
        }

        private IReadOnlyList<string> GetEnemyTitles()
        {
            if (_enemyConfigs == null || _enemyConfigs.Count == 0)
                return null;

            var titles = new List<string>(_enemyConfigs.Count);

            foreach (var config in _enemyConfigs)
                titles.Add(config.Title);

            return titles;
        }

        [Core.Button]
        private void NormalizePlatformSpawnChances()
        {
            var spawnChanceSum = 0f;

            foreach (var config in _platformConfigs)
                spawnChanceSum += config.SpawnChance;

            var spawnChanceFactor = 1f / spawnChanceSum;

            foreach (var config in _platformConfigs)
                config.ChangeSpawnChance(spawnChanceFactor);
        }
#endif
    }
}

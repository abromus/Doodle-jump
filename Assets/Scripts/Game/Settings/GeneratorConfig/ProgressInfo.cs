using System.Collections.Generic;
using UnityEngine;

namespace DoodleJump.Game.Settings
{
    [CreateAssetMenu(fileName = nameof(ProgressInfo), menuName = ConfigKeys.GamePathKey + nameof(ProgressInfo))]
    internal sealed class ProgressInfo : ScriptableObject, IProgressInfo
    {
        [SerializeField] private float _minProgress;
        [SerializeField] private float _maxProgress;
        [SerializeField] private float _minOffsetY;
        [SerializeField] private float _maxOffsetY;
        [Core.Separator(Core.CustomColor.Lime)]
        [SerializeField] private int _platformMaxCount;
        [Core.Separator(Core.CustomColor.MediumTurquoise)]
        [SerializeField] private int _enemySimultaneouslyCount;
        [SerializeField] private int _enemyMaxCount;
        [SerializeField] private float _enemySpawnProbability;
        [SerializeField] private float _enemySpawnProbabilityFactor;
        [Core.Separator(Core.CustomColor.Elsie)]
        [SerializeField] private int _boosterSimultaneouslyCount;
        [SerializeField] private int _boosterMaxCount;
        [SerializeField] private float _boosterSpawnProbability;
        [SerializeField] private float _boosterSpawnProbabilityFactor;

        [Core.Separator(Core.CustomColor.Presley)]
#if UNITY_EDITOR
        [Core.Label(nameof(GetPlatformTitles))]
#endif
        [SerializeReference] private List<IPlatformConfig> _platformConfigs = new(16);

#if UNITY_EDITOR
        [Core.Label(nameof(GetEnemyTitles))]
#endif
        [SerializeReference] private List<IEnemyConfig> _enemyConfigs = new(16);

#if UNITY_EDITOR
        [Core.Label(nameof(GetWorldBoosterTitles))]
#endif
        [SerializeReference] private List<IWorldBoosterConfig> _worldBoosterConfigs = new(16);

        public float MinProgress => _minProgress;

        public float MaxProgress => _maxProgress;

        public float MinOffsetY => _minOffsetY;

        public float MaxOffsetY => _maxOffsetY;

        public int PlatformMaxCount => _platformMaxCount;

        public int EnemySimultaneouslyCount => _enemySimultaneouslyCount;

        public int EnemyMaxCount => _enemyMaxCount;

        public float EnemySpawnProbability => _enemySpawnProbability;

        public float EnemySpawnProbabilityFactor => _enemySpawnProbabilityFactor;

        public int BoosterSimultaneouslyCount => _boosterSimultaneouslyCount;

        public int BoosterMaxCount => _boosterMaxCount;

        public float BoosterSpawnProbability => _boosterSpawnProbability;

        public float BoosterSpawnProbabilityFactor => _boosterSpawnProbabilityFactor;

        public IReadOnlyList<IPlatformConfig> PlatformConfigs => _platformConfigs;

        public IReadOnlyList<IEnemyConfig> EnemyConfigs => _enemyConfigs;

        public IReadOnlyList<IWorldBoosterConfig> WorldBoosterConfigs => _worldBoosterConfigs;

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

        [Core.Button]
        private void AddWorldBoosterConfig()
        {
            var menu = new UnityEditor.GenericMenu();
            var configs = GetAllConfigs(typeof(IWorldBoosterConfig));

            foreach (var config in configs)
                menu.AddItem(new GUIContent((System.Activator.CreateInstance(config) as IWorldBoosterConfig)?.Title), false, AddBoosterConfig, config);

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

        private void AddBoosterConfig(object abstractConfig)
        {
            if (TryGetConfigItem<IWorldBoosterConfig>(abstractConfig, out var configItem) == false)
                return;

            _worldBoosterConfigs.Add(configItem);

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

        private IReadOnlyList<string> GetWorldBoosterTitles()
        {
            if (_worldBoosterConfigs == null || _worldBoosterConfigs.Count == 0)
                return null;

            var titles = new List<string>(_worldBoosterConfigs.Count);

            foreach (var config in _worldBoosterConfigs)
                titles.Add(config.Title);

            return titles;
        }

        [Core.Button]
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void NormalizePlatformSpawnChances()
        {
            NormalizeSpawnChances(_platformConfigs);
        }

        [Core.Button]
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void NormalizeEnemySpawnChances()
        {
            NormalizeSpawnChances(_enemyConfigs);
        }

        [Core.Button]
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void NormalizeBoosterSpawnChances()
        {
            NormalizeSpawnChances(_worldBoosterConfigs);
        }

        private void NormalizeSpawnChances<T>(List<T> probables) where T : IProbable
        {
            var spawnChanceSum = 0f;

            foreach (var config in probables)
                spawnChanceSum += config.SpawnChance;

            if (spawnChanceSum == 0f)
            {
                var spawnChance = 1f / probables.Count;

                foreach (var config in probables)
                    config.SetSpawnChance(spawnChance);
            }
            else
            {
                var spawnChanceFactor = 1f / spawnChanceSum;

                foreach (var config in probables)
                    config.ChangeSpawnChance(spawnChanceFactor);
            }
        }
#endif
    }
}

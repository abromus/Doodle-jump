namespace DoodleJump.Game.Settings
{
    [UnityEngine.CreateAssetMenu(fileName = nameof(BoostersConfig), menuName = ConfigKeys.GamePathKey + nameof(BoostersConfig))]
    internal class BoostersConfig : UnityEngine.ScriptableObject, IBoostersConfig
    {
#if UNITY_EDITOR
        [Core.Label(nameof(GetBoosterTitles))]
#endif
        [UnityEngine.SerializeReference] private System.Collections.Generic.List<IBoosterConfig> _boosterConfigs = new(16);

        public System.Collections.Generic.IReadOnlyList<IBoosterConfig> BoosterConfigs => _boosterConfigs;

        public T GetBoosterConfig<T>() where T : IBoosterConfig
        {
            foreach (var boosterConfig in _boosterConfigs)
                if (boosterConfig is T targetConfig)
                    return targetConfig;

            return default;
        }

#if UNITY_EDITOR
        [Core.Button]
        private void AddBoosterConfig()
        {
            var menu = new UnityEditor.GenericMenu();
            var configs = GetAllConfigs(typeof(IBoosterConfig));

            foreach (var config in configs)
                menu.AddItem(new UnityEngine.GUIContent((System.Activator.CreateInstance(config) as IBoosterConfig)?.Title), false, AddBoosterConfig, config);

            menu.ShowAsContext();
        }

        private System.Collections.Generic.IReadOnlyList<System.Type> GetAllConfigs(System.Type sampleType)
        {
            var types = new System.Collections.Generic.List<System.Type>(16);

            foreach (var type in sampleType.Assembly.GetTypes())
            {
                var interfaces = type.GetInterfaces();

                foreach (var interfaceType in interfaces)
                    if (interfaceType.Equals(sampleType))
                        types.Add(type);
            }

            return types;
        }

        private void AddBoosterConfig(object abstractConfig)
        {
            if (TryGetConfigItem<IBoosterConfig>(abstractConfig, out var configItem) == false)
                return;

            _boosterConfigs.Add(configItem);

            UnityEditor.AssetDatabase.SaveAssets();
            UnityEditor.AssetDatabase.Refresh();
        }

        private bool TryGetConfigItem<T>(object abstractConfig, out T configItem)
        {
            configItem = default;

            if (abstractConfig is not System.Type config)
            {
                UnityEngine.Debug.LogError($"[BoosterConfig]: Wrong config. Current: {abstractConfig.GetType().Name}!");

                return false;
            }

            configItem = (T)System.Activator.CreateInstance(config);

            if (configItem is not null)
                return true;

            UnityEngine.Debug.LogError($"[BoosterConfig]: Wrong config instance. Current: {abstractConfig.GetType().Name}!");

            return false;
        }

        private System.Collections.Generic.IReadOnlyList<string> GetBoosterTitles()
        {
            if (_boosterConfigs == null || _boosterConfigs.Count == 0)
                return null;

            var titles = new System.Collections.Generic.List<string>(_boosterConfigs.Count);

            foreach (var config in _boosterConfigs)
                titles.Add(config.Title);

            return titles;
        }
#endif
    }
}

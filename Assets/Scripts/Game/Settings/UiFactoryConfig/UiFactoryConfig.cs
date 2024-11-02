namespace DoodleJump.Game.Settings
{
    [UnityEngine.CreateAssetMenu(fileName = nameof(UiFactoryConfig), menuName = ConfigKeys.GamePathKey + nameof(UiFactoryConfig))]
    internal sealed class UiFactoryConfig : UnityEngine.ScriptableObject, Core.Settings.IUiFactoryConfig
    {
        [UnityEngine.SerializeField] private System.Collections.Generic.List<Core.Factories.BaseUiFactory> _uiFactories;

        public System.Collections.Generic.IReadOnlyList<Core.Factories.IUiFactory> UiFactories => _uiFactories;
    }
}

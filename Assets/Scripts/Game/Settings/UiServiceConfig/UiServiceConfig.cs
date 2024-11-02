namespace DoodleJump.Game.Settings
{
    [UnityEngine.CreateAssetMenu(fileName = nameof(UiServiceConfig), menuName = ConfigKeys.GamePathKey + nameof(UiServiceConfig))]
    internal sealed class UiServiceConfig : UnityEngine.ScriptableObject, IUiServiceConfig
    {
        [UnityEngine.SerializeField] private System.Collections.Generic.List<Services.BaseUiService> _uiServices;

        public System.Collections.Generic.IReadOnlyList<Services.IUiService> UiServices => _uiServices;
    }
}

using UnityEngine;

namespace DoodleJump.Game.Settings
{
    [CreateAssetMenu(fileName = nameof(ScreenSystemConfig), menuName = ConfigKeys.GamePathKey + nameof(ScreenSystemConfig))]
    internal sealed class ScreenSystemConfig : ScriptableObject, IScreenSystemConfig
    {
        [SerializeField] private MainScreenConfig _mainScreenConfig;

        public IMainScreenConfig MainScreenConfig => _mainScreenConfig;
    }
}

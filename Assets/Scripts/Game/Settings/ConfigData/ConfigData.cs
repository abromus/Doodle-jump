using UnityEngine;

namespace DoodleJump.Game.Settings
{
    [CreateAssetMenu(fileName = nameof(ConfigData), menuName = ConfigKeys.GamePathKey + nameof(ConfigData))]
    internal sealed class ConfigData : ScriptableObject, IConfigData
    {
    }
}

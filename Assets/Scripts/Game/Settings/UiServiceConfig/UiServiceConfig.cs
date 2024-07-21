using System.Collections.Generic;
using DoodleJump.Game.Services;
using UnityEngine;

namespace DoodleJump.Game.Settings
{
    [CreateAssetMenu(fileName = nameof(UiServiceConfig), menuName = ConfigKeys.GamePathKey + nameof(UiServiceConfig))]
    internal sealed class UiServiceConfig : ScriptableObject, IUiServiceConfig
    {
        [SerializeField] private List<UiService> _uiServices;

        public IReadOnlyList<IUiService> UiServices => _uiServices;
    }
}

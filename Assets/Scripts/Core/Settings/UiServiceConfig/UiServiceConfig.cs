using System.Collections.Generic;
using DoodleJump.Core.Services;
using UnityEngine;

namespace DoodleJump.Core.Settings
{
    [CreateAssetMenu(fileName = nameof(UiServiceConfig), menuName = ConfigKeys.CorePathKey + nameof(UiServiceConfig))]
    internal sealed class UiServiceConfig : ScriptableObject, IUiServiceConfig
    {
        [SerializeField] private List<BaseUiService> _uiServices;

        public IReadOnlyList<IUiService> UiServices => _uiServices;
    }
}

using System.Collections.Generic;
using DoodleJump.Core.Factories;
using DoodleJump.Core.Settings;
using UnityEngine;

namespace DoodleJump.Game.Settings
{
    [CreateAssetMenu(fileName = nameof(UiFactoryConfig), menuName = ConfigKeys.GamePathKey + nameof(UiFactoryConfig))]
    internal sealed class UiFactoryConfig : ScriptableObject, IUiFactoryConfig
    {
        [SerializeField] private List<BaseUiFactory> _uiFactories;

        public IReadOnlyList<IUiFactory> UiFactories => _uiFactories;
    }
}

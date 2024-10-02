using System.Collections.Generic;
using DoodleJump.Core.Factories;
using UnityEngine;

namespace DoodleJump.Core.Settings
{
    [CreateAssetMenu(fileName = nameof(UiFactoryConfig), menuName = ConfigKeys.CorePathKey + nameof(UiFactoryConfig))]
    internal sealed class UiFactoryConfig : ScriptableObject, IUiFactoryConfig
    {
        [SerializeField] private List<BaseUiFactory> _uiFactories;

        public IReadOnlyList<IUiFactory> UiFactories => _uiFactories;
    }
}

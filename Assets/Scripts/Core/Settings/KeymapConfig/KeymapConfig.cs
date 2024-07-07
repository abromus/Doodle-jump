using System.Collections.Generic;
using DoodleJump.Core.Services;
using UnityEngine;

namespace DoodleJump.Core.Settings
{
    [CreateAssetMenu(fileName = nameof(KeymapConfig), menuName = ConfigKeys.CorePathKey + nameof(KeymapConfig))]
    internal sealed class KeymapConfig : ScriptableObject, IKeymapConfig
    {
        [SerializeField] private List<ButtonInfo> _buttons;

        private IDictionary<KeyName, ButtonInfo> _buttonInfos;

        public IDictionary<KeyName, ButtonInfo> GetButtonInfos(bool force = false)
        {
            if (_buttonInfos != null && force)
                return _buttonInfos;

            _buttonInfos = new Dictionary<KeyName, ButtonInfo>(_buttons.Count);

            foreach (var info in _buttons)
                _buttonInfos.Add(info.Name, info);

            return _buttonInfos;
        }
    }
}

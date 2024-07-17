using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace DoodleJump.Core.Editor
{
    internal sealed class Button
    {
        private readonly string _displayName;
        private readonly MethodInfo _info;

        internal Button(MethodInfo method, ButtonAttribute buttonAttribute)
        {
            var name = buttonAttribute.MethodName;

            _displayName = string.IsNullOrEmpty(name) ? ObjectNames.NicifyVariableName(method.Name) : name;
            _info = method;
        }

        internal void Draw(IEnumerable<object> targets)
        {
            if (GUILayout.Button(_displayName) == false)
                return;

            foreach (var target in targets)
                _info.Invoke(target, null);
        }
    }
}

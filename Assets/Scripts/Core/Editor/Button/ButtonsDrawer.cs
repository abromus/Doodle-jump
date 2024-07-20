#if UNITY_EDITOR
using System.Collections.Generic;
using System.Reflection;

namespace DoodleJump.Core.Editor
{
    internal class ButtonsDrawer
    {
        private readonly List<Button> _buttons = new(32);
        private readonly BindingFlags _flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

        internal ButtonsDrawer(object target)
        {
            var methods = target.GetType().GetMethods(_flags);

            foreach (var method in methods)
            {
                var buttonAttribute = method.GetCustomAttribute<ButtonAttribute>();

                if (buttonAttribute == null)
                    continue;

                _buttons.Add(new Button(method, buttonAttribute));
            }
        }

        internal void Draw(IEnumerable<object> targets)
        {
            foreach (var button in _buttons)
                button.Draw(targets);
        }
    }
}
#endif

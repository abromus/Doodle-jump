#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace DoodleJump.Core.Editor
{
    [CustomPropertyDrawer(typeof(SeparatorAttribute))]
    internal sealed class SeparatorDrawer : DecoratorDrawer
    {
        public override void OnGUI(Rect position)
        {
            base.OnGUI(position);

            var separatorAttribute = attribute as SeparatorAttribute;
            var rect = new Rect(position.xMin, position.yMin + separatorAttribute.Spacing, position.width, separatorAttribute.Height);
            var color = separatorAttribute.Color.GetColor();

            EditorGUI.DrawRect(rect, color);
        }

        public override float GetHeight()
        {
            var separatorAttribute = attribute as SeparatorAttribute;

            return separatorAttribute.Spacing * 2f + separatorAttribute.Height;
        }
    }
}
#endif

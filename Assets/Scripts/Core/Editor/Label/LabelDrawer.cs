using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DoodleJump.Core.Editor
{
    [CustomPropertyDrawer(typeof(LabelAttribute))]
    internal class LabelDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var labelAttribute = attribute as LabelAttribute;

            if (string.IsNullOrEmpty(labelAttribute.Name))
            {
                EditorGUI.PropertyField(position, property, label, true);
                return;
            }

            var targetObject = property.serializedObject.targetObject;
            var methodInfo = targetObject.GetType().GetMethod(labelAttribute.Name, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public);

            if (methodInfo == null)
            {
                var name = labelAttribute.Name;
                label.text = string.IsNullOrEmpty(name) ? property.displayName : name;
                EditorGUI.PropertyField(position, property, label, true);

                return;
            }

            var methodResult = methodInfo.Invoke(targetObject, null);

            if (methodResult is IReadOnlyList<string> labelTexts)
            {
                var propertyPath = property.propertyPath;
                var pathParts = propertyPath.Split('.');
                var lastPart = pathParts[pathParts.Length - 1];
                var index = lastPart.Replace("data[", "").Replace("]", "");
                var childIndex = int.Parse(index);

                label.text = labelTexts[childIndex];
                EditorGUI.PropertyField(position, property, label, true);
            }
            else if (methodResult is string labelText)
            {
                label.text = labelText;
                EditorGUI.PropertyField(position, property, label, true);
            }
            else
            {
                EditorGUI.HelpBox(position, $"Method '{labelAttribute.Name}' should return a string.", MessageType.Error);
                EditorGUI.PropertyField(position, property, label, true);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var height = EditorGUI.GetPropertyHeight(property, label);

            return height;
        }
    }
}

﻿using UnityEditor;
using UnityEngine;

namespace DoodleJump.Core.Editor
{
    [CustomEditor(typeof(Object), true)]
    [CanEditMultipleObjects]
    internal class ObjectEditor : UnityEditor.Editor
    {
        private ButtonsDrawer _buttonsDrawer;

        private void OnEnable()
        {
            _buttonsDrawer = new ButtonsDrawer(target);
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            _buttonsDrawer.Draw(targets);
        }
    }
}
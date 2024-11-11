namespace DoodleJump.Game.Settings.Editor
{
#if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(DoodlerConfig))]
    internal sealed class DoodlerConfigEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var config = (DoodlerConfig)target;

            UnityEditor.Undo.RecordObject(config, $"Modify {nameof(DoodlerConfig)}");

            serializedObject.Update();

            config.SetMovementVelocity(UnityEditor.EditorGUILayout.FloatField(nameof(config.MovementVelocity), config.MovementVelocity));
            config.SetMaxShots(UnityEditor.EditorGUILayout.IntField(nameof(config.MaxShots), config.MaxShots));

            var mode = (Worlds.Entities.ShootingMode)UnityEditor.EditorGUILayout.EnumPopup(nameof(config.ShootingMode), config.ShootingMode);
            config.SetShootingMode(mode);

            config.SetShootModeDuration(UnityEditor.EditorGUILayout.FloatField(nameof(config.ShootModeDuration), config.ShootModeDuration));

            var isConeMode = mode == Worlds.Entities.ShootingMode.Cone;
            var angle = isConeMode ? config.MaxAngle : 0f;

            if (isConeMode)
                config.SetMaxAngle(UnityEditor.EditorGUILayout.FloatField(nameof(config.MaxAngle), angle));
            else
                config.SetMaxAngle(angle);

            serializedObject.ApplyModifiedProperties();

            if (UnityEngine.GUI.changed)
                UnityEditor.EditorUtility.SetDirty(config);
        }
    }
#endif
}

namespace DoodleJump.Game.Settings.Editor
{
#if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(DoodlerConfig))]
    public class DoodlerConfigEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var config = (DoodlerConfig)target;

            config.SetMovementVelocity(UnityEditor.EditorGUILayout.FloatField(nameof(config.MovementVelocity), config.MovementVelocity));
            var mode = (Worlds.Entities.ShootingMode)UnityEditor.EditorGUILayout.EnumPopup(nameof(config.ShootingMode), config.ShootingMode);
            config.SetShootingMode(mode);

            var isConeMode = mode == Worlds.Entities.ShootingMode.Cone;
            var angle = isConeMode ? config.MaxAngle : 0f;

            if (isConeMode)
                config.SetMaxAngle(UnityEditor.EditorGUILayout.FloatField(nameof(config.MaxAngle), angle));
            else
                config.SetMaxAngle(angle);

            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}

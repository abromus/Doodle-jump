namespace DoodleJump.Game.Settings
{
    [UnityEngine.CreateAssetMenu(fileName = nameof(CameraConfig), menuName = ConfigKeys.GamePathKey + nameof(CameraConfig))]
    internal class CameraConfig : UnityEngine.ScriptableObject, ICameraConfig
    {
        [UnityEngine.SerializeField] private UnityEngine.Vector3 _offset = new(0f, 0f, -10f);

        public UnityEngine.Vector3 Offset => _offset;
    }
}

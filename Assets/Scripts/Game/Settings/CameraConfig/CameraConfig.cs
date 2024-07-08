using UnityEngine;

namespace DoodleJump.Game.Settings
{
    [CreateAssetMenu(fileName = nameof(CameraConfig), menuName = ConfigKeys.GamePathKey + nameof(CameraConfig))]
    public class CameraConfig : ScriptableObject, ICameraConfig
    {
        [SerializeField] private Vector3 _offset = new(0f, 0f, -10f);

        public Vector3 Offset => _offset;
    }
}

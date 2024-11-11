namespace DoodleJump.Game.Worlds.Entities
{
    internal sealed class DoodlerNose : UnityEngine.MonoBehaviour, IDoodlerNose
    {
        [UnityEngine.SerializeField] private UnityEngine.Transform _rotationPoint;
        [UnityEngine.SerializeField] private float _height;

        private UnityEngine.Vector3 _rotationOffset;

        public UnityEngine.Vector3 RotationOffset => _rotationOffset;

        public UnityEngine.Vector3 RotationPointPosition => _rotationPoint.position;

        public float Height => _height;

        public void Init()
        {
            _rotationOffset = transform.position - _rotationPoint.position;
        }
    }
}

using DoodleJump.Game.Services;

namespace DoodleJump.Game.Worlds.Platforms
{
    internal abstract class BasePlatform : UnityEngine.MonoBehaviour, IPlatform
    {
        [UnityEngine.SerializeField] private int _id;
        [UnityEngine.SerializeField] private UnityEngine.Vector2 _platformSize;
        [UnityEngine.SerializeField] private PlatformClipType _clipType;
        [UnityEngine.SerializeField] private UnityEngine.Transform _boosterContainer;

        private float _xMin;
        private float _xMax;
        private float _yMin;
        private float _yMax;
        private IAudioService _audioService;

        public int Id => _id;

        public UnityEngine.Vector2 Size => _platformSize;

        public PlatformClipType ClipType => _clipType;

        public UnityEngine.Vector3 Position => transform.position;

        public abstract event System.Action<IPlatformCollisionInfo> Collided;

        public abstract event System.Action<IPlatform> Destroyed;

        public virtual void Init(Data.IGameData gameData)
        {
            _audioService = gameData.ServiceStorage.GetAudioService();
        }

        public void InitBooster(Boosters.IWorldBooster worldBooster)
        {
            var boosterTransform = worldBooster.GameObject.transform;
            boosterTransform.SetParent(_boosterContainer);
            boosterTransform.localPosition = UnityEngine.Vector3.zero;
        }

        public virtual void InitConfig(Settings.IPlatformConfig platformConfig) { }

        public virtual void InitPosition(UnityEngine.Vector3 position)
        {
            transform.position = position;

            var xCenter = position.x;
            var yCenter = position.y;
            var xOffset = Size.x * Constants.Half;
            var yOffset = Size.y * Constants.Half;

            _xMin = xCenter - xOffset;
            _xMax = xCenter + xOffset;
            _yMin = yCenter - yOffset;
            _yMax = yCenter + yOffset;

            gameObject.SetActive(true);
        }

        public bool IsIntersectedArea(UnityEngine.Vector2 center, UnityEngine.Vector2 size)
        {
            var xCenter = center.x;
            var yCenter = center.y;
            var xOffset = size.x * Constants.Half;
            var yOffset = size.y * Constants.Half;

            var xMin = UnityEngine.Mathf.Max(_xMin, xCenter - xOffset);
            var xMax = UnityEngine.Mathf.Min(_xMax, xCenter + xOffset);
            var yMin = UnityEngine.Mathf.Max(_yMin, yCenter - yOffset);
            var yMax = UnityEngine.Mathf.Min(_yMax, yCenter + yOffset);

            return xMin <= xMax && yMin <= yMax;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Clear()
        {
            gameObject.SetActive(false);
        }

        public abstract void Destroy();

        protected void PlaySound(PlatformClipType type)
        {
            _audioService.PlaySound(type);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            UnityEngine.Gizmos.color = UnityEngine.Color.cyan;
            UnityEngine.Gizmos.DrawWireCube(Position, Size);
        }
#endif
    }
}

namespace DoodleJump.Core.Services
{
    public interface ICameraService : IService
    {
        public UnityEngine.Camera Camera { get; }

        public event System.Action<UnityEngine.Transform> Attached;

        public void Init(UnityEngine.Transform container);

        public void AttachTo(UnityEngine.Transform parent);

        public void Detach();

        public UnityEngine.Rect GetScreenRect();
    }
}

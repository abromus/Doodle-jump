namespace DoodleJump.Core.Services
{
    public interface IEventSystemService : IService
    {
        public bool AddTo(in UnityEngine.SceneManagement.Scene scene);

        public void Detach(in UnityEngine.SceneManagement.Scene scene);

        public UnityEngine.EventSystems.EventSystem Get(in UnityEngine.SceneManagement.Scene scene);

        public bool IsPointerOverGameObject(in UnityEngine.SceneManagement.Scene scene);

        public bool IsPointerOverGameObject(in UnityEngine.SceneManagement.Scene scene, int fingerId);
    }
}

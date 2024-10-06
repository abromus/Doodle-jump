namespace DoodleJump.Core.Services
{
    public interface IEventSystemService : IService
    {
        public bool AddTo(UnityEngine.SceneManagement.Scene scene);

        public void Detach(UnityEngine.SceneManagement.Scene scene);

        public UnityEngine.EventSystems.EventSystem Get(UnityEngine.SceneManagement.Scene scene);

        public bool IsPointerOverGameObject(UnityEngine.SceneManagement.Scene scene);

        public bool IsPointerOverGameObject(UnityEngine.SceneManagement.Scene scene, int fingerId);
    }
}

using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace DoodleJump.Core.Services
{
    public interface IEventSystemService : IService
    {
        public bool AddTo(Scene scene);

        public void Detach(Scene scene);

        public EventSystem Get(Scene scene);
    }
}

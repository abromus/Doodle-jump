using UnityEngine;

namespace DoodleJump.Core
{
    public abstract class SceneController : MonoBehaviour, IDestroyable
    {
        public abstract void Run(IGameData gameData);

        public abstract void Destroy();
    }
}

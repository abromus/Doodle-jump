using DoodleJump.Core.Data;
using UnityEngine;

namespace DoodleJump.Core
{
    public abstract class SceneController : MonoBehaviour, IDestroyable
    {
        public abstract void Run(ICoreData gameData);

        public abstract void Destroy();
    }
}

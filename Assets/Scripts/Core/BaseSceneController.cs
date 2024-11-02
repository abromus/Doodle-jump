namespace DoodleJump.Core
{
    public abstract class BaseSceneController : UnityEngine.MonoBehaviour, IDestroyable
    {
        public abstract void Run(Data.ICoreData gameData);

        public abstract void Destroy();
    }
}

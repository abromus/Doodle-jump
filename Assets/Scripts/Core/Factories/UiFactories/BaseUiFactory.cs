namespace DoodleJump.Core.Factories
{
    public abstract class BaseUiFactory : UnityEngine.MonoBehaviour, IUiFactory
    {
        public virtual void Destroy() { }
    }
}
